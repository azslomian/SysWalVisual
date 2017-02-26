using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.InteropServices;
using System.Net;

    namespace SysWal
    {
   
        class ReadXMLData
        {
            List<Currency> CurrencyList = new List<Currency>();
            List<Currency90Days> Currency90DaysList = new List<Currency90Days>();
            List<Currency90DaysWithCurrencyList> Currency90DaysWithCurrencyList = new List<Currency90DaysWithCurrencyList>();

        public bool CheckForInternetConnection()
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        using (var stream = client.OpenRead("http://www.google.com"))
                        {
                            return true;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }

        public bool CreateList()
        {
                ReadAndUpdateData r = new ReadAndUpdateData();
                CurrencyList.Clear();
                if (CheckForInternetConnection())
                {
                    XmlReader xmlReader = XmlReader.Create("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
                    int i = 2;
                    bool check = false;
                    AddToCurrencyList("EUR", "1.00", 1); //CurrencyList.Last().GetCurrencyId() + 1);
                    while (xmlReader.Read())
                    {
                        if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
                        {
                            if (xmlReader.HasAttributes)
                            {
                                if (check == false)
                                {
                                    check = true;
                                }
                                else
                                {
                                    Currency cur = new Currency();

                                    //  cur.SetCurrencyId(i);
                                    cur.SetCurrencyName(xmlReader.GetAttribute("currency"));
                                    cur.SetCurrencyRate(xmlReader.GetAttribute("rate"));

                                    CurrencyList.Add(cur);
                                    i++;
                                }
                            }

                        }
                    }
                    r.UpdateCurrencyList(CurrencyList);
                    return true;
                }
                else
                {
                    CurrencyList = r.ReadCurrencyList();
                    return false;
                }
         }

             

        public void AddToCurrencyList(string name, string rate, int id)
            {
                Currency cur = new Currency();

                // cur.SetCurrencyId(id);
                cur.SetCurrencyName(name);
                cur.SetCurrencyRate(rate);

                CurrencyList.Add(cur);
            }

            public List<Currency> GetCurrencyList()
            {
                return CurrencyList;
            }

            /// <summary>
            /// /////////////////////////////////////////////////////////
            /// ////////////////////////////////////
            /// 
            /// 
            /// 
            /// ///////////////////////////////////////////////////////////////////////////
            /// </summary>

            public bool Create90DaysWithCurrencyList()
            {
                if (CheckForInternetConnection())
                {
                    XmlReader xmlReader = XmlReader.Create("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml?8e98690e32f6914990146c9b53a350b9");
                    int i = 31;

                    Currency90DaysWithCurrencyList currencyOneDay = new Currency90DaysWithCurrencyList();
                    currencyOneDay.CreateCurrencyList();
                    while (xmlReader.Read())
                    {
                        if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
                        {
                            if (xmlReader.HasAttributes)
                            {
                                if (i == 31)
                                {
                                    i = 0;
                                    Currency90DaysWithCurrencyList.Add(currencyOneDay);
                                    currencyOneDay = new Currency90DaysWithCurrencyList();
                                    currencyOneDay.CreateCurrencyList();
                                    Currency cur = new Currency();
                                    cur.SetCurrencyName("EUR");
                                    cur.SetCurrencyRate("1.00");
                                    currencyOneDay.AddToCurrencyList(cur);
                                    currencyOneDay.SetCurrencyDate(xmlReader.GetAttribute("time"));
                                }
                                else
                                {
                                    Currency cur = new Currency();

                                    cur.SetCurrencyName(xmlReader.GetAttribute("currency"));
                                    cur.SetCurrencyRate(xmlReader.GetAttribute("rate"));

                                    currencyOneDay.AddToCurrencyList(cur);
                                    i++;
                                }

                            }
                        }
                    }
                    return true;
                }
                else
                {
                 return false;
                }
            }

            public List<Currency90DaysWithCurrencyList> GetCurrency90DaysWithCurrencyList()
            {
                return Currency90DaysWithCurrencyList;
            }
        }
    }

