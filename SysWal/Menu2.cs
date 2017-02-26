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
    public partial class Menu2 : Form
    {
        private List<Currency> CurrencyList;
        private Currency CurrencyFrom;
        private Currency CurrencyTo;
        private Currency CurrencyFrom2;
        private Currency CurrencyTo2;
        private Currency CurrencyCredit;
        private Currency CurrencyFromResult = new Currency();
        private Currency CurrencyToResult = new Currency();
        private int id;
        private UserData SignedInuser = new UserData();
        private List<UserIdLogin> UserList;
        private ReadAndUpdateData r = new ReadAndUpdateData();
        private List<UserIdLoginAmount> FriendsList = null;
        private List<UserAskData> AskList;
        private DateTime dateFrom;
        private DateTime dateTo;
        private DateTime today = DateTime.Now;
        private List<HistoryData> HistoryList;
        private ReadXMLData xml = new ReadXMLData();

        public bool CheckAccountNo(string accountNo)
        {
            if (accountNo.Length == 26)
            {
                int i = 0;
                while (i < accountNo.Length)
                {
                    if ((int)accountNo[i] < 48 || (int)accountNo[i] > 57)
                    {
                        return false;
                    }
                    i++;
                }
                return true;
            }
            return false;
        }
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

        private void ShowWalletPanel(string panel, string panelFather)
        {
            foreach (Control c in Controls)
            {
                if (c is Panel)
                {
                    if (c.Name != panelFather)
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




        public Menu2(int i)
        {
            InitializeComponent();
            id = i;
            SignedInuser = r.GetOneUser(i);
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(i);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        public bool isEmpty(TextBox textbox)
        {
            if (textbox.Text == null || textbox.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CheckNumericTextBox(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
             (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ////////            panel 1  // Exchange Money
        /// </summary>
        /////////////////////////////////////////////////////////////////////
        private void Panel1Create(int i)
        {
            id = i;
            UserData user = new UserData();
            //CurrencyList.Clear();
            CurrencyFromComboBox.Items.Clear();
            CurrencyToComboBox.Items.Clear();

            user = r.GetOneUser(id);
            nameLabel.Text = user.GetName() + " " + user.GetSurname();

            ReadXMLData xml = new ReadXMLData();
            var ComboItems = new BindingList<string> { };
            xml.CreateList();

            CurrencyList = xml.GetCurrencyList();

            foreach (Currency prime in CurrencyList)
            {
                CurrencyFromComboBox.Items.Add(prime.GetCurrencyName());
                CurrencyToComboBox.Items.Add(prime.GetCurrencyName());
            }
            CurrencyFromComboBox.SelectedIndex = 0;
            CurrencyToComboBox.SelectedIndex = 0;
            CurrencyFrom = CurrencyList.First();
            CurrencyTo = CurrencyList.First();
        }

        private void ExchangeTogether_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel2Create(id);
                ShowPanel("panel2");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }
        private void Credit_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel3Create(id);
                ShowPanel("panel3");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }
        private void History_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel4Create(id);
                ShowPanel("panel4");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void Wallet_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel7Create(id);
                ShowPanel("panel7");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            LogOutFunction();
        }

        private void LogOutFunction()
        {
            Hide();
            Start start = new Start();
            start.ShowDialog();
            Close();
            start.Dispose();
        }





        void TogetherAskExchange(UserAskData ask, List<UserAskData> PrepareAskUserList)
        {
            if (ask.GetControlNo() == ask.GetFriends())
            {
                bool check = true;
                Currency currency1 = new Currency();
                Currency currency2 = new Currency();
                Wallet wallet = new Wallet();
                PrepareAskUserList = r.ReadUserAskWithAskId(ask.GetAskId());
                foreach (UserAskData prepareAsk in PrepareAskUserList)
                {
                    wallet = r.ReadWalletData(prepareAsk.GetUserId());
                    currency1.SetCurrencyName(prepareAsk.GetCurrencyFrom());
                    currency1.SetCurrencyRate(prepareAsk.Getcash().ToString());
                    currency2.SetCurrencyName(prepareAsk.GetCurrencyTo());
                    currency2.SetCurrencyRate(Calculation(prepareAsk).ToString());
                    if (!r.CheckExchangeTakeIfPossible(wallet, currency1, currency2) && !r.CheckExchangeGiveIfPossible(wallet, currency1, currency2))
                    {
                        check = false;
                    }
                }
                if (check == false)
                {
                    MessageBox.Show("Któryś z użytkowników nie ma wystarczających środków na koncie \n"
                    + "lub nie ma w portfelu miejsca na nową walutę ");
                    DeleteAskListData(ask.GetAskId());
                }
                else
                {
                    bool check2 = true;
                    //////// Zapis każdego do portfela ///////////
                    foreach (UserAskData prepareAsk in PrepareAskUserList)
                    {
                        int type = 2;
                        wallet = r.ReadWalletData(prepareAsk.GetUserId());
                        currency1.SetCurrencyName(prepareAsk.GetCurrencyFrom());
                        currency1.SetCurrencyRate(prepareAsk.Getcash().ToString());
                        currency2.SetCurrencyName(prepareAsk.GetCurrencyTo());
                        currency2.SetCurrencyRate(Calculation(prepareAsk).ToString());
                        r.UpdateExchangeWallet(prepareAsk.GetUserId(), currency1, currency2, ref check, ref check2);
                        r.SaveHistory(
                            prepareAsk.GetUserId(),
                            0,
                            type,
                            Convert.ToDouble(currency1.GetCurrencyRate()),
                            Convert.ToDouble(currency2.GetCurrencyRate()),
                            prepareAsk.GetCurrencyFrom(),
                            prepareAsk.GetCurrencyTo(),
                            today,
                            null,
                            null
                            );

                    }

                    DeleteAskListData(ask.GetAskId());
                    MessageBox.Show("Wymiana wspólna zakończona powodzeniem!");
                }
            }

        }


        public void cardPicture_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                AskList = r.ReadUserAskDataWithUserId(id);
                foreach (UserAskData ask in AskList)
                {
                    if (ask.GetAccepted() == false)
                    {
                        DialogResult dialogResult = MessageBox.Show("Czy wyrażasz zgodę na wymianę: \n "
                            + ask.Getcash() + " " + ask.GetCurrencyFrom() + "  =  " + Calculation(ask) + " " + ask.GetCurrencyTo(),
                            "Pytanie o wymianę", MessageBoxButtons.YesNoCancel);


                        if (dialogResult == DialogResult.Yes)
                        {
                            List<UserAskData> PrepareAskUserList = new List<UserAskData>();
                            List<UserAskData> ResultAskUserList = new List<UserAskData>();
                            int temp;
                            ask.SetAccepted(true);
                            PrepareAskUserList = r.ReadUserAskWithAskId(ask.GetAskId());
                            foreach (UserAskData askTemp in PrepareAskUserList)
                            {
                                temp = askTemp.GetControlNo() + 1;
                                askTemp.SetControlNo(temp);
                                if (askTemp.GetUserId() == ask.GetUserId())
                                {
                                    askTemp.SetAccepted(true);
                                }
                                ResultAskUserList.Add(askTemp);
                            }
                            r.UpdateUserAskList(ResultAskUserList);

                            PrepareAskUserList.Clear();
                            TogetherAskExchange(ResultAskUserList.First(), PrepareAskUserList);
                            ResultAskUserList.Clear();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            DeleteAskListData(ask.GetAskId());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void DeleteAskListData(int AskId)
        {
            List<UserAskData> PrepareAskUserList = new List<UserAskData>();
            PrepareAskUserList = r.ReadUserAskWithAskId(AskId);
            r.DeleteUserAskList(PrepareAskUserList);
        }

        private double Calculation(UserAskData ask)
        {
            Currency AskCurrencyTo = new Currency();
            Currency AskCurrencyFrom = new Currency();
            AskCurrencyTo = CurrencyList.Where(i => i.GetCurrencyName() == ask.GetCurrencyTo()).FirstOrDefault();
            AskCurrencyFrom = CurrencyList.Where(i => i.GetCurrencyName() == ask.GetCurrencyFrom()).FirstOrDefault();
            double amount = Convert.ToDouble(ask.Getcash(), System.Globalization.CultureInfo.InvariantCulture);
            double exchangeRateDivide = Convert.ToDouble(AskCurrencyFrom.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
            double exchangeRate = Convert.ToDouble(AskCurrencyTo.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);

            exchangeRate = exchangeRate / exchangeRateDivide * amount;
            exchangeRate = Math.Round(exchangeRate, 2);

            return exchangeRate;
        }

        private void CurrencyFromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyFrom = CurrencyList.Where(i => i.GetCurrencyName() == (string)CurrencyFromComboBox.SelectedItem).FirstOrDefault();
        }

        private void CurrencyToComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyTo = CurrencyList.Where(i => i.GetCurrencyName() == (string)CurrencyToComboBox.SelectedItem).FirstOrDefault();
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            bool sth = Calculate();
        }

        private bool Calculate()
        {
            if (!isEmpty(AmountText))
            {
                double amount = Convert.ToDouble(AmountText.Text, System.Globalization.CultureInfo.InvariantCulture);
                double exchangeRateDivide = Convert.ToDouble(CurrencyFrom.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
                double exchangeRate = Convert.ToDouble(CurrencyTo.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);

                exchangeRate = exchangeRate / exchangeRateDivide * amount;
                exchangeRate = Math.Round(exchangeRate, 2);
                ExchangeAmountText.Text = exchangeRate.ToString();

                CurrencyFromResult.SetCurrencyName(CurrencyFrom.GetCurrencyName());
                CurrencyToResult.SetCurrencyName(CurrencyTo.GetCurrencyName());
                CurrencyToResult.SetCurrencyRate(exchangeRate.ToString());
                CurrencyFromResult.SetCurrencyRate(amount.ToString());
                return true;
            }
            else
            {
                MessageBox.Show("Wypełnij pole Amount!");
                return false;
            }
        }
        private void CompleteExchangeButton_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                bool check1 = false;
                bool check2 = true;
                if (Calculate())
                {
                    r.UpdateExchangeWallet(id, CurrencyFromResult, CurrencyToResult, ref check1, ref check2);
                    if (check2 == false)
                    {
                        MessageBox.Show("Nie masz miejsca w portfelu na nową walutę");
                    }
                    else if (check1 == true)
                    {
                        int type = 1;
                        r.SaveHistory(
                               SignedInuser.GetUserId(),
                               0,
                               type,
                               Convert.ToDouble(CurrencyFromResult.GetCurrencyRate()),
                               Convert.ToDouble(CurrencyToResult.GetCurrencyRate()),
                               CurrencyFromResult.GetCurrencyName(),
                               CurrencyToResult.GetCurrencyName(),
                               today,
                               null,
                               null
                               );
                        MessageBox.Show("Wymiana zakończona pomyslnie!");
                    }
                    else if (check1 == false)
                    {
                        MessageBox.Show("Nie masz wystarczającej ilości środków w portfelu!");
                    }
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        //////////////////////////////////////////////////////////////////////////////
        ////////////////////  panel2   // exchange money together
        ///////////////////////////////////////////////////////////////////////////////////

        private void Panel2Create(int i)
        {
            CurrencyList.Clear();
            CurrencyFromComboBox2.Items.Clear();
            CurrencyToComboBox2.Items.Clear();
            ReadXMLData xml = new ReadXMLData();
            var ComboItems = new BindingList<string> { };
            xml.CreateList();
            FriendsList = new List<UserIdLoginAmount>();
            FriendsList.Clear();

            CurrencyList = xml.GetCurrencyList();

            foreach (Currency prime in CurrencyList)
            {
                CurrencyFromComboBox2.Items.Add(prime.GetCurrencyName());
                CurrencyToComboBox2.Items.Add(prime.GetCurrencyName());
            }
            CurrencyFromComboBox2.SelectedIndex = 0;
            CurrencyToComboBox2.SelectedIndex = 0;
            CurrencyFrom2 = CurrencyList.First();
            CurrencyTo2 = CurrencyList.First();


            FriendsTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            FriendsTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            // ReadAndUpdateData r = new ReadAndUpdateData();

            UserList = r.ReadIdLogin();
            foreach (UserIdLogin user in UserList)
            {
                coll.Add(user.GetLogin());
            }
            coll.Remove(SignedInuser.GetLogin());
            FriendsTextBox.AutoCompleteCustomSource = coll;
        }

        private void Exchange2_Click_1(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(id);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void Credit2_Click_1(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel3Create(id);
                ShowPanel("panel3");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void History2_Click_1(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                ShowPanel("panel4");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void Wallet2_Click_1(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel7Create(id);
                ShowPanel("panel7");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void LogOut2_Click_1(object sender, EventArgs e)
        {
            Hide();
            Start start = new Start();
            start.ShowDialog();
            Close();
            //start = null;
            start.Dispose();
        }

        private void CurrencyFromComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyFrom2 = CurrencyList.Where(i => i.GetCurrencyName() == (string)CurrencyFromComboBox2.SelectedItem).FirstOrDefault();
            FriendsList.Clear();
            FriendsListBox.Items.Clear();
            ExchangeAmount2Text.Text = null;
            ResultFriendTextBox.Text = null;
        }

        private void CurrencyToComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyTo2 = CurrencyList.Where(i => i.GetCurrencyName() == (string)CurrencyToComboBox2.SelectedItem).FirstOrDefault();
            FriendsList.Clear();
            FriendsListBox.Items.Clear();
            ExchangeAmount2Text.Text = null;
            ResultFriendTextBox.Text = null;
        }

        private void AcceptFriend_Click(object sender, EventArgs e)
        {
            // ReadAndUpdateData r = new ReadAndUpdateData();
            if (FriendsTextBox.Text == SignedInuser.GetLogin())
            {
                MessageBox.Show("Błąd! To jest Twój login!");
            }
            else
            {
                if (!CheckIfInListBox(FriendsTextBox.Text))
                {
                    if (r.CheckIfExists(FriendsTextBox.Text))
                    {
                        if (CalculateWithFriends())
                        {
                            FriendsListBox.Items.Add(FriendsTextBox.Text + "\t \t" + ResultFriendTextBox.Text);
                            int id = r.GetOneUserId(FriendsTextBox.Text.ToString());
                            UserIdLoginAmount user = new UserIdLoginAmount();
                            user.SetLogin(FriendsTextBox.Text);
                            user.SetId(id);
                            user.SetAmount(Convert.ToDouble(AmountFriendTextBox.Text));
                            FriendsList.Add(user);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Podany login nie istnieje! Spróbuj ponownie");
                    }
                }
            }

        }

        public bool CheckIfInListBox(string login)
        {
            foreach (UserIdLoginAmount user in FriendsList)
            {
                if (user.GetLogin() == login)
                {
                    MessageBox.Show("Podany login występuje już w wymianie!");
                    return true;
                }
            }
            return false;
        }

        private bool Calculate2()
        {
            if (!isEmpty(Amount2Text))
            {
                double amount = Convert.ToDouble(Amount2Text.Text, System.Globalization.CultureInfo.InvariantCulture);
                double exchangeRateDivide = Convert.ToDouble(CurrencyFrom2.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
                double exchangeRate = Convert.ToDouble(CurrencyTo2.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);

                exchangeRate = exchangeRate / exchangeRateDivide * amount;
                exchangeRate = Math.Round(exchangeRate, 2);
                ExchangeAmount2Text.Text = exchangeRate.ToString();

                CurrencyFromResult.SetCurrencyName(CurrencyFrom2.GetCurrencyName());
                CurrencyToResult.SetCurrencyName(CurrencyTo2.GetCurrencyName());
                CurrencyToResult.SetCurrencyRate(exchangeRate.ToString());
                CurrencyFromResult.SetCurrencyRate(amount.ToString());
                return true;
            }
            else
            {
                MessageBox.Show("Wypełnij pole Amount!");
                return false;
            }
        }

        private void CalculateButton2_Click(object sender, EventArgs e)
        {
            Calculate2();
        }
        private void CompleteTogetherExchange_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                //  ReadAndUpdateData r = new ReadAndUpdateData();
                bool check1 = false;
                bool check2 = true;
                if (Calculate2())
                {
                    Wallet wallet = new SysWal.Wallet();
                    wallet = r.ReadWalletData(id);
                    check1 = r.CheckExchangeTakeIfPossible(wallet, CurrencyFromResult, CurrencyToResult);
                    check2 = r.CheckExchangeGiveIfPossible(wallet, CurrencyFromResult, CurrencyToResult);
                    if (check1 && check2)
                    {
                        MakeListofAsks();
                        r.SaveUserAskList(AskList);
                        MessageBox.Show("Zapytanie zostało wysłane do użytkowników!");
                        FriendsList.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Masz za mało środków w portfelu!");
                    }
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        public void MakeListofAsks()
        {
            AskList = new List<UserAskData>();
            int AskId = r.GetLastAskId();
            AskId = AskId + 1;

            UserAskData MyAsk = new UserAskData();
            MyAsk.SetAskId(AskId);
            MyAsk.SetUserId(SignedInuser.GetUserId());
            MyAsk.Setcash(Convert.ToDouble(Amount2Text.Text));
            MyAsk.SetFromCurrency(CurrencyFrom2.GetCurrencyName());
            MyAsk.SetToCurrency(CurrencyTo2.GetCurrencyName());
            MyAsk.SetFriends(FriendsListBox.Items.Count + 1);
            MyAsk.SetControlNo(1);
            MyAsk.SetAccepted(true);
            MyAsk.SetResult(0.00);

            AskList.Add(MyAsk);


            foreach (UserIdLoginAmount friend in FriendsList)
            {
                UserAskData ask = new UserAskData();
                ask.SetAskId(AskId);
                ask.SetUserId(friend.GetId());
                ask.Setcash(friend.GetAmount());
                ask.SetFromCurrency(CurrencyFrom2.GetCurrencyName());
                ask.SetToCurrency(CurrencyTo2.GetCurrencyName());
                ask.SetFriends(FriendsListBox.Items.Count + 1);
                ask.SetControlNo(1);
                ask.SetAccepted(false);
                ask.SetResult(0.00);

                AskList.Add(ask);
            }

        }

        private bool CalculateWithFriends()
        {
            if (!isEmpty(AmountFriendTextBox))
            {
                double amount = Convert.ToDouble(AmountFriendTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                double exchangeRateDivide = Convert.ToDouble(CurrencyFrom2.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);
                double exchangeRate = Convert.ToDouble(CurrencyTo2.GetCurrencyRate(), System.Globalization.CultureInfo.InvariantCulture);

                exchangeRate = exchangeRate / exchangeRateDivide * amount;
                exchangeRate = Math.Round(exchangeRate, 2);
                ResultFriendTextBox.Text = exchangeRate.ToString();
                return true;
            }
            else
            {
                MessageBox.Show("Wypełnij pole Amount!");
                return false;
            }
        }

        private void FriendsCalculate_Click(object sender, EventArgs e)
        {
            CalculateWithFriends();
        }



        /// //////////////////////////////////////////////////////////////////////////
        ///////////////////////// Panel 3  // take credit
        ////////////////////////////////////////////////////////////////////////

        private void Panel3Create(int i)
        {
            CurrencyList.Clear();
            ReadXMLData xml = new ReadXMLData();
            var ComboItems = new BindingList<string> { };
            xml.CreateList();
          //  FriendsList = new List<UserIdLoginAmount>();

            CurrencyList = xml.GetCurrencyList();

            foreach (Currency prime in CurrencyList)
            {
                CurrencyComboBox3.Items.Add(prime.GetCurrencyName());
            }
            CurrencyComboBox3.SelectedIndex = 0;
            CurrencyCredit = CurrencyList.First();
        }

        private void PayOff1_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Wallet wallet = r.ReadWalletData(id);
                if (wallet.GetCredit().GetCurrencyName() != null)
                {
                    ShowPanel("panel5");
                }
                else
                {
                    MessageBox.Show("Nie masz zaciągniętego Kredytu");
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void Back1_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(id);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }


        private void CurrencyComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyCredit = CurrencyList.Where(i => i.GetCurrencyName() == (string)CurrencyComboBox3.SelectedItem).FirstOrDefault();
        }

        private void TakeCredit3_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                if (!isEmpty(CreditAmountTextBox))
                {
                    string CreditAmount = CreditAmountTextBox.Text;
                    CurrencyCredit.SetCurrencyRate(CreditAmount);
                    if (r.CheckCreditIfPossible(id))
                    {
                        if (r.CheckPlaceInWallet(id, CurrencyCredit))
                        {
                            int type = 3;
                            Wallet wallet = r.CalculateCredit(id, CurrencyCredit);
                            r.UpdateWalletData(wallet);
                            r.SaveHistory(
                               SignedInuser.GetUserId(),
                               0,
                               type,
                               Convert.ToDouble(CurrencyCredit.GetCurrencyRate()),
                               0,
                               CurrencyCredit.GetCurrencyName(),
                               null,
                               today,
                               null,
                               null
                               );
                            MessageBox.Show("Wziąłeś kredyt na kwotę: " + CurrencyCredit.GetCurrencyRate() + " " + CurrencyCredit.GetCurrencyName());
                        }
                        else
                        {
                            MessageBox.Show("Nie masz miejsca w portfelu na nową walutę!");
                        }

                    }

                    else
                    {
                        MessageBox.Show("Masz już wzięty kredyt. \nNie możesz wziąć następnego, do momentu spłaty poprzedniego! ");
                    }

                }
                else
                {
                    MessageBox.Show("Wypełnij pole Amount!");
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        /// //////////////////////////////////////////////////////////////////////////
        ///////////////////////// Panel 4   // History
        ////////////////////////////////////////////////////////////////////////

        private void Exchange4_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(id);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void ExchangeTogether4_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel2Create(id);
                ShowPanel("panel2");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void Credit4_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel3Create(id);
                ShowPanel("panel3");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void Wallet4_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel7Create(id);
                ShowPanel("panel7");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }
        private void LogOut4_Click(object sender, EventArgs e)
        {
            Hide();
            Start start = new Start();
            start.ShowDialog();
            Close();
            start.Dispose();
            //start = null;
        }


        private string WriteTypeOfOperation(HistoryData operation)
        {
            string typeName;
            switch (operation.GetTypeName())
            {
                case 1:
                    typeName = operation.GetOperationDate() + "\tWymiana Własna: " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom() + " za " + operation.GetResult() + " " + operation.GetCurrencyTo();
                    break;

                case 2:
                    typeName = operation.GetOperationDate() + "\tWymiana Wspólna: " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom() + " za " + operation.GetResult() + " " + operation.GetCurrencyTo();
                    break;

                case 3:
                    typeName = operation.GetOperationDate() + "\tPobrany Kredyt na: " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom();
                    break;

                case 4:
                    typeName = operation.GetOperationDate() + "\tSpłata kredytu: " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom() + " Pozostało: " + operation.GetResult() + " " + operation.GetCurrencyFrom();
                    break;

                case 5:
                    typeName = operation.GetOperationDate() + "\tCałkowita spłata kredytu: " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom();
                    break;

                case 6:
                    typeName = operation.GetOperationDate() + "\tPrzelew zewnętrzny: " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom() + " na nr konta: " + operation.GetAccountNo();
                    break;

                case 7:
                    UserData user = r.GetOneUser(operation.GetUserCoopId());
                    typeName = operation.GetOperationDate() + "\tPrzelew wewnętrzny: Przelałeś " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom() + " na konto użytkownika: " + user.GetLogin();
                    break;

                case 8:
                    typeName = operation.GetOperationDate() + "\tWpłaciłeś na konto: " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom() + " z nr konta: " + operation.GetAccountNo();
                    break;

                case 9:
                    user = r.GetOneUser(operation.GetUserCoopId());
                    typeName = operation.GetOperationDate() + "\tPrzelew wewnętrzny: Otrzymałeś " + operation.GetCash() + " "
                        + operation.GetCurrencyFrom() + " od użytkownika: " + user.GetLogin();
                    break;

                default:
                    typeName = "ERROR";
                    break;
            } 
            
            return typeName;
        }

        private void FromdateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            dateFrom = FromdateTimePicker.Value.Date;
        }

        private void TodateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            dateTo = TodateTimePicker.Value.Date;
        }

        private void WriteHistoryList(List<HistoryData> HistoryList, BindingList<string>  myItems1)
        {
            HistoryList.Reverse();
            foreach (HistoryData operation in HistoryList)
            {
                myItems1.Add(WriteTypeOfOperation(operation));
            }
            HistoryListBox1.DataSource = myItems1;
        }
        private void Panel4Create(int i)
        {
            dateFrom = DateTime.Today;
            dateTo = DateTime.Today;
            var myItems1 = new BindingList<string> { };
            var HistoryList = new List<HistoryData>();

            HistoryList = r.ReadHistoryData(id);
            WriteHistoryList(HistoryList, myItems1);
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                dateTo = TodateTimePicker.Value.Date;
                var myItems1 = new BindingList<string> { };
                dateTo = dateTo.AddDays(1);
                HistoryList = r.ReadHistoryData(id);
                var HistoryListFilter = new List<HistoryData>();

                foreach (HistoryData operation in HistoryList)
                {
                    if (operation.GetOperationDate() > dateTo)
                    {
                        break;
                    }
                    if (operation.GetOperationDate() >= dateFrom)
                    {
                        HistoryListFilter.Add(operation);
                    }
                }

                WriteHistoryList(HistoryListFilter, myItems1);
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        /*
        DateTime myDate = datePortionDateTimePicker.Value.Date +
                    timePortionDateTimePicker.Value.TimeOfDay;
datePortionDateTimePicker.Value  = myDate.Date;  
timePortionDateTimePicker.Value  = myDate.TimeOfDay
*/

        /// //////////////////////////////////////////////////////////////////////////
        ///////////////////////// Panel 5   // PayOff Credit
        ////////////////////////////////////////////////////////////////////////
        private void TakeCredit2_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel3Create(id);
                ShowPanel("panel3");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void Back2_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(id);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void PayOffInInstButton_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                if (!isEmpty(CreditPayOffAmountText))
                {
                    Currency credit = r.ReadCreditData(id);
                    if (r.CheckIfPayAmountCredit(id, CreditPayOffAmountText.Text))
                    {
                        if (r.PayPartOfCredit(id, CreditPayOffAmountText.Text))
                        {
                            MessageBox.Show("Spłaciłeś swój kredyt!");
                        }
                        else
                        {
                            MessageBox.Show("Spłaciłeś " + CreditPayOffAmountText.Text + " " + credit.GetCurrencyName() + " !");
                            Panel1Create(id);
                            ShowPanel("panel1");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nie masz tyle środków na koncie");
                    }
                }
                else
                {
                    MessageBox.Show("Wypełnij pole Amount!");
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void PayAllButton_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Wallet wallet;
                wallet = r.ReadWalletData(id);
                if (r.CheckIfPayOffCreditPossible(id))
                {
                    //int type = 5;
                    r.PayOffWholeCredit(id);
                    /*   r.SaveHistory(
                                  SignedInuser.GetUserId(),
                                  0,
                                  type,
                                  Convert.ToDouble(wallet.GetCredit().GetCurrencyRate()),
                                  0,
                                  CurrencyCredit.GetCurrencyName(),
                                  null,
                                  today,
                                  null,
                                  null
                                  );*/
                    MessageBox.Show("Spłaciłeś swój kredyt!");
                    Panel1Create(id);
                    ShowPanel("panel1");
                }
                else
                {
                    MessageBox.Show("Nie masz tyle środków na koncie");
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }




        /// //////////////////////////////////////////////////////////////////////////
        ///////////////////////// Panel 6   // Wallet InsideMoneyTransfer
        ////////////////////////////////////////////////////////////////////////

        private void Panel6Create(int i)
        {
            CurrencyList.Clear();
            CurrencyTransferComboBox1.Items.Clear();
            ReadXMLData xml = new ReadXMLData();
            var ComboItems = new BindingList<string> { };
            xml.CreateList();

            CurrencyList = xml.GetCurrencyList();

            foreach (Currency prime in CurrencyList)
            {
                CurrencyTransferComboBox1.Items.Add(prime.GetCurrencyName());
            }
            CurrencyTransferComboBox1.SelectedIndex = 0;
            CurrencyFrom = CurrencyList.First();
        }


        private void InsideMoneyTransfer1_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel8Create(id);
                ShowPanel("panel8");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void MoneyIncomeWallet1_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel9Create(id);
                ShowPanel("panel9");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void ShowWallet1_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel7Create(id);
                ShowPanel("panel7");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void BackWallet1_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(id);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void CompleteOutsideTransfer_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                if (CheckAccountNo(AccNoTextBox.Text))
                {
                    if (!isEmpty(AmountTransfer1) && !isEmpty(AccNoTextBox) && !isEmpty(Description1))
                    {
                        Currency currency = new Currency();
                        currency.SetCurrencyName(CurrencyFrom.GetCurrencyName());
                        currency.SetCurrencyRate(AmountTransfer1.Text);
                        if (r.CheckIfOutsideTransferPossible(id, currency))
                        {
                            int type = 6;
                            r.DoOutsideTransfer(id, currency);
                            r.SaveHistory(
                               SignedInuser.GetUserId(),
                               0,
                               type,
                               Convert.ToDouble(AmountTransfer1.Text),
                               0,
                               CurrencyFrom.GetCurrencyName().ToString(),
                               null,
                               today,
                               Description1.Text,
                               AccNoTextBox.Text
                               );
                            MessageBox.Show("Przelew zakończony pomyślnie!");
                        }
                        else
                        {
                            MessageBox.Show("Masz za mało środków na koncie");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Wypełnij wszystkie pola");
                    }
                }
                else
                {
                    MessageBox.Show("Numer konta musi zawierać 26 cyfr");
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void CurrencyTransferComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyFrom = CurrencyList.Where(i => i.GetCurrencyName() == (string)CurrencyTransferComboBox1.SelectedItem).FirstOrDefault();
        }



        /// //////////////////////////////////////////////////////////////////////////
        ///////////////////////// Panel 7   // ShowWallet 
        ////////////////////////////////////////////////////////////////////////

        private void Panel7Create(int i)
        {
            
            AccountNoLabel.Text = SignedInuser.GetAccountNo();
            var myItems1 = new BindingList<string> { };
            WalletListBox.DataSource = myItems1;
            Wallet wallet = r.ReadWalletData(id);
            foreach (Currency walletCurrency in wallet.GetCurrencyList())
            {
                if (walletCurrency.GetCurrencyName() != null)
                {
                    myItems1.Add("\n");
                    myItems1.Add(walletCurrency.GetCurrencyName() + "\t\t" + walletCurrency.GetCurrencyRate());
                }
            }
            WalletListBox.DataSource = myItems1;
            if (Convert.ToDouble(wallet.GetCredit().GetCurrencyRate()) != 0.00)
            {
                CreditLabel.Text = wallet.GetCredit().GetCurrencyRate() + " " + wallet.GetCredit().GetCurrencyName();
            }
            else
            {
                CreditLabelTitle.Text = null;
                CreditLabel.Text = null;
            }
        }

        private void InsideTransfer2_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel8Create(id);
                ShowPanel("panel8");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void OutsideTransfer2_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel6Create(id);
                ShowPanel("panel6");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void MoneyIncome2_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel9Create(id);
                ShowPanel("panel9");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void backWallet2_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(id);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }



        /// //////////////////////////////////////////////////////////////////////////
        ///////////////////////// Panel 8   // InsideTogetherExchange 
        ////////////////////////////////////////////////////////////////////////

        private void OutsideTransferWallet3_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel6Create(id);
                ShowPanel("panel6");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void MoneyIncomeWallet3_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel9Create(id);
                ShowPanel("panel9");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void ShowWallet3_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel7Create(id);
                ShowPanel("panel7");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void BackWallet3_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(id);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

        private void Panel8Create(int id)
        {
            CurrencyList.Clear();
            InsideTransferCurrencyComboBox.Items.Clear();
            ReadXMLData xml = new ReadXMLData();
            var ComboItems = new BindingList<string> { };
            xml.CreateList();

            CurrencyList = xml.GetCurrencyList();

            foreach (Currency prime in CurrencyList)
            {
                InsideTransferCurrencyComboBox.Items.Add(prime.GetCurrencyName());
            }
            InsideTransferCurrencyComboBox.SelectedIndex = 0;
            CurrencyFrom = CurrencyList.First();

            loginTransferTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            loginTransferTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            UserList = r.ReadIdLogin();
            foreach (UserIdLogin user in UserList)
            {
                coll.Add(user.GetLogin());
            }
            coll.Remove(SignedInuser.GetLogin());
            loginTransferTextBox.AutoCompleteCustomSource = coll;
            ShowPanel("panel8");
        }

        private void CompleteInsideTransfer_Click(object sender, EventArgs e)
        {
            if (xml.CheckForInternetConnection())
            {
                if (!isEmpty(loginTransferTextBox) && !isEmpty(InsideTransferAmountTextBox) && !isEmpty(DescriptionInsideTransfer3))
                {
                    Currency currency = new Currency();
                    currency.SetCurrencyName(CurrencyFrom.GetCurrencyName());
                    currency.SetCurrencyRate(InsideTransferAmountTextBox.Text);
                    if (r.CheckIfOutsideTransferPossible(id, currency))
                    {
                        if (r.CheckIfUser2HasPlaceInWallet(currency, loginTransferTextBox.Text))
                        {
                            int type = 7;
                            r.DoIntsideTransfer(id, currency, loginTransferTextBox.Text);
                            r.SaveHistory(
                               SignedInuser.GetUserId(),
                               r.GetOneUserId(loginTransferTextBox.Text),
                               type,
                               Convert.ToDouble(InsideTransferAmountTextBox.Text),
                               0,
                               CurrencyFrom.GetCurrencyName(),
                               null,
                               today,
                               DescriptionInsideTransfer3.Text,
                               null
                               );

                            int CoopId = r.GetOneUserId(loginTransferTextBox.Text);
                            type = 9;
                            r.SaveHistory(
                               CoopId,
                               SignedInuser.GetUserId(),
                               type,
                               Convert.ToDouble(InsideTransferAmountTextBox.Text),
                               0,
                               CurrencyFrom.GetCurrencyName(),
                               null,
                               today,
                               DescriptionInsideTransfer3.Text,
                               null
                               );
                            MessageBox.Show("Przelew zakończony pomyślnie!");
                        }
                        else
                        {
                            MessageBox.Show("Odbiorca nie ma miejsca na nową walutę");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Masz za mało środków na koncie");
                    }
                }
                else
                {
                    MessageBox.Show("Wypełnij wszystkie pola");
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

    
    private void InsideTransferCurrencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrencyFrom = CurrencyList.Where(i => i.GetCurrencyName() == (string)InsideTransferCurrencyComboBox.SelectedItem).FirstOrDefault();
    }



    /// //////////////////////////////////////////////////////////////////////////
    ///////////////////////// Panel 9   // MoneyIncome
    ////////////////////////////////////////////////////////////////////////



    private void InsideMoneyTransfer4_Click(object sender, EventArgs e)
    {
            if (xml.CheckForInternetConnection())
            {
                Panel8Create(id);
                ShowPanel("panel8");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

    private void OutsideMoneyTransfer4_Click(object sender, EventArgs e)
    {
            if (xml.CheckForInternetConnection())
            {
                Panel6Create(id);
                ShowPanel("panel6");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

    private void ShowWallet4_Click(object sender, EventArgs e)
    {
            if (xml.CheckForInternetConnection())
            {
                Panel7Create(id);
                ShowPanel("panel7");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

    private void Back4_Click(object sender, EventArgs e)
    {
            if (xml.CheckForInternetConnection())
            {
                Panel1Create(id);
                ShowPanel("panel1");
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }

    private void MoneyIncomeCurrencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrencyFrom = CurrencyList.Where(i => i.GetCurrencyName() == (string)MoneyIncomeCurrencyComboBox.SelectedItem).FirstOrDefault();
    }
    private void CompleteMoneyIncome_Click(object sender, EventArgs e)
    {
            if (xml.CheckForInternetConnection())
            {
                if (CheckAccountNo(MoneyIncomeAccountNoTextBox.Text))
                {
                    if (!isEmpty(MoneyIncomeAmountTextBox) && !isEmpty(MoneyIncomeAccountNoTextBox))
                    {
                        Currency currency = new Currency();
                        currency.SetCurrencyName(CurrencyFrom.GetCurrencyName());
                        currency.SetCurrencyRate(MoneyIncomeAmountTextBox.Text);
                        if (r.CheckIfMoneyIncomeAmountPossible(id, currency))
                        {
                            int type = 8;
                            r.DoMoneyIncome(id, currency);
                            r.SaveHistory(
                               SignedInuser.GetUserId(),
                               0,
                               type,
                               Convert.ToDouble(MoneyIncomeAmountTextBox.Text),
                               0,
                               CurrencyFrom.GetCurrencyName().ToString(),
                               null,
                               today,
                               null,
                               MoneyIncomeAccountNoTextBox.Text
                               );
                            MessageBox.Show("Przelew zakończony pomyślnie!");
                        }
                        else
                        {
                            MessageBox.Show("Nie masz miejsca w portfelu na nową walutę!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Wypełnij wszystkie pola");
                    }
                }
                else
                {
                    MessageBox.Show("Numer konta musi zawierać 26 cyfr");
                }
            }
            else
            {
                MessageBox.Show("Brak połączenia z Internetem");
                LogOutFunction();
            }
        }


        private void Panel9Create(int id)
        {
            CurrencyList.Clear();
            MoneyIncomeCurrencyComboBox.Items.Clear();
            ReadXMLData xml = new ReadXMLData();
            var ComboItems = new BindingList<string> { };
            xml.CreateList();

            CurrencyList = xml.GetCurrencyList();

            foreach (Currency prime in CurrencyList)
            {
                MoneyIncomeCurrencyComboBox.Items.Add(prime.GetCurrencyName());
            }
            MoneyIncomeCurrencyComboBox.SelectedIndex = 0;
            CurrencyFrom = CurrencyList.First();
        }

       
    }
}

