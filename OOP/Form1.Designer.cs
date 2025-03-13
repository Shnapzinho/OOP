using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OOP
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			buttonCreate = new Button();
			textBoxName = new TextBox();
			pictureBoxAnimal = new PictureBox();
			comboBoxAnimalType = new ComboBox();
			labelAnimalInfo = new Label();
			labelAnimalCount = new Label();
			textBoxBreed = new TextBox();
			textBoxDepth = new TextBox();
			textBoxAnimalsInfo = new TextBox();
			textBoxSpeed = new TextBox();
			((System.ComponentModel.ISupportInitialize)pictureBoxAnimal).BeginInit();
			SuspendLayout();
			// 
			// buttonCreate
			// 
			buttonCreate.Location = new Point(281, 14);
			buttonCreate.Margin = new Padding(2);
			buttonCreate.Name = "buttonCreate";
			buttonCreate.Size = new Size(99, 23);
			buttonCreate.TabIndex = 0;
			buttonCreate.Text = "Create";
			buttonCreate.UseVisualStyleBackColor = true;
			buttonCreate.Click += buttonCreate_Click;
			// 
			// textBoxName
			// 
			textBoxName.Location = new Point(11, 69);
			textBoxName.Margin = new Padding(2);
			textBoxName.Name = "textBoxName";
			textBoxName.PlaceholderText = "Enter name";
			textBoxName.Size = new Size(172, 23);
			textBoxName.TabIndex = 1;
			// 
			// pictureBoxAnimal
			// 
			pictureBoxAnimal.Location = new Point(247, 140);
			pictureBoxAnimal.Margin = new Padding(2);
			pictureBoxAnimal.Name = "pictureBoxAnimal";
			pictureBoxAnimal.Size = new Size(182, 137);
			pictureBoxAnimal.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBoxAnimal.TabIndex = 2;
			pictureBoxAnimal.TabStop = false;
			// 
			// comboBoxAnimalType
			// 
			comboBoxAnimalType.FormattingEnabled = true;
			comboBoxAnimalType.Location = new Point(11, 14);
			comboBoxAnimalType.Name = "comboBoxAnimalType";
			comboBoxAnimalType.Size = new Size(172, 23);
			comboBoxAnimalType.TabIndex = 7;
			comboBoxAnimalType.SelectedIndexChanged += comboBoxAnimalType_SelectedIndexChanged;
			// 
			// labelAnimalInfo
			// 
			labelAnimalInfo.AutoSize = true;
			labelAnimalInfo.Location = new Point(200, 46);
			labelAnimalInfo.Name = "labelAnimalInfo";
			labelAnimalInfo.Size = new Size(0, 15);
			labelAnimalInfo.TabIndex = 8;
			// 
			// labelAnimalCount
			// 
			labelAnimalCount.AutoSize = true;
			labelAnimalCount.Location = new Point(200, 72);
			labelAnimalCount.Name = "labelAnimalCount";
			labelAnimalCount.Size = new Size(0, 15);
			labelAnimalCount.TabIndex = 9;
			// 
			// textBoxBreed
			// 
			textBoxBreed.Location = new Point(11, 42);
			textBoxBreed.Margin = new Padding(2);
			textBoxBreed.Name = "textBoxBreed";
			textBoxBreed.PlaceholderText = "Enter breed";
			textBoxBreed.Size = new Size(172, 23);
			textBoxBreed.TabIndex = 10;
			// 
			// textBoxDepth
			// 
			textBoxDepth.Location = new Point(11, 42);
			textBoxDepth.Margin = new Padding(2);
			textBoxDepth.Name = "textBoxDepth";
			textBoxDepth.PlaceholderText = "Enter depth";
			textBoxDepth.Size = new Size(172, 23);
			textBoxDepth.TabIndex = 11;
			// 
			// textBoxAnimalsInfo
			// 
			textBoxAnimalsInfo.Location = new Point(11, 107);
			textBoxAnimalsInfo.Margin = new Padding(2);
			textBoxAnimalsInfo.Multiline = true;
			textBoxAnimalsInfo.Name = "textBoxAnimalsInfo";
			textBoxAnimalsInfo.ScrollBars = ScrollBars.Vertical;
			textBoxAnimalsInfo.Size = new Size(217, 196);
			textBoxAnimalsInfo.TabIndex = 12;
			// 
			// textBoxSpeed
			// 
			textBoxSpeed.Location = new Point(11, 43);
			textBoxSpeed.Margin = new Padding(2);
			textBoxSpeed.Name = "textBoxSpeed";
			textBoxSpeed.PlaceholderText = "Enter flight speed";
			textBoxSpeed.Size = new Size(172, 23);
			textBoxSpeed.TabIndex = 13;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(468, 314);
			Controls.Add(textBoxSpeed);
			Controls.Add(textBoxAnimalsInfo);
			Controls.Add(textBoxDepth);
			Controls.Add(textBoxBreed);
			Controls.Add(labelAnimalCount);
			Controls.Add(labelAnimalInfo);
			Controls.Add(comboBoxAnimalType);
			Controls.Add(pictureBoxAnimal);
			Controls.Add(textBoxName);
			Controls.Add(buttonCreate);
			Margin = new Padding(2);
			Name = "Form1";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)pictureBoxAnimal).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button buttonCreate;
		private TextBox textBoxName;
		private PictureBox pictureBoxAnimal;
		private ComboBox comboBoxAnimalType;
		private Label labelAnimalInfo;
		private Label labelAnimalCount;
		private TextBox textBoxBreed;
		private TextBox textBoxDepth;
		private TextBox textBoxAnimalsInfo;
		private TextBox textBoxSpeed;
	}
}
