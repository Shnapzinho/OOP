using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace OOP
{
	public partial class Form1 : Form
	{
		private AnimalList animalList;
		private Animal selectedAnimal;
		private History history;
		private int? editingAnimalIndex = null;
		private Dictionary<string, ISerializer> serializers;
		private List<IDataProcessorPlugin> plugins = new List<IDataProcessorPlugin>();
		private Dictionary<string, bool> activePlugins = new Dictionary<string, bool>();

		public Form1()
		{
			InitializeComponent();
			history = new History();
			animalList = new AnimalList();
			InitializeSerializers();
			LoadPlugins();
			InitializeComboBox();
			InitializeAnimalListView();
			InitializeMenu();
			textBoxBreed.Visible = false;
			textBoxDepth.Visible = false;
			textBoxName.Visible = false;
			textBoxFlightSpeed.Visible = false;
			textBoxManeSize.Visible = false;
			textBoxMaxSpeed.Visible = false;	
			comboBoxAnimalType.SelectedIndexChanged += comboBoxAnimalType_SelectedIndexChanged;
			buttonDelete.Enabled = false;
			buttonEdit.Enabled = false;
			UpdateUndoRedoButtons();
		}

		private void InitializeSerializers()
		{
			serializers = new Dictionary<string, ISerializer>
	{
		{ "JSON", new JsonAnimalSerializer() },
		{ "XML", new XmlAnimalSerializer() } 
    };
		}

		private void InitializeMenu()
		{
			MenuStrip mainMenu = new MenuStrip();
			this.MainMenuStrip = mainMenu;
			this.Controls.Add(mainMenu);

			ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");

			ToolStripMenuItem saveMenu = new ToolStripMenuItem("Save");

			foreach (var format in serializers.Keys)
			{
				ToolStripMenuItem formatItem = new ToolStripMenuItem(format);
				formatItem.Click += (sender, e) => SaveToFile(format);
				saveMenu.DropDownItems.Add(formatItem);
			}

			ToolStripMenuItem openMenu = new ToolStripMenuItem("Open");

			foreach (var format in serializers.Keys)
			{
				ToolStripMenuItem formatItem = new ToolStripMenuItem(format);
				formatItem.Click += (sender, e) => OpenFromFile(format);
				openMenu.DropDownItems.Add(formatItem);
			}

			fileMenu.DropDownItems.Add(saveMenu);
			fileMenu.DropDownItems.Add(openMenu);
			mainMenu.Items.Add(fileMenu);

			ToolStripMenuItem pluginsMenu = new ToolStripMenuItem("Plugins");
			foreach (var plugin in plugins)
			{
				ToolStripMenuItem pluginItem = new ToolStripMenuItem(plugin.Name)
				{
					CheckOnClick = true,
					Checked = activePlugins[plugin.Name]
				};

				pluginItem.Click += (sender, e) =>
				{
					activePlugins[plugin.Name] = pluginItem.Checked;
				};

				pluginsMenu.DropDownItems.Add(pluginItem);
			}

			mainMenu.Items.Add(pluginsMenu);
		}

		private void SaveToFile(string format)
		{
			using var saveFileDialog = new SaveFileDialog
			{
				Filter = $"{format} files (*.{format.ToLower()})|*.{format.ToLower()}",
				Title = $"Save animals as {format}",
				DefaultExt = format.ToLower(),
				AddExtension = true,
				OverwritePrompt = true 
			};

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					var animals = animalList.GetAnimals();
					if (!animals.Any())
					{
						throw new Exception("Animal list is empty");
					}

					string data = serializers[format].Serialize(animals);

					// Применяем активные плагины
					foreach (var plugin in plugins.Where(p => activePlugins[p.Name]))
					{
						data = plugin.ProcessBeforeSave(data);
					}

					File.WriteAllText(saveFileDialog.FileName, data);

					MessageBox.Show($"File saved successfully!\n{saveFileDialog.FileName}", "Success",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error saving file:\n{ex.Message}", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void OpenFromFile(string format)
		{
			using var openFileDialog = new OpenFileDialog
			{
				Filter = $"{format} files (*.{format.ToLower()})|*.{format.ToLower()}",
				Title = $"Open animals from {format} file",
				CheckFileExists = true,
				Multiselect = false
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					string data = File.ReadAllText(openFileDialog.FileName);

					// Применяем плагины в обратном порядке
					foreach (var plugin in plugins.Where(p => activePlugins[p.Name]).Reverse())
					{
						data = plugin.ProcessAfterLoad(data);
					}

					var animals = serializers[format].Deserialize(data) ?? throw new Exception("No animals found in file");

					if (!animals.Any())
					{
						throw new Exception("File is incorrect");
					}

					if (animalList.Count > 0)
					{
						var result = MessageBox.Show("This will overwrite current animals. Continue?", "Confirmation",
							MessageBoxButtons.YesNo, MessageBoxIcon.Question);

						if (result != DialogResult.Yes) return;
					}

					animalList = new AnimalList();
					animals.ForEach(animalList.AddAnimal);
					UpdateAnimalListView();
					UpdateAnimalCount();
					ClearSelection();
					history = new History();
					UpdateUndoRedoButtons();

					MessageBox.Show($"File loaded successfully!\n{openFileDialog.FileName}", "Success",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error loading file:\n{ex.Message}", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void InitializeAnimalListView()
		{
			animalListView.View = View.Details;
			animalListView.FullRowSelect = true;
			animalListView.Columns.Add("Type", 100);
			animalListView.Columns.Add("Details", 200);
			animalListView.SelectedIndexChanged += AnimalListView_SelectedIndexChanged;
		}

		private void AnimalListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (animalListView.SelectedItems.Count > 0)
			{
				int selectedIndex = animalListView.SelectedIndices[0];
				selectedAnimal = animalList.GetAnimals()[selectedIndex];
				editingAnimalIndex = selectedIndex;

				try
				{
					if (File.Exists(selectedAnimal.ImagePath))
					{
						pictureBoxAnimal.Image = Image.FromFile(selectedAnimal.ImagePath);
					}
					else
					{
						pictureBoxAnimal.Image = null;
						MessageBox.Show($"Image file not found: {selectedAnimal.ImagePath}", "Error",
									  MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error loading image: {ex.Message}", "Error",
									MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				textBoxAnimalInfo.Text = selectedAnimal.ToString();
				buttonDelete.Enabled = true;
				buttonEdit.Enabled = true;
			}
			else
			{
				buttonDelete.Enabled = false;
				buttonEdit.Enabled = false;
				editingAnimalIndex = null;
			}
		}


		private void InitializeComboBox()
		{
			comboBoxAnimalType.Items.Clear();
			foreach (var typeName in AnimalFactory.GetAvailableAnimalTypes())
			{
				comboBoxAnimalType.Items.Add(typeName);
			}
		}

		private void comboBoxAnimalType_SelectedIndexChanged(object sender, EventArgs e)
		{
			string animalType = comboBoxAnimalType.SelectedItem?.ToString();
			if (animalType == null) return;

			// Сброс всех полей
			ResetAllInputFields();

			// Получаем тип через AnimalFactory
			Type type = AnimalFactory.GetAnimalType(animalType);
			if (type == null) return;

			// Анализ параметров конструктора
			AnalyzeConstructorParameters(type);
		}

		private void ResetAllInputFields()
		{
			textBoxBreed.Visible = false;
			textBoxName.Visible = false;
			textBoxDepth.Visible = false;
			textBoxFlightSpeed.Visible = false;
			textBoxMaxSpeed.Visible = false;
			textBoxManeSize.Visible = false;
		}

		private void AnalyzeConstructorParameters(Type type)
		{
			var constructor = type.GetConstructors()
				.OrderByDescending(c => c.GetParameters().Length)
				.FirstOrDefault();

			if (constructor == null) return;

			foreach (var param in constructor.GetParameters().Skip(1))
			{
				UpdateInputFieldVisibility(param);
			}
		}

		private void UpdateInputFieldVisibility(ParameterInfo param)
		{
			string paramName = param.Name.ToLower();
			switch (paramName)
			{
				case "name":
					textBoxName.Visible = true;
					break;
				case "breed":
					textBoxBreed.Visible = true;
					break;
				case "depth":
					textBoxDepth.Visible = true;
					break;
				case "flightspeed":
					textBoxFlightSpeed.Visible = true;
					break;
				case "manesize":
					textBoxManeSize.Visible = true;
					break;
				case "maxspeed":
					textBoxMaxSpeed.Visible = true;
					break;
			}
		}

		private void buttonCreate_Click(object sender, EventArgs e)
		{
			try
			{
				string animalType = comboBoxAnimalType.SelectedItem?.ToString();
				if (animalType == null)
				{
					throw new Exception("Select an animal type");
				}

				string imagePath = $"{animalType.ToLower()}.png";
				Type type = FindAnimalType(animalType);

				if (type == null)
				{
					throw new Exception($"Unknown animal type: {animalType}");
				}

				var constructor = type.GetConstructors()
					.OrderByDescending(c => c.GetParameters().Length)
					.FirstOrDefault() ?? throw new Exception($"No constructor found for {animalType}");

				var parameters = constructor.GetParameters();
				object[] constructorParams = PrepareConstructorParameters(imagePath, parameters);

				Animal animal = (Animal)constructor.Invoke(constructorParams);
				history.Execute(new HistoryAdd(animalList, animal));

				UpdateAnimalListView();
				UpdateAnimalCount();
				ClearInputFields();
				UpdateUndoRedoButtons();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private Type FindAnimalType(string animalType)
		{
			return AnimalFactory.GetAnimalType(animalType);
		}

		private object[] PrepareConstructorParameters(string imagePath, ParameterInfo[] parameters)
		{
			var additionalParams = parameters.Skip(1).ToList();
			object[] constructorParams = new object[additionalParams.Count + 1];
			constructorParams[0] = imagePath;

			for (int i = 0; i < additionalParams.Count; i++)
			{
				var param = additionalParams[i];
				string paramName = param.Name.ToLower();
				Control[] controls = this.Controls.Find($"textBox{paramName}", true);

				if (controls.Length == 0 || !(controls[0] is TextBox textBox))
					throw new Exception($"Input field for {param.Name} not found");

				if (string.IsNullOrWhiteSpace(textBox.Text))
					throw new Exception($"Field '{param.Name}' cannot be empty");

				constructorParams[i + 1] = Convert.ChangeType(textBox.Text, param.ParameterType);
			}
			return constructorParams;
		}

		private void ClearInputFields()
		{
			textBoxBreed.Text = "";
			textBoxName.Text = "";
			textBoxDepth.Text = "";
			textBoxFlightSpeed.Text = "";
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (animalListView.SelectedItems.Count > 0 && selectedAnimal != null)
			{
				var result = MessageBox.Show($"Delete {selectedAnimal.GetType().Name}?", "Confirm",
										  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					int index = animalListView.SelectedIndices[0];
					history.Execute(new HistoryRemove(animalList, selectedAnimal, index));

					UpdateAnimalListView();
					UpdateAnimalCount();
					ClearSelection();
					UpdateUndoRedoButtons();
				}
			}
		}

		private void ClearSelection()
		{
			pictureBoxAnimal.Image = null;
			textBoxAnimalInfo.Text = "";
			buttonDelete.Enabled = false;
			buttonEdit.Enabled = false;
			editingAnimalIndex = null;
		}

		private void buttonEdit_Click(object sender, EventArgs e)
		{
			if (selectedAnimal == null || !editingAnimalIndex.HasValue) return;

			Form editForm = new Form();
			editForm.Text = $"Edit {selectedAnimal.GetType().Name}";
			editForm.Size = new Size(300, 200);
			editForm.StartPosition = FormStartPosition.CenterParent;
			Animal originalAnimal = selectedAnimal;
			Animal modifiedAnimal = null;
			Type animalType = selectedAnimal.GetType();
			var constructor = animalType.GetConstructors()
				.OrderByDescending(c => c.GetParameters().Length)
				.FirstOrDefault();

			if (constructor == null)
			{
				MessageBox.Show("Canэt find constructor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var parameters = constructor.GetParameters();
			var editableParameters = parameters.Skip(1).ToList();
			int yPos = 20;
			foreach (var param in editableParameters)
			{
				object currentValue = null;
				var property = animalType.GetProperty(param.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
				if (property != null)
				{
					currentValue = property.GetValue(selectedAnimal);
				}
				Label label = new Label()
				{
					Text = $"{param.Name}:",
					Left = 10,
					Top = yPos
				};
				TextBox textBox = new TextBox()
				{
					Text = currentValue?.ToString() ?? "",
					Left = 120,
					Top = yPos,
					Width = 150,
					Name = param.Name,
					Tag = param.ParameterType
				};

				editForm.Controls.Add(label);
				editForm.Controls.Add(textBox);
				yPos += 30;
			}

			Button saveButton = new Button() { Text = "Save", Left = 100, Top = yPos + 10, Width = 100 };
			saveButton.Click += (s, ev) =>
			{
				try
				{
					List<object> constructorParams = new List<object> { selectedAnimal.ImagePath };

					foreach (var param in editableParameters)
					{
						var textBox = (TextBox)editForm.Controls[param.Name];
						object value;

						try
						{
							if (param.ParameterType == typeof(int))
							{
								if (!int.TryParse(textBox.Text, out int intValue))
									throw new FormatException($"{param.Name} must be a number");
								value = intValue;
							}
							else
							{
								value = Convert.ChangeType(textBox.Text, param.ParameterType);
							}
						}
						catch
						{
							throw new FormatException($"Invalid value for {param.Name}. Expected type: {param.ParameterType.Name}");
						}

						constructorParams.Add(value);
					}

					modifiedAnimal = (Animal)constructor.Invoke(constructorParams.ToArray());
					editForm.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			};

			editForm.Controls.Add(saveButton);

			editForm.FormClosed += (s, args) =>
			{
				if (modifiedAnimal != null && editingAnimalIndex.HasValue)
				{
					history.Execute(new HistoryModify(
						animalList, editingAnimalIndex.Value, originalAnimal, modifiedAnimal));

					UpdateAnimalListView();
					UpdateUndoRedoButtons();
				}
			};

			editForm.ShowDialog();
		}

		private void UpdateAnimalListView()
		{
			animalListView.BeginUpdate();
			animalListView.Items.Clear();

			foreach (var animal in animalList.GetAnimals())
			{
				ListViewItem item = new ListViewItem(animal.GetType().Name);
				item.SubItems.Add(animal.ToString());
				item.Tag = animal;
				animalListView.Items.Add(item);
			}

			animalListView.EndUpdate();

			if (selectedAnimal != null)
			{
				textBoxAnimalInfo.Text = selectedAnimal.ToString();
			}
		}

		private void buttonUndo_Click(object sender, EventArgs e)
		{
			history.Undo();
			UpdateAnimalListView();
			UpdateAnimalCount();
			UpdateUndoRedoButtons();
		}

		private void buttonRedo_Click(object sender, EventArgs e)
		{
			history.Redo();
			UpdateAnimalListView();
			UpdateAnimalCount();
			UpdateUndoRedoButtons();
		}

		private void UpdateUndoRedoButtons()
		{
			buttonUndo.Enabled = history.CanUndo;
			buttonRedo.Enabled = history.CanRedo;
		}
		private void UpdateAnimalCount()
		{
			labelAnimalCount.Text = $"Total animals created: {animalList.Count}";
		}

		private void buttonLoadDll_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "DLL files (*.dll)|*.dll";
				openFileDialog.Title = "Select animal DLL";
				openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						AnimalFactory.LoadAssembly(Assembly.LoadFrom(openFileDialog.FileName));
						UpdateAnimalComboBox();
						MessageBox.Show("DLL loaded successfully!", "Success",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Error loading DLL: {ex.Message}", "Error",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void UpdateAnimalComboBox()
		{
			comboBoxAnimalType.DataSource = null;
			comboBoxAnimalType.DataSource = AnimalFactory.GetAvailableAnimalTypes().ToList();
		}

		
		private void LoadPlugins()
		{
			plugins = PluginLoader.LoadPlugins().ToList();

			// Отладочный вывод
			Console.WriteLine($"Loaded {plugins.Count} plugins:");
			foreach (var plugin in plugins)
			{
				Console.WriteLine($"- {plugin.Name}");
				activePlugins[plugin.Name] = false;
			}

			if (plugins.Count == 0)
			{
				MessageBox.Show("No plugins were loaded. Check Plugins directory.",
							  "Plugin Warning",
							  MessageBoxButtons.OK,
							  MessageBoxIcon.Warning);
			}
		}
	}

}