﻿using System;
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
    public partial class LoginScreen : Form
    {
        public static USER_ userNow = null;
        public LoginScreen()
        {
            InitializeComponent();

            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\LoginScreen.jpg");
            string sFilePath = Path.GetFullPath(sFile);
            pbBackground.Image = Image.FromFile(sFilePath);

            sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\logo.png");
            sFilePath = Path.GetFullPath(sFile);
            pbLogo.Image = Image.FromFile(sFilePath);
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

        private void lbSignup_Click(object sender, EventArgs e)
        {
            SignupScreen signupScreen = new SignupScreen();
            this.Hide();
            signupScreen.ShowDialog();
        }

        private void vbButton1_Click(object sender, EventArgs e)
        {
            USER_ temp = new USER_ { username = "", password = "", fullname = "new user" };
            this.Hide();
            HomeScreen homeScreen = new HomeScreen(temp);
            homeScreen.ShowDialog();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Please Enter Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = SystemColors.WindowText;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length == 0)
            {
                txtPassword.Text = "Please Enter Password";
                txtPassword.ForeColor = SystemColors.GrayText;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Please Enter Your Name" || txtPassword.Text == "Please Enter Password")
            {
                MessageBox.Show("Invalid username or password", "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (RogaDatabaseEntities data = new RogaDatabaseEntities())
            {
                var listUser = from user in data.USER_
                               select user;
                bool flag = false;
                foreach (USER_ u in listUser)
                {
                    if (u.username == txtUsername.Text)
                    {
                        flag = true;
                        if (u.password != txtPassword.Text)
                        {
                            MessageBox.Show("Wrong password", "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            userNow = u;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    MessageBox.Show("Username does not exist", "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            string str = "true";
            File.WriteAllText(MainScreen.getFilePath((@"..\..\..\Roga\Assets\Saves\State.txt")), str);
            str = userNow.id.ToString();
            File.WriteAllText(MainScreen.getFilePath((@"..\..\..\Roga\Assets\Saves\save.txt")), str);
            Program.loginState = true;
            this.Hide();
            HomeScreen home = new HomeScreen(userNow);
            home.ShowDialog();
        }

        private void MyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // we don't accept whitespace characters
            if (char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

    }
}
