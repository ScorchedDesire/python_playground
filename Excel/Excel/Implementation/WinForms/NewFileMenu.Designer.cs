namespace Excel.Core.Implementation.WinForms
{
    partial class NewFileMenu
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
            confirmButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            label2 = new Label();
            rowCount = new NumericUpDown();
            columnCount = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)rowCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)columnCount).BeginInit();
            SuspendLayout();
            // 
            // confirmButton
            // 
            confirmButton.Location = new Point(12, 95);
            confirmButton.Name = "confirmButton";
            confirmButton.Size = new Size(94, 29);
            confirmButton.TabIndex = 0;
            confirmButton.Text = "OK";
            confirmButton.UseVisualStyleBackColor = true;
            confirmButton.Click += confirmButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(142, 95);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 29);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(44, 20);
            label1.TabIndex = 2;
            label1.Text = "Rows";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 53);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 3;
            label2.Text = "Columns";
            // 
            // rowCount
            // 
            rowCount.Location = new Point(86, 12);
            rowCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            rowCount.Name = "rowCount";
            rowCount.Size = new Size(150, 27);
            rowCount.TabIndex = 4;
            rowCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // columnCount
            // 
            columnCount.Location = new Point(86, 51);
            columnCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            columnCount.Name = "columnCount";
            columnCount.Size = new Size(150, 27);
            columnCount.TabIndex = 5;
            columnCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // NewFileMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(252, 137);
            Controls.Add(columnCount);
            Controls.Add(rowCount);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelButton);
            Controls.Add(confirmButton);
            Name = "NewFileMenu";
            Text = "New file...";
            ((System.ComponentModel.ISupportInitialize)rowCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)columnCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button confirmButton;
        private Button cancelButton;
        private Label label1;
        private Label label2;
        private NumericUpDown rowCount;
        private NumericUpDown columnCount;
    }
}