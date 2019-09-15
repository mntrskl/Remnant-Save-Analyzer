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
using Remnant.Models;

namespace Remnant
{
    public partial class MainForm : Form
    {
        private readonly MainFormViewModel _viewModel;

        public MainForm(MainFormViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        private void LoadSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseDialog = new OpenFileDialog();
            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                _viewModel.FileUpdate(browseDialog.FileName);
                textBox1.Text = browseDialog.FileName;
            }
        }

        private void ExportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GVCampaign.DataSource == null) return;
            SaveFileDialog dialog = new SaveFileDialog()
            {
                FileName = "remnant_export",
                DefaultExt = ".csv",
                InitialDirectory = Directory.GetCurrentDirectory()
            };

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _viewModel.ExportCSV(dialog.FileName, GVCampaign.DataSource as DataTable);
            }
        }

        private void AutoRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoRefreshToolStripMenuItem.Checked = _viewModel.SetWatcher(!autoRefreshToolStripMenuItem.Checked);
        }

        public void UpdateCampaignGrid(DataTable table)
        {
            this.Invoke(new Action(() => { FillCampaignGrid(table); }));
        }

        public void UpdateAdventureGrid(DataTable table)
        {
            this.Invoke(new Action(() => { FillAdventureGrid(table); }));
        }

        public void FillCampaignGrid(DataTable table)
        {
            GVCampaign.DataSource = table;
            GVCampaign.AutoResizeColumns();
            CBFilterGroup.DisplayMember = string.Empty; 
        }

        public void FillAdventureGrid(DataTable table)
        {
            GVAdventure.DataSource = table;
            GVAdventure.AutoResizeColumns();
        }
            {
                dt.Rows.Add(item.zone, item.location, item.eventType, item.name, item.complete);
            }

            GVAdventure.DataSource = dt;
            GVAdventure.AutoResizeColumns();
        }
    }
}
