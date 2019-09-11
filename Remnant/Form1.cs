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
            Program.OnReroll += RefreshCampaign;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Reading...";
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                Program.WatchForChanges(dialog.FileName);
            }
            else
            {
                textBox1.Text = "No save file selected";
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
        private void RefreshCampaign(string current, List<RemnantEvent> events)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    RefreshCampaign(current, events);
                });
                return;
            }
            // Text
            textBox2.Text = current;
            // Tree
            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            foreach (var e in events)
            {
                treeView1.Nodes.AddIf(new TreeNode(e.zone) { Name = e.zone });
                treeView1.Nodes[e.zone].Nodes.AddIf(new TreeNode(e.mainLocation) { Name = e.mainLocation });
                treeView1.Nodes[e.zone].Nodes[e.mainLocation].Nodes.AddIf(new TreeNode(e.subLocation) { Name = e.subLocation });
                treeView1.Nodes[e.zone].Nodes[e.mainLocation].Nodes[e.subLocation].Nodes.AddIf(new TreeNode(e.eventType) { Name = e.eventType });
                treeView1.Nodes[e.zone].Nodes[e.mainLocation].Nodes[e.subLocation].Nodes[e.eventType].Nodes.AddIf(new TreeNode(e.eventName) { Name = e.eventName });
            }
            treeView1.ExpandAll();
        }
    }
}
