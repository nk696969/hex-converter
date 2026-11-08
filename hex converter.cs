using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace hex_converter
{
    public partial class Form1 : Form
    {
        private string selectedFilePath = "";
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select";
                dialog.Filter = "(*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = dialog.FileName;
                    textBox1.Text = "Selected: " + selectedFilePath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    MessageBox.Show("Select your file first");
                    return;
                }

                try
                {
                    byte[] bytes = File.ReadAllBytes(selectedFilePath);
                    StringBuilder hex = new StringBuilder();

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (i > 0 && i % 16 == 0)
                        {
                            hex.AppendLine();
                        }

                        hex.Append(bytes[i].ToString("X2"));
                        hex.Append(' ');
                    }

                    textBox1.Text = hex.ToString().TrimEnd();
                }
                catch
                {
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Textbox is blank");
                    return;
                }

                try
                {
                    Clipboard.SetText(textBox1.Text);
                    MessageBox.Show("Coppied");
                }
                catch
                {
                    return;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Textbox is blank");
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export";
                dialog.Filter = "(*.txt)|*.txt";
                dialog.DefaultExt = "txt";
                dialog.AddExtension = true;
                dialog.FileName = "hex_output.txt";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(
                            dialog.FileName,
                            textBox1.Text,
                            new UTF8Encoding(false)
                        );

                        MessageBox.Show("Saved as \n" + dialog.FileName);
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }
    }
}
