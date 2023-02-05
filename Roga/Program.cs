using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roga
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            LoginScreen loginScreen = new LoginScreen();
            HomeScreen home = new HomeScreen();
            //home.ShowDialog();
            loginScreen.ShowDialog();
        }
    }
}
