using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roga
{
    public partial class SignupScreen : Form
    {
        public SignupScreen()
        {
            InitializeComponent();

            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\LoginScreen.jpg");
            string sFilePath = Path.GetFullPath(sFile);
            pbBackground.Image = Image.FromFile(sFilePath);
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Please Enter Your Name")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length == 0)
            {
                txtUsername.Text = "Please Enter Your Name";
                txtUsername.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Enter Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = SystemColors.WindowText;
                txtPassword.UseSystemPasswordChar = true;
            }
        }


        private void txtConfirm_Enter(object sender, EventArgs e)
        {
            if (txtConfirm.Text == "Confirm Password")
            {
                txtConfirm.Text = "";
                txtConfirm.ForeColor = SystemColors.WindowText;
                txtConfirm.UseSystemPasswordChar = true;
            }
        }

        private void lbLogin_Click(object sender, EventArgs e)
        {
            Program.loginState = false;
            this.Hide();
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.ShowDialog();
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length == 0)
            {
                txtPassword.Text = "Enter Password";
                txtPassword.ForeColor = SystemColors.GrayText;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void txtConfirm_Leave(object sender, EventArgs e)
        {
            if (txtConfirm.Text.Length == 0)
            {
                txtConfirm.Text = "Confirm Password";
                txtConfirm.ForeColor = SystemColors.GrayText;
                txtConfirm.UseSystemPasswordChar = false;
            }
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length == 0 || txtUsername.Text == "Please Enter Your Name")
            {
                MessageBox.Show("Invalid username", "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassword.Text == "Enter Password" || txtConfirm.Text == "Confirm Password")
            {
                MessageBox.Show("Invalid password or confirm password", "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must have at least 8 characters", "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show("Confirm password wrong", "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (RogaDatabaseEntities data = new RogaDatabaseEntities())
            {
                var listUser = from user in data.USER_
                               select user;
                foreach (USER_ u in listUser)
                {
                    if (u.username == txtUsername.Text)
                    {
                        MessageBox.Show("Username already exist", "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                LoginScreen.userNow = new USER_ { username = txtUsername.Text, password = txtPassword.Text, fullname = "new user" };
                data.USER_.Add(LoginScreen.userNow);
                data.SaveChanges();
            }
            Program.loginState = true;
            this.Hide();
            HomeScreen home = new HomeScreen(LoginScreen.userNow);
            home.ShowDialog();
        }
    }
}
