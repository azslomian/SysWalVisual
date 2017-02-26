using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysWal
{
    public partial class PayStatistic : Form
    {
        private MoneyStatistic m = new MoneyStatistic();
        private List<Currency90DaysWithCurrencyList> currency90DaysWithCurrencyList;
        private double ratioMax;
        private double ratioMin;


        private void ShowPanel(string panel)
        {
            foreach (Control c in Controls)
            {
                if (c is Panel)
                {
                    if (c.Name != "panel1")
                    {
                        if (c.Name.Contains(panel))
                        {
                            c.Visible = true;
                            return;
                        }
                        else
                        {
                            c.Visible = false;
                        }
                    }
                }
            }
        }
        public PayStatistic()
        {
            InitializeComponent();
            CreatePanels();
            ShowPanel("panel1");
        }

        public void CreatePanels()
        {
            string cur1Min= null;
            string cur2Min = null;
            string cur1Max = null;
            string cur2Max = null;
            m.CreateList();
            currency90DaysWithCurrencyList = m.Get90DaysCurrencyList();
            m.FindBestAndWorst(ref cur1Min, ref cur2Min, ref cur1Max, ref cur2Max, ref ratioMax, ref ratioMin);

            CreateChartGrowth(cur1Max, cur2Max);
            CreateChartDrop(cur1Min, cur2Min);

        }

        public void CreateChartGrowth(string cName1, string cName2)
        {
            GrowthChart.Series["Currency"].BorderWidth = 4;
            Currency2Label.Text = "[" + cName2 + "]";
            Currency1Label.Text = "[" + cName1 + "]";

            ratioMax = Math.Round(ratioMax, 5);
            GrowthLabel.Text = ratioMax.ToString();
            if (ratioMax > 1) { GrowthLabel.ForeColor = Color.Green; }
            else if (ratioMax < 1) { GrowthLabel.ForeColor = Color.Red; }
            
            double min = 0;
            double max = 0;

            currency90DaysWithCurrencyList.Reverse();
            foreach (Currency90DaysWithCurrencyList day in currency90DaysWithCurrencyList)
            {
                double result;
                Currency cur1 = null;
                Currency cur2 = null;
                foreach (Currency curr in day.GetCurrencyList())
                {
                    if (cName1 == curr.GetCurrencyName())
                    {
                        cur1 = curr;
                    }
                    if (cName2 == curr.GetCurrencyName())
                    {
                        cur2 = curr;
                    }
                }
                result = ReturnRate(cur1, cur2);
                if (result < min)
                {
                    min = result;
                }
                else if (result > max)
                {
                    max = result;
                }
                GrowthChart.Series["Currency"].Points.AddXY(day.GetCurrencyDate(), result);
            }
            GrowthChart.ChartAreas[0].AxisX.IsMarginVisible = false;
            GrowthChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            // CurrencyChart.ChartAreas[0].AxisY.Maximum = max;
            //CurrencyChart.ChartAreas[0].AxisY.Minimum = min;
        }

        public void CreateChartDrop(string cName1, string cName2)
        {
            DropChart.Series["Currency"].BorderWidth = 4;
            CurrencyDrop2.Text = "[" + cName2 + "]";
            CurrencyDrop1.Text = "[" + cName1 + "]";

            ratioMin = Math.Round(ratioMin, 5);
            DropLabel.Text = ratioMin.ToString();
            if (ratioMin > 1) { DropLabel.ForeColor = Color.Green; }
            else if (ratioMin < 1) { DropLabel.ForeColor = Color.Red; }

            double min = 0;
            double max = 0;

           // currency90DaysWithCurrencyList.Reverse();
            foreach (Currency90DaysWithCurrencyList day in currency90DaysWithCurrencyList)
            {
                double result;
                Currency cur1 = null;
                Currency cur2 = null;
                foreach (Currency curr in day.GetCurrencyList())
                {
                    if (cName1 == curr.GetCurrencyName())
                    {
                        cur1 = curr;
                    }
                    if (cName2 == curr.GetCurrencyName())
                    {
                        cur2 = curr;
                    }
                }
                result = ReturnRate(cur1, cur2);
                if (result < min)
                {
                    min = result;
                }
                else if (result > max)
                {
                    max = result;
                }
                DropChart.Series["Currency"].Points.AddXY(day.GetCurrencyDate(), result);
            }
            DropChart.ChartAreas[0].AxisX.IsMarginVisible = false;
            DropChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            // CurrencyChart.ChartAreas[0].AxisY.Maximum = max;
            //CurrencyChart.ChartAreas[0].AxisY.Minimum = min;
        }


        private double ReturnRate(Currency cur1, Currency cur2)
        /*Calculates the exchange rate for all currencies*/
        {
            double exchangeRateDivide = double.Parse(cur1.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
            double exchangeRate = Convert.ToDouble(cur2.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
            exchangeRate = exchangeRate / exchangeRateDivide;
            exchangeRate = Math.Round(exchangeRate, 10);
            return exchangeRate;
        }
        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
            Start start = new Start();
            start.ShowDialog();
            Close();
            start = null;
        }

        private void dropButton_Click(object sender, EventArgs e)
        {
            ShowPanel("panel3");
        }

        private void growth2_Click(object sender, EventArgs e)
        {
            ShowPanel("panel1");
        }

        private void backDrop_Click(object sender, EventArgs e)
        {
            Hide();
            Start start = new Start();
            start.ShowDialog();
            Close();
            start = null;
        }

        
    }
}
