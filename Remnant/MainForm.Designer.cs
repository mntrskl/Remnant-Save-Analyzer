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
            this.BBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GVAdventure = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.GVCampaign = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GVAdventure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVCampaign)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(600, 20);
            this.textBox1.TabIndex = 0;
            // 
            // BBrowse
            // 
            this.BBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BBrowse.Location = new System.Drawing.Point(618, 12);
            this.BBrowse.Name = "BBrowse";
            this.BBrowse.Size = new System.Drawing.Size(106, 20);
            this.BBrowse.TabIndex = 1;
            this.BBrowse.Text = "browse";
            this.BBrowse.UseVisualStyleBackColor = true;
            this.BBrowse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Campaign";
            // 
            // GVAdventure
            // 
            this.GVAdventure.AllowUserToAddRows = false;
            this.GVAdventure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVAdventure.Location = new System.Drawing.Point(12, 313);
            this.GVAdventure.Name = "GVAdventure";
            this.GVAdventure.RowHeadersVisible = false;
            this.GVAdventure.Size = new System.Drawing.Size(712, 220);
            this.GVAdventure.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Adventure";
            // 
            // GVCampaign
            // 
            this.GVCampaign.AllowUserToAddRows = false;
            this.GVCampaign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVCampaign.Location = new System.Drawing.Point(12, 65);
            this.GVCampaign.Name = "GVCampaign";
            this.GVCampaign.RowHeadersVisible = false;
            this.GVCampaign.Size = new System.Drawing.Size(712, 229);
            this.GVCampaign.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 545);
            this.Controls.Add(this.GVCampaign);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GVAdventure);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BBrowse);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MainForm";
            this.Text = "Remnant Save Parser";
            ((System.ComponentModel.ISupportInitialize)(this.GVAdventure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVCampaign)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GVAdventure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView GVCampaign;
    }
}

