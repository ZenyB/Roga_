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
    public partial class LoginScreen : Form
    {
        RogaDatabaseEntities RogaEntities = new RogaDatabaseEntities();
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

        public void LoadData()
        {
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
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
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
    }
}
