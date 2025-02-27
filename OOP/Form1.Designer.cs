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
			Button1 = new Button();
			TextBox1 = new TextBox();
			pictureBoxTiger = new PictureBox();
			pictureBoxEagle = new PictureBox();
			pictureBoxPike = new PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBoxTiger).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxEagle).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPike).BeginInit();
			SuspendLayout();
			// 
			// Button1
			// 
			Button1.Location = new Point(22, 126);
			Button1.Margin = new Padding(2);
			Button1.Name = "Button1";
			Button1.Size = new Size(99, 26);
			Button1.TabIndex = 0;
			Button1.Text = "Watch";
			Button1.UseVisualStyleBackColor = true;
			Button1.Click += Button1_Click;
			// 
			// TextBox1
			// 
			TextBox1.Location = new Point(142, 15);
			TextBox1.Margin = new Padding(2);
			TextBox1.Multiline = true;
			TextBox1.Name = "TextBox1";
			TextBox1.ReadOnly = true;
			TextBox1.Size = new Size(199, 254);
			TextBox1.TabIndex = 1;
			// 
			// pictureBoxTiger
			// 
			pictureBoxTiger.Location = new Point(381, 15);
			pictureBoxTiger.Margin = new Padding(2);
			pictureBoxTiger.Name = "pictureBoxTiger";
			pictureBoxTiger.Size = new Size(166, 78);
			pictureBoxTiger.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBoxTiger.TabIndex = 2;
			pictureBoxTiger.TabStop = false;
			// 
			// pictureBoxEagle
			// 
			pictureBoxEagle.Location = new Point(381, 97);
			pictureBoxEagle.Margin = new Padding(2);
			pictureBoxEagle.Name = "pictureBoxEagle";
			pictureBoxEagle.Size = new Size(166, 84);
			pictureBoxEagle.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBoxEagle.TabIndex = 3;
			pictureBoxEagle.TabStop = false;
			// 
			// pictureBoxPike
			// 
			pictureBoxPike.Location = new Point(381, 185);
			pictureBoxPike.Margin = new Padding(2);
			pictureBoxPike.Name = "pictureBoxPike";
			pictureBoxPike.Size = new Size(166, 82);
			pictureBoxPike.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBoxPike.TabIndex = 4;
			pictureBoxPike.TabStop = false;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(559, 279);
			Controls.Add(pictureBoxPike);
			Controls.Add(pictureBoxEagle);
			Controls.Add(pictureBoxTiger);
			Controls.Add(TextBox1);
			Controls.Add(Button1);
			Margin = new Padding(2);
			Name = "Form1";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)pictureBoxTiger).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxEagle).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPike).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button Button1;
		private TextBox TextBox1;
		private PictureBox pictureBoxTiger;
		private PictureBox pictureBoxEagle;
		private PictureBox pictureBoxPike;
	}
}
