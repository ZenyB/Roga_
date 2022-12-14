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
        //bool loginState = true;
        ImageList imageList = new ImageList();
        public HomeScreen()
        {
            InitializeComponent();

            //Create Icon
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\logo.ico");
            string sFilePath = Path.GetFullPath(sFile);
            Icon icon = Icon.ExtractAssociatedIcon(sFilePath);
            this.Icon = icon;


            //Insert Logo
            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\logo.png");
            sFilePath = Path.GetFullPath(sFile);
            pictureBox1.Image = Image.FromFile(sFilePath);

            //
            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\testImage.png");
            sFilePath = Path.GetFullPath(sFile);
            Image avtImg = Image.FromFile(sFilePath);
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


            //btnBackGround
            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\btnImport.png");
            sFilePath = Path.GetFullPath(sFile);
            btnImport.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\btnBlank.png");
            sFilePath = Path.GetFullPath(sFile);
            btnBlank.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\pen.png");
            sFilePath = Path.GetFullPath(sFile);
            btnRename.BackgroundImage = Image.FromFile(sFilePath);
            btnRename.BackgroundImage = Image.FromFile(sFilePath);

            //ListViewImage

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\logo.png");
            sFilePath = Path.GetFullPath(sFile);
            imageList.Images.Add(Image.FromFile(sFile));
            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\testImage.png");
            sFilePath = Path.GetFullPath(sFile);
            imageList.Images.Add(Image.FromFile(sFile));
            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\btnBlank.png");
            sFilePath = Path.GetFullPath(sFile);
            imageList.Images.Add(Image.FromFile(sFile));

            listView1.View = View.LargeIcon;
            imageList.ImageSize = new Size(255, 255);
            listView1.LargeImageList = imageList;
            for (int j = 0; j < imageList.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = j;
                listView1.Items.Add(item);
            }

            //noLogin - comming soon
            //if (loginState == false)
            //{
            //    loginLabel.Visible = true;
            //    linkLabelLogin.Visible = true;
            //    listView1.Visible = false;
            //}
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
            //DemoChangeAVT
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PNG, JPG, JPEG Files|*.png; *.jpg; *.jpeg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image imgAvt = Image.FromFile(open.FileName);
                if (imgAvt.Width > imgAvt.Height)
                {
                    imgAvt = resizeImage(imgAvt, new Size(90 * imgAvt.Width / imgAvt.Height, 90));
                }
                else
                {
                    imgAvt = resizeImage(imgAvt, new Size(90, imgAvt.Width * imgAvt.Height / 90));
                }
                ptbAvt.Image = imgAvt;
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
        }

        //Import an image
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PNG, JPG, JPEG Files|*.png; *.jpg; *.jpeg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(open.FileName);
                MainScreen mainForm = new MainScreen(img);
                mainForm.Show();
                this.Close();
            }
        }

        //A blank project
        private void btnBlank_Click(object sender, EventArgs e)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\blank.png");
            string sFilePath = Path.GetFullPath(sFile);
            Image img = Image.FromFile(sFilePath);
            MainScreen mainForm = new MainScreen(img);
            mainForm.Show();
            this.Close();
        }
    }
}
