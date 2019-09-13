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

        public void UpdateCampaignGrid(List<EventModel> eventList)
        {
            this.Invoke(new Action(() => { FillCampaignGrid(eventList); }));
        }

        public void UpdateAdventureGrid(List<EventModel> eventList)
        {
            this.Invoke(new Action(() => { FillAdventureGrid(eventList); }));
        }

        public void FillCampaignGrid(List<EventModel> eventList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Zone", typeof(string));
            dt.Columns.Add("Sub-Zone", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Complete", typeof(bool));

            foreach (var item in eventList)
            {
                dt.Rows.Add(item.zone, item.subZone, item.location, item.eventType.ToReadableString(), item.name, item.complete);
            }

            GVCampaign.DataSource = dt;
            GVCampaign.AutoResizeColumns();
        }

        public void FillAdventureGrid(List<EventModel> eventList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Zone", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("Type", typeof(EventType));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Complete", typeof(bool));

            foreach (var item in eventList)
            {
                dt.Rows.Add(item.zone, item.location, item.eventType, item.name, item.complete);
            }

            GVAdventure.DataSource = dt;
            GVAdventure.AutoResizeColumns();
        }
    }
}
