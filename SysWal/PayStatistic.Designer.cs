namespace SysWal
{
    partial class PayStatistic
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Back = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Currency1Label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GrowthLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Currency2Label = new System.Windows.Forms.Label();
            this.GrowthChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.growthButton = new System.Windows.Forms.Button();
            this.dropButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.backDrop = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CurrencyDrop1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DropLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CurrencyDrop2 = new System.Windows.Forms.Label();
            this.DropChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.growth2 = new System.Windows.Forms.Button();
            this.drop2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrowthChart)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backDrop)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DropChart)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Back);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.growthButton);
            this.panel1.Controls.Add(this.dropButton);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 263);
            this.panel1.TabIndex = 0;
            // 
            // Back
            // 
            this.Back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Back.Image = global::SysWal.Properties.Resources.backRound;
            this.Back.Location = new System.Drawing.Point(384, 11);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(26, 26);
            this.Back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Back.TabIndex = 19;
            this.Back.TabStop = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.Currency1Label);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.GrowthLabel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Currency2Label);
            this.panel2.Controls.Add(this.GrowthChart);
            this.panel2.Location = new System.Drawing.Point(14, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(399, 206);
            this.panel2.TabIndex = 6;
            // 
            // Currency1Label
            // 
            this.Currency1Label.AutoSize = true;
            this.Currency1Label.Location = new System.Drawing.Point(288, 184);
            this.Currency1Label.Name = "Currency1Label";
            this.Currency1Label.Size = new System.Drawing.Size(35, 13);
            this.Currency1Label.TabIndex = 6;
            this.Currency1Label.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(329, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "->";
            // 
            // GrowthLabel
            // 
            this.GrowthLabel.AutoSize = true;
            this.GrowthLabel.Location = new System.Drawing.Point(178, 183);
            this.GrowthLabel.Name = "GrowthLabel";
            this.GrowthLabel.Size = new System.Drawing.Size(35, 13);
            this.GrowthLabel.TabIndex = 4;
            this.GrowthLabel.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "3-months growth:";
            // 
            // Currency2Label
            // 
            this.Currency2Label.AutoSize = true;
            this.Currency2Label.Location = new System.Drawing.Point(351, 184);
            this.Currency2Label.Name = "Currency2Label";
            this.Currency2Label.Size = new System.Drawing.Size(35, 13);
            this.Currency2Label.TabIndex = 2;
            this.Currency2Label.Text = "label1";
            // 
            // GrowthChart
            // 
            chartArea3.Name = "ChartArea1";
            this.GrowthChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.GrowthChart.Legends.Add(legend3);
            this.GrowthChart.Location = new System.Drawing.Point(3, 3);
            this.GrowthChart.Name = "GrowthChart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "Currency";
            this.GrowthChart.Series.Add(series3);
            this.GrowthChart.Size = new System.Drawing.Size(396, 164);
            this.GrowthChart.TabIndex = 1;
            this.GrowthChart.Text = "GrowthChart";
            // 
            // growthButton
            // 
            this.growthButton.BackColor = System.Drawing.Color.Gold;
            this.growthButton.Location = new System.Drawing.Point(17, 14);
            this.growthButton.Name = "growthButton";
            this.growthButton.Size = new System.Drawing.Size(75, 23);
            this.growthButton.TabIndex = 1;
            this.growthButton.Text = "growth";
            this.growthButton.UseVisualStyleBackColor = false;
            // 
            // dropButton
            // 
            this.dropButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dropButton.Location = new System.Drawing.Point(107, 14);
            this.dropButton.Name = "dropButton";
            this.dropButton.Size = new System.Drawing.Size(75, 23);
            this.dropButton.TabIndex = 2;
            this.dropButton.Text = "drop";
            this.dropButton.UseVisualStyleBackColor = true;
            this.dropButton.Click += new System.EventHandler(this.dropButton_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.backDrop);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.growth2);
            this.panel3.Controls.Add(this.drop2);
            this.panel3.Location = new System.Drawing.Point(0, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(428, 263);
            this.panel3.TabIndex = 20;
            // 
            // backDrop
            // 
            this.backDrop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backDrop.Image = global::SysWal.Properties.Resources.backRound;
            this.backDrop.Location = new System.Drawing.Point(384, 11);
            this.backDrop.Name = "backDrop";
            this.backDrop.Size = new System.Drawing.Size(26, 26);
            this.backDrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.backDrop.TabIndex = 19;
            this.backDrop.TabStop = false;
            this.backDrop.Click += new System.EventHandler(this.backDrop_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel4.Controls.Add(this.CurrencyDrop1);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.DropLabel);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.CurrencyDrop2);
            this.panel4.Controls.Add(this.DropChart);
            this.panel4.Location = new System.Drawing.Point(14, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(399, 206);
            this.panel4.TabIndex = 6;
            // 
            // CurrencyDrop1
            // 
            this.CurrencyDrop1.AutoSize = true;
            this.CurrencyDrop1.Location = new System.Drawing.Point(279, 183);
            this.CurrencyDrop1.Name = "CurrencyDrop1";
            this.CurrencyDrop1.Size = new System.Drawing.Size(35, 13);
            this.CurrencyDrop1.TabIndex = 6;
            this.CurrencyDrop1.Text = "label1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "->";
            // 
            // DropLabel
            // 
            this.DropLabel.AutoSize = true;
            this.DropLabel.Location = new System.Drawing.Point(178, 183);
            this.DropLabel.Name = "DropLabel";
            this.DropLabel.Size = new System.Drawing.Size(35, 13);
            this.DropLabel.TabIndex = 4;
            this.DropLabel.Text = "label2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "3-months drop:";
            // 
            // CurrencyDrop2
            // 
            this.CurrencyDrop2.AutoSize = true;
            this.CurrencyDrop2.Location = new System.Drawing.Point(342, 183);
            this.CurrencyDrop2.Name = "CurrencyDrop2";
            this.CurrencyDrop2.Size = new System.Drawing.Size(35, 13);
            this.CurrencyDrop2.TabIndex = 2;
            this.CurrencyDrop2.Text = "label1";
            // 
            // DropChart
            // 
            chartArea4.Name = "ChartArea1";
            this.DropChart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.DropChart.Legends.Add(legend4);
            this.DropChart.Location = new System.Drawing.Point(3, 3);
            this.DropChart.Name = "DropChart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.IsVisibleInLegend = false;
            series4.Legend = "Legend1";
            series4.Name = "Currency";
            this.DropChart.Series.Add(series4);
            this.DropChart.Size = new System.Drawing.Size(396, 164);
            this.DropChart.TabIndex = 1;
            this.DropChart.Text = "chart1";
            // 
            // growth2
            // 
            this.growth2.BackColor = System.Drawing.SystemColors.Control;
            this.growth2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.growth2.Location = new System.Drawing.Point(17, 14);
            this.growth2.Name = "growth2";
            this.growth2.Size = new System.Drawing.Size(75, 23);
            this.growth2.TabIndex = 1;
            this.growth2.Text = "growth";
            this.growth2.UseVisualStyleBackColor = false;
            this.growth2.Click += new System.EventHandler(this.growth2_Click);
            // 
            // drop2
            // 
            this.drop2.BackColor = System.Drawing.Color.Gold;
            this.drop2.Location = new System.Drawing.Point(107, 14);
            this.drop2.Name = "drop2";
            this.drop2.Size = new System.Drawing.Size(75, 23);
            this.drop2.TabIndex = 2;
            this.drop2.Text = "drop";
            this.drop2.UseVisualStyleBackColor = false;
            // 
            // PayStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 264);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PayStatistic";
            this.Text = "PayStatistic";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrowthChart)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.backDrop)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DropChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button growthButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart GrowthChart;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.Label Currency2Label;
        private System.Windows.Forms.Label GrowthLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Currency1Label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox backDrop;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label CurrencyDrop1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label DropLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label CurrencyDrop2;
        private System.Windows.Forms.DataVisualization.Charting.Chart DropChart;
        private System.Windows.Forms.Button growth2;
        private System.Windows.Forms.Button dropButton;
        private System.Windows.Forms.Button drop2;
    }
}