using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysWal
{

    class MoneyStatistic
    {
        class StatisticData
        {
            private string currency1;
            private string currency2;
            private double ratio;

        }

        private List<Currency90DaysWithCurrencyList> currency90DaysWithCurrencyList;
       // private string best;
       // private string worst;

        public void CreateList()
        {
            ReadXMLData xml = new ReadXMLData();
            xml.Create90DaysWithCurrencyList();
            currency90DaysWithCurrencyList = xml.GetCurrency90DaysWithCurrencyList();
            currency90DaysWithCurrencyList.RemoveAt(0);
        }

        public List<Currency90DaysWithCurrencyList> Get90DaysCurrencyList()
        {
            return currency90DaysWithCurrencyList;
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

/*

        public void FindBestAndWorst(ref string cur1Min, ref string cur2Min, ref string cur1Max, ref string cur2Max, ref double ratio)
        {
            Currency90DaysWithCurrencyList firstDay = currency90DaysWithCurrencyList.First();
            Currency90DaysWithCurrencyList lastDay = currency90DaysWithCurrencyList.Last();

            double max = 0;
            double min = 0;
            double result;

            foreach (Currency curFirst in firstDay.GetCurrencyList())
            {
                foreach (Currency curLast in lastDay.GetCurrencyList())
                {
                    if (curFirst.GetCurrencyName() != curLast.GetCurrencyName())
                    {
                        result = ReturnRate(curFirst, curLast);
                        result = result / Convert.ToDouble(curFirst.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
                        if (result > max)
                        {
                            max = result;
                            cur1Max = curFirst.GetCurrencyName();
                            cur2Max = curLast.GetCurrencyName();
                            ratio = result;
                        }
                        if (result < min)
                        {
                            min = result;
                            cur1Min = curFirst.GetCurrencyName();
                            cur2Min = curLast.GetCurrencyName();
                            ratio = result;
                        }
                    }
                }
            }
        }

*/
        
        public Currency ReturnElement(string CurrencyName)
        {
            Currency currencyResult = null;
            Currency90DaysWithCurrencyList lastDay = currency90DaysWithCurrencyList.Last();
            foreach(Currency cur in lastDay.GetCurrencyList())
            {
                if(cur.GetCurrencyName() == CurrencyName)
                {
                    currencyResult = cur;
                }
            }
            return currencyResult;
        }
        public void FindBestAndWorst(ref string cur1Min, ref string cur2Min, ref string cur1Max, ref string cur2Max, ref double ratioMax, ref double ratioMin)
        {
            Currency90DaysWithCurrencyList firstDay = currency90DaysWithCurrencyList.First();
            Currency90DaysWithCurrencyList lastDay = currency90DaysWithCurrencyList.Last();

            double max = 0;
            double min = 100;
            double result;
            double FirstDayRatio;
            double LastDayRatio;

            foreach (Currency curFirst in firstDay.GetCurrencyList())
            {
                foreach (Currency curFirst2 in firstDay.GetCurrencyList())
                {
                    if (curFirst.GetCurrencyName() != curFirst2.GetCurrencyName())
                    {
                        FirstDayRatio = ReturnRate(curFirst, curFirst2);
                        LastDayRatio = ReturnRate(ReturnElement(curFirst.GetCurrencyName()), ReturnElement(curFirst2.GetCurrencyName()));
                        // result = result / Convert.ToDouble(curFirst.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
                        result = FirstDayRatio / LastDayRatio;
                        if (result > max)
                        {
                            max = result;
                            cur1Max = curFirst.GetCurrencyName();
                            cur2Max = curFirst2.GetCurrencyName();
                            ratioMax = result;
                        }
                        if (result < min)
                        {
                            min = result;
                            cur1Min = curFirst.GetCurrencyName();
                            cur2Min = curFirst2.GetCurrencyName();
                            ratioMin = result;
                        }
                    }
                }
            }
        }

        /*
        foreach (Currency curFirst in firstDay.GetCurrencyList())
        {
            foreach (Currency curLast in lastDay.GetCurrencyList())
            {
                result = Convert.ToDouble(curLast.GetCurrencyRate()) - Convert.ToDouble(curFirst.GetCurrencyRate());
                if (result < min)
                {
                    min = result;
                }
                else if (result > max)
                {
                    max = result;
                }
            }
        }
        */
    }    
            
    
}
