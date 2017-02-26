using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SysWal
{
    class ReadAndUpdateData
    {
        private List<UserLoginAndPassword> userLoginAndPassword = new List<UserLoginAndPassword>();
        private List<UserIdLogin> userIdLogin = new List<UserIdLogin>();
        private DateTime today = DateTime.Now;


        /*  public void SaveCurrencyList(List<Currency> CurrencyList)
          {
              string command = "INSERT INTO CurrencyRates(";
              int i = 32;
              foreach (Currency c in CurrencyList)
              {
                  i = i - 1;
                  if (i != 0)
                  {
                      command = command + c.GetCurrencyName() + ", ";
                  }
                  else
                  {
                      command = command + c.GetCurrencyName() + ") values (";
                  }
              }
              i = 32;
              string name;
              SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal; Integrated Security=True");
              con.Open();
              SqlCommand cmd = new SqlCommand(command, con);
              foreach (Currency c in CurrencyList)
              {
                  i--;
                  if (i != 0)
                  {
                      name = "@Currency" + i;
                      cmd.Parameters.AddWithValue(name, c.GetCurrencyRate());
                      command = command + name + ", ";
                  }
                  else
                  {
                      name = "@Currency" + i;
                      cmd.Parameters.AddWithValue(name, c.GetCurrencyRate());
                      command = command + name + ")";
                  }
              }
              cmd.ExecuteNonQuery();
          }
          */

        public List<Currency> ReadCurrencyList()
        {
            List<Currency> CurrencyList = new List<Currency>();
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select CurrencyName, CurrencyRate from CurrencyRates", con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Currency cur = new Currency();
                cur.SetCurrencyName(myReader["CurrencyName"].ToString());
                cur.SetCurrencyRate(myReader["CurrencyRate"].ToString());

                CurrencyList.Add(cur);
            }
            return CurrencyList;
        }
        public void UpdateCurrencyList(List<Currency> CurrencyList)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal; Integrated Security=True");
            con.Open();
            foreach (Currency c in CurrencyList)
            {
                string command = "Update CurrencyRates Set  CurrencyRate = @rate where CurrencyName = @name";
                SqlCommand cmd = new SqlCommand(command, con);

                cmd.Parameters.AddWithValue("@name", c.GetCurrencyName());
                cmd.Parameters.AddWithValue("@rate", c.GetCurrencyRate());

                cmd.ExecuteNonQuery();
            }
        }
        public void SaveCurrencyList(List<Currency> CurrencyList)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal; Integrated Security=True");
            con.Open();
            foreach (Currency c in CurrencyList)
            {
                string command = "INSERT INTO CurrencyRates(CurrencyName, CurrencyRate) values (@name, @rate)";
                SqlCommand cmd = new SqlCommand(command, con);

                cmd.Parameters.AddWithValue("@name", c.GetCurrencyName());
                cmd.Parameters.AddWithValue("@rate", c.GetCurrencyRate());

                cmd.ExecuteNonQuery();
            }
        }


        public int GetLastUserId()
        {
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");

            con.Open();
            string command = "SELECT MAX(UserID)FROM UserData";
            SqlCommand myCommand = new SqlCommand(command, con);
            int id = (int)myCommand.ExecuteScalar();
            return id;
        }

        public int GetLastAskId()
        {
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");

            con.Open();
            string command = "SELECT MAX(AskID)FROM UserAskData";
            SqlCommand myCommand = new SqlCommand(command, con);
            int id = (int)myCommand.ExecuteScalar();
            return id;
        }

        public int GetOneUserId(string Userlogin)
        /* Reads login */
        {
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            string command = "select UserId from UserData where login = '" + Userlogin + "'";
            SqlCommand myCommand = new SqlCommand(command, con);
            int id = 0;
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            if (myReader.Read())
            {
                id = (int)myReader["UserId"];
            }
            return id;
        }



        public bool CheckIfExists(string login)
        /* Finds login in list, if exixts return true */
        {
            userLoginAndPassword = ReadLoginAndPassword();
            foreach (UserLoginAndPassword user in userLoginAndPassword)
            {
                if (user.GetLogin() == login)
                {
                    return true;
                }
            }
            return false;
        }

        public bool LogInProcess(ref int id, string login, string password)
        /* Sign in - true if you can */
        {
            HashPassword hsp = new HashPassword();
            foreach (UserLoginAndPassword user in userLoginAndPassword)
            {
                if (user.GetLogin() == login)
                {
                    string passwordDataBase = user.GetPassword();
                    string passwordTextBox = hsp.GenerateSHA256Hash(password, user.GetSalt());
                    if (user.GetPassword() == passwordTextBox)
                    {
                        id = user.GetId();
                        return true;
                    }
                }
            }
            return false;
        }


        public List<UserData> ReadAllUserData()
        /* Reads all data from UserData*/
        {
            List<UserData> UserDataList = new List<UserData>();

            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select * from UserData", con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                UserData user = new UserData();
                user.SetUserId((int)myReader["UserId"]);
                user.SetLogin(myReader["login"].ToString());
                user.SetPassword(myReader["password"].ToString());
                user.SetSalt(myReader["salt"].ToString());
                user.SetName(myReader["name"].ToString());
                user.SetSurname(myReader["surname"].ToString());
                user.SetPESEL(myReader["PESEL"].ToString());
                user.SetAccountNo(myReader["accountNo"].ToString());
                user.SetEmail(myReader["email"].ToString());

                UserDataList.Add(user);
            }
            return UserDataList;
        }


        public void CreateWallet(int id)
        /* Creates new Wallet for new User */
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO UserWallet (UserId, Currency1, Amount1, Currency2, Amount2, Currency3, Amount3, Currency4, Amount4, Currency5, Amount5, CreditType, CreditAmount) values (@UserId, @Currency1, @Amount1, @Currency2, @Amount2, @Currency3, @Amount3, @Currency4, @Amount4, @Currency5, @Amount5, @CreditType, @CreditAmount)", con);

            cmd.Parameters.AddWithValue("@UserId", id);
            int nameIter = 0;
            for (int i = 0; i < 5; i++)
            {
                {
                    nameIter = i + 1;
                    cmd.Parameters.AddWithValue("@Currency" + nameIter, DBNull.Value);
                    cmd.Parameters.AddWithValue("@Amount" + nameIter, 0.00);
                }
            }
            cmd.Parameters.AddWithValue("@CreditType", DBNull.Value);
            cmd.Parameters.AddWithValue("@CreditAmount", 0.00);

            cmd.ExecuteNonQuery();
        }


        public UserData GetOneUser(int id)
        /* Reads Data about one user */
        {
            UserData user = new UserData();

            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            string command = "select * from UserData where UserId =" + id;
            SqlCommand myCommand = new SqlCommand(command, con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            if (myReader.Read())
            {
                user.SetUserId((int)myReader["UserId"]);
                user.SetLogin(myReader["login"].ToString());
                user.SetPassword(myReader["password"].ToString());
                user.SetSalt(myReader["salt"].ToString());
                user.SetName(myReader["name"].ToString());
                user.SetSurname(myReader["surname"].ToString());
                user.SetPESEL(myReader["PESEL"].ToString());
                user.SetAccountNo(myReader["accountNo"].ToString());
                user.SetEmail(myReader["email"].ToString());
            }
            return user;
        }


        public List<UserLoginAndPassword> ReadLoginAndPassword()
        /* Reads all Login and password data from UserData Database (to Sign in)*/
        {
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select UserId, login, password, salt from UserData", con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                UserLoginAndPassword user = new UserLoginAndPassword();
                user.SetId((int)myReader["UserId"]);
                user.SetLogin(myReader["login"].ToString());
                user.SetPassword(myReader["password"].ToString());
                user.SetSalt(myReader["salt"].ToString());

                userLoginAndPassword.Add(user);
            }
            return userLoginAndPassword;
        }

        public List<UserIdLogin> ReadIdLogin()
        /* Reads all Login and password data from UserData Database (to Sign in)*/
        {
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select UserId, login from UserData", con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                UserIdLogin user = new UserIdLogin();
                user.SetId((int)myReader["UserId"]);
                user.SetLogin(myReader["login"].ToString());


                userIdLogin.Add(user);
            }
            return userIdLogin;
        }

        /*
                public void CheckPlaceInWallet()
                {
                    if (checkifAdded == false)
                    {
                        check2 = false;
                        check1 = false;
                        for (i = 0; i < 5; i++)
                        {
                            if (CurrencyList[i].GetCurrencyName() == null)
                            {
                                CurrencyList[i].SetCurrencyName(CurrencyGive.GetCurrencyName());
                                CurrencyList[i].SetCurrencyRate(CurrencyGive.GetCurrencyRate());
                                check2 = true;
                                check1 = true;
                                break;
                            }
                        }
                    }
                }
        */

        public bool CheckExchangeGiveIfPossible(Wallet wallet, Currency CurrencyTake, Currency CurrencyGive)
        {
            List<Currency> CurrencyList = new List<Currency>();
            CurrencyList = wallet.GetCurrencyList();

            for (int i = 0; i < 5; i++)
            {
                if (CurrencyList[i].GetCurrencyName() == CurrencyGive.GetCurrencyName())
                {
                    return true;
                }
                if (CurrencyList[i].GetCurrencyName() == null)
                {
                    return true;
                }
            }
            return false;
        }


        public bool CheckExchangeTakeIfPossible(Wallet wallet, Currency currencyTake, Currency currencyGive)
        /*Checks if the user has enough money for exchange */
        {
            double temp;
            bool check = false;
            int i = 0;
            List<Currency> CurrencyList = new List<Currency>();
            CurrencyList = wallet.GetCurrencyList();
            for (i = 0; i < 5; i++)
            {
                temp = Convert.ToDouble(CurrencyList[i].GetCurrencyRate());
                if (CurrencyList[i].GetCurrencyName() == currencyTake.GetCurrencyName())
                {
                    temp = temp - Convert.ToDouble(currencyTake.GetCurrencyRate());
                    check = true;
                    if (temp < 0) return false;
                }
            }
            if (check == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void UpdateExchangeWallet(int id, Currency CurrencyTake, Currency CurrencyGive, ref bool check1, ref bool check2)
        /* Update User's wallet and remove currency without money */
        /* 1. Check if possible
           2. Do exchange
           3. If currency not exists, creates new
           4. If there is no spot in wallet
           */
        {
            Wallet wallet = new Wallet();
            wallet = ReadWalletData(id);
            double temp;
            int i = 0;
            bool checkifAdded = false;
            List<Currency> CurrencyList = new List<Currency>();
            CurrencyList = wallet.GetCurrencyList();
            check1 = CheckExchangeTakeIfPossible(wallet, CurrencyTake, CurrencyGive);

            if (check1)
            {
                for (i = 0; i < 5; i++)
                {
                    temp = Convert.ToDouble(CurrencyList[i].GetCurrencyRate());
                    if (CurrencyList[i].GetCurrencyName() == CurrencyTake.GetCurrencyName())
                    {
                        temp = temp - Convert.ToDouble(CurrencyTake.GetCurrencyRate());
                        if (temp == 0)
                        {
                            CurrencyList[i].SetCurrencyName(null);
                        }
                        else
                        {
                            CurrencyList[i].SetCurrencyRate(temp.ToString());
                        }

                    }
                    if (CurrencyList[i].GetCurrencyName() == CurrencyGive.GetCurrencyName())
                    {
                        temp = temp + Convert.ToDouble(CurrencyGive.GetCurrencyRate());
                        CurrencyList[i].SetCurrencyRate(temp.ToString());
                        checkifAdded = true;
                    }
                }
                if (checkifAdded == false)
                {
                    check2 = false;
                    check1 = false;
                    for (i = 0; i < 5; i++)
                    {
                        if (CurrencyList[i].GetCurrencyName() == null)
                        {
                            CurrencyList[i].SetCurrencyName(CurrencyGive.GetCurrencyName());
                            CurrencyList[i].SetCurrencyRate(CurrencyGive.GetCurrencyRate());
                            check2 = true;
                            check1 = true;
                            break;
                        }
                    }
                }
                UpdateWalletData(wallet);
            }

        }

        /* public void UpdateTakeCreditWallet(int id, Currency CurrencyGive)
              /* Makes possible to take credit*/
        /*    {
                 Wallet wallet = new Wallet();

                 wallet = ReadWalletData(id);
                 wallet.SetCredit(CurrencyGive.GetCurrencyName(), CurrencyGive.GetCurrencyRate());
                 UpdateWalletData(wallet);
             }*/

        /*     public void UpdatePayWallet(int id, Currency CurrencyTake, double result)
             {
                 Wallet wallet = new Wallet();
                 wallet = ReadWalletData(id);
             }*/

        public void ChangeIntoNull(ref Wallet wallet)
        {
            foreach (Currency cur in wallet.GetCurrencyList())
            {
                if (cur.GetCurrencyName() == "")
                {
                    cur.SetCurrencyName(null);
                }
            }
            if (wallet.GetCredit().GetCurrencyName() == "")
            {
                string value = wallet.GetCredit().GetCurrencyRate();
                wallet.SetCredit(null, value);
            }
        }
        public Wallet ReadWalletData(int ID)
        /* Reads wallet info from database */
        {
            Wallet UserWallet = new Wallet();
            string creditName;
            string creditAmount;

            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select * from UserWallet where UserId =" + ID, con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            if (myReader.Read())
            {
                UserWallet.SetUserId((int)myReader["UserId"]);
                for (int i = 1; i < 6; i++)
                {
                    UserWallet.AddCurrency(myReader["Currency" + i.ToString()].ToString(), myReader["Amount" + i.ToString()].ToString());
                }
                creditName = myReader["CreditType"].ToString();
                creditAmount = myReader["CreditAmount"].ToString();
                UserWallet.SetCredit(creditName, creditAmount);
                //UserWallet.SetCredit(myReader["CreditType"].ToString(), myReader["CreditAmount"].ToString());
            }
            ChangeIntoNull(ref UserWallet);
            return UserWallet;
        }
        public void UpdateWalletData(Wallet wallet)
        /* Updates wallet for User */
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Update UserWallet Set Currency1 = @Currency1, Amount1 = @Amount1, Currency2 = @Currency2, Amount2 = @Amount2, Currency3 = @Currency3, Amount3 = @Amount3, Currency4 = @Currency4, Amount4 = @Amount4, Currency5 = @Currency5, Amount5 = @Amount5, CreditType = @CreditType, CreditAmount = @CreditAmount Where UserId = @UserId;", con);

            cmd.Parameters.AddWithValue("@UserId", wallet.GetUserId());
            for (int i = 0; i < 5; i++)
            {
                int nameIter = i + 1;
                if (wallet.GetCurrencyList()[i].GetCurrencyName() == null)
                {
                    cmd.Parameters.AddWithValue("@Currency" + nameIter, DBNull.Value);
                    cmd.Parameters.AddWithValue("@Amount" + nameIter, 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Currency" + nameIter, wallet.GetCurrencyList()[i].GetCurrencyName());
                    cmd.Parameters.AddWithValue("@Amount" + nameIter, Convert.ToDouble(wallet.GetCurrencyList()[i].GetCurrencyRate()));
                }
            }

            if (wallet.GetCredit().GetCurrencyName() == null)
            {
                cmd.Parameters.AddWithValue("@CreditType", DBNull.Value);
                cmd.Parameters.AddWithValue("@CreditAmount", 0.00);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CreditType", wallet.GetCredit().GetCurrencyName());
                cmd.Parameters.AddWithValue("@CreditAmount", Convert.ToDouble(wallet.GetCredit().GetCurrencyRate()));
            }

            cmd.ExecuteNonQuery();
        }


        public List<UserAskData> ReadUserAskWithAskId(int AskId)
        {
            List<UserAskData> AskDataList = new List<UserAskData>();
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select * from UserAskData where AskID = " + AskId, con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                UserAskData ask = new UserAskData();
                ask.SetAskId((int)myReader["AskId"]);
                ask.SetUserId((int)myReader["UserId"]);
                ask.Setcash(Convert.ToDouble(myReader["cash"]));
                ask.SetFromCurrency(myReader["fromCurrency"].ToString());
                ask.SetToCurrency(myReader["toCurrency"].ToString());
                //ask.SetResult(Convert.ToDouble(myReader["result"]));
                ask.SetFriends((int)myReader["friends"]);
                ask.SetControlNo((int)myReader["controlNo"]);
                ask.SetAccepted((bool)myReader["accepted"]);
                //   ask.SetDone((bool)myReader["done"]);

                AskDataList.Add(ask);
            }
            return AskDataList;
        }

        public List<UserAskData> ReadUserAskDataWithUserId(int UserId)
        {
            List<UserAskData> AskDataList = new List<UserAskData>();
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select * from UserAskData where UserId = " + UserId, con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                UserAskData ask = new UserAskData();
                ask.SetAskId((int)myReader["AskId"]);
                ask.SetUserId((int)myReader["UserId"]);
                ask.Setcash(Convert.ToDouble(myReader["cash"]));
                ask.SetFromCurrency(myReader["fromCurrency"].ToString());
                ask.SetToCurrency(myReader["toCurrency"].ToString());
                // ask.SetResult(Convert.ToDouble(myReader["result"]));
                ask.SetFriends((int)myReader["friends"]);
                ask.SetControlNo((int)myReader["controlNo"]);
                ask.SetAccepted((bool)myReader["accepted"]);
                //ask.SetDone((bool)myReader["done"]);
                ask.SetResult(0.00);

                AskDataList.Add(ask);
            }
            return AskDataList;
        }

        public void SaveUserAskData(UserAskData ask)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO UserAskData(AskId, UserId, cash, fromCurrency, toCurrency, friends, controlNo, accepted) values (@AskId, @UserId, @cash, @fromCurrency, @toCurrency, @Friends, @ControlNo, @Accepted)", con);

            cmd.Parameters.AddWithValue("@AskId", ask.GetAskId());
            cmd.Parameters.AddWithValue("@UserId", ask.GetUserId());
            cmd.Parameters.AddWithValue("@cash", ask.Getcash());
            cmd.Parameters.AddWithValue("@fromCurrency", ask.GetCurrencyFrom());
            cmd.Parameters.AddWithValue("@toCurrency", ask.GetCurrencyTo());
            // cmd.Parameters.AddWithValue("@Result", ask.Getresult());
            cmd.Parameters.AddWithValue("@Friends", ask.GetFriends());
            cmd.Parameters.AddWithValue("@ControlNo", ask.GetControlNo());
            cmd.Parameters.AddWithValue("@Accepted", ask.GetAccepted());
            //   cmd.Parameters.AddWithValue("@Done", ask.GetDone());

            cmd.ExecuteNonQuery();
        }

        public void SaveUserAskList(List<UserAskData> AskList)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal;Integrated Security=True");
            con.Open();

            foreach (UserAskData ask in AskList)
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO UserAskData(AskId, UserId, cash, fromCurrency, toCurrency, friends, controlNo, accepted) values (@AskId, @UserId, @cash, @fromCurrency, @toCurrency, @Friends, @ControlNo, @Accepted)", con);

                cmd.Parameters.AddWithValue("@AskId", ask.GetAskId());
                cmd.Parameters.AddWithValue("@UserId", ask.GetUserId());
                cmd.Parameters.AddWithValue("@cash", ask.Getcash());
                cmd.Parameters.AddWithValue("@fromCurrency", ask.GetCurrencyFrom());
                cmd.Parameters.AddWithValue("@toCurrency", ask.GetCurrencyTo());

                cmd.Parameters.AddWithValue("@Friends", ask.GetFriends());
                cmd.Parameters.AddWithValue("@ControlNo", ask.GetControlNo());
                cmd.Parameters.AddWithValue("@Accepted", ask.GetAccepted());
                //   cmd.Parameters.AddWithValue("@Done", ask.GetDone());

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUserAskList(List<UserAskData> AskList)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal;Integrated Security=True");
            con.Open();

            foreach (UserAskData ask in AskList)
            {

                SqlCommand cmd = new SqlCommand("Update UserAskData Set controlNo = @ControlNo, accepted = @Accepted where UserId = " + ask.GetUserId() + " and AskId = " + ask.GetAskId(), con);


                //  cmd.Parameters.AddWithValue("@AskId", ask.GetAskId());
                //  cmd.Parameters.AddWithValue("@UserId", ask.GetUserId());
                //  cmd.Parameters.AddWithValue("@cash", ask.Getcash());
                // cmd.Parameters.AddWithValue("@fromCurrency", ask.GetCurrencyFrom());
                //  cmd.Parameters.AddWithValue("@toCurrency", ask.GetCurrencyTo());

                // cmd.Parameters.AddWithValue("@Friends", ask.GetFriends());
                cmd.Parameters.AddWithValue("@ControlNo", ask.GetControlNo());
                cmd.Parameters.AddWithValue("@Accepted", ask.GetAccepted());
                // cmd.Parameters.AddWithValue("@Done", ask.GetDone());

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUserAskList(List<UserAskData> AskList)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal;Integrated Security=True");
            con.Open();
            foreach (UserAskData ask in AskList)
            {
                SqlCommand cmd = new SqlCommand("Delete from UserAskData where UserId = " + ask.GetUserId() + " and AskId = " + ask.GetAskId(), con);


                //  cmd.Parameters.AddWithValue("@AskId", ask.GetAskId());
                //  cmd.Parameters.AddWithValue("@UserId", ask.GetUserId());
                //  cmd.Parameters.AddWithValue("@cash", ask.Getcash());
                // cmd.Parameters.AddWithValue("@fromCurrency", ask.GetCurrencyFrom());
                //  cmd.Parameters.AddWithValue("@toCurrency", ask.GetCurrencyTo());

                // cmd.Parameters.AddWithValue("@Friends", ask.GetFriends());
                //cmd.Parameters.AddWithValue("@ControlNo", ask.GetControlNo());
                // cmd.Parameters.AddWithValue("@Accepted", ask.GetAccepted());

                cmd.ExecuteNonQuery();
            }
        }

        public bool CheckCreditIfPossible(int id)
        {
            Wallet wallet = ReadWalletData(id);
            if (wallet.GetCredit().GetCurrencyName() == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPlaceInWallet(int id, Currency credit)
        {
            Wallet wallet = ReadWalletData(id);
            List<Currency> list = wallet.GetCurrencyList();
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == credit.GetCurrencyName())
                {
                    return true;
                }
                else if (list[i].GetCurrencyName() == null)
                {
                    return true;
                }
            }
            return false;
        }



        public void TakeCreditWithCreditData(Currency CreditCurrency, int id)
        /* Updates wallet for User */
        {
            SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=SysWal;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Update UserWallet Set CreditType = @CreditType, CreditAmount = @CreditAmount Where UserId = @UserId;", con);

            cmd.Parameters.AddWithValue("@UserId", id);

            if (CreditCurrency.GetCurrencyName() == null)
            {
                cmd.Parameters.AddWithValue("@CreditType", DBNull.Value);
                cmd.Parameters.AddWithValue("@CreditAmount", 0.00);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CreditType", CreditCurrency.GetCurrencyName());
                cmd.Parameters.AddWithValue("@CreditAmount", Convert.ToDouble(CreditCurrency.GetCurrencyRate()));
            }
            cmd.ExecuteNonQuery();
        }

        public Wallet CalculateCredit(int id, Currency credit)
        {
            Wallet wallet = ReadWalletData(id);
            List<Currency> list = wallet.GetCurrencyList();
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == credit.GetCurrencyName())
                {
                    list[i].SetCurrencyRate((Convert.ToDouble(list[i].GetCurrencyRate()) + Convert.ToDouble(credit.GetCurrencyRate())).ToString());
                    break;
                }
                else if (list[i].GetCurrencyName() == null)
                {
                    list[i].SetCurrencyRate(credit.GetCurrencyRate());
                    list[i].SetCurrencyName(credit.GetCurrencyName());
                    break;
                }
            }
            wallet.SetCredit(credit.GetCurrencyName(), credit.GetCurrencyRate());
            wallet.SetCurrencyList(list);
            return wallet;
        }

        /* public Wallet PayOffWholeCredit(int id)
         {
             Wallet wallet = ReadWalletData(id);
             List<Currency> list = wallet.GetCurrencyList();
             for (int i = 0; i < 5; i++)
             {
                 if (list[i].GetCurrencyName() == wallet.GetCredit().GetCurrencyName())
                 {
                     list[i].SetCurrencyRate((Convert.ToDouble(list[i].GetCurrencyRate()) + Convert.ToDouble(credit.GetCurrencyRate())).ToString());
                     break;
                 }
                 else if (list[i].GetCurrencyName() == null)
                 {
                     list[i].SetCurrencyRate(credit.GetCurrencyRate());
                     list[i].SetCurrencyName(credit.GetCurrencyName());
                     break;
                 }
             }
             wallet.SetCredit(credit.GetCurrencyName(), credit.GetCurrencyRate());
             wallet.SetCurrencyList(list);
             return wallet;
         }
         */
        public bool CheckIfPayOffCreditPossible(int id)
        {
            Wallet wallet = ReadWalletData(id);
            List<Currency> list = wallet.GetCurrencyList();
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == wallet.GetCredit().GetCurrencyName())
                {
                    if (Convert.ToDouble(list[i].GetCurrencyRate()) >= Convert.ToDouble(wallet.GetCredit().GetCurrencyRate()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool CheckIfPayAmountCredit(int id, string amount)
        {
            Wallet wallet = ReadWalletData(id);
            List<Currency> list = wallet.GetCurrencyList();
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == wallet.GetCredit().GetCurrencyName())
                {
                    if (Convert.ToDouble(list[i].GetCurrencyRate()) >= Convert.ToDouble(amount))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public void PayOffWholeCredit(int id)
        {
            double temp;
            Wallet wallet = ReadWalletData(id);
            string creditCurrencyName = wallet.GetCredit().GetCurrencyName();
            string creditCurrencyRate = wallet.GetCredit().GetCurrencyRate();
            int type = 5;
            for (int i = 0; i < 5; i++)
            {
                if (wallet.GetCredit().GetCurrencyName() == wallet.GetCurrencyList()[i].GetCurrencyName())
                {
                    temp = Convert.ToDouble(wallet.GetCurrencyList()[i].GetCurrencyRate());
                    temp = temp - Convert.ToDouble(wallet.GetCredit().GetCurrencyRate());
                    if (temp == 0.00)
                    {
                        wallet.GetCredit().SetCurrencyName(null);
                    }
                    wallet.GetCurrencyList()[i].SetCurrencyRate(temp.ToString());
                    break;
                }
            }
            wallet.SetCredit(null, null);
            UpdateWalletData(wallet);
            SaveHistory(
                           id,
                           0,
                           type,
                           Convert.ToDouble(creditCurrencyRate),
                           0,
                           creditCurrencyName,
                           null,
                           today,
                           null,
                           null
                           );
        }

        public bool PayPartOfCredit(int id, string amount)
        {

            double temp;
            Wallet wallet = ReadWalletData(id);
            double credit = Convert.ToDouble(wallet.GetCredit().GetCurrencyRate());
            if (Convert.ToDouble(amount) < credit)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (wallet.GetCredit().GetCurrencyName() == wallet.GetCurrencyList()[i].GetCurrencyName())
                    {
                        temp = Convert.ToDouble(wallet.GetCurrencyList()[i].GetCurrencyRate());
                        temp = temp - Convert.ToDouble(amount);
                        if (temp == 0.00)
                        {
                            wallet.GetCurrencyList()[i].SetCurrencyName(null);
                        }
                        wallet.GetCurrencyList()[i].SetCurrencyRate(temp.ToString());
                        break;
                    }

                }
                string name = wallet.GetCredit().GetCurrencyName();
                string rate = wallet.GetCredit().GetCurrencyRate();
                string result = (Convert.ToDouble(rate) - Convert.ToDouble(amount)).ToString();
                wallet.SetCredit(name, result);
                ChangeIntoNull(ref wallet);
                UpdateWalletData(wallet);
                int type = 4;
                SaveHistory(
                           id,
                           0,
                           type,
                           Convert.ToDouble(rate),
                           Convert.ToDouble(result),
                           name,
                           null,
                           today,
                           null,
                           null
                           );
                return false;
            }
            else
            {
                PayOffWholeCredit(id);
                return true;
            }
        }

        public Currency ReadCreditData(int ID)
        /* Reads wallet info from database */
        {
            Currency credit = new Currency();
            string creditName;
            string creditAmount;

            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select CreditType, CreditAmount from UserWallet where UserId =" + ID, con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            if (myReader.Read())
            {
                creditName = myReader["CreditType"].ToString();
                creditAmount = myReader["CreditAmount"].ToString();
                credit.SetCurrencyName(creditName);
                credit.SetCurrencyRate(creditAmount);
            }

            return credit;
        }



        public bool CheckIfOutsideTransferPossible(int id, Currency currency)
        {
            Wallet wallet = ReadWalletData(id);
            List<Currency> list = wallet.GetCurrencyList();
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == currency.GetCurrencyName())
                {
                    if (Convert.ToDouble(list[i].GetCurrencyRate()) >= Convert.ToDouble(currency.GetCurrencyRate()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public void DoOutsideTransfer(int id, Currency currency)
        {
            Wallet wallet = ReadWalletData(id);
            List<Currency> list = wallet.GetCurrencyList();
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == currency.GetCurrencyName())
                {
                    double temp = Convert.ToDouble(list[i].GetCurrencyRate());
                    temp = temp - Convert.ToDouble(currency.GetCurrencyRate());
                    if (temp == 0)
                    {
                        list[i].SetCurrencyName(null);
                    }
                    list[i].SetCurrencyRate(temp.ToString());
                }
            }
            wallet.SetCurrencyList(list);
            ChangeIntoNull(ref wallet);
            UpdateWalletData(wallet);
        }

        public void DoIntsideTransfer(int id, Currency currency, string Userlogin)
        {
            ///// User1

            Wallet wallet = ReadWalletData(id);
            List<Currency> list = wallet.GetCurrencyList();
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == currency.GetCurrencyName())
                {
                    double temp = Convert.ToDouble(list[i].GetCurrencyRate());
                    temp = temp - Convert.ToDouble(currency.GetCurrencyRate());
                    if (temp == 0)
                    {
                        list[i].SetCurrencyName(null);
                    }
                    list[i].SetCurrencyRate(temp.ToString());
                }
            }
            wallet.SetCurrencyList(list);
            ChangeIntoNull(ref wallet);
            UpdateWalletData(wallet);


            ///////////// User2

            int idUser2 = GetOneUserId(Userlogin);
            Wallet wallet2 = ReadWalletData(idUser2);
            bool added = false;
            list = wallet2.GetCurrencyList();
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == currency.GetCurrencyName())
                {
                    double temp = Convert.ToDouble(list[i].GetCurrencyRate());
                    temp = temp + Convert.ToDouble(currency.GetCurrencyRate());
                    list[i].SetCurrencyRate(temp.ToString());
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (list[i].GetCurrencyName() == null)
                    {
                        list[i].SetCurrencyRate(currency.GetCurrencyRate());
                        list[i].SetCurrencyName(currency.GetCurrencyName());
                        added = true;
                        break;
                    }
                }
            }
            wallet2.SetCurrencyList(list);
            ChangeIntoNull(ref wallet2);
            UpdateWalletData(wallet2);
        }


        public bool CheckIfUser2HasPlaceInWallet(Currency currency, string Userlogin)
        {
            int idUser2 = GetOneUserId(Userlogin);
            Wallet wallet2 = ReadWalletData(idUser2);
            List<Currency> list = wallet2.GetCurrencyList();

            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == currency.GetCurrencyName())
                {
                    return true;
                }
                if (list[i].GetCurrencyName() == null)
                {
                    return true;
                }
            }
            return false;
        }


        public bool CheckIfMoneyIncomeAmountPossible(int id, Currency currency)
        {
            Wallet wallet2 = ReadWalletData(id);
            List<Currency> list = wallet2.GetCurrencyList();

            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == currency.GetCurrencyName())
                {
                    return true;
                }
                if (list[i].GetCurrencyName() == null)
                {
                    return true;
                }
            }
            return false;
        }

        public void DoMoneyIncome(int id, Currency currency)
        {
            Wallet wallet2 = ReadWalletData(id);
            List<Currency> list = wallet2.GetCurrencyList();
            bool added = false;
            for (int i = 0; i < 5; i++)
            {
                if (list[i].GetCurrencyName() == currency.GetCurrencyName())
                {
                    double temp = Convert.ToDouble(list[i].GetCurrencyRate());
                    temp = temp + Convert.ToDouble(currency.GetCurrencyRate());
                    list[i].SetCurrencyRate(temp.ToString());
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (list[i].GetCurrencyName() == null)
                    {
                        list[i].SetCurrencyRate(currency.GetCurrencyRate());
                        list[i].SetCurrencyName(currency.GetCurrencyName());
                        added = true;
                        break;
                    }
                }
            }
            wallet2.SetCurrencyList(list);
            ChangeIntoNull(ref wallet2);
            UpdateWalletData(wallet2);
        }

        public void SaveHistoryToDatabase(HistoryData history)
        {
            {
                SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO UserOperation (UserId, UserIdCooperate, typeName, cash, result, CurrencyFrom, CurrencyTo, dateOperation, descriptionOperation, AccountNo) values (@Userid, @UserCooperate, @TypeName, @Cash, @Result, @FromCurrency, @ToCurrency, @DateOperate, @DescriptionOp, @AccountNo)", con);

               // cmd.Parameters.AddWithValue("@OperationId", history.GetOperationId());
                cmd.Parameters.AddWithValue("@Userid", history.GetUserId());
                cmd.Parameters.AddWithValue("@UserCooperate", (int)history.GetUserCoopId());
                cmd.Parameters.AddWithValue("@TypeName", (int)history.GetTypeName());
                cmd.Parameters.AddWithValue("@Cash", Convert.ToDouble(history.GetCash()));
                cmd.Parameters.AddWithValue("@Result", Convert.ToDouble(history.GetResult()));
                cmd.Parameters.AddWithValue("@FromCurrency", history.GetCurrencyFrom().ToString());
                if(history.GetCurrencyTo() == null)
                {
                    cmd.Parameters.AddWithValue("@ToCurrency", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ToCurrency", history.GetCurrencyTo());
                }
                cmd.Parameters.AddWithValue("@DateOperate", today);
                //cmd.Parameters.AddWithValue("@DateOperate", DBNull.Value);

                if (history.GetDescription() == null)
                {
                    cmd.Parameters.AddWithValue("@DescriptionOp", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DescriptionOp", history.GetDescription());
                }
                if (history.GetAccountNo() == null)
                {
                    cmd.Parameters.AddWithValue("@AccountNo", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccountNo", history.GetAccountNo()); 
                }

                cmd.ExecuteNonQuery();
            }
        }


        public List<HistoryData> ReadHistoryData(int UserId)
        {
            List<HistoryData> historyList = new List<HistoryData>();
            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            SqlCommand myCommand = new SqlCommand("select * from UserOperation where UserID = " + UserId, con);
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                HistoryData operation = new HistoryData();
                operation.SetOperationId((int)myReader["OperationId"]);
                operation.SetUserId((int)myReader["UserId"]);
                operation.SetUserIdCooperate((int)myReader["UserIdCooperate"]);
                operation.SetTypeName((int)myReader["typeName"]);
                operation.SetCash(Convert.ToDouble(myReader["cash"]));
                operation.SetResult(Convert.ToDouble(myReader["result"]));
                operation.SetCurrencyFrom(myReader["CurrencyFrom"].ToString());
                operation.SetCurrencyTo(myReader["CurrencyTo"].ToString());
                operation.SetOperationDate((DateTime)myReader["dateOperation"]);
                operation.SetDescription(myReader["descriptionOperation"].ToString());
                operation.SetAccountNo(myReader["AccountNo"].ToString());

                historyList.Add(operation);
            }
            return historyList;
        }


        public void SaveHistory
            (
            int userId,
            int userIdCoop,
            int type,
            double cash,
            double result,
            string from,
            string to, 
            DateTime date,
            string description,
            string accNo
            )
        {
            HistoryData operation = new HistoryData();
            operation.SetOperationId(0);
            operation.SetUserId(userId);
            operation.SetUserIdCooperate(userIdCoop);
            operation.SetTypeName(type);
            operation.SetCash(cash);
            operation.SetResult(result);
            operation.SetCurrencyFrom(from);
            operation.SetCurrencyTo(to);
            operation.SetOperationDate(date);
            operation.SetDescription(description);
            operation.SetAccountNo(accNo);

            SaveHistoryToDatabase(operation);
        }

    }
}
