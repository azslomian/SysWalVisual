namespace SysWal
{
    partial class Menu
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
            this.label1 = new System.Windows.Forms.Label();
            this.Wallet = new System.Windows.Forms.Button();
            this.History = new System.Windows.Forms.Button();
            this.Credit = new System.Windows.Forms.Button();
            this.ExchangeTogether = new System.Windows.Forms.Button();
            this.LogOut = new System.Windows.Forms.Button();
            this.Exchange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.Wallet);
            this.splitContainer1.Panel1.Controls.Add(this.History);
            this.splitContainer1.Panel1.Controls.Add(this.Credit);
            this.splitContainer1.Panel1.Controls.Add(this.ExchangeTogether);
            this.splitContainer1.Panel1.Controls.Add(this.LogOut);
            this.splitContainer1.Panel1.Controls.Add(this.Exchange);
            this.splitContainer1.Size = new System.Drawing.Size(664, 321);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(46, 9);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(76, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "Menu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Wallet
            // 
            this.Wallet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Wallet.Location = new System.Drawing.Point(14, 164);
            this.Wallet.Name = "Wallet";
            this.Wallet.Size = new System.Drawing.Size(140, 23);
            this.Wallet.TabIndex = 10;
            this.Wallet.Text = "Wallet";
            this.Wallet.UseVisualStyleBackColor = true;
            // 
            // History
            // 
            this.History.Cursor = System.Windows.Forms.Cursors.Hand;
            this.History.Location = new System.Drawing.Point(14, 135);
            this.History.Name = "History";
            this.History.Size = new System.Drawing.Size(140, 23);
            this.History.TabIndex = 9;
            this.History.Text = "History";
            this.History.UseVisualStyleBackColor = true;
            // 
            // Credit
            // 
            this.Credit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Credit.Location = new System.Drawing.Point(14, 106);
            this.Credit.Name = "Credit";
            this.Credit.Size = new System.Drawing.Size(140, 23);
            this.Credit.TabIndex = 8;
            this.Credit.Text = "Credit";
            this.Credit.UseVisualStyleBackColor = true;
            // 
            // ExchangeTogether
            // 
            this.ExchangeTogether.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExchangeTogether.Location = new System.Drawing.Point(14, 77);
            this.ExchangeTogether.Name = "ExchangeTogether";
            this.ExchangeTogether.Size = new System.Drawing.Size(140, 23);
            this.ExchangeTogether.TabIndex = 7;
            this.ExchangeTogether.Text = "Exchange Together";
            this.ExchangeTogether.UseVisualStyleBackColor = true;
            // 
            // LogOut
            // 
            this.LogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogOut.Location = new System.Drawing.Point(14, 208);
            this.LogOut.Name = "LogOut";
            this.LogOut.Size = new System.Drawing.Size(140, 23);
            this.LogOut.TabIndex = 6;
            this.LogOut.Text = "Log Out";
            this.LogOut.UseVisualStyleBackColor = true;
            this.LogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // Exchange
            // 
            this.Exchange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exchange.Location = new System.Drawing.Point(14, 48);
            this.Exchange.Name = "Exchange";
            this.Exchange.Size = new System.Drawing.Size(140, 23);
            this.Exchange.TabIndex = 5;
            this.Exchange.Text = "Exchange";
            this.Exchange.UseVisualStyleBackColor = true;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 321);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.Text = "Menu";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button LogOut;
        private System.Windows.Forms.Button Exchange;
        private System.Windows.Forms.Button Wallet;
        private System.Windows.Forms.Button History;
        private System.Windows.Forms.Button Credit;
        private System.Windows.Forms.Button ExchangeTogether;
        private System.Windows.Forms.Label label1;
    }
}