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

		public Form1()
		{
			InitializeComponent();
			animalList = new AnimalList();
			InitializeComboBox();
			InitializeAnimalListView();
			textBoxBreed.Visible = false;
			textBoxDepth.Visible = false;
			textBoxName.Visible = false;
			textBoxSpeed.Visible = false;
			comboBoxAnimalType.SelectedIndexChanged += comboBoxAnimalType_SelectedIndexChanged;
			buttonDelete.Enabled = false;
			buttonEdit.Enabled = false;
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

			textBoxBreed.Visible = false;
			textBoxName.Visible = false;
			textBoxDepth.Visible = false;
			textBoxSpeed.Visible = false;

			if (animalType == "Dog")
			{
				textBoxBreed.Visible = true;
				textBoxName.Visible = true;
			}
			else if (animalType == "Carp" || animalType == "Pike")
			{
				textBoxDepth.Visible = true;
			}
			else if (animalType == "Eagle")
			{
				textBoxSpeed.Visible = true;
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
				object[] additionalParameters = null;

				if (animalType == "Dog")
				{
					string breed = textBoxBreed.Text;
					string name = textBoxName.Text;

					if (string.IsNullOrWhiteSpace(breed) || string.IsNullOrWhiteSpace(name))
					{
						throw new Exception("Enter breed and name for the dog");
					}

					additionalParameters = new object[] { breed, name };
				}
				else if (animalType == "Carp" || animalType == "Pike")
				{
					if (!int.TryParse(textBoxDepth.Text, out int depth))
					{
						throw new Exception("Enter a valid depth (number)");
					}
					additionalParameters = new object[] { depth };
				}
				else if (animalType == "Eagle")
				{
					if (!int.TryParse(textBoxSpeed.Text, out int flightspeed))
					{
						throw new Exception("Enter a valid flight speed (number)");
					}
					additionalParameters = new object[] { flightspeed };
				}

				Animal animal = AnimalFactory.CreateAnimal(animalType, imagePath, additionalParameters);
				animalList.AddAnimal(animal);

				ListViewItem item = new ListViewItem(animal.GetType().Name);
				item.SubItems.Add(animal.ToString());
				item.Tag = animal;
				animalListView.Items.Add(item);

				labelAnimalCount.Text = $"Total animals created: {Animal.Count}";
				textBoxBreed.Text = "";
				textBoxName.Text = "";
				textBoxDepth.Text = "";
				textBoxSpeed.Text = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (animalListView.SelectedItems.Count > 0 && selectedAnimal != null)
			{
				var result = MessageBox.Show($"Delete {selectedAnimal.GetType().Name}?", "Confirm",
										  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					animalList.RemoveAnimal(selectedAnimal);
					animalListView.Items.RemoveAt(animalListView.SelectedIndices[0]);
					Animal.Count--;
					labelAnimalCount.Text = $"Total animals created: {Animal.Count}";

					pictureBoxAnimal.Image = null;
					textBoxAnimalInfo.Text = "";
					buttonDelete.Enabled = false;
					buttonEdit.Enabled = false;
				}
			}
		}

		private void buttonEdit_Click(object sender, EventArgs e)
		{
			if (selectedAnimal == null) return;

			Form editForm = new Form();
			editForm.Text = $"Edit {selectedAnimal.GetType().Name}";
			editForm.Size = new Size(300, 200);
			editForm.StartPosition = FormStartPosition.CenterParent;

			if (selectedAnimal is Dog dog)
			{
				AddEditControls(editForm,
					("Breed", dog.Breed),
					("Name", dog.Name),
					() =>
					{
						dog.Breed = ((TextBox)editForm.Controls["Breed"]).Text;
						dog.Name = ((TextBox)editForm.Controls["Name"]).Text;
					});
			}
			else if (selectedAnimal is Carp carp)
			{
				AddEditControls(editForm,
					("Depth", carp.Depth.ToString()),
					() =>
					{
						if (int.TryParse(((TextBox)editForm.Controls["Depth"]).Text, out int depth))
							carp.Depth = depth;
						else
							throw new FormatException("Depth must be a number");
					});
			}
			else if (selectedAnimal is Pike pike)
			{
				AddEditControls(editForm,
					("Depth", pike.Depth.ToString()),
					() =>
					{
						if (int.TryParse(((TextBox)editForm.Controls["Depth"]).Text, out int depth))
							pike.Depth = depth;
						else
							throw new FormatException("Depth must be a number");
					});
			}
			else if (selectedAnimal is Eagle eagle)
			{
				AddEditControls(editForm,
					("Flight Speed", eagle.FlightSpeed.ToString()),
					() =>
					{
						if (int.TryParse(((TextBox)editForm.Controls["Flight Speed"]).Text, out int speed))
							eagle.FlightSpeed = speed;
						else
							throw new FormatException("Speed must be a number");
					});
			}

			editForm.ShowDialog();
			UpdateAnimalListView();
		}

		private void AddEditControls(Form form, params object[] controls)
		{
			int yPos = 20;
			Action saveAction = null;

			for (int i = 0; i < controls.Length; i++)
			{
				if (controls[i] is ValueTuple<string, string> control)
				{
					Label label = new Label() { Text = $"{control.Item1}:", Left = 10, Top = yPos };
					TextBox textBox = new TextBox() { Text = control.Item2, Left = 120, Top = yPos, Width = 150, Name = control.Item1 };
					form.Controls.Add(label);
					form.Controls.Add(textBox);
					yPos += 30;
				}
				else if (controls[i] is Action action)
				{
					saveAction = action;
				}
			}

			Button saveButton = new Button() { Text = "Save", Left = 100, Top = yPos + 10, Width = 100 };
			saveButton.Click += (s, e) =>
			{
				try
				{
					saveAction?.Invoke();
					form.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			};

			form.Controls.Add(saveButton);
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

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void buttonUndo_Click(object sender, EventArgs e)
		{

		}

		private void buttonRedo_Click(object sender, EventArgs e)
		{

		}
	}
}
