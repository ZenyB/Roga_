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
    }
}
