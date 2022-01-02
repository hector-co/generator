namespace Generator.Gui
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeModel = new System.Windows.Forms.TreeView();
            this.textFile = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.checkSelectAll = new System.Windows.Forms.CheckBox();
            this.checkDomain = new System.Windows.Forms.CheckBox();
            this.checkQueries = new System.Windows.Forms.CheckBox();
            this.checkDataAccessEf = new System.Windows.Forms.CheckBox();
            this.checkApi = new System.Windows.Forms.CheckBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.checkForceRegen = new System.Windows.Forms.CheckBox();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.checkCommands = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // treeModel
            // 
            this.treeModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeModel.CheckBoxes = true;
            this.treeModel.Location = new System.Drawing.Point(12, 91);
            this.treeModel.Name = "treeModel";
            this.treeModel.Size = new System.Drawing.Size(393, 312);
            this.treeModel.TabIndex = 7;
            // 
            // textFile
            // 
            this.textFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFile.Location = new System.Drawing.Point(12, 12);
            this.textFile.Name = "textFile";
            this.textFile.ReadOnly = true;
            this.textFile.Size = new System.Drawing.Size(344, 23);
            this.textFile.TabIndex = 9;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(362, 12);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(43, 23);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // checkSelectAll
            // 
            this.checkSelectAll.AutoSize = true;
            this.checkSelectAll.Location = new System.Drawing.Point(12, 41);
            this.checkSelectAll.Name = "checkSelectAll";
            this.checkSelectAll.Size = new System.Drawing.Size(72, 19);
            this.checkSelectAll.TabIndex = 2;
            this.checkSelectAll.Text = "Select all";
            this.checkSelectAll.UseVisualStyleBackColor = true;
            // 
            // checkDomain
            // 
            this.checkDomain.AutoSize = true;
            this.checkDomain.Location = new System.Drawing.Point(12, 66);
            this.checkDomain.Name = "checkDomain";
            this.checkDomain.Size = new System.Drawing.Size(68, 19);
            this.checkDomain.TabIndex = 3;
            this.checkDomain.Tag = "";
            this.checkDomain.Text = "Domain";
            this.checkDomain.UseVisualStyleBackColor = true;
            // 
            // checkQueries
            // 
            this.checkQueries.AutoSize = true;
            this.checkQueries.Location = new System.Drawing.Point(180, 66);
            this.checkQueries.Name = "checkQueries";
            this.checkQueries.Size = new System.Drawing.Size(66, 19);
            this.checkQueries.TabIndex = 4;
            this.checkQueries.Tag = "";
            this.checkQueries.Text = "Queries";
            this.checkQueries.UseVisualStyleBackColor = true;
            // 
            // checkDataAccessEf
            // 
            this.checkDataAccessEf.AutoSize = true;
            this.checkDataAccessEf.Location = new System.Drawing.Point(252, 66);
            this.checkDataAccessEf.Name = "checkDataAccessEf";
            this.checkDataAccessEf.Size = new System.Drawing.Size(96, 19);
            this.checkDataAccessEf.TabIndex = 5;
            this.checkDataAccessEf.Tag = "";
            this.checkDataAccessEf.Text = "DataAccessEf";
            this.checkDataAccessEf.UseVisualStyleBackColor = true;
            // 
            // checkApi
            // 
            this.checkApi.AutoSize = true;
            this.checkApi.Location = new System.Drawing.Point(354, 66);
            this.checkApi.Name = "checkApi";
            this.checkApi.Size = new System.Drawing.Size(44, 19);
            this.checkApi.TabIndex = 6;
            this.checkApi.Tag = "";
            this.checkApi.Text = "Api";
            this.checkApi.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerate.Location = new System.Drawing.Point(330, 412);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // checkForceRegen
            // 
            this.checkForceRegen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkForceRegen.AutoSize = true;
            this.checkForceRegen.Location = new System.Drawing.Point(223, 416);
            this.checkForceRegen.Name = "checkForceRegen";
            this.checkForceRegen.Size = new System.Drawing.Size(101, 19);
            this.checkForceRegen.TabIndex = 8;
            this.checkForceRegen.Text = "Overwrite files";
            this.checkForceRegen.UseVisualStyleBackColor = true;
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            this.openFile.Filter = "JSON files (*.json)|*.json";
            // 
            // checkCommands
            // 
            this.checkCommands.AutoSize = true;
            this.checkCommands.Location = new System.Drawing.Point(86, 66);
            this.checkCommands.Name = "checkCommands";
            this.checkCommands.Size = new System.Drawing.Size(88, 19);
            this.checkCommands.TabIndex = 10;
            this.checkCommands.Tag = "";
            this.checkCommands.Text = "Commands";
            this.checkCommands.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 444);
            this.Controls.Add(this.checkCommands);
            this.Controls.Add(this.checkForceRegen);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.checkApi);
            this.Controls.Add(this.checkDataAccessEf);
            this.Controls.Add(this.checkQueries);
            this.Controls.Add(this.checkDomain);
            this.Controls.Add(this.checkSelectAll);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textFile);
            this.Controls.Add(this.treeModel);
            this.MinimumSize = new System.Drawing.Size(431, 483);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeModel;
        private System.Windows.Forms.TextBox textFile;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.CheckBox checkSelectAll;
        private System.Windows.Forms.CheckBox checkDomain;
        private System.Windows.Forms.CheckBox checkQueries;
        private System.Windows.Forms.CheckBox checkDataAccessEf;
        private System.Windows.Forms.CheckBox checkApi;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.CheckBox checkForceRegen;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.CheckBox checkCommands;
    }
}