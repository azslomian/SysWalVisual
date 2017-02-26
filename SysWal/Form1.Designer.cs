namespace SysWal
{
    partial class Start
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Exit = new System.Windows.Forms.Button();
            this.SignUp = new System.Windows.Forms.Button();
            this.LoginContainer = new System.Windows.Forms.ToolStripContainer();
            this.SignIn = new System.Windows.Forms.Button();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.loginText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MoneyStatistic = new System.Windows.Forms.PictureBox();
            this.ExampleLabel = new System.Windows.Forms.Label();
            this.CurrencyListBox = new System.Windows.Forms.ListBox();
            this.CurrencyComboBox = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.LoginContainer.ContentPanel.SuspendLayout();
            this.LoginContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MoneyStatistic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.splitContainer1.Panel1.Controls.Add(this.Exit);
            this.splitContainer1.Panel1.Controls.Add(this.SignUp);
            this.splitContainer1.Panel1.Controls.Add(this.LoginContainer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MoneyStatistic);
            this.splitContainer1.Panel2.Controls.Add(this.ExampleLabel);
            this.splitContainer1.Panel2.Controls.Add(this.CurrencyListBox);
            this.splitContainer1.Panel2.Controls.Add(this.CurrencyComboBox);
            this.splitContainer1.Size = new System.Drawing.Size(604, 321);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 0;
            // 
            // Exit
            // 
            this.Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit.Location = new System.Drawing.Point(14, 274);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(140, 23);
            this.Exit.TabIndex = 6;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // SignUp
            // 
            this.SignUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SignUp.Location = new System.Drawing.Point(15, 245);
            this.SignUp.Name = "SignUp";
            this.SignUp.Size = new System.Drawing.Size(140, 23);
            this.SignUp.TabIndex = 5;
            this.SignUp.Text = "Sign Up";
            this.SignUp.UseVisualStyleBackColor = true;
            this.SignUp.Click += new System.EventHandler(this.SignUp_Click);
            // 
            // LoginContainer
            // 
            // 
            // LoginContainer.ContentPanel
            // 
            this.LoginContainer.ContentPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.LoginContainer.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginContainer.ContentPanel.Controls.Add(this.SignIn);
            this.LoginContainer.ContentPanel.Controls.Add(this.passwordText);
            this.LoginContainer.ContentPanel.Controls.Add(this.loginText);
            this.LoginContainer.ContentPanel.Controls.Add(this.label3);
            this.LoginContainer.ContentPanel.Controls.Add(this.label2);
            this.LoginContainer.ContentPanel.Size = new System.Drawing.Size(153, 103);
            this.LoginContainer.Location = new System.Drawing.Point(3, 3);
            this.LoginContainer.Name = "LoginContainer";
            this.LoginContainer.Padding = new System.Windows.Forms.Padding(5);
            this.LoginContainer.Size = new System.Drawing.Size(163, 113);
            this.LoginContainer.TabIndex = 0;
            this.LoginContainer.Text = "toolStripContainer1";
            // 
            // SignIn
            // 
            this.SignIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SignIn.Location = new System.Drawing.Point(36, 65);
            this.SignIn.Name = "SignIn";
            this.SignIn.Size = new System.Drawing.Size(85, 23);
            this.SignIn.TabIndex = 4;
            this.SignIn.Text = "Sign In";
            this.SignIn.UseVisualStyleBackColor = true;
            this.SignIn.Click += new System.EventHandler(this.SignIn_Click);
            // 
            // passwordText
            // 
            this.passwordText.Location = new System.Drawing.Point(61, 39);
            this.passwordText.Name = "passwordText";
            this.passwordText.Size = new System.Drawing.Size(86, 20);
            this.passwordText.TabIndex = 3;
            // 
            // loginText
            // 
            this.loginText.Location = new System.Drawing.Point(61, 13);
            this.loginText.Name = "loginText";
            this.loginText.Size = new System.Drawing.Size(86, 20);
            this.loginText.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(4, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "login";
            // 
            // MoneyStatistic
            // 
            this.MoneyStatistic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MoneyStatistic.Image = global::SysWal.Properties.Resources.StatystykiMoney;
            this.MoneyStatistic.Location = new System.Drawing.Point(367, 12);
            this.MoneyStatistic.Name = "MoneyStatistic";
            this.MoneyStatistic.Size = new System.Drawing.Size(42, 41);
            this.MoneyStatistic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MoneyStatistic.TabIndex = 19;
            this.MoneyStatistic.TabStop = false;
            this.MoneyStatistic.Click += new System.EventHandler(this.MoneyStatistic_Click);
            // 
            // ExampleLabel
            // 
            this.ExampleLabel.AutoSize = true;
            this.ExampleLabel.Location = new System.Drawing.Point(10, 43);
            this.ExampleLabel.Name = "ExampleLabel";
            this.ExampleLabel.Size = new System.Drawing.Size(89, 13);
            this.ExampleLabel.TabIndex = 3;
            this.ExampleLabel.Text = "CurrencyExample";
            // 
            // CurrencyListBox
            // 
            this.CurrencyListBox.Location = new System.Drawing.Point(13, 59);
            this.CurrencyListBox.Name = "CurrencyListBox";
            this.CurrencyListBox.Size = new System.Drawing.Size(396, 238);
            this.CurrencyListBox.TabIndex = 1;
            this.CurrencyListBox.SelectedIndexChanged += new System.EventHandler(this.CurrencyListBox_SelectedIndexChanged);
            this.CurrencyListBox.DoubleClick += new System.EventHandler(this.CurrencyListBox_DoubleClick);
            // 
            // CurrencyComboBox
            // 
            this.CurrencyComboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CurrencyComboBox.FormattingEnabled = true;
            this.CurrencyComboBox.Location = new System.Drawing.Point(13, 12);
            this.CurrencyComboBox.Name = "CurrencyComboBox";
            this.CurrencyComboBox.Size = new System.Drawing.Size(121, 21);
            this.CurrencyComboBox.TabIndex = 0;
            this.CurrencyComboBox.SelectedIndexChanged += new System.EventHandler(this.CurrencyComboBox_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(-1, 62);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Size = new System.Drawing.Size(150, 100);
            this.splitContainer2.TabIndex = 0;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 321);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Start";
            this.Text = "System Walutowy";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.LoginContainer.ContentPanel.ResumeLayout(false);
            this.LoginContainer.ContentPanel.PerformLayout();
            this.LoginContainer.ResumeLayout(false);
            this.LoginContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MoneyStatistic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripContainer LoginContainer;
        private System.Windows.Forms.Button SignIn;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.TextBox loginText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button SignUp;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ComboBox CurrencyComboBox;
        private System.Windows.Forms.ListBox CurrencyListBox;
        private System.Windows.Forms.Label ExampleLabel;
        private System.Windows.Forms.PictureBox MoneyStatistic;
    }
}

