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
			textBoxAnimalInfo = new TextBox();
			textBoxSpeed = new TextBox();
			animalListView = new ListView();
			buttonDelete = new Button();
			buttonEdit = new Button();
			buttonRedo = new Button();
			buttonUndo = new Button();
			((System.ComponentModel.ISupportInitialize)pictureBoxAnimal).BeginInit();
			SuspendLayout();
			// 
			// buttonCreate
			// 
			buttonCreate.Location = new Point(244, 14);
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
			pictureBoxAnimal.Location = new Point(371, 82);
			pictureBoxAnimal.Margin = new Padding(2);
			pictureBoxAnimal.Name = "pictureBoxAnimal";
			pictureBoxAnimal.Size = new Size(172, 164);
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
			// textBoxAnimalInfo
			// 
			textBoxAnimalInfo.Location = new Point(351, 249);
			textBoxAnimalInfo.Margin = new Padding(2);
			textBoxAnimalInfo.Multiline = true;
			textBoxAnimalInfo.Name = "textBoxAnimalInfo";
			textBoxAnimalInfo.ReadOnly = true;
			textBoxAnimalInfo.ScrollBars = ScrollBars.Vertical;
			textBoxAnimalInfo.Size = new Size(211, 96);
			textBoxAnimalInfo.TabIndex = 12;
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
			// animalListView
			// 
			animalListView.Location = new Point(11, 97);
			animalListView.Name = "animalListView";
			animalListView.Size = new Size(245, 248);
			animalListView.TabIndex = 14;
			animalListView.UseCompatibleStateImageBehavior = false;
			// 
			// buttonDelete
			// 
			buttonDelete.Location = new Point(454, 14);
			buttonDelete.Margin = new Padding(2);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new Size(99, 23);
			buttonDelete.TabIndex = 15;
			buttonDelete.Text = "Delete";
			buttonDelete.UseVisualStyleBackColor = true;
			buttonDelete.Click += buttonDelete_Click;
			// 
			// buttonEdit
			// 
			buttonEdit.Location = new Point(351, 14);
			buttonEdit.Margin = new Padding(2);
			buttonEdit.Name = "buttonEdit";
			buttonEdit.Size = new Size(99, 23);
			buttonEdit.TabIndex = 16;
			buttonEdit.Text = "Edit";
			buttonEdit.UseVisualStyleBackColor = true;
			buttonEdit.Click += buttonEdit_Click;
			// 
			// buttonRedo
			// 
			buttonRedo.Location = new Point(463, 43);
			buttonRedo.Margin = new Padding(2);
			buttonRedo.Name = "buttonRedo";
			buttonRedo.Size = new Size(59, 22);
			buttonRedo.TabIndex = 17;
			buttonRedo.Text = "Redo";
			buttonRedo.UseVisualStyleBackColor = true;
			buttonRedo.Click += buttonRedo_Click;
			// 
			// buttonUndo
			// 
			buttonUndo.Location = new Point(400, 42);
			buttonUndo.Margin = new Padding(2);
			buttonUndo.Name = "buttonUndo";
			buttonUndo.Size = new Size(59, 24);
			buttonUndo.TabIndex = 18;
			buttonUndo.Text = "Undo";
			buttonUndo.UseVisualStyleBackColor = true;
			buttonUndo.Click += buttonUndo_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(596, 357);
			Controls.Add(buttonUndo);
			Controls.Add(buttonRedo);
			Controls.Add(buttonEdit);
			Controls.Add(buttonDelete);
			Controls.Add(animalListView);
			Controls.Add(textBoxSpeed);
			Controls.Add(textBoxAnimalInfo);
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
			Load += Form1_Load;
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
		private TextBox textBoxAnimalInfo;
		private TextBox textBoxSpeed;
		private ListView animalListView;
		private Button buttonDelete;
		private Button buttonEdit;
		private Button buttonRedo;
		private Button buttonUndo;
	}
}
