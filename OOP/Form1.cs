using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OOP
{
	public partial class Form1 : Form
	{
		private AnimalList animalList;

		public Form1()
		{
			InitializeComponent();
			animalList = new AnimalList();
			comboBoxAnimalType.Items.AddRange(new string[] { "Dog", "Tiger", "Carp", "Pike", "Eagle" });
			textBoxBreed.Visible = false;
			textBoxDepth.Visible = false;
			textBoxName.Visible = false;
			textBoxSpeed.Visible = false;
			comboBoxAnimalType.SelectedIndexChanged += comboBoxAnimalType_SelectedIndexChanged;
		}
		private void comboBoxAnimalType_SelectedIndexChanged(object sender, EventArgs e)
		{
			string animalType = comboBoxAnimalType.SelectedItem.ToString();

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
					try
					{
						string breed = textBoxBreed.Text;
						string name = textBoxName.Text;

						if (string.IsNullOrWhiteSpace(breed) || string.IsNullOrWhiteSpace(name))
						{
							throw new Exception("enter breed and name for the dog");
						}

						additionalParameters = new object[] { breed, name };
					}
					catch (Exception ex)
					{
						throw new Exception($"Dog creation error: {ex.Message}");
					}
				}
				else if (animalType == "Carp" || animalType == "Pike")
				{
					try
					{
						int depth = int.Parse(textBoxDepth.Text);
						additionalParameters = new object[] { depth };
					}
					catch (FormatException)
					{
						throw new Exception("Enter a valid depth (number)");
					}
					catch (Exception ex)
					{
						throw new Exception($"Fish creation error: {ex.Message}");
					}
				}
				else if (animalType == "Eagle")
				{
					try
					{
						int flightspeed = int.Parse(textBoxSpeed.Text);
						additionalParameters = new object[] { flightspeed };
					}
					catch (FormatException)
					{
						throw new Exception("Enter a valid flight speed (number)");
					}
					catch (Exception ex)
					{
						throw new Exception($"Eagle creation error: {ex.Message}");
					}
				}

				Animal animal = AnimalFactory.CreateAnimal(animalType, imagePath, additionalParameters);

				animalList.AddAnimal(animal);

				try
				{
					if (!File.Exists(animal.ImagePath))
					{
						throw new Exception($"Image file not found: {animal.ImagePath}");
					}
					pictureBoxAnimal.Image = Image.FromFile(animal.ImagePath);
				}
				catch (Exception ex)
				{
					throw new Exception($"Image loading error: {ex.Message}");
				}
				UpdateAnimalsInfoTextBox();
				labelAnimalCount.Text = $"Total animals created: {Animal.Count}";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void UpdateAnimalsInfoTextBox()
		{
			textBoxAnimalsInfo.Clear();

			foreach (var animal in animalList.GetAnimals())
			{
				textBoxAnimalsInfo.AppendText(animal.ToString() + Environment.NewLine);
			}
		}
	}
}