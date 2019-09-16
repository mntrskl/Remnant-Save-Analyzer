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
            ResetFilterComboBoxes();
        }

        public void ResetFilterComboBoxes()
        {
            CBFilterGroup.Text = " ";
            CBFilterValue.TextChanged -= CBFilterValue_TextChanged;
            CBFilterValue.Text = string.Empty;
            CBFilterValue.DataSource = null;
            CBFilterValue.TextChanged += CBFilterValue_TextChanged;
        }

        public void FillAdventureGrid(DataTable table)
        {
            GVAdventure.DataSource = table;
            GVAdventure.AutoResizeColumns();
        }

        private void CBFilterGroup_TextChanged(object sender, EventArgs e)
        {
            if (CBFilterGroup.Text.Equals(string.Empty) 
                || CBFilterGroup.Text.Equals(" "))
            {
                CBFilterValue.Text = string.Empty;
                return;
            }

            CBFilterValue.TextChanged -= CBFilterValue_TextChanged;
            CBFilterValue.DataSource = GetFilterValues();
            CBFilterValue.TextChanged += CBFilterValue_TextChanged;
        }

        private List<string> GetFilterValues()
        {
            List<string> stringList = new List<string>();
            stringList.Add(string.Empty);
            if (GVCampaign.DataSource == null)
                return stringList;

            foreach (DataRow row in (GVCampaign.DataSource as DataTable).Rows)
            {
                string value = row[CBFilterGroup.Text].ToString();
                if (!stringList.Contains(value))
                    stringList.Add(value);
            }

            return stringList;
        }

        private void CBFilterValue_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (GVCampaign.DataSource == null) return;
            if (CBFilterValue.Text.Equals(string.Empty)) (GVCampaign.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            else (GVCampaign.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] = '{1}'", CBFilterGroup.Text, CBFilterValue.Text);
        }
    }
}
