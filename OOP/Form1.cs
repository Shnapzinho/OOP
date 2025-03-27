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
		private readonly History history = new History();
		private int? editingAnimalIndex = null;

		public Form1()
		{
			InitializeComponent();
			animalList = new AnimalList();
			InitializeComboBox();
			InitializeAnimalListView();
			textBoxBreed.Visible = false;
			textBoxDepth.Visible = false;
			textBoxName.Visible = false;
			textBoxFlightSpeed.Visible = false;
			comboBoxAnimalType.SelectedIndexChanged += comboBoxAnimalType_SelectedIndexChanged;
			buttonDelete.Enabled = false;
			buttonEdit.Enabled = false;
			UpdateUndoRedoButtons();
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
			var assembly = Assembly.GetExecutingAssembly();
			var allTypes = assembly.GetTypes();
			var baseClassType = typeof(Animal);

			foreach (var type in allTypes)
			{
				if (type.IsClass && !type.IsAbstract && baseClassType.IsAssignableFrom(type))
				{
					comboBoxAnimalType.Items.Add(type.Name);
				}
			}
		}

		private void comboBoxAnimalType_SelectedIndexChanged(object sender, EventArgs e)
		{
			string animalType = comboBoxAnimalType.SelectedItem?.ToString();
			if (animalType == null) return;

			textBoxBreed.Visible = false;
			textBoxName.Visible = false;
			textBoxDepth.Visible = false;
			textBoxFlightSpeed.Visible = false;

			var assembly = Assembly.GetExecutingAssembly();
			var allTypes = assembly.GetTypes();
			var baseClassType = typeof(Animal);

			foreach (var type in allTypes)
			{
				if (type.IsClass && !type.IsAbstract && baseClassType.IsAssignableFrom(type) && type.Name == animalType)
				{
					var constructor = type.GetConstructors()
						.OrderByDescending(c => c.GetParameters().Length)
						.FirstOrDefault();

					if (constructor == null) break;

					var parameters = constructor.GetParameters().Skip(1);

					foreach (var param in parameters)
					{
						switch (param.Name.ToLower())
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
						}
					}
					break;
				}
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
				var assembly = Assembly.GetExecutingAssembly();
				var allTypes = assembly.GetTypes();
				var baseClassType = typeof(Animal);

				Type type = null;
				foreach (var t in allTypes)
				{
					if (t.IsClass && !t.IsAbstract && baseClassType.IsAssignableFrom(t) && t.Name == animalType)
					{
						type = t;
						break;
					}
				}

				if (type == null)
				{
					throw new Exception($"Unknown animal type: {animalType}");
				}
				var constructor = type.GetConstructors()
					.OrderByDescending(c => c.GetParameters().Length)
					.FirstOrDefault();

				if (constructor == null)
				{
					throw new Exception($"No suitable constructor found for {animalType}");
				}

				var parameters = constructor.GetParameters();
				var additionalParams = parameters.Skip(1).ToList();
				object[] constructorParams = new object[additionalParams.Count + 1];
				constructorParams[0] = imagePath;
				for (int i = 0; i < additionalParams.Count; i++)
				{
					var param = additionalParams[i];
					string paramName = param.Name.ToLower();
					Control[] controls = this.Controls.Find($"textBox{paramName}", true);
					if (controls.Length == 0 || !(controls[0] is TextBox textBox))
					{
						throw new Exception($"Input field for {param.Name} not found");
					}
					if (string.IsNullOrWhiteSpace(textBox.Text))
					{
						throw new Exception($"Field '{param.Name}' cannot be empty");
					}

					try
					{
						if (param.ParameterType == typeof(int))
						{
							if (!int.TryParse(textBox.Text, out int intValue))
							{
								throw new Exception($"Enter a valid {param.Name} (number)");
							}
							constructorParams[i + 1] = intValue;
						}
						else
						{
							constructorParams[i + 1] = Convert.ChangeType(textBox.Text, param.ParameterType);
						}
					}
					catch (Exception ex)
					{
						throw new Exception($"Invalid value for {param.Name}: {ex.Message}");
					}
				}

				Animal animal = (Animal)constructor.Invoke(constructorParams);
				history.Execute(new HistoryAdd(animalList, animal));

				UpdateAnimalListView();
				UpdateAnimalCount();
				ClearInputFields();
				UpdateUndoRedoButtons();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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
				MessageBox.Show("Cannot find appropriate constructor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
	}

}