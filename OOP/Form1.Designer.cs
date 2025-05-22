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
			textBoxFlightSpeed = new TextBox();
			animalListView = new ListView();
			buttonDelete = new Button();
			buttonEdit = new Button();
			buttonRedo = new Button();
			buttonUndo = new Button();
			openFileDialog = new OpenFileDialog();
			saveFileDialog = new SaveFileDialog();
			textBoxMaxSpeed = new TextBox();
			textBoxManeSize = new TextBox();
			buttonLoadDll = new Button();
			((System.ComponentModel.ISupportInitialize)pictureBoxAnimal).BeginInit();
			SuspendLayout();
			// 
			// buttonCreate
			// 
			buttonCreate.Location = new Point(346, 48);
			buttonCreate.Name = "buttonCreate";
			buttonCreate.Size = new Size(141, 38);
			buttonCreate.TabIndex = 0;
			buttonCreate.Text = "Create";
			buttonCreate.UseVisualStyleBackColor = true;
			buttonCreate.Click += buttonCreate_Click;
			// 
			// textBoxName
			// 
			textBoxName.Location = new Point(13, 140);
			textBoxName.Name = "textBoxName";
			textBoxName.PlaceholderText = "Enter name";
			textBoxName.Size = new Size(244, 31);
			textBoxName.TabIndex = 1;
			// 
			// pictureBoxAnimal
			// 
			pictureBoxAnimal.Location = new Point(527, 162);
			pictureBoxAnimal.Name = "pictureBoxAnimal";
			pictureBoxAnimal.Size = new Size(246, 273);
			pictureBoxAnimal.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBoxAnimal.TabIndex = 2;
			pictureBoxAnimal.TabStop = false;
			// 
			// comboBoxAnimalType
			// 
			comboBoxAnimalType.FormattingEnabled = true;
			comboBoxAnimalType.Location = new Point(13, 48);
			comboBoxAnimalType.Margin = new Padding(4, 5, 4, 5);
			comboBoxAnimalType.Name = "comboBoxAnimalType";
			comboBoxAnimalType.Size = new Size(244, 33);
			comboBoxAnimalType.TabIndex = 7;
			comboBoxAnimalType.SelectedIndexChanged += comboBoxAnimalType_SelectedIndexChanged;
			// 
			// labelAnimalInfo
			// 
			labelAnimalInfo.AutoSize = true;
			labelAnimalInfo.Location = new Point(283, 102);
			labelAnimalInfo.Margin = new Padding(4, 0, 4, 0);
			labelAnimalInfo.Name = "labelAnimalInfo";
			labelAnimalInfo.Size = new Size(0, 25);
			labelAnimalInfo.TabIndex = 8;
			// 
			// labelAnimalCount
			// 
			labelAnimalCount.AutoSize = true;
			labelAnimalCount.Location = new Point(283, 145);
			labelAnimalCount.Margin = new Padding(4, 0, 4, 0);
			labelAnimalCount.Name = "labelAnimalCount";
			labelAnimalCount.Size = new Size(0, 25);
			labelAnimalCount.TabIndex = 9;
			// 
			// textBoxBreed
			// 
			textBoxBreed.Location = new Point(13, 95);
			textBoxBreed.Name = "textBoxBreed";
			textBoxBreed.PlaceholderText = "Enter breed";
			textBoxBreed.Size = new Size(244, 31);
			textBoxBreed.TabIndex = 10;
			// 
			// textBoxDepth
			// 
			textBoxDepth.Location = new Point(13, 95);
			textBoxDepth.Name = "textBoxDepth";
			textBoxDepth.PlaceholderText = "Enter depth";
			textBoxDepth.Size = new Size(244, 31);
			textBoxDepth.TabIndex = 11;
			// 
			// textBoxAnimalInfo
			// 
			textBoxAnimalInfo.Location = new Point(498, 440);
			textBoxAnimalInfo.Multiline = true;
			textBoxAnimalInfo.Name = "textBoxAnimalInfo";
			textBoxAnimalInfo.ReadOnly = true;
			textBoxAnimalInfo.ScrollBars = ScrollBars.Vertical;
			textBoxAnimalInfo.Size = new Size(300, 157);
			textBoxAnimalInfo.TabIndex = 12;
			// 
			// textBoxFlightSpeed
			// 
			textBoxFlightSpeed.Location = new Point(13, 96);
			textBoxFlightSpeed.Name = "textBoxFlightSpeed";
			textBoxFlightSpeed.PlaceholderText = "Enter flight speed";
			textBoxFlightSpeed.Size = new Size(244, 31);
			textBoxFlightSpeed.TabIndex = 13;
			// 
			// animalListView
			// 
			animalListView.Location = new Point(13, 187);
			animalListView.Margin = new Padding(4, 5, 4, 5);
			animalListView.Name = "animalListView";
			animalListView.Size = new Size(348, 411);
			animalListView.TabIndex = 14;
			animalListView.UseCompatibleStateImageBehavior = false;
			// 
			// buttonDelete
			// 
			buttonDelete.Location = new Point(646, 48);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new Size(141, 38);
			buttonDelete.TabIndex = 15;
			buttonDelete.Text = "Delete";
			buttonDelete.UseVisualStyleBackColor = true;
			buttonDelete.Click += buttonDelete_Click;
			// 
			// buttonEdit
			// 
			buttonEdit.Location = new Point(498, 48);
			buttonEdit.Name = "buttonEdit";
			buttonEdit.Size = new Size(141, 38);
			buttonEdit.TabIndex = 16;
			buttonEdit.Text = "Edit";
			buttonEdit.UseVisualStyleBackColor = true;
			buttonEdit.Click += buttonEdit_Click;
			// 
			// buttonRedo
			// 
			buttonRedo.Location = new Point(658, 97);
			buttonRedo.Name = "buttonRedo";
			buttonRedo.Size = new Size(84, 37);
			buttonRedo.TabIndex = 17;
			buttonRedo.Text = "Redo";
			buttonRedo.UseVisualStyleBackColor = true;
			buttonRedo.Click += buttonRedo_Click;
			// 
			// buttonUndo
			// 
			buttonUndo.Location = new Point(568, 95);
			buttonUndo.Name = "buttonUndo";
			buttonUndo.Size = new Size(84, 40);
			buttonUndo.TabIndex = 18;
			buttonUndo.Text = "Undo";
			buttonUndo.UseVisualStyleBackColor = true;
			buttonUndo.Click += buttonUndo_Click;
			// 
			// openFileDialog
			// 
			openFileDialog.FileName = "openFileDialog1";
			// 
			// textBoxMaxSpeed
			// 
			textBoxMaxSpeed.Location = new Point(13, 142);
			textBoxMaxSpeed.Name = "textBoxMaxSpeed";
			textBoxMaxSpeed.PlaceholderText = "Enter max speed";
			textBoxMaxSpeed.Size = new Size(244, 31);
			textBoxMaxSpeed.TabIndex = 19;
			// 
			// textBoxManeSize
			// 
			textBoxManeSize.Location = new Point(13, 96);
			textBoxManeSize.Name = "textBoxManeSize";
			textBoxManeSize.PlaceholderText = "Enter mane size";
			textBoxManeSize.Size = new Size(244, 31);
			textBoxManeSize.TabIndex = 20;
			// 
			// buttonLoadDll
			// 
			buttonLoadDll.Location = new Point(487, 98);
			buttonLoadDll.Name = "buttonLoadDll";
			buttonLoadDll.Size = new Size(75, 34);
			buttonLoadDll.TabIndex = 21;
			buttonLoadDll.Text = "Load Animal DLL";
			buttonLoadDll.Click += buttonLoadDll_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(865, 630);
			Controls.Add(textBoxManeSize);
			Controls.Add(textBoxMaxSpeed);
			Controls.Add(buttonUndo);
			Controls.Add(buttonRedo);
			Controls.Add(buttonEdit);
			Controls.Add(buttonDelete);
			Controls.Add(animalListView);
			Controls.Add(textBoxFlightSpeed);
			Controls.Add(textBoxAnimalInfo);
			Controls.Add(textBoxDepth);
			Controls.Add(textBoxBreed);
			Controls.Add(labelAnimalCount);
			Controls.Add(labelAnimalInfo);
			Controls.Add(comboBoxAnimalType);
			Controls.Add(pictureBoxAnimal);
			Controls.Add(textBoxName);
			Controls.Add(buttonCreate);
			Controls.Add(buttonLoadDll);
			Name = "Form1";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)pictureBoxAnimal).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Button buttonLoadDll;
		private Button buttonCreate;
		private TextBox textBoxName;
		private PictureBox pictureBoxAnimal;
		private ComboBox comboBoxAnimalType;
		private Label labelAnimalInfo;
		private Label labelAnimalCount;
		private TextBox textBoxBreed;
		private TextBox textBoxDepth;
		private TextBox textBoxAnimalInfo;
		private TextBox textBoxFlightSpeed;
		private ListView animalListView;
		private Button buttonDelete;
		private Button buttonEdit;
		private Button buttonRedo;
		private Button buttonUndo;
		private OpenFileDialog openFileDialog;
		private SaveFileDialog saveFileDialog;
		private TextBox textBoxMaxSpeed;
		private TextBox textBoxManeSize;
	}
}
