using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysWal
{
    class Wallet
    {
        private List<Currency> CurrencyList = new List<Currency>();
        private int Userid;
        private Currency Credit = new Currency();

        public void SetUserId(int id)
        {
            Userid = id;
        }
        public int GetUserId()
        {
            return Userid;
        }
    
        public void AddCurrency(string name, string rate)
        {
            Currency cur = new Currency();
            cur.SetCurrencyName(name);
            cur.SetCurrencyRate(rate);

            CurrencyList.Add(cur);
        }

        public List<Currency> GetCurrencyList()
        {
            return CurrencyList;
        }
        public void SetCurrencyList(List<Currency> list)
        {
            CurrencyList = list;
        }

        public Currency GetCredit()
        {
            return Credit;
        }

        public void SetCredit(string name, string creditAmount)
        {
            Credit.SetCurrencyName(name);
            Credit.SetCurrencyRate(creditAmount);
        }

    }
}
