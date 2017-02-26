using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SysWal
{
    public partial class Start : Form
    {
        private List<Currency> CurrencyList;
       // private List<Currency90Days> Currency90DaysList;
        //private List<Currency90DaysWithCurrencyList> Currency90DaysWithCurrencyList;
        private double exchangeRate;
        private double exchangeRateDivide;
        private Currency result;
        private ReadXMLData xml = new ReadXMLData();




        public Start()
        {
            InitializeComponent();
            CreatePanel1();
        } 
        
        private void CreatePanel1()
            /*Creates Form*/
            
       {

            var myItems1 = new BindingList<string> { };
            var ComboItems = new BindingList<string> { };


            xml.CreateList();
            CurrencyList = xml.GetCurrencyList();
            //  xml.Create90DaysList();
            // Currency90DaysList = xml.GetCurrency90DaysList();

            // passwordText.PasswordChar = '*';
            //var list = (from cur in CurrencyList where cur.GetCurrencyName() == "EUR" select cur).ToList();
           // var abc = CurrencyList.Where(x => x.GetCurrencyName() == "EUR").ToList().First();
            foreach (Currency prime in CurrencyList)
            {
                myItems1.Add(prime.GetCurrencyName() + "\t\t" + DecimalToString(prime.GetCurrencyRate())); 
                CurrencyComboBox.Items.Add(prime.GetCurrencyName());
            }
            
           myItems1.RemoveAt(0);
           CurrencyComboBox.SelectedIndex = 0;
           CurrencyListBox.DataSource = myItems1;
           MakeLabel(CurrencyComboBox.Text);
        }

        public string DecimalToString(string strdec)
        {
            return strdec.Contains(",") ? strdec.TrimEnd('0').TrimEnd(',') : strdec;
        }

        /*
                private void CreatePanel1()
                /*Creates Form*/
        /*     {
                 ReadXMLData xml = new ReadXMLData();
                 var myItems1 = new BindingList<string> { };
                 var ComboItems = new BindingList<string> { };

                 xml.CreateList();
                 CurrencyList = xml.GetCurrencyList();
                 xml.Create90DaysList();
                 Currency90DaysList = xml.GetCurrency90DaysList();

                 passwordText.PasswordChar = '*';

                 int i = 31; 

                 foreach (Currency90Days prime in Currency90DaysList)
                 {
                     if (i == 31)
                     {
                         myItems1.Add(prime.GetCurrencyName() + "\t\t" + prime.GetCurrencyRate() + "\t\t" + prime.GetCurrencyDate());
                         i = 0;
                     }
                     else
                     {
                         myItems1.Add(prime.GetCurrencyName() + "\t\t" + prime.GetCurrencyRate());
                         i++;
                     }
                    // CurrencyComboBox.Items.Add(prime.GetCurrencyName());
                 }


                 //myItems1.RemoveAt(0);
                 //CurrencyComboBox.SelectedIndex = 0;
                 CurrencyListBox.DataSource = myItems1;
                 //MakeLabel(CurrencyComboBox.Text);
             }*/


        /*
    private void CreatePanel1()
    /*Creates Form*/
        /*      {
                  ReadXMLData xml = new ReadXMLData();
                  var myItems1 = new BindingList<string> { };
                  var ComboItems = new BindingList<string> { };

                  xml.CreateList();
                  CurrencyList = xml.GetCurrencyList();
                  //xml.Create90DaysList();
                  //Currency90DaysList = xml.GetCurrency90DaysList();

                  xml.Create90DaysWithCurrencyList();
                  Currency90DaysWithCurrencyList = xml.GetCurrency90DaysWithCurrencyList();
                  Currency90DaysWithCurrencyList.RemoveAt(0);


                  passwordText.PasswordChar = '*';

                  int i = 31;

                  foreach (Currency90DaysWithCurrencyList prime in Currency90DaysWithCurrencyList)
                  {
                      myItems1.Add(prime.GetCurrencyDate());
                      foreach (Currency cur in prime.GetCurrencyList())
                      {
                              myItems1.Add(cur.GetCurrencyName() + "\t\t" + cur.GetCurrencyRate());
                              i++;
                      }
                      // CurrencyComboBox.Items.Add(prime.GetCurrencyName());
                  }


                  //myItems1.RemoveAt(0);
                  //CurrencyComboBox.SelectedIndex = 0;
                  CurrencyListBox.DataSource = myItems1;
                  //MakeLabel(CurrencyComboBox.Text);
              }

              */
        private void CurrencyListBox_DoubleClick(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                string text = CurrencyListBox.GetItemText(CurrencyListBox.SelectedItem);
                if (text.Length > 3)
                {
                    text = text.Substring(0, 3);
                }
                Currency cur2 = FindCurrency(text);
                //Hide();
                Statistic stat = new Statistic((string)CurrencyComboBox.SelectedItem, cur2.GetCurrencyName());
                stat.ShowDialog();
                //Show();
                stat = null;
            }
            else
            {
                MessageBox.Show("Brak połączenia Internetowego!");
            }
        }


        private void MakeLabel(string name)
            /*Creates Currency Example Label above the listbox*/ 
        {
            Currency cur1 = new Currency();
            Currency cur2 = new Currency();
            string text = CurrencyListBox.GetItemText(CurrencyListBox.SelectedItem);
            if (text.Length > 3)
            {
                text = text.Substring(0, 3);
            }

            cur1 = FindCurrency(name);
            cur2 = FindCurrency(text);
            if (cur2 ==null)
            {
                cur2 = FindCurrency("USD");
            }
            ExampleLabel.Text = "e.g.  1 " + name  + "  =  " + ReturnRate(cur1, cur2) + " " + cur2.GetCurrencyName();
        }

        private Currency FindCurrency(string name)
            /*Seek Currency object in a list with a name*/
        {
            Currency cur;
            foreach (Currency prime in CurrencyList)
            {
                if(prime.GetCurrencyName() == name )
                {
                    cur = prime;
                    return cur;
                }
            }
            return null;
        }

        private double ReturnRate(Currency cur1, Currency cur2)
            /*Calculates the exchange rate for all currencies*/
        {
            exchangeRateDivide = double.Parse(cur1.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
            exchangeRate = Convert.ToDouble(cur2.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
            exchangeRate = exchangeRate / exchangeRateDivide ;
            exchangeRate = Math.Round(exchangeRate, 4);
            return exchangeRate;
        }


        private void SignIn_Click(object sender, EventArgs e)
            /* Goes to Menu2 form */
        {
            /* HashPassword hsp = new HashPassword();
             string salt = hsp.CreateSalt(10);
             loginText.Text = hsp.ComparePassword(loginText.Text, salt);
             passwordText.Text = hsp.ComparePassword(loginText.Text, salt);*/
            if (xml.CheckForInternetConnection())
            {
                string login;
                string password;
                int id = 0;
                ReadAndUpdateData r = new ReadAndUpdateData();


                login = loginText.Text;
                password = passwordText.Text;


                r.ReadLoginAndPassword();

                if (r.LogInProcess(ref id, login, password))
                {
                    Hide();
                    Menu2 menu = new Menu2(id);
                    menu.ShowDialog();
                    Close();
                    menu = null;
                }
                else
                {
                    MessageBox.Show("Niepoprawne dane!");
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia Internetowego!");
            }
        }

        private void Exit_Click(object sender, EventArgs e)
            /*Quit aplplication*/
        {
            Application.Exit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUp_Click(object sender, EventArgs e)
            /* Goes to SignUp form*/
        {
            if (xml.CheckForInternetConnection())
            {
                Hide();
                SignUp signUp = new SignUp();
                signUp.ShowDialog();
                Close();
                signUp = null;
            }
            else
            {
                MessageBox.Show("Brak połączenia Internetowego!");
            }
        }

        private void CurrencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
            /* Creates Listbox with currencyRates*/
        {
            Currency result = CurrencyList.Where(i => i.GetCurrencyName() == (string)CurrencyComboBox.SelectedItem).FirstOrDefault();
            var myItems1 = new BindingList<string> { };

            foreach (Currency prime in CurrencyList)
            {
                if (prime != result)
                {
                    exchangeRate = ReturnRate(result, prime);
                    myItems1.Add(prime.GetCurrencyName() + "\t\t" + exchangeRate.ToString());
                }
            }
            MakeLabel(result.GetCurrencyName());
            CurrencyListBox.DataSource = myItems1;
        }

        private void CurrencyListBox_SelectedIndexChanged(object sender, EventArgs e)
            /*Creates Label for every change of currencies types*/
        {
            result = CurrencyList.Where(i => i.GetCurrencyName() == (string)CurrencyComboBox.SelectedItem).FirstOrDefault();
            MakeLabel(result.GetCurrencyName());
        }

        private void MoneyStatistic_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Hide();
                PayStatistic stat = new PayStatistic();
                stat.ShowDialog();
                Close();
                stat = null;
            }
            else
            {
                MessageBox.Show("Brak połączenia Internetowego!");
            }
        }
        
    }
}
