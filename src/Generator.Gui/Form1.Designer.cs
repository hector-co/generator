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
            treeModel = new System.Windows.Forms.TreeView();
            textFile = new System.Windows.Forms.TextBox();
            buttonBrowse = new System.Windows.Forms.Button();
            checkSelectAll = new System.Windows.Forms.CheckBox();
            checkDomain = new System.Windows.Forms.CheckBox();
            checkQueries = new System.Windows.Forms.CheckBox();
            checkDataAccessEf = new System.Windows.Forms.CheckBox();
            checkApi = new System.Windows.Forms.CheckBox();
            buttonGenerate = new System.Windows.Forms.Button();
            checkForceRegen = new System.Windows.Forms.CheckBox();
            openFile = new System.Windows.Forms.OpenFileDialog();
            checkCommands = new System.Windows.Forms.CheckBox();
            checkOneOf = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // treeModel
            // 
            treeModel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            treeModel.CheckBoxes = true;
            treeModel.Location = new System.Drawing.Point(12, 91);
            treeModel.Name = "treeModel";
            treeModel.Size = new System.Drawing.Size(393, 312);
            treeModel.TabIndex = 8;
            // 
            // textFile
            // 
            textFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textFile.Location = new System.Drawing.Point(12, 12);
            textFile.Name = "textFile";
            textFile.ReadOnly = true;
            textFile.Size = new System.Drawing.Size(344, 23);
            textFile.TabIndex = 9;
            // 
            // buttonBrowse
            // 
            buttonBrowse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonBrowse.Location = new System.Drawing.Point(362, 12);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new System.Drawing.Size(43, 23);
            buttonBrowse.TabIndex = 1;
            buttonBrowse.Text = "...";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // checkSelectAll
            // 
            checkSelectAll.AutoSize = true;
            checkSelectAll.Location = new System.Drawing.Point(12, 41);
            checkSelectAll.Name = "checkSelectAll";
            checkSelectAll.Size = new System.Drawing.Size(72, 19);
            checkSelectAll.TabIndex = 2;
            checkSelectAll.Text = "Select all";
            checkSelectAll.UseVisualStyleBackColor = true;
            // 
            // checkDomain
            // 
            checkDomain.AutoSize = true;
            checkDomain.Location = new System.Drawing.Point(12, 66);
            checkDomain.Name = "checkDomain";
            checkDomain.Size = new System.Drawing.Size(68, 19);
            checkDomain.TabIndex = 3;
            checkDomain.Tag = "";
            checkDomain.Text = "Domain";
            checkDomain.UseVisualStyleBackColor = true;
            // 
            // checkQueries
            // 
            checkQueries.AutoSize = true;
            checkQueries.Location = new System.Drawing.Point(180, 66);
            checkQueries.Name = "checkQueries";
            checkQueries.Size = new System.Drawing.Size(66, 19);
            checkQueries.TabIndex = 5;
            checkQueries.Tag = "";
            checkQueries.Text = "Queries";
            checkQueries.UseVisualStyleBackColor = true;
            // 
            // checkDataAccessEf
            // 
            checkDataAccessEf.AutoSize = true;
            checkDataAccessEf.Location = new System.Drawing.Point(252, 66);
            checkDataAccessEf.Name = "checkDataAccessEf";
            checkDataAccessEf.Size = new System.Drawing.Size(96, 19);
            checkDataAccessEf.TabIndex = 6;
            checkDataAccessEf.Tag = "";
            checkDataAccessEf.Text = "DataAccessEf";
            checkDataAccessEf.UseVisualStyleBackColor = true;
            // 
            // checkApi
            // 
            checkApi.AutoSize = true;
            checkApi.Location = new System.Drawing.Point(354, 66);
            checkApi.Name = "checkApi";
            checkApi.Size = new System.Drawing.Size(44, 19);
            checkApi.TabIndex = 7;
            checkApi.Tag = "";
            checkApi.Text = "Api";
            checkApi.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            buttonGenerate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonGenerate.Location = new System.Drawing.Point(330, 412);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new System.Drawing.Size(75, 23);
            buttonGenerate.TabIndex = 0;
            buttonGenerate.Text = "Generate";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // checkForceRegen
            // 
            checkForceRegen.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkForceRegen.AutoSize = true;
            checkForceRegen.Location = new System.Drawing.Point(223, 416);
            checkForceRegen.Name = "checkForceRegen";
            checkForceRegen.Size = new System.Drawing.Size(101, 19);
            checkForceRegen.TabIndex = 9;
            checkForceRegen.Text = "Overwrite files";
            checkForceRegen.UseVisualStyleBackColor = true;
            // 
            // openFile
            // 
            openFile.FileName = "openFileDialog1";
            openFile.Filter = "JSON files (*.json)|*.json";
            // 
            // checkCommands
            // 
            checkCommands.AutoSize = true;
            checkCommands.Location = new System.Drawing.Point(86, 66);
            checkCommands.Name = "checkCommands";
            checkCommands.Size = new System.Drawing.Size(88, 19);
            checkCommands.TabIndex = 4;
            checkCommands.Tag = "";
            checkCommands.Text = "Commands";
            checkCommands.UseVisualStyleBackColor = true;
            // 
            // checkOneOf
            // 
            checkOneOf.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            checkOneOf.AutoSize = true;
            checkOneOf.Location = new System.Drawing.Point(12, 416);
            checkOneOf.Name = "checkOneOf";
            checkOneOf.Size = new System.Drawing.Size(83, 19);
            checkOneOf.TabIndex = 10;
            checkOneOf.Tag = "";
            checkOneOf.Text = "Use OneOf";
            checkOneOf.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(415, 444);
            Controls.Add(checkOneOf);
            Controls.Add(checkCommands);
            Controls.Add(checkForceRegen);
            Controls.Add(buttonGenerate);
            Controls.Add(checkApi);
            Controls.Add(checkDataAccessEf);
            Controls.Add(checkQueries);
            Controls.Add(checkDomain);
            Controls.Add(checkSelectAll);
            Controls.Add(buttonBrowse);
            Controls.Add(textFile);
            Controls.Add(treeModel);
            MinimumSize = new System.Drawing.Size(431, 483);
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Generator";
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.CheckBox checkOneOf;
    }
}