using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Roga
{
    public partial class BeforeAddImage : Form
    {
        static private string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        int crpX = 0, crpY = 0, rectW, rectH, slX, slY;
        Pen crpPen = new Pen(Color.White);
        Image imgNow = null;
        Image imgTemp = null;
        private Stack<Image> stackImage = new Stack<Image>();
        private Stack<Image> stackSourceImage = new Stack<Image>();
        private bool buttonAddPressed = false;
        private bool canCrop = false;

        public BeforeAddImage(Image img)
        {
            imgNow = new Bitmap(img);
            InitializeComponent();
            resizePic(imgNow);
            pic.Image = imgTemp;
            stackImage.Push(imgTemp);
            stackSourceImage.Push(imgNow);

            InitFeature();
        }

        public Image getImageToAdd()
        {
            return imgNow;
        }
        static private string getFilePath(string relativePath)
        {
            string sFile = System.IO.Path.Combine(sCurrentDirectory, relativePath);
            string sFilePath = Path.GetFullPath(sFile);
            return sFilePath;
        }
        private void InitFeature()
        {
            rectW = imgNow.Width;
            rectH = imgNow.Height;

            PictureBox picSelect, picCrop, picRotate, picFlip;
            picSelect = new PictureBox();
            picCrop = new PictureBox();
            picRotate = new PictureBox();
            picFlip = new PictureBox();

            //set image
            picSelect.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Crop and rotate\select.png"));
            picCrop.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Crop and rotate\crop.png"));
            picRotate.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Crop and rotate\rotate.png"));
            picFlip.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Crop and rotate\flip.png"));

            picSelect.Cursor = Cursors.Hand;
            picCrop.Cursor = Cursors.Hand;
            picRotate.Cursor = Cursors.Hand;
            picFlip.Cursor = Cursors.Hand;

            picSelect.Size = picCrop.Size = picRotate.Size = picFlip.Size = new Size(75, 75);

            picSelect.SizeMode = picCrop.SizeMode = picRotate.SizeMode = picFlip.SizeMode = PictureBoxSizeMode.StretchImage;

            picSelect.Location = new Point(17, 20);
            panel1.Controls.Add(picSelect);
            picCrop.Location = new Point(115, 20);
            panel1.Controls.Add(picCrop);
            picRotate.Location = new Point(17, 115);
            panel1.Controls.Add(picRotate);
            picFlip.Location = new Point(115, 115);
            panel1.Controls.Add(picFlip);

            Button buttonAdd = new Button();
            buttonAdd.Location = new Point(20, 210);
            buttonAdd.Text = "Add";
            buttonAdd.Cursor = Cursors.Hand;
            buttonAdd.BackColor = Color.DimGray;
            buttonAdd.ForeColor = Color.White;
            buttonAdd.FlatStyle = FlatStyle.Flat;
            buttonAdd.MouseDown += new MouseEventHandler(buttonAdd_MouseDown);
            panel1.Controls.Add(buttonAdd);

            picSelect.Click += new EventHandler(picSelect_Click);
            picCrop.Click += new EventHandler(picCrop_Click);
            picRotate.Click += new EventHandler(picRotate_Click);
            picFlip.Click += new EventHandler(picFlip_Click);
        }

        private void buttonAdd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                buttonAddPressed = true;
                this.Close();
            }    
        }

        //ctrl + z
        private void BeforeAddImage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && (Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                //When user pressed Ctrl + Z, stackImage will be pop and imgNow return to previous state
                //If stackImage has only one image, this is original image, user can't goback
                if (stackImage.Count > 1)
                {
                    pic.Cursor = Cursors.Default;
                    stackImage.Pop();
                    stackSourceImage.Pop();
                    //If image change size -> resize picturebox
                    if (imgTemp.Size != stackImage.Peek().Size)
                        resizePic(stackImage.Peek());
                    imgTemp = stackImage.Peek();
                    imgNow = stackSourceImage.Peek();
                    pic.Image = imgTemp;
                }
                pic.Refresh();
            }
        }

        private void BeforeAddImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!buttonAddPressed)
                imgNow = null;
        }

        private void picSelect_Click(object sender, EventArgs e)
        {
            pic.MouseDown += pic_Select_MouseDown;
            pic.MouseMove += pic_Select_MouseMove;
            pic.MouseEnter += pic_Select_MouseEnter;
        }

        private void pic_Select_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Cross;
        }

        private void pic_Select_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                crpPen.Color = Color.Black;
                crpPen.Width = 2;

                crpX = slX = e.X;
                crpY = slY = e.Y;

                canCrop = true;
            }
        }

        private void pic_Select_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                pic.Refresh();

                crpX = Math.Min(slX, e.X);
                if (crpX < 0)
                    crpX = 0;
                crpY = Math.Min(slY, e.Y);
                if (crpY < 0)
                    crpY = 0;

                rectW = Math.Abs(e.X - slX);
                if (e.X < 0)
                    rectW = Math.Abs(0 - slX);
                if (rectW + crpX >= imgTemp.Width)
                    rectW = imgTemp.Width - slX - 1;
                rectH = Math.Abs(e.Y - slY);
                if (e.Y < 0)
                    rectH = Math.Abs(0 - slY);
                if (rectH + crpY >= imgTemp.Height)
                    rectH = imgTemp.Height - slY - 1;
                Graphics g = pic.CreateGraphics();
                g.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                g.Dispose();
            }
        }


        private Image CropImage(Image sourceImage)
        {
            Cursor = Cursors.Default;
            Bitmap bmp2 = new Bitmap(sourceImage.Width, sourceImage.Height);
            pic.DrawToBitmap(bmp2, pic.ClientRectangle);

            Bitmap crpImage = new Bitmap(rectW, rectH);

            for (int i = 0; i < rectW; i++)
            {
                for (int j = 0; j < rectH; j++)
                {
                    Color pxlclr = bmp2.GetPixel(crpX + i, crpY + j);
                    crpImage.SetPixel(i, j, pxlclr);
                }
            }
            rectH = crpImage.Height;
            rectW = crpImage.Width;
            crpX = slX = 0;
            crpY = slY = 0;
            return (Image)crpImage;
        }

        private Image CropSourceImage(Image sourceImage)
        {
            Cursor = Cursors.Default;
            Bitmap bmp2 = new Bitmap(sourceImage, sourceImage.Width, sourceImage.Height);

            int tempRectW = (int)(rectW * ((float)imgNow.Width / imgTemp.Width));
            int tempRectH = (int)(rectH * ((float)imgNow.Width / imgTemp.Width));

            Bitmap crpImage = new Bitmap(tempRectW, tempRectH);

            for (int i = 0; i < tempRectW; i++)
            {
                for (int j = 0; j < tempRectH; j++)
                {
                    Color pxlclr = bmp2.GetPixel((int)(crpX * ((float)imgNow.Width / imgTemp.Width)) + i, (int)(crpY * ((float)imgNow.Width / imgTemp.Width)) + j);
                    crpImage.SetPixel(i, j, pxlclr);
                }
            }
            return (Image)crpImage;
        }

        private void picCrop_Click(object sender, EventArgs e)
        {
            if (canCrop)
            {
                imgNow = CropSourceImage(imgNow);
                imgTemp = CropImage(imgTemp);
                resizePic(imgTemp);
                pic.Image = imgTemp;
                stackImage.Push(imgTemp);
                stackSourceImage.Push(imgNow);
            }
            canCrop = false;
        }

        //rorate
        private Image Rotate_Image(Image imageSource)
        {
            Image img = new Bitmap(imageSource);
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return img;
        }

        private void picRotate_Click(object sender, EventArgs e)
        {
            imgNow = Rotate_Image(imgNow);
            imgTemp = Rotate_Image(imgTemp);
            resizePic(imgTemp);
            rectW = imgTemp.Width;
            rectH = imgTemp.Height;
            crpX = crpY = slX = slY = 0;
            pic.Image = imgTemp;
            stackImage.Push(imgTemp);
            stackSourceImage.Push(imgNow);
            canCrop = false;
        }

        //flip
        private Bitmap Flip_Image(Image sourceImage)
        {
            Bitmap flip = new Bitmap(sourceImage);
            flip.RotateFlip(RotateFlipType.Rotate180FlipY);

            return flip;
        }

        private void picFlip_Click(object sender, EventArgs e)
        {
            imgNow = Flip_Image(imgNow);
            imgTemp = Flip_Image(imgTemp);
            rectW = imgTemp.Width;
            rectH = imgTemp.Height;
            crpX = crpY = slX = slY = 0;
            pic.Image = imgTemp;
            stackImage.Push(imgTemp);
            stackSourceImage.Push(imgNow);
            canCrop = false;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }

        static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void resizePic(Image img)
        {
            //Size picSize = pic.Size;
            pic.Size = new Size(Math.Min(img.Width, this.Width - panel1.Width - 50), Math.Min(img.Height, panel1.Height));
            if (img.Width <= this.Width - panel1.Width - 50 && img.Height <= panel1.Height)
            {
                pic.Size = img.Size;
                imgTemp = resizeImage(img, pic.Size);
                pic.Image = imgTemp;
            }    
            else
            {
                int newHeight, newWidth;
                if (img.Width > img.Height)
                {
                    newWidth = this.Width - panel1.Width - 50;
                    newHeight = (int)(img.Height * ((float)newWidth / img.Width));
                }
                else
                {
                    newHeight = panel1.Height;
                    newWidth = (int)(img.Width * ((float)newHeight / img.Height));
                }
                pic.Size = new Size(newWidth, newHeight);
                imgTemp = resizeImage(img, pic.Size);
                pic.Image = imgTemp;
                //imgNow = pic.Image;
            }    
            pic.Refresh();
        }
    }
}
