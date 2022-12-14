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
<<<<<<< HEAD
            Application.Run(new HomeScreen());
=======
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.ShowDialog();
>>>>>>> 0bbabf045849fdd8fb27513ddab173617a4901ea
        }
    }
}
