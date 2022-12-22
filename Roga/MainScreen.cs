﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Roga
{
    public partial class MainScreen : Form
    {
        Point lastPoint = Point.Empty;// null for a Point object
        static private string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory; //get current directory
        bool isMouseDown = new Boolean();//this is used to evaluate whether our mousebutton is down or not
        string LastMouseType = string.Empty;//hold the lastest mouse type to remove the button events
        private string _mouseType;//hold the mouse type to add the button event
        private Pen pen = new Pen(Color.Black, 3);
        private Image imgNow; //The image is being processed
        private Stack<Image> stackImage = new Stack<Image>(); //stack images has been processed
        public string MouseType
        {
            get { return _mouseType; }
            set
            {
                RemoveRightPanelDetails();
                if (canMove != false)
                    CanMove = false;
                _mouseType = value;
                switch (LastMouseType)
                {
                    case "pen":
                        Remove_Draw();
                        break;
                    case "brightness&contrast":
                        //remove brightness
                        break;
                    case "eraser":
                        //remove eraser
                        break;
                    case "RGB":
                        //remove Color Channel
                        break;
                    case "exposure":
                        //remove 
                        break;
                    case "AddPicture":
                        //remove
                        break;
                    case "AddText":
                        //remove
                        break;
                    case "crop":
                        Remove_Select();
                        break;
                    case "shape":
                        removeShapesEvent();
                        break;
                    case "filter":
                        break;
                }
                switch (value)
                {
                    case "pen":
                        Add_Draw();
                        break;
                    case "brightness&contrast":
                        //add brightness
                        break;
                    case "eraser":
                        //add eraser
                        break;
                    case "RGB":
                        //add Color Channel
                        break;
                    case "exposure":
                        //add 
                        break;
                    case "AddPicture":
                        //add
                        break;
                    case "AddText":
                        //add
                        break;
                    case "crop":
                        InitCropDetails();
                        break;
                    case "shape":
                        InitPenLineColorDetails();
                        InitShapesDetails();
                        addShapesEvent();
                        break;
                    case "filter":
                        InitFilterDetails();
                        break;
                    default:
                        break;
                }
            }
        }

        #region constructor

        public MainScreen()
        {
            InitializeComponent();
            InitBackgroundImageForButton();

            this.MouseWheel += new MouseEventHandler(Form4_MouseWheel);
            this.MinimumSize = new System.Drawing.Size(1000, 800);
            MouseType = "";

            pic = new PictureBox();

            pic.Anchor = 0;
            pic.Location = new Point(400, 260);
            pic.Name = "pictureBox1";
            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.SendToBack();

            //pic.MouseWheel += Pic_MouseWheel;

            int newHeight, newWidth;
            newWidth = this.picturePanel2.Width;
            newHeight = this.picturePanel2.Height;
            pic.Size = new Size(newWidth, newHeight);
            pic.Location = new Point((Width / 2) - (newWidth / 2), (Height / 2) - (newHeight / 2));
            Bitmap picture = new Bitmap(newWidth, newHeight);
            using (Graphics graph = Graphics.FromImage(picture))
            {
                Rectangle ImageSize = new Rectangle(0, 0, newWidth, newHeight);
                graph.FillRectangle(Brushes.White, ImageSize);
            }
            pic.Image = picture;

            imgNow = picture;
            stackImage.Push(imgNow);

            Controls.Add(pic);
            pic.BringToFront();

        }//create a blank project
        public MainScreen(string selectedPath)
        {
            InitializeComponent();
            InitBackgroundImageForButton();

            this.MouseWheel += new MouseEventHandler(Form4_MouseWheel);
            this.MinimumSize = new System.Drawing.Size(1000, 800);
            MouseType = "";

            pic = new PictureBox();

            pic.Anchor = 0;
            pic.Location = new Point(400, 260);
            pic.Name = "pictureBox1";
            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.SendToBack();

            //pic.MouseWheel += Pic_MouseWheel;

            if (Image.FromFile(selectedPath).Width > Width || Image.FromFile(selectedPath).Height > Height)
            {
                int newHeight, newWidth;
                if (Image.FromFile(selectedPath).Width > Image.FromFile(selectedPath).Height)
                {
                    newWidth = Width - 225 * 2;
                    newHeight = (int)(Image.FromFile(selectedPath).Height * ((float)newWidth / Image.FromFile(selectedPath).Width));
                }
                else
                {
                    newHeight = Height;
                    newWidth = (int)(Image.FromFile(selectedPath).Width * ((float)newHeight / Image.FromFile(selectedPath).Height));
                }
                Console.WriteLine(newWidth);
                Console.WriteLine(newHeight);
                pic.Size = new Size(newWidth, newHeight);
                pic.Location = new Point((Width / 2) - (newWidth / 2) - 7, (Height / 2) - (newHeight / 2));
                Image temp = Image.FromFile(selectedPath);
                temp = resizeImage(temp, pic.Size);
                pic.Image = temp;
                imgNow = pic.Image;
                stackImage.Push(imgNow);
                Console.WriteLine(pic.Size.ToString());
            }
            else
            {
                int newHeight, newWidth;
                newWidth = Image.FromFile(selectedPath).Width;
                newHeight = Image.FromFile(selectedPath).Height;
                pic.Size = new Size(newWidth, newHeight);
                pic.Location = new Point((Width / 2) - (newWidth / 2), (Height / 2) - (newHeight / 2));
                pic.Image = Image.FromFile(selectedPath);
                imgNow = pic.Image;
                stackImage.Push(imgNow);
            }
            Controls.Add(pic);
            pic.BringToFront();

        }//create a project with image path
        public MainScreen(Image picture)
        {
            InitializeComponent();
            InitBackgroundImageForButton();

            this.MouseWheel += new MouseEventHandler(Form4_MouseWheel);
            this.MinimumSize = new System.Drawing.Size(1000, 800);
            MouseType = "";

            pic = new PictureBox();

            pic.Anchor = 0;
            pic.Location = new Point(400, 260);
            pic.Name = "pictureBox1";
            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.SendToBack();

            //pic.MouseWheel += Pic_MouseWheel;

            if (picture.Width > Width || picture.Height > Height)
            {
                int newHeight, newWidth;
                if (picture.Width > picture.Height)
                {
                    newWidth = Width - 225 * 2;
                    newHeight = (int)(picture.Height * ((float)newWidth / picture.Width));
                }
                else
                {
                    newHeight = Height;
                    newWidth = (int)(picture.Width * ((float)newHeight / picture.Height));
                }
                Console.WriteLine(newWidth);
                Console.WriteLine(newHeight);
                pic.Size = new Size(newWidth, newHeight);
                pic.Location = new Point((Width / 2) - (newWidth / 2) - 7, (Height / 2) - (newHeight / 2));
                Image temp = picture;
                temp = resizeImage(temp, pic.Size);
                pic.Image = temp;
                imgNow = pic.Image;
                stackImage.Push(imgNow);
                Console.WriteLine(pic.Size.ToString());
            }
            else
            {
                int newHeight, newWidth;
                newWidth = picture.Width;
                newHeight = picture.Height;
                pic.Size = new Size(newWidth, newHeight);
                pic.Location = new Point((Width / 2) - (newWidth / 2), (Height / 2) - (newHeight / 2));
                pic.Image = picture;
                imgNow = pic.Image;
                stackImage.Push(imgNow);
            }
            Controls.Add(pic);
            pic.BringToFront();

        }//creat a project with selected image

        //method to set background image for button in constructor
        private void InitBackgroundImageForButton()
        {
            Pen_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\pencil.jpg"));
            Back_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\back.png"));
            Hand_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\hand.png"));
            Save_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\save.png"));
            Eraser_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\eraser.png"));
            BrightnessAndContrast_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\brightness.png"));
            ColorChannel_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\color.png"));
            Exposure_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\exposure.png"));
            AddPicture_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\addpicture.png"));
            AddText_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\text.png"));
            Crop_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\crop.png"));
            Shape_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\shape.png"));
            Filter_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\filter.png"));
        }
        #endregion

        //method to resize image
        static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        //method to get filePath
        static private string getFilePath(string relativePath)
        {
            string sFile = System.IO.Path.Combine(sCurrentDirectory, relativePath);
            string sFilePath = Path.GetFullPath(sFile);
            return sFilePath;
        }

        //pencil event
        //
        private void Pic_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            lastPoint = Point.Empty;
        }
        private void Pic_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)//check to see if the mouse button is down

            {
                if (lastPoint != null)//if our last point is not null, which in this case we have assigned above

                {

                    if (pic.Image == null)//if no available bitmap exists on the picturebox to draw on

                    {
                        //create a new bitmap
                        Bitmap bmp = new Bitmap(pic.Width, pic.Height);

                        pic.Image = bmp; //assign the picturebox.Image property to the bitmap created

                    }

                    using (Graphics g = Graphics.FromImage(pic.Image))

                    {//we need to create a Graphics object to draw on the picture box, its our main tool

                        //when making a Pen object, you can just give it color only or give it color and pen size

                        g.DrawLine(new Pen(Color.Black, 2), lastPoint, e.Location);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        //this is to give the drawing a more smoother, less sharper look

                    }

                    pic.Invalidate();//refreshes the picturebox

                    lastPoint = e.Location;//keep assigning the lastPoint to the current mouse position

                }

            }

        }
        private void Pic_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;//current mouse position: e.Location
            isMouseDown = true;//mouse button is down (clicked)
        }
        private void Add_Draw()
        {
            pic.MouseDown += Pic_Draw_MouseDown;
            pic.MouseMove += Pic_Draw_MouseMove;
            pic.MouseUp += Pic_Draw_MouseUp;
        }
        private void Remove_Draw()
        {
            pic.MouseDown -= Pic_Draw_MouseDown;
            pic.MouseMove -= Pic_Draw_MouseMove;
            pic.MouseUp -= Pic_Draw_MouseUp;
        }
        //

        //2 cai nay hok biet viet vo chi nua cu de ik
        //
        private void MainScreen_Load(object sender, EventArgs e)
        {
        }
        void Form4_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        //Event ctrl + Z
        private void MainScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && (Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                //When user pressed Ctrl + Z, stackImage will be pop and imgNow return to previous state
                //If stackImage has only one image, this is original image, user can't goback
                if (stackImage.Count > 1)
                {
                    stackImage.Pop();
                    //If image change size -> resize picturebox
                    if (imgNow.Size != stackImage.Peek().Size)
                        resizePic(stackImage.Peek());
                    imgNow = stackImage.Peek();
                    pic.Image = imgNow;
                }
                if (canMove != false)
                {
                    canMove = false;
                    rectNow = new Rectangle(0, 0, 0, 0);
                }
                pic.Refresh();
            }
        }

        //Remove all control on right panel
        private void RemoveRightPanelDetails()
        {
            panel3.Controls.Clear();
        }

        //Init Right Panel With Width and Color of pen
        //use method InitPenLineColorDetails() to generate details on right panel
        #region SetWidthAndColorForPen
        private void InitPenLineColorDetails()
        {
            //Panel penLine
            Panel p = new Panel();
            p.Size = new Size(150, 60);
            p.BackColor = Color.FromArgb(80, 80, 80);
            p.Location = new Point(26, 20);
            //3 Line
            CustomButton.VBButton btnLine1 = new CustomButton.VBButton();
            CustomButton.VBButton btnLine2 = new CustomButton.VBButton();
            CustomButton.VBButton btnLine3 = new CustomButton.VBButton();
            btnLine1.Size = new Size(116, 10);
            btnLine2.Size = new Size(116, 5);
            btnLine3.Size = new Size(116, 3);
            btnLine1.BackColor = btnLine2.BackColor = btnLine3.BackColor = Color.FromArgb(217, 217, 217);
            btnLine1.Location = new Point(17, 11);
            btnLine2.Location = new Point(17, 29);
            btnLine3.Location = new Point(17, 42);
            btnLine1.FlatStyle = btnLine2.FlatStyle = btnLine3.FlatStyle = FlatStyle.Flat;
            btnLine1.BorderColor = btnLine2.BorderColor = btnLine3.BorderColor = Color.Black;
                //set default width
            switch (pen.Width)
            {
                case 3:
                    btnLine3.BorderSize = 1;
                    break;
                case 5:
                    btnLine2.BorderSize = 1;
                    break;
                case 10:
                    btnLine1.BorderSize = 1;
                    break;
            }
            btnLine1.BorderRadius = btnLine2.BorderRadius = btnLine3.BorderRadius = 0;
            btnLine1.Cursor = btnLine2.Cursor = btnLine3.Cursor = Cursors.Hand;
            btnLine1.MouseDown += delegate (object sender, MouseEventArgs e) { SetWidth_10(sender, e, btnLine1, btnLine2, btnLine3); };
            btnLine2.MouseDown += delegate (object sender, MouseEventArgs e) { SetWidth_5(sender, e, btnLine1, btnLine2, btnLine3); };
            btnLine3.MouseDown += delegate (object sender, MouseEventArgs e) { SetWidth_3(sender, e, btnLine1, btnLine2, btnLine3); };
            p.Controls.Add(btnLine1);
            p.Controls.Add(btnLine2);
            p.Controls.Add(btnLine3);
            panel3.Controls.Add(p);
            //Init SelectdColor
            CustomButton.VBButton selectedColor = new CustomButton.VBButton();
            selectedColor.Size = new Size(25, 25);
            selectedColor.Location = new Point(30, 560);
            selectedColor.BorderColor = Color.White;
            selectedColor.BorderSize = 1;
            selectedColor.BorderRadius = 0;
            panel3.Controls.Add(selectedColor);
            Label l = new Label();
            l.Text = "Selected Color";
            l.ForeColor = Color.White;
            l.Location = new Point(60, 565);
            l.AutoSize = true;
            panel3.Controls.Add(l);
            //Matrix of Color
            CustomButton.VBButton[,] matrixColor = new CustomButton.VBButton[10, 3];
            //set Size, Location, BackColor...
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrixColor[i, j] = new CustomButton.VBButton();
                    matrixColor[i, j].Size = new Size(25, 25);
                    matrixColor[i, j].Location = new Point(30 + j*34 + j*25, 100 + i*20 + i*25 );
                    matrixColor[i, j].ForeColor = Color.Red;
                    matrixColor[i, j].BorderColor = Color.White;
                    matrixColor[i, j].BorderSize = 1;
                    matrixColor[i, j].BorderRadius = 0;
                    panel3.Controls.Add(matrixColor[i, j]);
                }   
            }
            //setColor
            matrixColor[0, 1].BackColor = Color.FromArgb(255, 255, 255);
            matrixColor[1, 1].BackColor = Color.FromArgb(142, 142, 142);
            matrixColor[2, 1].BackColor = Color.FromArgb(118, 70, 43);
            matrixColor[3, 1].BackColor = Color.FromArgb(217, 165, 179);
            matrixColor[4, 1].BackColor = Color.FromArgb(192, 127, 0);
            matrixColor[5, 1].BackColor = Color.FromArgb(163, 156, 124);
            matrixColor[6, 1].BackColor = Color.FromArgb(185, 215, 123);
            matrixColor[7, 1].BackColor = Color.FromArgb(141, 186, 226);
            matrixColor[8, 1].BackColor = Color.FromArgb(58, 67, 112);
            //matrixColor[9, 1].BackColor = Color.FromArgb(156, 135, 184);
            matrixColor[0, 2].BackColor = Color.FromArgb(0, 0, 0);
            matrixColor[1, 2].BackColor = Color.FromArgb(66, 66, 66);
            matrixColor[2, 2].BackColor = Color.FromArgb(112, 0, 0);
            matrixColor[3, 2].BackColor = Color.FromArgb(255, 0, 0);
            matrixColor[4, 2].BackColor = Color.FromArgb(148, 89, 0);
            matrixColor[5, 2].BackColor = Color.FromArgb(255, 245, 0);
            matrixColor[6, 2].BackColor = Color.FromArgb(52, 168, 83);
            matrixColor[7, 2].BackColor = Color.FromArgb(24, 104, 174);
            matrixColor[8, 2].BackColor = Color.FromArgb(27, 41, 114);
            //matrixColor[9, 2].BackColor = Color.FromArgb(151, 71, 255);
            //setEvent
            for (int i = 0; i < 9; i++)
            {
                //matrixColor[i, 0].Click += new EventHandler(SetColorForBlank);
                matrixColor[i, 0].BackColor = Color.White;
                matrixColor[i, 0].BackColorChanged += delegate (object sender, EventArgs e) { CustomColor_BackColorChanged(sender, e, selectedColor); };
                matrixColor[i, 0].MouseDown += delegate (object sender, MouseEventArgs e) { SetCustomColor(sender, e, selectedColor); };
                matrixColor[i, 1].MouseDown += delegate (object sender, MouseEventArgs e) { SetColor(sender, e, selectedColor); };
                matrixColor[i, 2].MouseDown += delegate (object sender, MouseEventArgs e) { SetColor(sender, e, selectedColor); };
            }
            //Init EditColor
            Panel edit = new Panel();
            edit.Size = new Size(147, 56);
            edit.Location = new Point(28, 500);
            edit.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\EditColor.png"));
            edit.MouseDown += delegate (object sender, MouseEventArgs e) { editColor_MouseDown(sender, e, matrixColor); };
            panel3.Controls.Add(edit);
            selectedColor.BackColor = pen.Color;
        }

        private void SetWidth_10(object sender, MouseEventArgs e, CustomButton.VBButton btnLine1, CustomButton.VBButton btnLine2, CustomButton.VBButton btnLine3)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (pen.Width)
                {
                    case 3:
                        btnLine3.BorderSize = 0;
                        break;
                    case 5:
                        btnLine2.BorderSize = 0;
                        break;
                    case 10:
                        btnLine1.BorderSize = 0;
                        break;
                }
                pen.Width = 10;
                btnLine1.BorderSize = 1;
            }
        }
        private void SetWidth_5(object sender, MouseEventArgs e, CustomButton.VBButton btnLine1, CustomButton.VBButton btnLine2, CustomButton.VBButton btnLine3)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (pen.Width)
                {
                    case 3:
                        btnLine3.BorderSize = 0;
                        break;
                    case 5:
                        btnLine2.BorderSize = 0;
                        break;
                    case 10:
                        btnLine1.BorderSize = 0;
                        break;
                }
                pen.Width = 5;
                btnLine2.BorderSize = 1;
            }
        }
        private void SetWidth_3(object sender, MouseEventArgs e, CustomButton.VBButton btnLine1, CustomButton.VBButton btnLine2, CustomButton.VBButton btnLine3)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (pen.Width)
                {
                    case 3:
                        btnLine3.BorderSize = 0;
                        break;
                    case 5:
                        btnLine2.BorderSize = 0;
                        break;
                    case 10:
                        btnLine1.BorderSize = 0;
                        break;
                }
                pen.Width = 3;
                btnLine3.BorderSize = 1;
            }
        }

        private void SetColor(object sender, MouseEventArgs e, CustomButton.VBButton b)
        {
            if (e.Button == MouseButtons.Left)
            {
                CustomButton.VBButton button = (CustomButton.VBButton)sender;
                b.BackColor = button.BackColor;
                pen.Color = button.BackColor;
            }
        }
        private void SetCustomColor(object sender, MouseEventArgs e, CustomButton.VBButton b)
        {
            if (e.Button == MouseButtons.Left)
            {
                CustomButton.VBButton button = (CustomButton.VBButton)sender;
                if (button.ForeColor != Color.Red)
                {
                    b.BackColor = button.BackColor;
                    pen.Color = button.BackColor;
                }
            }
        }

        //private void SetColorForBlank(object sender, EventArgs e)
        //{
        //    CustomButton.VBButton button = (CustomButton.VBButton)sender;
        //    if (button.ForeColor == Color.Red)
        //    {
        //        ColorDialog color = new ColorDialog();
        //        if (color.ShowDialog() == DialogResult.OK)
        //        {
        //            button.BackColor = color.Color;
        //            button.ForeColor = Color.White;
        //            pen.Color = button.BackColor;
        //        }
        //    }
        //    else
        //        pen.Color = button.BackColor;
        //}
        private void editColor_MouseDown(object sender, MouseEventArgs e, CustomButton.VBButton[,] matrixColor)
        {
            if (e.Button == MouseButtons.Left)
            {
                ColorDialog color = new ColorDialog();
                if (color.ShowDialog() == DialogResult.OK)
                {
                    //if BlankColor is not full, add new color, else delete first color
                    int blankPosition;
                    for (blankPosition = 0; blankPosition < 9; blankPosition++)
                    {
                        if (matrixColor[blankPosition, 0].ForeColor == Color.Red)
                        {
                            break;
                        }
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        if (matrixColor[i, 0].ForeColor != Color.Red && matrixColor[i, 0].BackColor == color.Color)
                        {
                            if (blankPosition - 1 < i)
                                return;
                            CustomButton.VBButton temp = new CustomButton.VBButton();
                            temp.BackColor = matrixColor[i, 0].BackColor;
                            matrixColor[i, 0].BackColor = matrixColor[blankPosition - 1, 0].BackColor;
                            matrixColor[blankPosition - 1, 0].BackColor = temp.BackColor;
                            pen.Color = color.Color;
                            return;
                        }
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        if (matrixColor[i, 0].ForeColor == Color.Red)
                        {
                            matrixColor[i, 0].ForeColor = Color.White;
                            matrixColor[i, 0].BackColor = color.Color;
                            pen.Color = color.Color;
                            break;
                        }
                        else if (i == 8)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                matrixColor[j, 0].BackColor = matrixColor[j + 1, 0].BackColor;
                            }
                            matrixColor[8, 0].BackColor = color.Color;
                            pen.Color = color.Color;
                        }
                    }
                }
            }
        }
        private void CustomColor_BackColorChanged(object sender, EventArgs e, CustomButton.VBButton selectedColor)
        {
            CustomButton.VBButton b = (CustomButton.VBButton)sender;
            selectedColor.BackColor = b.BackColor;
        }
        #endregion

        //Filter feature
        #region Filter

        private void InitFilterDetails()
        {
            PictureBox picGray, picTrans, picSepiaTone, picNegative, picRed, picGreen, picBlue;
            picGray = new PictureBox();
            picTrans = new PictureBox();
            picSepiaTone = new PictureBox();
            picNegative = new PictureBox();
            picRed = new PictureBox();
            picGreen = new PictureBox();
            picBlue = new PictureBox();

            //SetImage
            picGray.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Filter\GrayImg.png"));
            picNegative.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Filter\NegativeImg.png"));
            picTrans.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Filter\TransparencyImg.png"));
            picSepiaTone.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Filter\SepiaImg.png"));
            picRed.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Filter\RedImg.png"));
            picGreen.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Filter\GreenImg.png"));
            picBlue.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Filter\BlueImg.png"));

            //setSize
            picGray.Size = picNegative.Size = picTrans.Size = picSepiaTone.Size = picRed.Size = picGreen.Size = picBlue.Size = new Size(80, 80);

            //setSizeMode
            picGray.SizeMode = picNegative.SizeMode = picTrans.SizeMode = picSepiaTone.SizeMode = picRed.SizeMode = picGreen.SizeMode = picBlue.SizeMode = PictureBoxSizeMode.StretchImage;

            //setEvent
            picGray.MouseDown += new MouseEventHandler(picGray_MouseDown);
            picNegative.MouseDown += new MouseEventHandler(picNegative_MouseDown);
            picTrans.MouseDown += new MouseEventHandler(picTrans_MouseDown);
            picSepiaTone.MouseDown += new MouseEventHandler(picSepiaTone_MouseDown);
            picRed.MouseDown += new MouseEventHandler(picRed_MouseDown);
            picGreen.MouseDown += new MouseEventHandler(picGreen_MouseDown);
            picBlue.MouseDown += new MouseEventHandler(picBlue_MouseDown);

            //setPosition
            picGray.Location = new Point(13, 20);
            panel3.Controls.Add(picGray);
            picNegative.Location = new Point(110, 20);
            panel3.Controls.Add(picNegative);
            picTrans.Location = new Point(13, 115);
            panel3.Controls.Add(picTrans);
            picSepiaTone.Location = new Point(110, 115);
            panel3.Controls.Add(picSepiaTone);
            picRed.Location = new Point(13, 210);
            panel3.Controls.Add(picRed);
            picGreen.Location = new Point(110, 210);
            panel3.Controls.Add(picGreen);
            picBlue.Location = new Point(13, 305);
            panel3.Controls.Add(picBlue);

        }

        //GetARGB
        private Bitmap GetArgbCopy(Image sourceImage)
        {
            Bitmap bmpNew = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);


            using (Graphics graphics = Graphics.FromImage(bmpNew))
            {
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
                graphics.Flush();
            }


            return bmpNew;
        }

        //ApplyColorMatrix
        private Bitmap ApplyColorMatrix(Image sourceImage, ColorMatrix colorMatrix)
        {
            Bitmap bmp32BppSource = GetArgbCopy(sourceImage);
            Bitmap bmp32BppDest = new Bitmap(bmp32BppSource.Width, bmp32BppSource.Height, PixelFormat.Format32bppArgb);


            using (Graphics graphics = Graphics.FromImage(bmp32BppDest))
            {
                ImageAttributes bmpAttributes = new ImageAttributes();
                bmpAttributes.SetColorMatrix(colorMatrix);

                graphics.DrawImage(bmp32BppSource, new Rectangle(0, 0, bmp32BppSource.Width, bmp32BppSource.Height),
                                    0, 0, bmp32BppSource.Width, bmp32BppSource.Height, GraphicsUnit.Pixel, bmpAttributes);


            }
            bmp32BppSource.Dispose();
            return bmp32BppDest;
        }

        //Transparency Filter
        private Bitmap ImageWithTransparency(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                                {
                            new float[]{1, 0, 0, 0, 0},
                            new float[]{0, 1, 0, 0, 0},
                            new float[]{0, 0, 1, 0, 0},
                            new float[]{0, 0, 0, 0.3f, 0},
                            new float[]{0, 0, 0, 0, 1}
                                });
            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        //SepiaTone Filter
        private Bitmap ImageWithSepiaTone(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                       {
                        new float[]{.393f, .349f, .272f, 0, 0},
                        new float[]{.769f, .686f, .534f, 0, 0},
                        new float[]{.189f, .168f, .131f, 0, 0},
                        new float[]{0, 0, 0, 1, 0},
                        new float[]{0, 0, 0, 0, 1}
                       });
            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        //Negative Filter
        private Bitmap ImageWithNegative(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                           {
                            new float[]{-1, 0, 0, 0, 0},
                            new float[]{0, -1, 0, 0, 0},
                            new float[]{0, 0, -1, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{1, 1, 1, 1, 1}
                           });
            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        //Red Filter
        private Bitmap ImageWithRed(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                           {
                            new float[]{0, 0, 0, 0, 0},
                            new float[]{0, -1, 0, 0, 0},
                            new float[]{0, 0, -1, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{1, 1, 1, 1, 1}
                           });
            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        //Green Filter
        private Bitmap ImageWithGreen(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                           {
                            new float[]{-1, 0, 0, 0, 0},
                            new float[]{0, 0, 0, 0, 0},
                            new float[]{0, 0, -1, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{1, 1, 1, 1, 1}
                           });
            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        //Blue Filter
        private Bitmap ImageWithBlue(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                           {
                            new float[]{-1, 0, 0, 0, 0},
                            new float[]{0, -1, 0, 0, 0},
                            new float[]{0, 0, 0, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{1, 1, 1, 1, 1}
                           });
            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        //pic..._Click
        private void picGray_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Bitmap bit = new Bitmap(imgNow);
                int width = bit.Width;
                int height = bit.Height;

                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        Color color = bit.GetPixel(x, y);

                        //extract ARGB value from p
                        int a = color.A;
                        int r = color.R;
                        int g = color.G;
                        int b = color.B;

                        //caculate average
                        int avg = (r + g + b) / 3;

                        //set new pixel value
                        bit.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                    }
                imgNow = bit;
                pic.Image = imgNow;
                stackImage.Push(imgNow);
                //btnGray.BorderSize = 2;
                //filterType = 1;
            }
        }

        private void picNegative_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithNegative(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
            }    
        }

        private void picTrans_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithTransparency(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
            }    
        }

        private void picSepiaTone_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithSepiaTone(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
            }
        }

        private void picRed_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithRed(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
            }
        }

        private void picGreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithGreen(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
            }
        }

        private void picBlue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithBlue(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
            }
        }

        #endregion

        //Add Shapes feature
        #region Shapes
        private int width, height, X, Y;
        private bool shiftDown = false; //if user long press shift, shapes will be drawn based on the square 
        private Pen penRect = new Pen(Color.LightSkyBlue, 1); //pen for rectangle around shapes
        private string shapesType = "0";
        public string ShapesType
        {
            get
            {
                return shapesType;
            }
            set
            {
                CanMove = false;
                shapesType = value;
            }
        }
        private Rectangle rectNow = new Rectangle(0, 0, 0, 0);
        private Point startMouseMoveShape = new Point(0, 0);
        private bool canMove = false, canResize = false;
        private Point mouseDownLocation = new Point(0, 0); 
        public bool CanMove
        {
            get
            {
                return canMove;
            }
            set
            {
                canMove = value;
                if (!canMove)
                {
                    using (Graphics g = Graphics.FromImage(imgNow))
                    {
                        switch (ShapesType)
                        {
                            case "0":
                                //g.DrawLine(pen, X, Y, e.X, e.Y);
                                break;
                            case "1":
                                g.DrawEllipse(pen, rectNow);
                                break;
                            case "2":
                                g.DrawRectangle(pen, rectNow);
                                break;
                            case "3":
                                g.DrawPolygon(pen, CaculateRightTriangle(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "4":
                                g.DrawPolygon(pen, CaculateIsoscelesTriangle(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "5":
                                g.DrawPolygon(pen, CaculateRhombus(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "6":
                                g.DrawPolygon(pen, CaculatePentagon(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "7":
                                g.DrawPolygon(pen, CaculateHexagon(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "8":
                                g.DrawPolygon(pen, CaculateFiveStar(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "9":
                                g.DrawPolygon(pen, CaculateSixStar(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "10":
                                g.DrawPolygon(pen, CaculateRightArrow(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "11":
                                g.DrawPolygon(pen, CaculateLeftArrow(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "12":
                                g.DrawPolygon(pen, CaculateUpArrow(rectNow.X, rectNow.Y, width, height));
                                break;
                            case "13":
                                g.DrawPolygon(pen, CaculateDownArrow(rectNow.X, rectNow.Y, width, height));
                                break;
                        }
                    }
                    rectNow = new Rectangle(0, 0, 0, 0);
                    stackImage.Push(imgNow);
                    pic.Refresh();
                }
                //if (canMove)
                //    pic.Cursor = Cursors.SizeAll;
                //else
                //    pic.Cursor = Cursors.Default;
            }
        }

        private void InitShapesDetails()
        {
            FlowLayoutPanel pShapes = new FlowLayoutPanel();
            //pShapes.AutoScroll = true;
            pShapes.Location = new Point(23, 600);
            pShapes.BackColor = Color.FromArgb(217, 217, 217);
            pShapes.Size = new Size(157, 100);

            CustomButton.VBButton[] lstButton = new CustomButton.VBButton[14];
            for (int i = 0; i < 14; i++)
            {
                lstButton[i] = new CustomButton.VBButton();
                lstButton[i].Name = i.ToString();
                lstButton[i].BorderRadius = 0;
                lstButton[i].BorderSize = 0;
                lstButton[i].BorderColor = Color.Blue;
                lstButton[i].Size = new Size(25, 25);
                lstButton[i].BackColor = Color.FromArgb(217, 217, 217);
                pShapes.Controls.Add(lstButton[i]);
            }

            for (int i = 0; i < 14; i++)
            {
                lstButton[i].MouseDown += delegate (object sender, MouseEventArgs e) { Shapes_Button_MouseDown(sender, e, lstButton); };
            }

            //set image
            lstButton[0].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/line.png")), new Size(25, 25));
            lstButton[1].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/ellipse.png")), new Size(25, 25));
            lstButton[2].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/rectangle.png")), new Size(25, 25));
            lstButton[3].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/rightTriangle.png")), new Size(25, 25));
            lstButton[4].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/isoscelesTriangle.png")), new Size(25, 25));
            lstButton[5].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/rhombus.png")), new Size(25, 25));
            lstButton[6].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/pentagon.png")), new Size(25, 25));
            lstButton[7].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/hexagon.png")), new Size(25, 25));
            lstButton[8].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/fiveStar.png")), new Size(25, 25));
            lstButton[9].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/sixStar.png")), new Size(25, 25));
            lstButton[10].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/rightArrow.png")), new Size(25, 25));
            lstButton[11].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/leftArrow.png")), new Size(25, 25));
            lstButton[12].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/upArrow.png")), new Size(25, 25));
            lstButton[13].BackgroundImage = resizeImage(Image.FromFile(getFilePath(@"../../../Roga/Assets/Images/shapes/downArrow.png")), new Size(25, 25));


            panel3.Controls.Add(pShapes);
            //pen for rectangle around shapes
            penRect.DashStyle = DashStyle.DashDot;
            //StartCap & EndCap for line
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
        }

        private void Shapes_Button_MouseDown(object sender, MouseEventArgs e, CustomButton.VBButton[] lstButton)
        {
            if (e.Button == MouseButtons.Left)
            {
                CustomButton.VBButton button = (CustomButton.VBButton)sender;
                switch (shapesType)
                {
                    case "0":
                        lstButton[0].BorderSize = 0;
                        break;
                    case "1":
                        lstButton[1].BorderSize = 0;
                        break;
                    case "2":
                        lstButton[2].BorderSize = 0;
                        break;
                    case "3":
                        lstButton[3].BorderSize = 0;
                        break;
                    case "4":
                        lstButton[4].BorderSize = 0;
                        break;
                    case "5":
                        lstButton[5].BorderSize = 0;
                        break;
                    case "6":
                        lstButton[6].BorderSize = 0;
                        break;
                    case "7":
                        lstButton[7].BorderSize = 0;
                        break;
                    case "8":
                        lstButton[8].BorderSize = 0;
                        break;
                    case "9":
                        lstButton[9].BorderSize = 0;
                        break;
                    case "10":
                        lstButton[10].BorderSize = 0;
                        break;
                    case "11":
                        lstButton[11].BorderSize = 0;
                        break;
                    case "12":
                        lstButton[12].BorderSize = 0;
                        break;
                    case "13":
                        lstButton[13].BorderSize = 0;
                        break;
                }
                switch (button.Name)
                {
                    case "0":
                        ShapesType = "0";
                        break;
                    case "1":
                        ShapesType = "1";
                        break;
                    case "2":
                        ShapesType = "2";
                        break;
                    case "3":
                        ShapesType = "3";
                        break;
                    case "4":
                        ShapesType = "4";
                        break;
                    case "5":
                        ShapesType = "5";
                        break;
                    case "6":
                        ShapesType = "6";
                        break;
                    case "7":
                        ShapesType = "7";
                        break;
                    case "8":
                        ShapesType = "8";
                        break;
                    case "9":
                        ShapesType = "9";
                        break;
                    case "10":
                        ShapesType = "10";
                        break;
                    case "11":
                        ShapesType = "11";
                        break;
                    case "12":
                        ShapesType = "12";
                        break;
                    case "13":
                        ShapesType = "13";
                        break;
                }
                switch (shapesType)
                {
                    case "0":
                        lstButton[0].BorderSize = 1;
                        break;
                    case "1":
                        lstButton[1].BorderSize = 1;
                        break;
                    case "2":
                        lstButton[2].BorderSize = 1;
                        break;
                    case "3":
                        lstButton[3].BorderSize = 1;
                        break;
                    case "4":
                        lstButton[4].BorderSize = 1;
                        break;
                    case "5":
                        lstButton[5].BorderSize = 1;
                        break;
                    case "6":
                        lstButton[6].BorderSize = 1;
                        break;
                    case "7":
                        lstButton[7].BorderSize = 1;
                        break;
                    case "8":
                        lstButton[8].BorderSize = 1;
                        break;
                    case "9":
                        lstButton[9].BorderSize = 1;
                        break;
                    case "10":
                        lstButton[10].BorderSize = 1;
                        break;
                    case "11":
                        lstButton[11].BorderSize = 1;
                        break;
                    case "12":
                        lstButton[12].BorderSize = 1;
                        break;
                    case "13":
                        lstButton[13].BorderSize = 1;
                        break;
                }
            }
        }

        private void pic_Shapes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                X = e.X;
                Y = e.Y;
                mouseDownLocation = e.Location;
                pic.Refresh();
                isMouseDown = true;
                if ((e.X >= (rectNow.X - pen.Width - 3)) && (e.X <= (rectNow.X + rectNow.Width + pen.Width + 3))
                    && (e.Y >= (rectNow.Y - pen.Width - 3)) && (e.Y <= (rectNow.Y + rectNow.Height + pen.Width + 3)))
                {
                    CanMove = true;
                    startMouseMoveShape = e.Location;
                }
                else
                {
                    if (canMove)
                        CanMove = false;
                    else
                        canMove = false;
                }    
            }
        }

        private void pic_Shapes_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.X >= (rectNow.X - pen.Width - 3)) && (e.X <= (rectNow.X + rectNow.Width + pen.Width + 3))
                && (e.Y >= (rectNow.Y - pen.Width - 3)) && (e.Y <= (rectNow.Y + rectNow.Height + pen.Width + 3)))
            {
                pic.Cursor = Cursors.SizeAll;
            }
            else
                pic.Cursor = Cursors.Default;
            //MouseDown but cannot move, MouseMove event will draw a shapes
            if (isMouseDown && !canMove)
            {
                pic.Refresh();
                pic.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                width = Math.Abs(e.X - X);
                height = Math.Abs(e.Y - Y);
                if (shiftDown)
                {
                    width = height = Math.Min(width, height);
                }
                switch (ShapesType)
                {
                    case "0":
                        pic.CreateGraphics().DrawLine(pen, X, Y, e.X, e.Y);
                        break;
                    case "1":
                        pic.CreateGraphics().DrawEllipse(pen, Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                        break;
                    case "2":
                        pic.CreateGraphics().DrawRectangle(pen, Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                        break;
                    case "3":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateRightTriangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "4":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateIsoscelesTriangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "5":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateRhombus(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                        break;
                    case "6":
                        pic.CreateGraphics().DrawPolygon(pen, CaculatePentagon(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "7":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateHexagon(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "8":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateFiveStar(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "9":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateSixStar(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "10":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateRightArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "11":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateLeftArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "12":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateUpArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                    case "13":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateDownArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                        break;
                }    
                //pic.CreateGraphics().DrawLine(pen, X, Y, e.X, e.Y);
                //pic.CreateGraphics().DrawRectangle(pen, Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                //pic.CreateGraphics().DrawEllipse(pen, Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                //pic.CreateGraphics().DrawPolygon(pen, CaculateRightArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateLeftArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateDownArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateUpArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateIsoscelesTriangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateRightTriangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateRhombus(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculatePentagon(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateHexagon(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateFiveStar(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                //pic.CreateGraphics().DrawPolygon(pen, CaculateSixStar(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
            }
            //MouseDown and can move, relocation the rectNow
            else if (isMouseDown && canMove)
            {
                rectNow.Location = new Point((e.X - mouseDownLocation.X) + rectNow.Left, (e.Y - mouseDownLocation.Y) + rectNow.Top);
                mouseDownLocation = e.Location;
                pic.Refresh();
                pic.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                switch (ShapesType)
                {
                    case "0":
                        pic.CreateGraphics().DrawLine(pen, X, Y, e.X, e.Y);
                        break;
                    case "1":
                        pic.CreateGraphics().DrawEllipse(pen, rectNow);
                        break;
                    case "2":
                        pic.CreateGraphics().DrawRectangle(pen, rectNow);
                        break;
                    case "3":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateRightTriangle(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "4":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateIsoscelesTriangle(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "5":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateRhombus(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "6":
                        pic.CreateGraphics().DrawPolygon(pen, CaculatePentagon(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "7":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateHexagon(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "8":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateFiveStar(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "9":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateSixStar(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "10":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateRightArrow(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "11":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateLeftArrow(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "12":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateUpArrow(rectNow.X, rectNow.Y, width, height));
                        break;
                    case "13":
                        pic.CreateGraphics().DrawPolygon(pen, CaculateDownArrow(rectNow.X, rectNow.Y, width, height));
                        break;
                }
            }
        }

        private void pic_Shapes_MouseUp(object sender, MouseEventArgs e)
        {
            if (!canMove)
            {

                using (Graphics g = Graphics.FromImage(imgNow))
                {
                    canMove = true;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    int width, height;
                    width = Math.Abs(e.X - X);
                    height = Math.Abs(e.Y - Y);
                    if (shiftDown)
                    {
                        width = height = Math.Min(width, height);
                    }
                    switch (ShapesType)
                    {
                        case "0":
                            pic.CreateGraphics().DrawLine(pen, X, Y, e.X, e.Y);
                            break;
                        case "1":
                            pic.CreateGraphics().DrawEllipse(pen, Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                            break;
                        case "2":
                            pic.CreateGraphics().DrawRectangle(pen, Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                            break;
                        case "3":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateRightTriangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "4":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateIsoscelesTriangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "5":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateRhombus(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                            break;
                        case "6":
                            pic.CreateGraphics().DrawPolygon(pen, CaculatePentagon(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "7":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateHexagon(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "8":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateFiveStar(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "9":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateSixStar(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "10":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateRightArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "11":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateLeftArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "12":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateUpArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                        case "13":
                            pic.CreateGraphics().DrawPolygon(pen, CaculateDownArrow(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                            break;
                    }
                    if (shapesType != "0")
                    {
                        rectNow = new Rectangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                        pic.CreateGraphics().DrawRectangle(penRect, Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                    }
                    //pic.CreateGraphics().DrawLine(pen, X, Y, e.X, e.Y);
                    //g.DrawLine(pen, X, Y, e.X, e.Y);
                    //g.DrawRectangle(pen, Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height);
                    //g.DrawEllipse(pen, Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height);
                    //g.DrawPolygon(pen, CaculateRightArrow(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculateLeftArrow(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculateDownArrow(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculateUpArrow(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculateIsoscelesTriangle(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculateRightTriangle(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height)));
                    //g.DrawPolygon(pen, CaculateRhombus(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculatePentagon(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculateHexagon(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculateFiveStar(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //g.DrawPolygon(pen, CaculateSixStar(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), width, height));
                    //hasObject = true;
                    //leftTop = new Point(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y));
                    //rightBottom = new Point(leftTop.X + Math.Abs(e.X - X), leftTop.Y + Math.Abs(e.Y - Y));
                }
            }
            else
            {
                pic.CreateGraphics().DrawRectangle(penRect, rectNow);
            }    
            isMouseDown = false;
            shiftDown = false;
            //pic.Invalidate();
        }

        private void this_Shapes_ShiftKey(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                shiftDown = true;
            }
            else
            {
                shiftDown = false;
            }
        }

        private void this_Shapes_KeyUp(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
            {
                shiftDown = false;
            }
        }

        private void addShapesEvent()
        {
            pic.MouseDown += new MouseEventHandler(pic_Shapes_MouseDown);
            pic.MouseMove += new MouseEventHandler(pic_Shapes_MouseMove);
            pic.MouseUp += new MouseEventHandler(pic_Shapes_MouseUp);
            this.KeyDown += new KeyEventHandler(this_Shapes_ShiftKey);
            this.KeyUp += new KeyEventHandler(this_Shapes_KeyUp);
        }

        private void removeShapesEvent()
        {
            pic.MouseDown -= new MouseEventHandler(pic_Shapes_MouseDown);
            pic.MouseMove -= new MouseEventHandler(pic_Shapes_MouseMove);
            pic.MouseUp -= new MouseEventHandler(pic_Shapes_MouseUp);
            this.KeyDown -= new KeyEventHandler(this_Shapes_ShiftKey);
            this.KeyUp -= new KeyEventHandler(this_Shapes_KeyUp);
            pen.StartCap = LineCap.Square;
            pen.EndCap = LineCap.Square;
        }

        #region caculate points to draw polygon
        //Create seven point to draw an arrow by drawPolygon (right)
        private PointF[] CaculateRightArrow(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init, init, init, init };
            points[0].X = x;                                    points[0].Y = y + (float)height / 4;
            points[1].X = points[0].X + (float)width / 2;       points[1].Y = points[0].Y;
            points[2].X = points[1].X;                          points[2].Y = y;
            points[3].X = x + width;                            points[3].Y = y + (float)height / 2;
            //mirror
            points[4].X = points[2].X;                          points[4].Y = y + height;
            points[5].X = points[1].X;                          points[5].Y = y + 3 * ((float)height / 4);
            points[6].X = points[0].X;                          points[6].Y = points[5].Y;
            return points;
        }
        //Create seven point to draw an arrow by drawPolygon (left)
        private PointF[] CaculateLeftArrow(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init, init, init, init };
            points[0].X = x;                                    points[0].Y = y + (float)height / 2;
            points[1].X = x + (float)width / 2;                 points[1].Y = y;
            points[2].X = points[1].X;                          points[2].Y = y + (float)height / 4;
            points[3].X = x + width;                            points[3].Y = points[2].Y;
            //mirror
            points[4].X = points[3].X;                          points[4].Y = y + 3 * ((float)height / 4);
            points[5].X = points[2].X;                          points[5].Y = points[4].Y;
            points[6].X = points[1].X;                          points[6].Y = y + height;
            return points;
        }

        //Create seven point to draw an arrow by drawPolygon (up)
        private PointF[] CaculateUpArrow(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init, init, init, init };
            points[0].X = x + (float)width / 2;                 points[0].Y = y;
            points[1].X = x + width;                            points[1].Y = y + (float)height / 2;
            points[2].X = x + 3 * ((float)width / 4);           points[2].Y = points[1].Y;
            points[3].X = points[2].X;                          points[3].Y = y + height;
            //mirror
            points[4].X = x + (float)width / 4;                 points[4].Y = points[3].Y;
            points[5].X = points[4].X;                          points[5].Y = points[2].Y;
            points[6].X = x;                                    points[6].Y = points[5].Y;
            return points;
        }

        //Create seven point to draw an arrow by drawPolygon (down)
        private PointF[] CaculateDownArrow(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init, init, init, init };
            points[0].X = x + (float)width / 2;                 points[0].Y = y + height;
            points[1].X = x;                                    points[1].Y = y + (float)height / 2;
            points[2].X = x + (float)width / 4;                 points[2].Y = points[1].Y;
            points[3].X = points[2].X;                          points[3].Y = y;
            //mirror
            points[4].X = x + 3 * ((float)width / 4);           points[4].Y = points[3].Y;
            points[5].X = points[4].X;                          points[5].Y = points[2].Y;
            points[6].X = x + width;                            points[6].Y = points[5].Y;
            return points;
        }

        private PointF[] CaculateIsoscelesTriangle(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init };
            points[0].X = x;                                    points[0].Y = y + height;
            points[1].X = x + (float)width / 2;                 points[1].Y = y;
            points[2].X = x + width;                            points[2].Y = points[0].Y;
            return points;
        }

        private PointF[] CaculateRightTriangle(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init };
            points[0].X = x;                                    points[0].Y = y;
            points[1].X = x;                                    points[1].Y = y + height;
            points[2].X = x + width;                            points[2].Y = y + height;
            return points;
        }

        private PointF[] CaculateRhombus(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init };
            points[0].X = x + (float)width / 2;                 points[0].Y = y;
            points[1].X = x + width;                            points[1].Y = y + ((float)1 / 2) * height;
            points[2].X = points[0].X;                          points[2].Y = y + height;
            points[3].X = x;                                    points[3].Y = points[1].Y;
            return points;
        }

        private PointF[] CaculatePentagon(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init, init };
            points[0].X = x + (float)width / 2;                 points[0].Y = y;
            points[1].X = x + width;                            points[1].Y = y + ((float)3 / 8) * height;
            points[2].X = x + ((float)13 / 16) * width;         points[2].Y = y + height;
            points[3].X = x + ((float)3 / 16) * width;          points[3].Y = points[2].Y;
            points[4].X = x;                                    points[4].Y = points[1].Y;
            return points;
        }

        private PointF[] CaculateHexagon(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init, init, init };
            points[0].X = x + (float)width / 2; points[0].Y = y;
            points[1].X = x; points[1].Y = y + ((float)1 / 4) * height;
            points[2].X = x; points[2].Y = y + ((float)3 / 4) * height;
            points[3].X = points[0].X; points[3].Y = y + height;
            points[4].X = x + width; points[4].Y = points[2].Y;
            points[5].X = points[4].X; points[5].Y = points[1].Y;
            return points;
        }

        private PointF[] CaculateFiveStar(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init, init, init, init, init, init, init };
            points[0].X = x + (float)width / 2;                 points[0].Y = y;
            points[1].X = x + ((float)11 / 18) * width;         points[1].Y = y + ((float)2 / 5) * height;
            points[2].X = x + width;                            points[2].Y = points[1].Y;
            points[3].X = x + ((float)2 / 3) * width;           points[3].Y = y + ((float)3 / 5) * height;
            points[4].X = x + ((float)3 / 4) * width;           points[4].Y = y + height;
            points[5].X = x + (float)width / 2;                 points[5].Y = y + ((float)4 / 5) * height;
            points[6].X = x + ((float)1 / 4) * width;           points[6].Y = points[4].Y;
            points[7].X = x + ((float)1 / 3) * width;           points[7].Y = points[3].Y;
            points[8].X = x;                                    points[8].Y = points[2].Y;
            points[9].X = x + ((float)7 / 18) * width;          points[9].Y = points[1].Y;
            return points;
        }

        private PointF[] CaculateSixStar(int x, int y, int width, int height)
        {
            Point init = new Point(x, y);
            PointF[] points = { init, init, init, init, init, init, init, init, init, init, init, init };
            points[0].X = x + (float)width / 2;                 points[0].Y = y;
            points[1].X = x + ((float)2 / 3) * width;           points[1].Y = y + ((float)1 / 4) * height;
            points[2].X = x + width;                            points[2].Y = points[1].Y;
            points[3].X = x + ((float)5 / 6) * width;           points[3].Y = y + ((float)1 / 2) * height;
            points[4].X = points[2].X;                          points[4].Y = y + ((float)3 / 4) * height;
            points[5].X = points[1].X;                          points[5].Y = points[4].Y;
            points[6].X = points[0].X;                          points[6].Y = y + height;
            points[7].X = x + ((float)1 / 3) * width;           points[7].Y = points[5].Y;
            points[8].X = x;                                    points[8].Y = points[4].Y;
            points[9].X = x + ((float)1 / 6) * width;           points[9].Y = points[3].Y;
            points[10].X = x;                                   points[10].Y = points[2].Y;
            points[11].X = points[7].X;                         points[11].Y = points[1].Y;
            return points;
        }
        #endregion

        #endregion

        //Crop feature
        #region Crop

        int crpX = 0, crpY = 0, rectW, rectH;
        Pen crpPen = new Pen(Color.White);

        //resize picturebox when change image size
        private void resizePic(Image imageSource)
        {
            pic.Size = imageSource.Size;
            pic.Location = new Point((Width / 2) - (pic.Width / 2), (Height / 2) - (pic.Height / 2));
        }

        private void InitCropDetails()
        {
            rectW = imgNow.Width;
            rectH = imgNow.Height;

            PictureBox picSelect, picCrop, picRotate, picFlip;
            picSelect = new PictureBox();
            picCrop = new PictureBox();
            picRotate = new PictureBox();
            picFlip = new PictureBox();

            //set image
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            picSelect.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Crop and rotate\select.png"));
            picCrop.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Crop and rotate\crop.png"));
            picRotate.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Crop and rotate\rotate.png"));
            picFlip.Image = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\Crop and rotate\flip.png"));

            //set size
            picSelect.Size = picCrop.Size = picRotate.Size = picFlip.Size = new Size(55, 55);

            //set sizemode
            picSelect.SizeMode = picCrop.SizeMode = picRotate.SizeMode = picFlip.SizeMode = PictureBoxSizeMode.StretchImage;

            //set position
            picSelect.Location = new Point(13, 20);
            panel3.Controls.Add(picSelect);
            picCrop.Location = new Point(110, 20);
            panel3.Controls.Add(picCrop);
            picRotate.Location = new Point(13, 115);
            panel3.Controls.Add(picRotate);
            picFlip.Location = new Point(110, 115);
            panel3.Controls.Add(picFlip);

            //set event
            picSelect.Click += new EventHandler(picSelect_Click);
            picCrop.Click += new EventHandler(picCrop_Click);
            picRotate.Click += new EventHandler(picRotate_Click);
            picFlip.Click += new EventHandler(picFlip_Click);



        }

        //select 
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

                crpX = e.X;
                crpY = e.Y;
            }
        }

        private void pic_Select_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                pic.Refresh();
                rectW = e.X - crpX;
                rectH = e.Y - crpY;
                Graphics g = pic.CreateGraphics();
                g.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                g.Dispose();
            }

        }

        private void Remove_Select()
        {
            pic.MouseDown -= pic_Select_MouseDown;
            pic.MouseMove -= pic_Select_MouseMove;
            pic.MouseEnter -= pic_Select_MouseEnter;
        }

        private void picSelect_Click(object sender, EventArgs e)
        {
            pic.MouseDown += pic_Select_MouseDown;
            pic.MouseMove += pic_Select_MouseMove;
            pic.MouseEnter += pic_Select_MouseEnter;

        }

        //crop
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
            crpX = 0;
            crpY = 0;
            return (Image)crpImage;
        }

        private void picCrop_Click(object sender, EventArgs e)
        {
            imgNow = CropImage(imgNow);
            resizePic(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
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
            resizePic(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
        }

        //flip
        private Bitmap Flip_Image(Image sourceImage)
        {
            Bitmap flip = GetArgbCopy(sourceImage);
            flip.RotateFlip(RotateFlipType.Rotate180FlipY);

            return flip;
        }

        private void picFlip_Click(object sender, EventArgs e)
        {
            Console.WriteLine(imgNow.Width.ToString() + " " + imgNow.Height.ToString());
            imgNow = Flip_Image(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
            Console.WriteLine(imgNow.Width.ToString() + " " + imgNow.Height.ToString());
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }

        #endregion


        #region button onclick
        private void Pen_Button_Click(object sender, EventArgs e)
        {
            MouseType = "pen";
            LastMouseType = "pen";
        }

        private void Hand_Button_Click(object sender, EventArgs e)
        {
            MouseType = "";
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            Thread t = new Thread((ThreadStart)(() =>
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = " Save file...";
                saveFileDialog1.InitialDirectory = "D:\\";
                saveFileDialog1.DefaultExt = "jpg";
                saveFileDialog1.Filter = " Image file (*.BMP,*.JPG,*.JPEG)|*.bmp;*.jpg;*.jpeg ";
                saveFileDialog1.OverwritePrompt = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    pic.Image.Save(saveFileDialog1.FileName);
            }));

            // Run your code from a thread that joins the STA Thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        private void Eraser_Button_Click(object sender, EventArgs e)
        {
            MouseType = "eraser";
            LastMouseType = MouseType;
        }

        private void BrightnessAndContrast_Button_Click(object sender, EventArgs e)
        {
            MouseType = "brightness&contrast";
            LastMouseType = MouseType;
        }

        private void ColorChannel_Button_Click(object sender, EventArgs e)
        {
            MouseType = "RGB";
            LastMouseType = MouseType;
        }

        private void Exposure_Button_Click(object sender, EventArgs e)
        {
            MouseType = "exposure";
            LastMouseType = MouseType;
        }

        private void AddPicture_Button_Click(object sender, EventArgs e)
        {
            MouseType = "AddPicture";
            LastMouseType = MouseType;
        }

        private void AddText_Button_Click(object sender, EventArgs e)
        {
            MouseType = "AddText";
            LastMouseType = MouseType;
        }

        private void Crop_Button_Click(object sender, EventArgs e)
        {
            MouseType = "crop";
            LastMouseType = MouseType;
        }

        private void Shape_Button_Click(object sender, EventArgs e)
        {
            MouseType = "shape";
            LastMouseType = MouseType;
        }

        private void Filter_Button_Click(object sender, EventArgs e)
        {
            MouseType = "filter";
            LastMouseType = MouseType;
        }
        #endregion
    }
}
