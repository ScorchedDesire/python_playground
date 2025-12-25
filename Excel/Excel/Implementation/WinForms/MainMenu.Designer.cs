namespace Excel
{
    partial class MainMenu
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
            excelGrid = new DataGridView();
            actionBar = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newFile = new ToolStripMenuItem();
            loadFile = new ToolStripMenuItem();
            saveFile = new ToolStripMenuItem();
            importFile = new ToolStripMenuItem();
            exportFile = new ToolStripMenuItem();
            accountToolStripMenuItem = new ToolStripMenuItem();
            loginToolStripMenuItem = new ToolStripMenuItem();
            registerToolStripMenuItem = new ToolStripMenuItem();
            accountStatusToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            applyChangesButton = new Button();
            option1 = new ComboBox();
            amountToApply = new NumericUpDown();
            logoutToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)excelGrid).BeginInit();
            actionBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)amountToApply).BeginInit();
            SuspendLayout();
            // 
            // excelGrid
            // 
            excelGrid.AllowUserToOrderColumns = true;
            excelGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            excelGrid.Location = new Point(12, 66);
            excelGrid.Name = "excelGrid";
            excelGrid.RowHeadersWidth = 51;
            excelGrid.Size = new Size(1329, 608);
            excelGrid.TabIndex = 0;
            excelGrid.CellBeginEdit += excelGrid_CellBeginEdit;
            excelGrid.CellEndEdit += excelGrid_CellEndEdit;
            // 
            // actionBar
            // 
            actionBar.ImageScalingSize = new Size(20, 20);
            actionBar.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, accountToolStripMenuItem, settingsToolStripMenuItem });
            actionBar.Location = new Point(0, 0);
            actionBar.Name = "actionBar";
            actionBar.Size = new Size(1353, 28);
            actionBar.TabIndex = 1;
            actionBar.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newFile, loadFile, saveFile, importFile, exportFile });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(55, 24);
            fileToolStripMenuItem.Text = "File...";
            // 
            // newFile
            // 
            newFile.Name = "newFile";
            newFile.Size = new Size(146, 26);
            newFile.Text = "New...";
            newFile.Click += newFile_Click;
            // 
            // loadFile
            // 
            loadFile.Name = "loadFile";
            loadFile.Size = new Size(146, 26);
            loadFile.Text = "Open...";
            loadFile.Click += loadFile_Click;
            // 
            // saveFile
            // 
            saveFile.Name = "saveFile";
            saveFile.Size = new Size(146, 26);
            saveFile.Text = "Save...";
            saveFile.Click += saveFile_Click;
            // 
            // importFile
            // 
            importFile.Name = "importFile";
            importFile.Size = new Size(146, 26);
            importFile.Text = "Import...";
            importFile.Click += importFile_Click;
            // 
            // exportFile
            // 
            exportFile.Name = "exportFile";
            exportFile.Size = new Size(146, 26);
            exportFile.Text = "Export...";
            exportFile.Click += exportFile_Click;
            // 
            // accountToolStripMenuItem
            // 
            accountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loginToolStripMenuItem, logoutToolStripMenuItem, registerToolStripMenuItem, accountStatusToolStripMenuItem });
            accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            accountToolStripMenuItem.Size = new Size(77, 24);
            accountToolStripMenuItem.Text = "Account";
            // 
            // loginToolStripMenuItem
            // 
            loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            loginToolStripMenuItem.Size = new Size(224, 26);
            loginToolStripMenuItem.Text = "Login...";
            loginToolStripMenuItem.Click += loginToolStripMenuItem_Click;
            // 
            // registerToolStripMenuItem
            // 
            registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            registerToolStripMenuItem.Size = new Size(224, 26);
            registerToolStripMenuItem.Text = "Register...";
            registerToolStripMenuItem.Click += registerToolStripMenuItem_Click;
            // 
            // accountStatusToolStripMenuItem
            // 
            accountStatusToolStripMenuItem.Name = "accountStatusToolStripMenuItem";
            accountStatusToolStripMenuItem.Size = new Size(224, 26);
            accountStatusToolStripMenuItem.Text = "Account status";
            accountStatusToolStripMenuItem.Click += accountStatusToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(76, 24);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // applyChangesButton
            // 
            applyChangesButton.Location = new Point(12, 31);
            applyChangesButton.Name = "applyChangesButton";
            applyChangesButton.Size = new Size(56, 29);
            applyChangesButton.TabIndex = 2;
            applyChangesButton.Text = "Apply";
            applyChangesButton.UseVisualStyleBackColor = true;
            applyChangesButton.Click += applyChangesButton_Click;
            // 
            // option1
            // 
            option1.FormattingEnabled = true;
            option1.Items.AddRange(new object[] { "Add Columns", "Remove Columns" });
            option1.Location = new Point(74, 32);
            option1.Name = "option1";
            option1.Size = new Size(143, 28);
            option1.TabIndex = 3;
            // 
            // amountToApply
            // 
            amountToApply.Location = new Point(223, 33);
            amountToApply.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            amountToApply.Name = "amountToApply";
            amountToApply.Size = new Size(57, 27);
            amountToApply.TabIndex = 5;
            amountToApply.TextAlign = HorizontalAlignment.Center;
            amountToApply.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new Size(224, 26);
            logoutToolStripMenuItem.Text = "Logout...";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 686);
            Controls.Add(amountToApply);
            Controls.Add(option1);
            Controls.Add(applyChangesButton);
            Controls.Add(excelGrid);
            Controls.Add(actionBar);
            MainMenuStrip = actionBar;
            Name = "MainMenu";
            Text = "Excel";
            ((System.ComponentModel.ISupportInitialize)excelGrid).EndInit();
            actionBar.ResumeLayout(false);
            actionBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)amountToApply).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView excelGrid;
        private MenuStrip actionBar;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newFile;
        private ToolStripMenuItem loadFile;
        private ToolStripMenuItem saveFile;
        private ToolStripMenuItem importFile;
        private ToolStripMenuItem exportFile;
        private ToolStripMenuItem accountToolStripMenuItem;
        private ToolStripMenuItem loginToolStripMenuItem;
        private ToolStripMenuItem registerToolStripMenuItem;
        private ToolStripMenuItem accountStatusToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Button applyChangesButton;
        private ComboBox option1;
        private NumericUpDown amountToApply;
        private ToolStripMenuItem logoutToolStripMenuItem;
    }
}
