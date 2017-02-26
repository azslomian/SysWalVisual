using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysWal
{
    class Currency
    {
      //  int CurrencyId;
        string CurrencyName;
        string CurrencyRate;     // to euro

     /*   public void SetCurrencyId(int id)
        {
            CurrencyId = id;
        }*/
        public void SetCurrencyName(string name)
        {
            CurrencyName = name;
        }
        public void SetCurrencyRate(string rate)
        {
            CurrencyRate = rate;
        }

        /*public int GetCurrencyId()
        {
            return CurrencyId;
        }
        */
        public string GetCurrencyName()
        {
            return CurrencyName;
        }

        public string GetCurrencyRate()
        {
            return CurrencyRate;
        }

    }



    class Currency90Days
    {
        //  int CurrencyId;
        string CurrencyName;
        string CurrencyRate;     // to euro
        string Date;

        
        public void SetCurrencyName(string name)
        {
            CurrencyName = name;
        }
        public void SetCurrencyRate(string rate)
        {
            CurrencyRate = rate;
        }
        public void SetCurrencyDate(string d)
        {
            Date = d;
        }


        public string GetCurrencyName()
        {
            return CurrencyName;
        }

        public string GetCurrencyRate()
        {
            return CurrencyRate;
        }

        public string GetCurrencyDate()
        {
            return Date;
        }

    }


    class Currency90DaysWithCurrencyList
    {
        //  int CurrencyId;
        List<Currency> CurrencyList;
        string Date;

        public void SetCurrencyDate(string d)
        {
            Date = d;
        }

        public string GetCurrencyDate()
        {
            return Date;
        }

        public List<Currency> GetCurrencyList()
        {
            return CurrencyList;
        }

        public void AddToCurrencyList(Currency cur)
        {
            CurrencyList.Add(cur);
        }

        public void CreateCurrencyList()
        {
            CurrencyList = new List<Currency>();
        }
    }
}
