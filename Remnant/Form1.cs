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

namespace Remnant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                Program.WatchForChanges(dialog.FileName);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Program.currentSave == "") return;
            SaveFileDialog dialog = new SaveFileDialog()
            {
                FileName = "remnant_export",
                DefaultExt = ".csv",
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Program.SaveCSV(dialog.FileName);
            }
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
