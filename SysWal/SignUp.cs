using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;



namespace SysWal
{
   
    public partial class SignUp : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private UserData user;
        private string CaptchaText;
       

        public SignUp()
        {
            InitializeComponent();
            LoadCaptcha();
        }

        public void LoadCaptcha()
        {
            Captcha c = new Captcha();
            // Random r1 = new Random();
            // int number = r1.Next(100, 10000);
            CaptchaText = c.RandomString(10);
            var image = new Bitmap(CaptchaBox.Width, CaptchaBox.Height);
            var font = new Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(CaptchaText, font, Brushes.Green, new Point(0, 0));
            CaptchaBox.Image = image;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadCaptcha();
        }

        private bool CheckCaptcha()
        {
            if(CaptchaTextBox.Text == CaptchaText)
            {
                return true;
            }
            return false;
        }

        /*  public void ConnectDB()
          {
              con = new SqlConnection(
                  @"Data Source=(local)\SQLEXPRESS;Initial Catalog=SysWal;Integrated Security=True");
              con.Open();
              cmd = new SqlCommand("Select * FROM UserData");
              cmd.Connection = con;

              da = new SqlDataAdapter(cmd);
              dt = new DataTable();
              da.Fill(dt);

          }*/
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

        public bool CheckData()
        {    
                user = new UserData();
                user.SetLogin(loginText.Text);
                user.SetPassword(passwordText.Text);
                user.SetName(nameText.Text);
                user.SetSurname(surnameText.Text);
                user.SetPESEL(PESELText.Text);
                user.SetAccountNo(accountNoText.Text);
                user.SetEmail(emailText.Text);

                if (user.CheckLogin() == false) { loginText.BackColor = Color.Red; } else { loginText.BackColor = Color.White; }
                if (user.CheckPassword() == false) { passwordText.BackColor = Color.Red; } else { passwordText.BackColor = Color.White; }
                if (user.CheckName() == false) { nameText.BackColor = Color.Red; } else { nameText.BackColor = Color.White; }
                if (user.CheckSurname() == false) { surnameText.BackColor = Color.Red; } else { surnameText.BackColor = Color.White; }
                if (user.CheckPESEL() == false) { PESELText.BackColor = Color.Red; } else { PESELText.BackColor = Color.White; }
                if (user.CheckAccountNo() == false) { accountNoText.BackColor = Color.Red; } else { accountNoText.BackColor = Color.White; }
                if (user.CheckEmail() == false) { emailText.BackColor = Color.Red; } else { emailText.BackColor = Color.White; }

            if(isEmpty(loginText) || isEmpty(passwordText) || isEmpty(nameText) || isEmpty(surnameText) || isEmpty(PESELText) || isEmpty(accountNoText) || isEmpty(emailText) || isEmpty(CaptchaTextBox))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
            
            

        private UserData CreateUser(ref bool check, string salt)
        {
            
            // UserData user(loginText.Text, passwordText.Text, nameText.Text, surnameText.Text, PESELText.Text, accountNoText.Text, emailText.Text, 0)
            user = new UserData();
            HashPassword hsp = new HashPassword();
            bool check2 = true;
          
                user.SetLogin(loginText.Text);
                user.SetPassword(passwordText.Text);
                user.SetName(nameText.Text);
                user.SetSurname(surnameText.Text);
                user.SetPESEL(PESELText.Text);
                user.SetAccountNo(accountNoText.Text);
                user.SetEmail(emailText.Text);

            if (user.CheckLogin() == false) { loginText.BackColor = Color.Red; check2 = false; } else { loginText.BackColor = Color.White; }
            if (user.CheckPassword() == false){ passwordText.BackColor = Color.Red; check2 = false; } else { passwordText.BackColor = Color.White; }
            if (user.CheckName() == false) { nameText.BackColor = Color.Red; check2 = false; } else { nameText.BackColor = Color.White; }
            if (user.CheckSurname() == false) { surnameText.BackColor = Color.Red; check2 = false; } else { surnameText.BackColor = Color.White; }
            if (user.CheckPESEL() == false) { PESELText.BackColor = Color.Red; check2 = false; } else { PESELText.BackColor = Color.White; }
            if (user.CheckAccountNo() == false) { accountNoText.BackColor = Color.Red; check2 = false; } else { accountNoText.BackColor = Color.White; }
            if (user.CheckEmail() == false) { emailText.BackColor = Color.Red; check2 = false; } else { emailText.BackColor = Color.White; }
            if (check2 == true)
            {
                user.SetSalt(salt);
                check = true;
            }
            return user;

        }

    /*   private int GetUserId()
        {
            UserData user = new UserData();

            SqlConnection con = new SqlConnection(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = SysWal; Integrated Security = True");
            // string command = "select * from UserData where UserId =" + id;
            string command = "SELECT @@IDENTITY";
            SqlCommand myCommand = new SqlCommand(command, con);
           
            con.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();
            

            return id;
        }
        */

       
        private void AddNewUser(ref bool check)
        {
            HashPassword hsp = new HashPassword();
            ReadAndUpdateData r = new ReadAndUpdateData();
            UserData userData = new UserData();
            string salt = hsp.CreateSalt(10);
            user = CreateUser(ref check, salt);
            if (check == true)
            {
                con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=SysWal;Integrated Security=True");
                con.Open();

                cmd = new SqlCommand("INSERT INTO UserData (login, password, name, surname, PESEL, accountNo, email, salt) values (@login, @password, @name, @surname, @PESEL, @accountNo, @email, @salt)", con);
                cmd.Parameters.AddWithValue("@login", user.GetLogin());
                cmd.Parameters.AddWithValue("@password", hsp.HashUserPassword(user.GetPassword(), salt));
                cmd.Parameters.AddWithValue("@name", user.GetName());
                cmd.Parameters.AddWithValue("@surname", user.GetSurname());
                cmd.Parameters.AddWithValue("@PESEL", user.GetPESEL());
                cmd.Parameters.AddWithValue("@accountNo", user.GetAccountNo());
                cmd.Parameters.AddWithValue("@email", user.GetEmail());
                cmd.Parameters.AddWithValue("@salt", user.GetSalt());

                cmd.ExecuteNonQuery();

                string command = "SELECT MAX(UserID)FROM UserData";
                SqlCommand myCommand = new SqlCommand(command, con);
                int id = (int)myCommand.ExecuteScalar();


                SendMail sm = new SendMail();
                MessageBox.Show(sm.MailSend(user.GetEmail(), user.GetName()));
                r.CreateWallet(id);
                MessageBox.Show("Rejestracja zakończona pomyslnie!");
            }
            else
            {
                MessageBox.Show("Niepoprawne Dane!");
                
            }
        }


       /* private void SignUpButton_Click(object sender, EventArgs e)
        {
            bool check = false;
            
            if (!CheckRules.Checked)
            {
                MessageBox.Show("Aby kontynuować, zaakceptuj Warunki Rejestracji!");
            }
            else
            {
                if (check == true)
                {
                    AddNewUser(ref check);
                    Hide();
                    Start start = new Start();
                    start.ShowDialog();
                    Close();
                    start = null;
                }
            }
        }
        */

        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
            Start start = new Start();
            start.ShowDialog();
            Close();
            start = null;
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (CheckData())
            {
                if (!CheckRules.Checked)
                {
                    MessageBox.Show("Aby kontynuować, zaakceptuj Warunki Rejestracji!");
                }
                else
                {
                    if (CheckCaptcha())
                    {
                        AddNewUser(ref check);
                        if (check == true)
                        {
                            Hide();
                            Start start = new Start();
                            start.ShowDialog();
                            Close();
                            start = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Niepoprawne Captcha!");
                        LoadCaptcha();
                    }
                }
            }
            else
            {
                MessageBox.Show("Wypełnij wszystkie pola!");
            }
        }

        
    }
}
