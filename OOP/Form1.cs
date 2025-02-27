using OOP;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOP
{
	public partial class Form1 : Form
	{
		private AnimalList animalList;

		public Form1()
		{
			InitializeComponent();
			animalList = new AnimalList();

			animalList.AddAnimal(new Tiger("tiger.png"));
			animalList.AddAnimal(new Eagle("eagle.png"));
			animalList.AddAnimal(new Pike("pike.png"));
		}

		private void LoadImagesToPictureBoxes()
		{
			pictureBoxTiger.Image = Image.FromFile(animalList.GetAnimals()[0].ImagePath);
			pictureBoxEagle.Image = Image.FromFile(animalList.GetAnimals()[1].ImagePath);
			pictureBoxPike.Image = Image.FromFile(animalList.GetAnimals()[2].ImagePath);
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			TextBox1.Clear();
			LoadImagesToPictureBoxes();

			foreach (var animal in animalList.GetAnimals())
			{
				TextBox1.AppendText($"Type: {animal.Type}\r\n");
				TextBox1.AppendText($"Species: {animal.Species}\r\n");
				TextBox1.AppendText($"Sound: {animal.Sound()}\r\n");
				TextBox1.AppendText($"Movement: {animal.Move()}\r\n\r\n");
			}
		}
	}
}