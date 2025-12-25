namespace Excel.Core.Implementation.WinForms
{
    partial class SettingsMenu
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
            submitButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cellWidthPicker = new NumericUpDown();
            cellHeightPicker = new NumericUpDown();
            valueAlignments = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)cellWidthPicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cellHeightPicker).BeginInit();
            SuspendLayout();
            // 
            // submitButton
            // 
            submitButton.Location = new Point(12, 120);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(94, 29);
            submitButton.TabIndex = 0;
            submitButton.Text = "OK";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(204, 120);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 29);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 2;
            label1.Text = "Cell Width";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 55);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 3;
            label2.Text = "Cell Height";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 89);
            label3.Name = "label3";
            label3.Size = new Size(118, 20);
            label3.TabIndex = 4;
            label3.Text = "Value Alignment";
            // 
            // cellWidthPicker
            // 
            cellWidthPicker.Location = new Point(147, 20);
            cellWidthPicker.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            cellWidthPicker.Name = "cellWidthPicker";
            cellWidthPicker.Size = new Size(150, 27);
            cellWidthPicker.TabIndex = 5;
            cellWidthPicker.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cellHeightPicker
            // 
            cellHeightPicker.Location = new Point(147, 53);
            cellHeightPicker.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            cellHeightPicker.Name = "cellHeightPicker";
            cellHeightPicker.Size = new Size(150, 27);
            cellHeightPicker.TabIndex = 6;
            cellHeightPicker.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // valueAlignments
            // 
            valueAlignments.FormattingEnabled = true;
            valueAlignments.Items.AddRange(new object[] { "Left", "Right", "Center" });
            valueAlignments.Location = new Point(147, 86);
            valueAlignments.Name = "valueAlignments";
            valueAlignments.Size = new Size(151, 28);
            valueAlignments.TabIndex = 7;
            // 
            // SettingsMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 160);
            Controls.Add(valueAlignments);
            Controls.Add(cellHeightPicker);
            Controls.Add(cellWidthPicker);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelButton);
            Controls.Add(submitButton);
            Name = "SettingsMenu";
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)cellWidthPicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)cellHeightPicker).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button submitButton;
        private Button cancelButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown cellWidthPicker;
        private NumericUpDown cellHeightPicker;
        private ComboBox valueAlignments;
    }
}