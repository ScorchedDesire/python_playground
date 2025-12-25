namespace Excel.Core.Implementation.WinForms
{
    partial class RegisterMenu
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
            emailBox = new TextBox();
            loginBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            passwordBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            passwordBox2 = new TextBox();
            registerButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // emailBox
            // 
            emailBox.Location = new Point(95, 43);
            emailBox.Name = "emailBox";
            emailBox.Size = new Size(278, 27);
            emailBox.TabIndex = 9;
            // 
            // loginBox
            // 
            loginBox.Location = new Point(95, 12);
            loginBox.Name = "loginBox";
            loginBox.Size = new Size(278, 27);
            loginBox.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 95);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 7;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 15);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 6;
            label1.Text = "Login";
            // 
            // passwordBox
            // 
            passwordBox.Location = new Point(95, 92);
            passwordBox.Name = "passwordBox";
            passwordBox.PasswordChar = '*';
            passwordBox.Size = new Size(278, 27);
            passwordBox.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 46);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 11;
            label3.Text = "Email";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 126);
            label4.Name = "label4";
            label4.Size = new Size(65, 20);
            label4.TabIndex = 12;
            label4.Text = "Confirm:";
            // 
            // passwordBox2
            // 
            passwordBox2.Location = new Point(95, 123);
            passwordBox2.Name = "passwordBox2";
            passwordBox2.PasswordChar = '*';
            passwordBox2.Size = new Size(278, 27);
            passwordBox2.TabIndex = 13;
            // 
            // registerButton
            // 
            registerButton.Location = new Point(11, 166);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(101, 29);
            registerButton.TabIndex = 14;
            registerButton.Text = "Register";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(272, 166);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(101, 29);
            cancelButton.TabIndex = 15;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // RegisterMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 207);
            Controls.Add(cancelButton);
            Controls.Add(registerButton);
            Controls.Add(passwordBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(passwordBox);
            Controls.Add(emailBox);
            Controls.Add(loginBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegisterMenu";
            Text = "Register...";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox passwordBox;
        private Label label3;
        private Label label4;
        private TextBox passwordBox2;
        private TextBox loginBox;
        private Label label2;
        private Label label1;
        private TextBox emailBox;
        private Button registerButton;
        private Button cancelButton;
    }
}