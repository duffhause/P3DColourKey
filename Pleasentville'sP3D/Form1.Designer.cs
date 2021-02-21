
namespace PleasentvillesP3D
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.resultBox = new System.Windows.Forms.GroupBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.configBox = new System.Windows.Forms.GroupBox();
			this.recursive = new System.Windows.Forms.CheckBox();
			this.thresholdAmount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.threshholdTrackbar = new System.Windows.Forms.TrackBar();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.colourLabel = new System.Windows.Forms.Label();
			this.colorDialog2 = new System.Windows.Forms.ColorDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.fileControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.submitSingleP3D = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.singleP3DTextbox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.folderTextbox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.resultBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.configBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.threshholdTrackbar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.fileControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// colorDialog1
			// 
			this.colorDialog1.Color = System.Drawing.Color.Red;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "P3D files|*.P3D";
			// 
			// resultBox
			// 
			this.resultBox.Controls.Add(this.pictureBox2);
			this.resultBox.Location = new System.Drawing.Point(12, 12);
			this.resultBox.Name = "resultBox";
			this.resultBox.Size = new System.Drawing.Size(776, 100);
			this.resultBox.TabIndex = 0;
			this.resultBox.TabStop = false;
			this.resultBox.Text = "Range";
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox2.Image = global::PleasentvillesP3D.Properties.Resources.rainbow;
			this.pictureBox2.InitialImage = null;
			this.pictureBox2.Location = new System.Drawing.Point(6, 21);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(764, 73);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			// 
			// configBox
			// 
			this.configBox.Controls.Add(this.recursive);
			this.configBox.Controls.Add(this.thresholdAmount);
			this.configBox.Controls.Add(this.label1);
			this.configBox.Controls.Add(this.threshholdTrackbar);
			this.configBox.Controls.Add(this.pictureBox1);
			this.configBox.Controls.Add(this.colourLabel);
			this.configBox.Location = new System.Drawing.Point(19, 118);
			this.configBox.Name = "configBox";
			this.configBox.Size = new System.Drawing.Size(388, 222);
			this.configBox.TabIndex = 1;
			this.configBox.TabStop = false;
			this.configBox.Text = "Config";
			// 
			// recursive
			// 
			this.recursive.AutoSize = true;
			this.recursive.Checked = true;
			this.recursive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.recursive.Location = new System.Drawing.Point(6, 27);
			this.recursive.Name = "recursive";
			this.recursive.Size = new System.Drawing.Size(200, 21);
			this.recursive.TabIndex = 8;
			this.recursive.Text = "Process folders recursively";
			this.recursive.UseVisualStyleBackColor = true;
			// 
			// thresholdAmount
			// 
			this.thresholdAmount.AutoSize = true;
			this.thresholdAmount.Location = new System.Drawing.Point(342, 202);
			this.thresholdAmount.Name = "thresholdAmount";
			this.thresholdAmount.Size = new System.Drawing.Size(40, 17);
			this.thresholdAmount.TabIndex = 5;
			this.thresholdAmount.Text = "9999";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15, 160);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Tolerance:";
			// 
			// threshholdTrackbar
			// 
			this.threshholdTrackbar.BackColor = System.Drawing.SystemColors.Control;
			this.threshholdTrackbar.LargeChange = 10;
			this.threshholdTrackbar.Location = new System.Drawing.Point(105, 160);
			this.threshholdTrackbar.Maximum = 255;
			this.threshholdTrackbar.Name = "threshholdTrackbar";
			this.threshholdTrackbar.Size = new System.Drawing.Size(277, 56);
			this.threshholdTrackbar.TabIndex = 3;
			this.threshholdTrackbar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.threshholdTrackbar.Value = 50;
			this.threshholdTrackbar.ValueChanged += new System.EventHandler(this.threshholdTrackbar_Scroll);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Red;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(105, 132);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(22, 22);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// colourLabel
			// 
			this.colourLabel.AutoSize = true;
			this.colourLabel.Location = new System.Drawing.Point(33, 132);
			this.colourLabel.Name = "colourLabel";
			this.colourLabel.Size = new System.Drawing.Size(53, 17);
			this.colourLabel.TabIndex = 1;
			this.colourLabel.Text = "Colour:";
			// 
			// fileControl
			// 
			this.fileControl.Controls.Add(this.tabPage1);
			this.fileControl.Controls.Add(this.tabPage2);
			this.fileControl.Location = new System.Drawing.Point(19, 346);
			this.fileControl.Name = "fileControl";
			this.fileControl.SelectedIndex = 0;
			this.fileControl.Size = new System.Drawing.Size(388, 92);
			this.fileControl.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.submitSingleP3D);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.singleP3DTextbox);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(380, 63);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Single P3D";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// submitSingleP3D
			// 
			this.submitSingleP3D.Location = new System.Drawing.Point(286, 35);
			this.submitSingleP3D.Name = "submitSingleP3D";
			this.submitSingleP3D.Size = new System.Drawing.Size(75, 23);
			this.submitSingleP3D.TabIndex = 3;
			this.submitSingleP3D.Text = "Submit";
			this.submitSingleP3D.UseVisualStyleBackColor = true;
			this.submitSingleP3D.Click += new System.EventHandler(this.submitSingleP3D_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(286, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Browse...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// singleP3DTextbox
			// 
			this.singleP3DTextbox.Enabled = false;
			this.singleP3DTextbox.Location = new System.Drawing.Point(78, 7);
			this.singleP3DTextbox.Name = "singleP3DTextbox";
			this.singleP3DTextbox.Size = new System.Drawing.Size(202, 22);
			this.singleP3DTextbox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(38, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "File:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.button3);
			this.tabPage2.Controls.Add(this.folderTextbox);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(380, 63);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Folder";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(286, 35);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 7;
			this.button2.Text = "Submit";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(286, 6);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "Browse...";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// folderTextbox
			// 
			this.folderTextbox.Enabled = false;
			this.folderTextbox.Location = new System.Drawing.Point(78, 7);
			this.folderTextbox.Name = "folderTextbox";
			this.folderTextbox.Size = new System.Drawing.Size(202, 22);
			this.folderTextbox.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "Folder:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.progressBar1);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Location = new System.Drawing.Point(413, 118);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(375, 320);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Output";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(7, 288);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(362, 23);
			this.progressBar1.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(7, 25);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(362, 254);
			this.textBox1.TabIndex = 0;
			this.textBox1.WordWrap = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(806, 461);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.fileControl);
			this.Controls.Add(this.configBox);
			this.Controls.Add(this.resultBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Pleasentville\'s P3D";
			this.resultBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.configBox.ResumeLayout(false);
			this.configBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.threshholdTrackbar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.fileControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.GroupBox resultBox;
		private System.Windows.Forms.GroupBox configBox;
		private System.Windows.Forms.Label colourLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar threshholdTrackbar;
		private System.Windows.Forms.Label thresholdAmount;
		private System.Windows.Forms.ColorDialog colorDialog2;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TabControl fileControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button submitSingleP3D;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox singleP3DTextbox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.CheckBox recursive;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox folderTextbox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox textBox1;
	}
}

