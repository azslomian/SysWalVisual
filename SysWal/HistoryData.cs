using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysWal
{
    class HistoryData
    {

        private int OperationId;// { get; set; }
        private DateTime OperationDate; //{ get; set; }
        private int UserId; //{ get; set; }
        private int UserIdCooperate; //{ get; set; }
        private int type; //{ get; set; }
        /*
          1 - Exchange
          2 - together exchange
          3 - take credit
          4 - Pay in Installments
          5 - Pay All
          6 - Outside Money Transfer
          7 - Inside Money Transfer
          8 - Money Income
          9 - Inside Money Transfer CooporateFriend
        */
        private double cash; //{ get; set; }
        private double result; //{ get; set; }
        private string CurrencyFrom; //{ get; set; }
        private string CurrencyTo; //{ get; set; }
        private string description; //{ get; set; }
        private string AccountNo; //{ get; set; }

        public int GetOperationId()
        {
            return OperationId;
        }

        public int GetUserId()
        {
            return UserId;
        }

        public int GetUserCoopId()
        {
            return UserIdCooperate;
        }

        public DateTime GetOperationDate()
        {
            return OperationDate;
        }

        public int GetTypeName()
        {
            return type;
        }

        public double GetCash()
        {
            return cash;
        }

        public double GetResult()
        {
            return result;
        }

        public string GetCurrencyFrom()
        {
            return CurrencyFrom;
        }

        public string GetCurrencyTo()
        {
            return CurrencyTo;
        }

        public string GetDescription()
        {
            return description;
        }
        public string GetAccountNo()
        {
            return AccountNo;
        }






        public void SetOperationId(int o)
        {
            OperationId = o;
        }
        public void SetUserId(int i)
        {
            UserId = i;
        }
        public void SetOperationDate(DateTime d)
        {
            OperationDate = d;
        }
        public void SetUserIdCooperate(int i)
        {
            UserIdCooperate = i;
        }

        public void SetTypeName(int t)
        {
            type = t;
        }

        

        public void SetCash(double c)
        {
            cash = c;
        }

        public void SetResult(double r)
        {
            result = r;
        }

        public void SetCurrencyFrom(string f)
        {
            CurrencyFrom = f;
        }

        public void SetCurrencyTo(string t)
        {
            CurrencyTo = t;
        }

        public void SetDescription(string d)
        {
            description = d;
        }

        public void SetAccountNo(string a)
        {
            AccountNo = a;
        }
    }
}
