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
    
    public partial class Statistic : Form
    {
        private List<Currency90DaysWithCurrencyList> Currency90DaysWithCurrencyList;
        private string currencyComboBoxValue;
        private string currencyFromListBoxValue;

        public Statistic(string currencyComboBox, string currencyFromListBox)
        {
            InitializeComponent();
            currencyComboBoxValue = currencyComboBox;
            currencyFromListBoxValue = currencyFromListBox;
            CreateStatistic();
            CreateChart();
        }


        public void CreateStatistic()
        {
            ReadXMLData xml = new ReadXMLData();
            xml.Create90DaysWithCurrencyList();
            Currency90DaysWithCurrencyList = xml.GetCurrency90DaysWithCurrencyList();
            Currency90DaysWithCurrencyList.RemoveAt(0);
        }

        public void CreateChart()
        {
            CurrencyChart.Series["Currency"].BorderWidth = 6;
            double min = 9999999999;
            double max = 0;
            CurrencyNameLabel.Text = "[" + currencyFromListBoxValue + "]" ;

            Currency90DaysWithCurrencyList.Reverse();
            foreach (Currency90DaysWithCurrencyList day in Currency90DaysWithCurrencyList)
            {
                double result;
                Currency cur1 = null;
                Currency cur2 = null;
                foreach(Currency curr in day.GetCurrencyList())
                {
                    if(currencyComboBoxValue == curr.GetCurrencyName())
                    {
                        cur1 = curr;
                    }
                    if(currencyFromListBoxValue == curr.GetCurrencyName())
                    {
                        cur2 = curr;
                    }
                }
                result = ReturnRate(cur1, cur2);
                if(result < min)
                {
                    min = result;
                }
                if(result > max)
                {
                    max = result;
                }
                CurrencyChart.Series["Currency"].Points.AddXY(day.GetCurrencyDate(), result);
            }
            MaxLabel.Text = max.ToString();
            MinLabel.Text = min.ToString();
            CurrencyChart.ChartAreas[0].AxisX.IsMarginVisible = false;
            CurrencyChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
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

    }
}
