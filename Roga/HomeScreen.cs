using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roga
{
    public partial class HomeScreen : Form
    {
        ImageList imageList = new ImageList();
        List<IMAGE_> imgRealList = new List<IMAGE_>();
        USER_ userNow;
        static private string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory; //get current directory
        public HomeScreen(USER_ user)
        {
            InitializeComponent();

            //
            userNow = user;

            //Insert Logo
            pictureBox1.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\logo.png"));

            //Name
            labelName.Text = "Hello, " + userNow.fullname;

            //Avt
            if (!Program.loginState || (Program.loginState && (userNow != null) && userNow.avatar == null))
            {
                Image avtImg = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\testImage.png"));
                //resize follow W or H
                if (avtImg.Width > avtImg.Height)
                {
                    avtImg = resizeImage(avtImg, new Size(90 * avtImg.Width / avtImg.Height, 90));
                }
                else
                {
                    avtImg = resizeImage(avtImg, new Size(90, avtImg.Width * avtImg.Height / 90));
                }
                ptbAvt.Image = avtImg;
            }
            else 
            {
                ptbAvt.Image = MainScreen.ConvertBinaryToImg(userNow.avatar);
            }


            //btnBackGround
            btnImport.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\btnImport.png"));
            btnBlank.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\btnBlank.png"));
            btnRename.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\pen.png"));


            //Storage
            if (Program.loginState)
            {
                //ListViewImage
                using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                {
                    var listImage_ = from image in data.IMAGE_
                                     where image.userid == userNow.id
                                     select image;
                    foreach (var img in listImage_)
                    {
                        IMAGE_ image = (IMAGE_)img;
                        imgRealList.Add(image);
                        imageList.Images.Add(handleBeforeListImage(MainScreen.ConvertBinaryToImg(image.img)));
                    }
                }

                listView1.View = View.LargeIcon;
                imageList.ImageSize = new Size(255, 255);
                listView1.LargeImageList = imageList;
                for (int j = 0; j < imageList.Images.Count; j++)
                {
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = j;
                    listView1.Items.Add(item);
                }
            }
            else
            {
                loginLabel.Visible = true;
                linkLabelLogin.Visible = true;
                listView1.Visible = false;
            }
        }

        static private string getFilePath(string relativePath)
        {
            string sFile = System.IO.Path.Combine(sCurrentDirectory, relativePath);
            string sFilePath = Path.GetFullPath(sFile);
            return sFilePath;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        //Create Gradient Background
        private void HomeScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush linear = new LinearGradientBrush(r, Color.FromArgb(217, 165, 179), Color.FromArgb(24, 104, 174), LinearGradientMode.Horizontal);
            g.FillRectangle(linear, r);
        }

        //Smooth Gradient Background
        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private Image handleBeforeListImage(Image img)
        {
            Image result = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\BackgoundForListImage.png"));
            Image temp = new Bitmap(255, 255);
            if (img.Width > img.Height)
            {
                temp = resizeImage(img, new Size(255, (int)(img.Height * (255 / (float)img.Width))));
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.DrawImage(temp, new PointF(0, 127.5f - (float)temp.Height / 2));
                }
            }    
            else
            {
                temp = resizeImage(img, new Size((int)(img.Width * (255 / (float)img.Height)), 255));
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.DrawImage(temp, new PointF(127.5f - (float)temp.Width/ 2, 0));
                }
            }      
            return result;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Demo Selected Image
            int lcount = 0;
            if (listView1.SelectedItems.Count > 0)
            {
                for (lcount = 0; lcount <= listView1.Items.Count - 1; lcount++)
                {
                    if (listView1.Items[lcount].Selected == true)
                    {
                        Image img = new Bitmap(MainScreen.ConvertBinaryToImg(imgRealList[lcount].img));
                        MainScreen mainForm = new MainScreen(img, "");
                        this.Hide();
                        mainForm.ShowDialog();
                        break;
                    }
                }
            }
            Image avtImg = (Image)imageList.Images[lcount];
            //{
            //    avtImg = resizeImage(avtImg, new Size(90, 90));
            //}
            //ptbAvt.Image = avtImg;
        }

        private void linkLabelLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //close Home, show Login
            LoginScreen login = new LoginScreen();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void labelName_TextChanged(object sender, EventArgs e)
        {
            //set position for labelName (x, 205)
            Label l = (Label)sender;
            l.Location = new Point(0, 167);
            if (l.Text.Length >= 27)
            {
                l.Location = new Point(-10, 167);
            }
            else
            {
                l.Location = new Point(((panel2.Width - btnRename.Width - labelName.Width) / 2), 167);
            }

            if (l.Text.Length > 27)
            {
                string temp = l.Text;
                temp = temp.Remove(24, l.Text.Length - 25 + 1);
                for (int i = 26; i < 29; i++)
                    temp = temp + ".";
                l.Text = temp;
            }
        }

        //ChangeAVT
        private void ptbAvt_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(BMP, PNG, JPG, JPEG Files)|*.bmp;*.png; *.jpg; *.jpeg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image imgAvt = Image.FromFile(open.FileName);
                if (imgAvt.Width > imgAvt.Height)
                {
                    imgAvt = resizeImage(imgAvt, new Size((int)(90 * (float)imgAvt.Width / imgAvt.Height), 90));
                }
                else
                {
                    imgAvt = resizeImage(imgAvt, new Size(90, (int)((float)imgAvt.Width * imgAvt.Height / 90)));
                }
                ptbAvt.Image = imgAvt;
                if (Program.loginState)
                {
                    userNow.avatar = MainScreen.ConvertImgToBinary(imgAvt);
                    using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                    {
                        var user = data.USER_.FirstOrDefault(u => u.id == userNow.id);
                        if (user != null)
                        {
                            user.avatar = userNow.avatar;
                            data.SaveChanges();
                        }
                    }
                }
            }
        }

        //Rename
        private void btnRename_Click(object sender, EventArgs e)
        {
            Rename renameForm = new Rename(this);
            renameForm.ShowDialog();
        }

        public void Rename(string name)
        {
            labelName.Text = "Hello, " + name;
            if (Program.loginState)
            {
                userNow.fullname = name;
                using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                {
                    var user = data.USER_.FirstOrDefault(u => u.id == userNow.id);
                    if (user != null)
                    {
                        user.fullname = name;
                        data.SaveChanges();
                    }
                }
            }
        }

        //Import an image
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(BMP, PNG, JPG, JPEG Files)|*.bmp;*.png; *.jpg; *.jpeg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image temp = Image.FromFile(open.FileName);
                Image img = new Bitmap(temp);
                temp.Dispose();
                MainScreen mainForm = new MainScreen(img, open.FileName);
                this.Hide();
                mainForm.ShowDialog();
            }
        }

        //A blank project
        private void btnBlank_Click(object sender, EventArgs e)
        {
            MainScreen mainForm = new MainScreen();
            this.Hide();
            mainForm.ShowDialog();
        }

        private void HomeScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
