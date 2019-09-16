namespace Remnant
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GVAdventure = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.GVCampaign = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.autoRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CBFilterGroup = new System.Windows.Forms.ComboBox();
            this.CBFilterValue = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.GVAdventure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVCampaign)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(903, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Campaign";
            // 
            // GVAdventure
            // 
            this.GVAdventure.AllowUserToAddRows = false;
            this.GVAdventure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GVAdventure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVAdventure.Location = new System.Drawing.Point(12, 385);
            this.GVAdventure.Name = "GVAdventure";
            this.GVAdventure.ReadOnly = true;
            this.GVAdventure.RowHeadersVisible = false;
            this.GVAdventure.Size = new System.Drawing.Size(713, 300);
            this.GVAdventure.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Adventure";
            // 
            // GVCampaign
            // 
            this.GVCampaign.AllowUserToAddRows = false;
            this.GVCampaign.AllowUserToDeleteRows = false;
            this.GVCampaign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GVCampaign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVCampaign.Location = new System.Drawing.Point(12, 66);
            this.GVCampaign.Name = "GVCampaign";
            this.GVCampaign.ReadOnly = true;
            this.GVCampaign.RowHeadersVisible = false;
            this.GVCampaign.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GVCampaign.Size = new System.Drawing.Size(713, 300);
            this.GVCampaign.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(927, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSaveToolStripMenuItem,
            this.exportCSVToolStripMenuItem,
            this.toolStripSeparator1,
            this.autoRefreshToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadSaveToolStripMenuItem
            // 
            this.loadSaveToolStripMenuItem.Name = "loadSaveToolStripMenuItem";
            this.loadSaveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadSaveToolStripMenuItem.Text = "Load Save";
            this.loadSaveToolStripMenuItem.Click += new System.EventHandler(this.LoadSaveToolStripMenuItem_Click);
            // 
            // exportCSVToolStripMenuItem
            // 
            this.exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            this.exportCSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportCSVToolStripMenuItem.Text = "Export CSV";
            this.exportCSVToolStripMenuItem.Click += new System.EventHandler(this.ExportCSVToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // autoRefreshToolStripMenuItem
            // 
            this.autoRefreshToolStripMenuItem.Name = "autoRefreshToolStripMenuItem";
            this.autoRefreshToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.autoRefreshToolStripMenuItem.Text = "Auto Refresh";
            this.autoRefreshToolStripMenuItem.Click += new System.EventHandler(this.AutoRefreshToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(731, 66);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Filter By:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(731, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Filter Value:";
            // 
            // CBFilterGroup
            // 
            this.CBFilterGroup.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CBFilterGroup.BackColor = System.Drawing.SystemColors.Window;
            this.CBFilterGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBFilterGroup.FormattingEnabled = true;
            this.CBFilterGroup.Items.AddRange(new object[] {
            " ",
            "Zone",
            "Sub-Zone",
            "Location",
            "Type"});
            this.CBFilterGroup.Location = new System.Drawing.Point(797, 63);
            this.CBFilterGroup.Name = "CBFilterGroup";
            this.CBFilterGroup.Size = new System.Drawing.Size(121, 21);
            this.CBFilterGroup.TabIndex = 11;
            this.CBFilterGroup.TextChanged += new System.EventHandler(this.CBFilterGroup_TextChanged);
            // 
            // CBFilterValue
            // 
            this.CBFilterValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CBFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBFilterValue.FormattingEnabled = true;
            this.CBFilterValue.Location = new System.Drawing.Point(797, 87);
            this.CBFilterValue.Name = "CBFilterValue";
            this.CBFilterValue.Size = new System.Drawing.Size(121, 21);
            this.CBFilterValue.TabIndex = 12;
            this.CBFilterValue.TextChanged += new System.EventHandler(this.CBFilterValue_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 691);
            this.Controls.Add(this.CBFilterValue);
            this.Controls.Add(this.CBFilterGroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GVCampaign);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GVAdventure);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1300, 730);
            this.MinimumSize = new System.Drawing.Size(600, 730);
            this.Name = "MainForm";
            this.Text = "Remnant Save Parser";
            ((System.ComponentModel.ISupportInitialize)(this.GVAdventure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVCampaign)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GVAdventure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView GVCampaign;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem autoRefreshToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CBFilterGroup;
        private System.Windows.Forms.ComboBox CBFilterValue;
    }
}

