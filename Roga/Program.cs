using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Roga
{
    class Program
    {
        public static bool loginState = false;
        [STAThread]
        static void Main(string[] args)
        {
            string curentstate = File.ReadAllText(MainScreen.getFilePath((@"..\..\..\Roga\Assets\Saves\State.txt")));
            if (curentstate == "false")
            {
                LoginScreen loginScreen = new LoginScreen();
                loginScreen.ShowDialog();
            }
            else
            {
                USER_ usernow = null;
                string curentuserID = File.ReadAllText(MainScreen.getFilePath((@"..\..\..\Roga\Assets\Saves\save.txt")));
                using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                {
                    var listUser = from user in data.USER_
                                   select user;
                    foreach (USER_ u in listUser)
                    {
                        if (u.id == Int32.Parse(curentuserID))
                        {
                            usernow = u;
                        }
                    }
                }
                loginState = true;
                HomeScreen home = new HomeScreen(usernow);
                home.ShowDialog();
            }
        }
    }
}
