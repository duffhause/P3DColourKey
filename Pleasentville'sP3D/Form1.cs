using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace PleasentvillesP3D
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			ShowColourRange();
		}

		private Color GetBound(Color baseColour, int threshold)
		{
			int lbr = baseColour.R + threshold;
			int lbg = baseColour.G + threshold;
			int lbb = baseColour.B + threshold;

			if (lbr < 0) lbr = 0;
			if (lbr > 255) lbr = 255;

			if (lbg < 0) lbg = 0;
			if (lbg > 255) lbg = 255;

			if (lbb< 0) lbb = 0;
			if (lbb > 255) lbb = 255;

			return Color.FromArgb(lbr, lbg, lbb);
		}

		private void ShowColourRange()
		{
			Color baseColour = colorDialog1.Color;
			int threshhold = threshholdTrackbar.Value;
			
			thresholdAmount.Text = Convert.ToString(threshhold);

			pictureBox1.BackColor = colorDialog1.Color;

			Bitmap rainbow = Images.ShindlerBitmap(Properties.Resources.rainbow, baseColour, threshhold);
			pictureBox2.Image = rainbow;
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() != DialogResult.Cancel)
			{
				ShowColourRange();
			}
		}

		private void threshholdTrackbar_Scroll(object sender, EventArgs e)
		{
			ShowColourRange();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
			{
				singleP3DTextbox.Text = openFileDialog1.FileName;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
			{
				folderTextbox.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void Shindler (string filepath)
		{
			textBox1.AppendText(Environment.NewLine);
			textBox1.AppendText(filepath);

			P3D p = new P3D();
			p.ReadP3D(filepath);
			p.Root = p.ShindlerTextures(p.Root, colorDialog1.Color, threshholdTrackbar.Value);
			p.WriteP3D(filepath);


			progressBar1.Value++;
			this.Update();
		}

		private void ProcessDir(string dir)
		{
			foreach (string file in Directory.GetFiles(dir))
			{
				if (Path.GetExtension(file).ToLower() == ".p3d")
				{
					Shindler(file);
				}
			}
			if (recursive.Checked) { 
				foreach (string file in Directory.GetDirectories(dir))
				{
					ProcessDir(file);
				}
			}
		}

		private int GetFileCount(string path, int i = 0)
		{
			foreach (string file in Directory.GetFiles(path))
			{
				if (Path.GetExtension(file).ToLower() == ".p3d")
				{
					i++;
				}
			}

			foreach (string file in Directory.GetDirectories(path))
			{
				i += GetFileCount(file);
			}

			return i;
		}

		// Submit single P3D
		private void submitSingleP3D_Click(object sender, EventArgs e)
		{
			progressBar1.Value = 0;
			progressBar1.Maximum = 1;
			Shindler(openFileDialog1.FileName);
			progressBar1.Value = 1;
		}

		// Submit folder
		private void button2_Click(object sender, EventArgs e)
		{
			int fileCount = GetFileCount(folderBrowserDialog1.SelectedPath);
			progressBar1.Maximum = fileCount;
			progressBar1.Value = 0;

			configBox.Enabled = false;
			fileControl.Enabled = false;

			// Run in a thread to keep GUI responsive
			ProcessDir(folderBrowserDialog1.SelectedPath);

			configBox.Enabled = true;
			fileControl.Enabled = true;
		}

	}
}
