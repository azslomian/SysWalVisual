using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SysWal
{
    class UserData
    {
        
        private string login;
        private string password;
        private string name;
        private string surname;
        private string PESEL;
        private string accountNo;
        private string email;
        private int id;
        private string salt;

       
       /* UserData(string l, string p, string n, string s, string pesel, string a, string e, int i) //:login(l), password(p), name(n), surname(s), PESEL(pesel), accountNo(a), email(e), id(i)
        {
            login = l;
            password = p;
            name = n;
            surname = s;
            PESEL = pesel;
            accountNo = a;
            email = e;
            id=i; 
        }*/

        public UserData()
        {
            login = null;   
            password = null;   
            name = null;    
            surname = null;     
            PESEL = null;  
            accountNo = null;   
            email = null;
            id = 0;
            salt = null;
        }
        ~UserData() { }

        public void SetUserId(int i)
        {
            id = i;
        }

        public void SetLogin(string l)
        {
            login = l;
        }

        public void SetPassword(string p)
        {
            password = p;
        }

        public void SetName(string n)
        {
            name = n;
        }

        public void SetSurname(string s)
        {
            surname = s;
        }

        public void SetPESEL(string p)
        {
            PESEL = p;
        }

        public void SetAccountNo(string a)
        {
            accountNo = a;
        }

        public void SetEmail(string e)
        {
            email = e;
        }
        public void SetSalt(string s)
        {
            salt = s;
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public int GetUserId()
        {
            return id;
        }

        public string GetLogin()
        {
            return login;
        }

        public string GetPassword()
        {
            return password;
        }

        public string GetName()
        {
            return name;
        }

        public string GetSurname()
        {
            return surname;
        }

        public string GetPESEL()
        {
            return PESEL;
        }

        public string GetAccountNo()
        {
            return accountNo;
        }

        public string GetEmail()
        {
            return email;
        }

        public string GetSalt()
        {
            return salt;
        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        public bool CheckLogin()
        {
            if (login.Length < 1)
            {
                return false;
            }
            else if (login.Length > 30)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }


        public bool CheckPassword()
        {
            if (password.Length < 4 || password.Length > 30)
            {
                return false;
            }
            return true;
        }

        public bool CheckName()
        {
            if (password.Length < 2 || password.Length > 44)
            {
                return false;
            }
            int i = 0;
            if (name.Length == 0)
            {
                return false;
            }
            else
            {
                while (i < name.Length)
                {
                    if ((int)name[i] > 48 && (int)name[i] < 57)
                    {
                        return false;
                    }
                    i++;
                }
            }
            return true;
        }

        public bool CheckSurname()
        {
            if (password.Length < 2 || password.Length > 44)
            {
                return false;
            }
            if (name.Length == 0)
            {
                return false;
            }
            int i = 0;
            while (i < surname.Length)
            {
                if ((int)surname[i] > 48 && (int)surname[i] < 57)
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        public bool CheckPESEL()
        {
            if (PESEL.Length == 11)
            {
                int i = 0;
                while (i < PESEL.Length)
                {
                    if ((int)PESEL[i] < 48 || (int)PESEL[i] > 57)
                    {
                        return false;
                    }
                    i++;
                }
                return true;
            }
            return false;
        }
        public bool CheckAccountNo()
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

     
        public bool CheckEmail()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
            /*   if (password.Length < 2 || password.Length > 255)
               {
                   return false;
               }
               if (email.ToLower().Contains('@'))
               {
                   return true;
               }
               else
               {
                   return false;
               }
               */
        }
    };

    class UserLoginAndPassword
    {
        private string login;
        private string password;
        private int id;
        private string salt;
        public void SetLogin(string l)
        {
            login = l;
        }

        public void SetPassword(string p)
        {
            password = p;
        }
        public void SetId(int i)
        {
            id = i;
        }

        public void SetSalt(string s)
        {
            salt = s;
        }

        public string GetLogin()
        {
            return login;
        }

        public string GetPassword()
        {
            return password;
        }

        public int GetId()
        {
            return id;
        }

        public string GetSalt()
        {
            return salt;
        }

    };

    class UserIdLogin
    {
        string login;
        int id;
        public int GetId()
        {
            return id;
        }

        public string GetLogin()
        {
            return login;
        }

        public void SetLogin(string l)
        {
            login = l;
        }
        public void SetId(int i)
        {
            id = i;
        }
    };

    class UserIdLoginAmount
    {
        string login;
        int id;
        double amount;
        public int GetId()
        {
            return id;
        }

        public string GetLogin()
        {
            return login;
        }

        public double GetAmount()
        {
            return amount;
        }

        public void SetLogin(string l)
        {
            login = l;
        }
        public void SetId(int i)
        {
            id = i;
        }

        public void SetAmount(double a)
        {
            amount = a;
        }
    };


    class UserAskData
    {
        int AskId;
        int UserId;
        double cash;
        string fromCurrency;
        string toCurrency;
        double result;
        int friends;
        int controlNo;
        bool accepted;

        public void SetAskId(int a)
        {
            AskId = a;
        }
        public void SetUserId(int u)
        {
            UserId = u;
        }
        public void Setcash(double c)
        {
            cash = c;
        }
        public void SetFromCurrency(string f)
        {
            fromCurrency = f;
        }
        public void SetToCurrency(string t)
        {
            toCurrency = t;
        }
        public void SetResult(double r)
        {
            result = r;
        }
        public void SetFriends(int f)
        {
            friends = f;
        }
        public void SetControlNo(int c)
        {
            controlNo = c;
        }
        public void SetAccepted(bool a)
        {
            accepted = a;
        }
     /*   public void SetDone(bool d)
        {
            done = d;
        }*/


        public int GetAskId()
        {
            return AskId;
        }
        public int GetUserId()
        {
            return UserId;
        }
        public double Getcash()
        {
            return cash;
        }
        public string GetCurrencyFrom()
        {
            return fromCurrency;
        }
        public string GetCurrencyTo()
        {
            return toCurrency;
        }
        public double Getresult()
        {
            return result;
        }
        public int GetFriends()
        {
            return friends;
        }
        public int GetControlNo()
        {
            return controlNo;
        }
        public bool GetAccepted()
        {
            return accepted;
        }
     /*   public bool GetDone()
        {
            return done;
        }*/
    };
    };
