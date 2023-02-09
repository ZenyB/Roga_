using System;
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
        bool isSelect = false;//this is used to know when seleclt feature is choose
        string LastMouseType = string.Empty;//hold the lastest mouse type to remove the button events
        private string _mouseType;//hold the mouse type to add the button event
        private Pen pen = new Pen(Color.Black, 3);
        private Image imgNow; //The image is being processed
        private Stack<Image> stackImage = new Stack<Image>(); //stack images has been processed
        string fileNameNow = ""; //location of current image
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
                        Remove_BrighnessAndContrast();
                        //remove brightness
                        break;
                    case "eraser":
                        Remove_Eraser();
                        //remove eraser
                        break;
                    case "RGB":
                        Remove_ColorRGB_Channel();
                        //remove Color Channel
                        break;
                    case "saturation":
                        Remove_Saturation();
                        //remove 
                        break;
                    case "addPicture":
                        removeAddImageEvent();
                        break;
                    case "AddText":
                        Remove_AddText();
                        break;
                    case "crop":
                        Remove_Select();
                        isSelect = false;
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
                        InitPenLineColorDetails();
                        Add_Draw();
                        break;
                    case "brightness&contrast":
                        InitBrighnessAndContrast();
                        Add_BrighnessAndContrast();
                        //add brightness
                        break;
                    case "eraser":
                        InitPenSize();
                        Add_Eraser();
                        //add eraser
                        break;
                    case "RGB":
                        InitColorRGB();
                        Add_ColorRGB_Channel();
                        //add Color Channel
                        break;
                    case "saturation":
                        InitSaturation();
                        Add_Saturation();
                        break;
                    case "addPicture":
                        addAddImageEvent();
                        break;
                    case "AddText":
                        InitTextDetails();
                        break;
                    case "crop":
                        InitCropDetails();
                        isSelect = true;
                        break;
                    case "shape":
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
        public bool isSave;

        #region constructor

        public MainScreen()
        {
            InitializeComponent();
            InitBackgroundImageForButton();

            this.MouseWheel += new MouseEventHandler(Form4_MouseWheel);
            this.MinimumSize = new System.Drawing.Size(Width, Height);
            MouseType = "";
            isSave = true;

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
            this.MinimumSize = new System.Drawing.Size(Width, Height);
            MouseType = "";
            isSave = true;

            pic = new PictureBox();

            pic.Anchor = 0;
            pic.Location = new Point(400, 260);
            pic.Name = "pictureBox1";
            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.SendToBack();

            //pic.MouseWheel += Pic_MouseWheel;

            if (Image.FromFile(selectedPath).Width > Width - 100 || Image.FromFile(selectedPath).Height > Height - 100)
            {
                int newHeight, newWidth;
                if (Image.FromFile(selectedPath).Width > Image.FromFile(selectedPath).Height)
                {
                    newWidth = Width - 225 * 2;
                    newHeight = (int)(Image.FromFile(selectedPath).Height * ((float)newWidth / Image.FromFile(selectedPath).Width));
                }
                else
                {
                    newHeight = Height - 50;
                    newWidth = (int)(Image.FromFile(selectedPath).Width * ((float)newHeight / Image.FromFile(selectedPath).Height));
                }
                pic.Size = new Size(newWidth, newHeight);
                Image temp = Image.FromFile(selectedPath);
                temp = resizeImage(temp, pic.Size);
                pic.Image = temp;
                imgNow = pic.Image;
                stackImage.Push(imgNow);
            }
            else
            {
                int newHeight, newWidth;
                newWidth = Image.FromFile(selectedPath).Width;
                newHeight = Image.FromFile(selectedPath).Height;
                pic.Size = new Size(newWidth, newHeight);
                //pic.Location = new Point((Width / 2) - (newWidth / 2), (Height / 2) - (newHeight / 2));
                pic.Image = Image.FromFile(selectedPath);
                imgNow = pic.Image;
                stackImage.Push(imgNow);
            }
            Controls.Add(pic);
            pic.BringToFront();

        }//create a project with image path
        public MainScreen(Image picture, string fileName)
        {
            InitializeComponent();
            InitBackgroundImageForButton();

            this.MouseWheel += new MouseEventHandler(Form4_MouseWheel);
            this.MinimumSize = new System.Drawing.Size(Width, Height);
            fileNameNow = fileName;
            MouseType = "";
            isSave = true;

            pic = new PictureBox();

            pic.Anchor = 0;
            pic.Location = new Point(400, 260);
            pic.Name = "pictureBox1";
            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.SendToBack();

            //pic.MouseWheel += Pic_MouseWheel;

            if (picture.Width > Width - 100 || picture.Height > Height - 100)
            {
                int newHeight, newWidth;
                if (picture.Width > picture.Height)
                {
                    newWidth = Width - 225 * 2;
                    newHeight = (int)(picture.Height * ((float)newWidth / picture.Width));
                }
                else
                {
                    newHeight = Height - 50;
                    newWidth = (int)(picture.Width * ((float)newHeight / picture.Height));
                }
                pic.Size = new Size(newWidth, newHeight);
                Image temp = picture;
                temp = resizeImage(temp, pic.Size);
                pic.Image = temp;
                imgNow = pic.Image;
                stackImage.Push(imgNow);
            }
            else
            {
                int newHeight, newWidth;
                newWidth = picture.Width;
                newHeight = picture.Height;
                pic.Size = new Size(newWidth, newHeight);
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
            Save_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\save.png"));
            Eraser_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\eraser.png"));
            BrightnessAndContrast_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\brightness.png"));
            ColorChannel_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\color.png"));
            Saturation_Button.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\icons\exposure.png"));
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
        static public string getFilePath(string relativePath)
        {
            string sFile = System.IO.Path.Combine(sCurrentDirectory, relativePath);
            string sFilePath = Path.GetFullPath(sFile);
            return sFilePath;
        }

        //method to set location for picture
        public void setLocation(Image img)
        {
            pic.Location = new Point((Width / 2) - (img.Width / 2) - 5, (Height / 2) - (img.Height / 2));
        }

            //2 cai nay hok biet viet vo chi nua cu de ik
            //
        private void MainScreen_Load(object sender, EventArgs e)
        {
            setLocation(pic.Image);
        }
        void Form4_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        //Event ctrl + Z

        private void backImage()
        {
            if (stackImage.Count > 1)
            {
                pic.Cursor = Cursors.Default;
                if (canMove == false)
                {
                    stackImage.Pop();
                    //If image change size -> resize picturebox
                    if (imgNow.Size != stackImage.Peek().Size)
                        resizePic(stackImage.Peek());
                    imgNow = stackImage.Peek();
                    imgProcess = new Bitmap(imgNow);
                    pic.Image = imgNow;
                }
            }
            if (canMove != false)
            {
                canMove = false;
                rectNow = new Rectangle(0, 0, 0, 0);
            }
            pic.Refresh();
        }    

        private void MainScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && (Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                //When user pressed Ctrl + Z, stackImage will be pop and imgNow return to previous state
                //If stackImage has only one image, this is original image, user can't goback
                backImage();
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

        private void InitPenSize()
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
        }
        private void InitPenLineColorDetails()
        {
            //Panel penLine
            InitPenSize();
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
                    matrixColor[i, j].Cursor = Cursors.Hand;
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
            edit.Cursor = Cursors.Hand;
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
                if (_mouseType == "shape" && canMove)
                {
                    pic.Refresh();
                    drawShapeOnPictureBox(rectNow);
                    if (shapesType == "0")
                    {
                        pic.CreateGraphics().DrawLine(pen, headLine, tailLine);

                        pic.CreateGraphics().FillRectangle(Brushes.White, headLine.X - 2, headLine.Y - 2, 5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), headLine.X - 2, headLine.Y - 2, 5, 5);

                        pic.CreateGraphics().FillRectangle(Brushes.White, tailLine.X - 2, tailLine.Y - 2, 5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), tailLine.X - 2, tailLine.Y - 2, 5, 5);

                        pic.CreateGraphics().FillRectangle(Brushes.White,
                                                            (float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - 2,
                                                            (float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - 2,
                                                            5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1),
                                                            (float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - 2,
                                                            (float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - 2,
                                                            5, 5);
                    }   
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            pic.CreateGraphics().FillRectangle(Brushes.White, resizePoint[i].X, resizePoint[i].Y, 5, 5);
                            pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), resizePoint[i].X, resizePoint[i].Y, 5, 5);
                        }
                        pic.CreateGraphics().DrawRectangle(penRect, rectNow);
                    }    
                }
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
                if (_mouseType == "shape" && canMove)
                {
                    pic.Refresh();
                    drawShapeOnPictureBox(rectNow);
                    if (shapesType == "0")
                    {
                        pic.CreateGraphics().DrawLine(pen, headLine, tailLine);

                        pic.CreateGraphics().FillRectangle(Brushes.White, headLine.X - 2, headLine.Y - 2, 5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), headLine.X - 2, headLine.Y - 2, 5, 5);

                        pic.CreateGraphics().FillRectangle(Brushes.White, tailLine.X - 2, tailLine.Y - 2, 5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), tailLine.X - 2, tailLine.Y - 2, 5, 5);

                        pic.CreateGraphics().FillRectangle(Brushes.White,
                                                            (float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - 2,
                                                            (float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - 2,
                                                            5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1),
                                                            (float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - 2,
                                                            (float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - 2,
                                                            5, 5);
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            pic.CreateGraphics().FillRectangle(Brushes.White, resizePoint[i].X, resizePoint[i].Y, 5, 5);
                            pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), resizePoint[i].X, resizePoint[i].Y, 5, 5);
                        }
                        pic.CreateGraphics().DrawRectangle(penRect, rectNow);
                    }
                }
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
                if (_mouseType == "shape" && canMove)
                {
                    pic.Refresh();
                    drawShapeOnPictureBox(rectNow);
                    if (shapesType == "0")
                    {
                        pic.CreateGraphics().DrawLine(pen, headLine, tailLine);

                        pic.CreateGraphics().FillRectangle(Brushes.White, headLine.X - 2, headLine.Y - 2, 5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), headLine.X - 2, headLine.Y - 2, 5, 5);

                        pic.CreateGraphics().FillRectangle(Brushes.White, tailLine.X - 2, tailLine.Y - 2, 5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), tailLine.X - 2, tailLine.Y - 2, 5, 5);

                        pic.CreateGraphics().FillRectangle(Brushes.White,
                                                            (float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - 2,
                                                            (float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - 2,
                                                            5, 5);
                        pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1),
                                                            (float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - 2,
                                                            (float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - 2,
                                                            5, 5);
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            pic.CreateGraphics().FillRectangle(Brushes.White, resizePoint[i].X, resizePoint[i].Y, 5, 5);
                            pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), resizePoint[i].X, resizePoint[i].Y, 5, 5);
                        }
                        pic.CreateGraphics().DrawRectangle(penRect, rectNow);
                    }
                }    
            }
        }

        private void SetColor(object sender, MouseEventArgs e, CustomButton.VBButton b)
        {
            if (e.Button == MouseButtons.Left)
            {
                CustomButton.VBButton button = (CustomButton.VBButton)sender;
                b.BackColor = button.BackColor;
                pen.Color = button.BackColor;
                txtAddText.ForeColor = pen.Color;
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
                    txtAddText.ForeColor = pen.Color;
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
                            txtAddText.ForeColor = pen.Color;
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
                            txtAddText.ForeColor = pen.Color;
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
                            txtAddText.ForeColor = pen.Color;
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

        //Pencil feature
        #region Pencil
        Image imgTemp;
        private void Pic_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            lastPoint = Point.Empty;
            imgNow = new Bitmap(imgTemp);
            stackImage.Push(imgNow);
            isSave = false;
            pic.Image = imgNow;
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
                        Bitmap bmp = new Bitmap(imgTemp);
                        pic.Image = bmp; //assign the picturebox.Image property to the bitmap created
                    }
                    using (Graphics g = Graphics.FromImage(imgTemp))
                    {//we need to create a Graphics object to draw on the picture box, its our main tool
                     //when making a Pen object, you can just give it color only or give it color and pen size
                        Rectangle rectangle = new Rectangle();
                        PaintEventArgs arg = new PaintEventArgs(g, rectangle);
                        DrawCircle(arg, e.X, e.Y, (int)pen.Width, (int)pen.Width);
                        g.DrawLine(pen, lastPoint, e.Location);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        //this is to give the drawing a more smoother, less sharper look
                    }
                    pic.Invalidate();//refreshes the picturebox
                    lastPoint = e.Location;//keep assigning the lastPoint to the current mouse position
                }
            }
        }
        private void DrawCircle(PaintEventArgs e, int x, int y, int width, int height)
        {
            SolidBrush brush = new SolidBrush(pen.Color);
            Rectangle rtg = new Rectangle(x - width / 2, y - height / 2, width, height);
            e.Graphics.FillEllipse(brush, rtg);
        }
        private void Pic_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;//current mouse position: e.Location
            isMouseDown = true;//mouse button is down (clicked)
            imgTemp = new Bitmap(imgNow);
            pic.Image = imgTemp;
        }
        private void Add_Draw()
        {
            pic.MouseDown += Pic_Draw_MouseDown;
            pic.MouseMove += Pic_Draw_MouseMove;
            pic.MouseUp += Pic_Draw_MouseUp;
            pic.Cursor = new Cursor(getFilePath(@"..\..\..\Roga\Assets\Cursors\PenCursor.cur"));
        }
        private void Remove_Draw()
        {
            pic.MouseDown -= Pic_Draw_MouseDown;
            pic.MouseMove -= Pic_Draw_MouseMove;
            pic.MouseUp -= Pic_Draw_MouseUp;
            pic.Cursor = Cursors.Default;
        }
        #endregion

        //Eraser feature
        #region Eraser
        private void Pic_Eraser_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            lastPoint = Point.Empty;
            imgNow = new Bitmap(imgTemp);
            stackImage.Push(imgNow);
            isSave = false;
            pic.Image = imgNow;
        }
        private void Pic_Eraser_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)//check to see if the mouse button is down
            {
                if (lastPoint != null)//if our last point is not null, which in this case we have assigned above
                {
                    if (pic.Image == null)//if no available bitmap exists on the picturebox to draw on
                    {
                        //create a new bitmap
                        Bitmap bmp = new Bitmap(imgTemp);
                        pic.Image = bmp; //assign the picturebox.Image property to the bitmap created
                    }
                    using (Graphics g = Graphics.FromImage(imgTemp))
                    {//we need to create a Graphics object to draw on the picture box, its our main tool
                     //when making a Pen object, you can just give it color only or give it color and pen size
                        Rectangle rectangle = new Rectangle();
                        PaintEventArgs arg = new PaintEventArgs(g, rectangle);
                        DrawCircle(arg, e.X, e.Y, (int)pen.Width, (int)pen.Width);
                        g.DrawLine(pen, lastPoint, e.Location);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        //this is to give the drawing a more smoother, less sharper look
                    }
                    pic.Invalidate();//refreshes the picturebox
                    lastPoint = e.Location;//keep assigning the lastPoint to the current mouse position
                }
            }
        }
        private void Pic_Eraser_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;//current mouse position: e.Location
            isMouseDown = true;//mouse button is down (clicked)
            imgTemp = new Bitmap(imgNow);
            pic.Image = imgTemp;
        }
        private void Add_Eraser()
        {
            pen.Color = Color.White;
            pic.MouseDown += Pic_Eraser_MouseDown;
            pic.MouseMove += Pic_Eraser_MouseMove;
            pic.MouseUp += Pic_Eraser_MouseUp;
            pic.Cursor = new Cursor(getFilePath(@"..\..\..\Roga\Assets\Cursors\EraserCursor.cur"));
        }
        private void Remove_Eraser()
        {
            pen.Color = Color.Black;
            pic.MouseDown -= Pic_Eraser_MouseDown;
            pic.MouseMove -= Pic_Eraser_MouseMove;
            pic.MouseUp -= Pic_Eraser_MouseUp;
            pic.Cursor = Cursors.Default;
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

            //setCursor
            picGray.Cursor = picNegative.Cursor = picTrans.Cursor = picSepiaTone.Cursor = picRed.Cursor = picGreen.Cursor = picBlue.Cursor = Cursors.Hand;

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
                isSave = false;
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
                isSave = false;
            }    
        }

        private void picTrans_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithTransparency(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
                isSave = false;
            }    
        }

        private void picSepiaTone_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithSepiaTone(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
                isSave = false;
            }
        }

        private void picRed_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithRed(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
                isSave = false;
            }
        }

        private void picGreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithGreen(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
                isSave = false;
            }
        }

        private void picBlue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgNow = ImageWithBlue(imgNow);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
                isSave = false;
            }
        }

        #endregion

        //Shapes feature
        #region Shapes
        private int width, height, X, Y;
        private bool shiftDown = false; //if user press and hold shift key, shapes will be drawn based on the square 
        private Pen penRect = new Pen(Color.LightSkyBlue, 1); //pen for rectangle around shapes
        private string shapesType = "0";
        private Image imgTempShapes;
        public string ShapesType
        {
            get
            {
                return shapesType;
            }
            set
            {
                if (canMove == true)
                    CanMove = false;
                if (canResize == true)
                    canResize = false;
                shapesType = value;
            }
        }
        private Rectangle rectNow = new Rectangle(0, 0, 0, 0); //rectangle containing the shapes inside
        private Point startMouseMoveShape = new Point(0, 0);
        private bool canMove = false, canResize = false;
        private bool canMoveLine = false; //true when mouse down in mouse area of line
        private Point mouseDownLocation = new Point(0, 0);
        private PointF[] resizePoint = new PointF[8]; //8 point to resize on rectNow
        private int resizePointNowIndex = -1; //index of resize point now
        private Point headLine = new Point(0, 0), tailLine = new Point(0, 0); //head and tail of line
        private bool resizePointLineNowIndex = true; //true if head, false if tail
        public bool CanMove
        {
            get
            {
                return canMove;
            }
            set
            {
                canMove = value;
                imgTempShapes = new Bitmap(imgNow);
                if (!canMove && _mouseType == "shape")
                {
                    using (Graphics g = Graphics.FromImage(imgTempShapes))
                    {
                        switch (ShapesType)
                        {
                            case "0":
                                g.DrawLine(pen, headLine, tailLine);
                                break;
                            case "1":
                                g.DrawEllipse(pen, rectNow);
                                break;
                            case "2":
                                g.DrawRectangle(pen, rectNow);
                                break;
                            case "3":
                                g.DrawPolygon(pen, CaculateRightTriangle(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "4":
                                g.DrawPolygon(pen, CaculateIsoscelesTriangle(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "5":
                                g.DrawPolygon(pen, CaculateRhombus(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "6":
                                g.DrawPolygon(pen, CaculatePentagon(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "7":
                                g.DrawPolygon(pen, CaculateHexagon(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "8":
                                g.DrawPolygon(pen, CaculateFiveStar(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "9":
                                g.DrawPolygon(pen, CaculateSixStar(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "10":
                                g.DrawPolygon(pen, CaculateRightArrow(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "11":
                                g.DrawPolygon(pen, CaculateLeftArrow(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "12":
                                g.DrawPolygon(pen, CaculateUpArrow(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                            case "13":
                                g.DrawPolygon(pen, CaculateDownArrow(rectNow.X, rectNow.Y, rectNow.Width, rectNow.Height));
                                break;
                        }
                        imgNow = new Bitmap(imgTempShapes);
                        pic.Image = imgNow;
                        stackImage.Push(imgNow);
                        isSave = false;
                    }
                    headLine = new Point(0, 0);
                    tailLine = new Point(0, 0);
                    rectNow = new Rectangle(0, 0, 0, 0);
                    pic.Refresh();
                }
                if (!canMove && _mouseType == "addPicture")
                {
                    if (imgAddNow != null)
                    {
                        using (Graphics g = Graphics.FromImage(imgTempShapes))
                        {
                            g.DrawImage(imgAddNow, imgLocation);
                            imgNow = new Bitmap(imgTempShapes);
                            pic.Image = imgNow;
                            stackImage.Push(imgNow);
                            isSave = false;
                        }
                        imgLocation = new Point(0, 0);
                        pic.Refresh();
                    }    
                }    
            }
        }
        public bool CanResize
        {
            get { return canResize; }
            set
            {
                canResize = value;
                if (!canResize && _mouseType == "addPicture" && imgTemp != null)
                {
                    imgAddNow = new Bitmap(imgTemp);
                }    
            }
        }

        private void InitShapesDetails()
        {
            InitPenLineColorDetails();

            imgTempShapes = new Bitmap(imgNow);
            FlowLayoutPanel pShapes = new FlowLayoutPanel();
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
                lstButton[i].Cursor = Cursors.Hand;
                pShapes.Controls.Add(lstButton[i]);
            }

            lstButton[0].BorderSize = 1;

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
            //init 8 point to resize
            for (int i = 0; i < 8; i++)
            {
                resizePoint[i] = new PointF(0, 0);
            }
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
                if (canMove == false)
                    pic.Refresh();
                isMouseDown = true;
                if (shapesType != "0")
                {
                    //if mouseDownLocation in rectNow, set canMove & canResize
                    if ((e.X >= (rectNow.X - penRect.Width)) && (e.X <= (rectNow.X + rectNow.Width + penRect.Width))
                        && (e.Y >= (rectNow.Y - penRect.Width)) && (e.Y <= (rectNow.Y + rectNow.Height + penRect.Width)))
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if ((e.X >= resizePoint[i].X) && (e.X <= resizePoint[i].X + 5) 
                                && (e.Y >= resizePoint[i].Y) && (e.Y <= resizePoint[i].Y + 5))
                            {
                                resizePointNowIndex = i;
                                CanResize = true;
                                break;
                            }
                        }
                        CanMove = true;
                        startMouseMoveShape = e.Location;
                    }
                    else
                    {
                        bool flag = true;
                        for (int i = 0; i < 8; i++)
                        {
                            if ((e.X >= resizePoint[i].X) && (e.X <= resizePoint[i].X + 5)
                                && (e.Y >= resizePoint[i].Y) && (e.Y <= resizePoint[i].Y + 5))
                            {
                                flag = false;
                                startMouseMoveShape = e.Location;
                                break;
                            }
                        }
                        if (flag)
                            CanResize = false;
                        if (canMove && !canResize)
                            CanMove = false;
                        else if (canMove && canResize)
                            canMove = false;
                    }
                }  
                //if shapesType is line
                else
                {
                    if (!canMove)
                    {
                        headLine = e.Location;
                    }
                    else if (Math.Abs(headLine.X - e.X) <= 2 && Math.Abs(headLine.Y - e.Y) <= 2)
                    {
                        CanResize = true;
                        resizePointLineNowIndex = true;
                        canMoveLine = false;
                        canMove = true;
                    }    
                    else if (Math.Abs(tailLine.X - e.X) <= 2 && Math.Abs(tailLine.Y - e.Y) <= 2)
                    {
                        CanResize = true;
                        resizePointLineNowIndex = false;
                        canMoveLine = false;
                        canMove = true;
                    }   
                    else if (Math.Abs((float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - e.X) <= 2 
                        && Math.Abs((float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - e.Y) <= 2)
                    {
                        canMove = true;
                        canMoveLine = true;
                        canResize = false;
                    }    
                    else
                    {
                        if (canMove == true)
                            CanMove = false;
                        else
                            canMove = false;
                        CanResize = false;
                        canMoveLine = false;
                        headLine = e.Location;
                    }    
                }
            }
        }

        private void pic_Shapes_MouseMove(object sender, MouseEventArgs e)
        {
            //set pic.Cursor
            if (!canResize)
            {
                if (shapesType != "0" && canMove)
                {
                    if ((e.X >= (rectNow.X - penRect.Width)) && (e.X <= (rectNow.X + rectNow.Width + penRect.Width))
                        && (e.Y >= (rectNow.Y - penRect.Width)) && (e.Y <= (rectNow.Y + rectNow.Height + penRect.Width)))
                    {
                        pic.Cursor = Cursors.SizeAll;
                        for (int i = 0; i < 8; i++)
                        {
                            if ((e.X >= resizePoint[i].X) && (e.X <= resizePoint[i].X + 5)
                                && (e.Y >= resizePoint[i].Y) && (e.Y <= resizePoint[i].Y + 5))
                            {
                                resizePointNowIndex = i;
                                switch (resizePointNowIndex)
                                {
                                    case 0:
                                        pic.Cursor = Cursors.SizeNWSE;
                                        break;
                                    case 1:
                                        pic.Cursor = Cursors.SizeNS;
                                        break;
                                    case 2:
                                        pic.Cursor = Cursors.SizeNESW;
                                        break;
                                    case 3:
                                        pic.Cursor = Cursors.SizeWE;
                                        break;
                                    case 4:
                                        pic.Cursor = Cursors.SizeNWSE;
                                        break;
                                    case 5:
                                        pic.Cursor = Cursors.SizeNS;
                                        break;
                                    case 6:
                                        pic.Cursor = Cursors.SizeNESW;
                                        break;
                                    case 7:
                                        pic.Cursor = Cursors.SizeWE;
                                        break;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        bool flag = true;
                        for (int i = 0; i < 8; i++)
                        {
                            if ((e.X >= resizePoint[i].X) && (e.X <= resizePoint[i].X + 5)
                                && (e.Y >= resizePoint[i].Y) && (e.Y <= resizePoint[i].Y + 5))
                            {
                                flag = false;
                                resizePointNowIndex = i;
                                switch (resizePointNowIndex)
                                {
                                    case 0:
                                        pic.Cursor = Cursors.SizeNWSE;
                                        break;
                                    case 1:
                                        pic.Cursor = Cursors.SizeNS;
                                        break;
                                    case 2:
                                        pic.Cursor = Cursors.SizeNESW;
                                        break;
                                    case 3:
                                        pic.Cursor = Cursors.SizeWE;
                                        break;
                                    case 4:
                                        pic.Cursor = Cursors.SizeNWSE;
                                        break;
                                    case 5:
                                        pic.Cursor = Cursors.SizeNS;
                                        break;
                                    case 6:
                                        pic.Cursor = Cursors.SizeNESW;
                                        break;
                                    case 7:
                                        pic.Cursor = Cursors.SizeWE;
                                        break;
                                }
                                break;
                            }
                        }
                        if (flag)
                            pic.Cursor = Cursors.Default;
                    }
                }
                else if (canMove)
                {
                    if (Math.Abs(headLine.X - e.X) <= 2 && Math.Abs(headLine.Y - e.Y) <= 2)
                    {
                        pic.Cursor = Cursors.SizeNS;
                    }
                    else if (Math.Abs(tailLine.X - e.X) <= 2 && Math.Abs(tailLine.Y - e.Y) <= 2)
                    {
                        pic.Cursor = Cursors.SizeNS;
                    }
                    else if (Math.Abs((float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - e.X) <= 2
                        && Math.Abs((float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - e.Y) <= 2)
                    {
                        pic.Cursor = Cursors.SizeAll;
                    }
                    else
                    {
                        pic.Cursor = Cursors.Default;
                    }
                }
            }    

            //MouseDown but cannot move and resize, MouseMove event will draw a shapes
            if (isMouseDown && !canMove && !canResize)
            {
                pic.Refresh();
                pic.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                width = Math.Abs(e.X - X);
                height = Math.Abs(e.Y - Y);
                if (shiftDown)
                {
                    width = height = Math.Min(width, height);
                    rectNow.Width = rectNow.Height = width;
                }
                if (shapesType == "0")
                {
                    tailLine = e.Location;
                }   
                drawShapeOnPictureBox(new Rectangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
            }
            //MouseDown, can move but cannot resize, relocation the rectNow/relocation line when move
            else if (isMouseDown && canMove && !canResize)
            {
                pic.Refresh();
                pic.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                if (shapesType != "0")
                {
                    rectNow.Location = new Point((e.X - mouseDownLocation.X) + rectNow.Left, (e.Y - mouseDownLocation.Y) + rectNow.Top);
                    mouseDownLocation = e.Location;
                    drawShapeOnPictureBox(rectNow);
                }
                else if (canMoveLine)
                {
                    headLine = new Point((e.X - mouseDownLocation.X) + headLine.X, (e.Y - mouseDownLocation.Y) + headLine.Y);
                    tailLine = new Point((e.X - mouseDownLocation.X) + tailLine.X, (e.Y - mouseDownLocation.Y) + tailLine.Y);
                    mouseDownLocation = e.Location;
                    pic.CreateGraphics().DrawLine(pen, headLine, tailLine);
                }
            }
            //MouseDown and can resize, resize and relocation the rectNow / resize line
            else if (isMouseDown && canResize)
            {
                //set cursor and resize rectNow/line
                if (shapesType != "0")
                {
                    //resize and relocation rectNow
                    switch (resizePointNowIndex)
                    {
                        case 0:
                            if (e.Y <= rectNow.Bottom - 2 && e.X <= rectNow.Right)
                            {
                                pic.Cursor = Cursors.SizeNWSE;
                                rectNow.Location = new Point(e.X, e.Y);
                                rectNow.Size = new Size(rectNow.Width + (mouseDownLocation.X - e.X), rectNow.Height + (mouseDownLocation.Y - e.Y));
                            }
                            break;
                        case 1:
                            if (e.Y <= rectNow.Bottom - 2)
                            {
                                pic.Cursor = Cursors.SizeNS;
                                rectNow.Location = new Point(rectNow.Left, e.Y);
                                rectNow.Size = new Size(rectNow.Width, rectNow.Height + (mouseDownLocation.Y - e.Y));
                            }
                            break;
                        case 2:
                            if (e.X >= (rectNow.Left + 2) && e.Y <= (rectNow.Bottom - 2))
                            {
                                pic.Cursor = Cursors.SizeNESW;
                                rectNow.Location = new Point(rectNow.Left, e.Y);
                                rectNow.Size = new Size(rectNow.Width - (mouseDownLocation.X - e.X), rectNow.Height + (mouseDownLocation.Y - e.Y));
                            }
                            break;
                        case 3:
                            if (e.X >= (rectNow.Left + 2))
                            {
                                pic.Cursor = Cursors.SizeWE;
                                rectNow.Size = new Size(rectNow.Width - (mouseDownLocation.X - e.X), rectNow.Height);
                            }
                            break;
                        case 4:
                            if (e.X >= (rectNow.Left + 2) && e.Y >= (rectNow.Top + 2))
                            {
                                pic.Cursor = Cursors.SizeNWSE;
                                rectNow.Size = new Size(rectNow.Width - (mouseDownLocation.X - e.X), rectNow.Height - (mouseDownLocation.Y - e.Y));
                            }
                            break;
                        case 5:
                            if (e.Y >= (rectNow.Top + 2))
                            {
                                pic.Cursor = Cursors.SizeNS;
                                rectNow.Size = new Size(rectNow.Width, rectNow.Height - (mouseDownLocation.Y - e.Y));
                            }
                            break;
                        case 6:
                            if (e.X <= (rectNow.Right - 2) && e.Y >= (rectNow.Top + 2))
                            {
                                pic.Cursor = Cursors.SizeNESW;
                                rectNow.Location = new Point(e.X, rectNow.Top);
                                rectNow.Size = new Size(rectNow.Width + (mouseDownLocation.X - e.X), rectNow.Height - (mouseDownLocation.Y - e.Y));
                            }
                            break;
                        case 7:
                            if (e.X <= (rectNow.Right - 2))
                            {
                                pic.Cursor = Cursors.SizeWE;
                                rectNow.Location = new Point(e.X, rectNow.Top);
                                rectNow.Size = new Size(rectNow.Width + (mouseDownLocation.X - e.X), rectNow.Height);
                            }
                            break;
                    }
                }    
                else 
                {
                    pic.Cursor = Cursors.SizeNS;
                    if (resizePointLineNowIndex)
                    {
                        headLine = e.Location;
                    }
                    else
                        tailLine = e.Location;
                }    
                mouseDownLocation = e.Location;
                pic.Refresh();
                pic.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                //draw shape
                drawShapeOnPictureBox(rectNow);
            }
        }

        private void pic_Shapes_MouseUp(object sender, MouseEventArgs e)
        {
            if (!canMove)
            {

                using (Graphics g = Graphics.FromImage(imgNow))
                {
                    if (e.X != X || e.Y != Y)
                    {
                        canMove = true;
                        //canResize = true;
                    }
                    else
                    {
                        canMove = false;
                        canResize = false;
                    }
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    if (shapesType != "0")
                    {
                        int width, height;
                        width = Math.Abs(e.X - X);
                        height = Math.Abs(e.Y - Y);
                        if (shiftDown)
                        {
                            width = height = Math.Min(width, height);
                        }
                    }   
                    else
                    {
                        tailLine = e.Location;
                    }
                    if (canMove)
                        drawShapeOnPictureBox(new Rectangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height));
                    //caculate resize point
                    if (canMove)
                    {
                        if (shapesType != "0")
                        {
                            rectNow = new Rectangle(Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
                            pic.CreateGraphics().DrawRectangle(penRect, rectNow);
                        }
                        createAndDrawResizePoint();
                    }    
                    else
                    {
                        if (e.Location != mouseDownLocation)
                            CanMove = false;
                        else
                            canMove = false;
                        CanResize = false;
                        if (shapesType == "0")
                            canMoveLine = false;
                    }
                }
            }
            else
            {
                if (shapesType != "0")
                    pic.CreateGraphics().DrawRectangle(penRect, rectNow);
                createAndDrawResizePoint();   
            }    
            isMouseDown = false;
            shiftDown = false;
            CanResize = false;
            //pic.Invalidate();
        }

        private void drawShapeOnPictureBox(Rectangle rect)
        {
            switch (ShapesType)
            {
                case "0":
                    pic.CreateGraphics().DrawLine(pen, headLine, tailLine);
                    break;
                case "1":
                    pic.CreateGraphics().DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    break;
                case "2":
                    pic.CreateGraphics().DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    break;
                case "3":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateRightTriangle(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "4":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateIsoscelesTriangle(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "5":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateRhombus(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "6":
                    pic.CreateGraphics().DrawPolygon(pen, CaculatePentagon(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "7":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateHexagon(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "8":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateFiveStar(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "9":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateSixStar(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "10":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateRightArrow(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "11":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateLeftArrow(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "12":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateUpArrow(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
                case "13":
                    pic.CreateGraphics().DrawPolygon(pen, CaculateDownArrow(rect.X, rect.Y, rect.Width, rect.Height));
                    break;
            }
        }

        private void createAndDrawResizePoint()
        {
            if (shapesType != "0")
            {
                resizePoint[0].X = rectNow.Left - 2;
                resizePoint[0].Y = rectNow.Top - 2;
                resizePoint[1].X = rectNow.Left + (float)rectNow.Width / 2 - 2;
                resizePoint[1].Y = rectNow.Top - 2;
                resizePoint[2].X = rectNow.Left + rectNow.Width - 2;
                resizePoint[2].Y = rectNow.Top - 2;
                resizePoint[3].X = rectNow.Left + rectNow.Width - 2;
                resizePoint[3].Y = rectNow.Top + (float)rectNow.Height / 2 - 2;
                resizePoint[4].X = rectNow.Left + rectNow.Width - 2;
                resizePoint[4].Y = rectNow.Top + rectNow.Height - 2;
                resizePoint[5].X = resizePoint[1].X;
                resizePoint[5].Y = rectNow.Top + rectNow.Height - 2;
                resizePoint[6].X = rectNow.Left - 2;
                resizePoint[6].Y = rectNow.Top + rectNow.Height - 2;
                resizePoint[7].X = rectNow.Left - 2;
                resizePoint[7].Y = resizePoint[3].Y;
                for (int i = 0; i < 8; i++)
                {
                    pic.CreateGraphics().FillRectangle(Brushes.White, resizePoint[i].X, resizePoint[i].Y, 5, 5);
                    pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), resizePoint[i].X, resizePoint[i].Y, 5, 5);
                }
            }
            else
            {
                pic.CreateGraphics().FillRectangle(Brushes.White, headLine.X - 2, headLine.Y - 2, 5, 5);
                pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), headLine.X - 2, headLine.Y - 2, 5, 5);

                pic.CreateGraphics().FillRectangle(Brushes.White, tailLine.X - 2, tailLine.Y - 2, 5, 5);
                pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), tailLine.X - 2, tailLine.Y - 2, 5, 5);

                pic.CreateGraphics().FillRectangle(Brushes.White,
                                                    (float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - 2,
                                                    (float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - 2,
                                                    5, 5);
                pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1),
                                                    (float)Math.Abs(headLine.X - tailLine.X) / 2 + Math.Min(headLine.X, tailLine.X) - 2,
                                                    (float)Math.Abs(headLine.Y - tailLine.Y) / 2 + Math.Min(headLine.Y, tailLine.Y) - 2,
                                                    5, 5);
            }
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

        int crpX = 0, crpY = 0, rectW, rectH, slX, slY;
        Pen crpPen = new Pen(Color.White);
        bool canDelete = false;

        //resize picturebox when change image size
        private void resizePic(Image imageSource)
        {
            pic.Size = imageSource.Size;
            setLocation(imageSource);
        }

        private void InitCropDetails()
        {
            resetSelect();
            CustomButton.VBButton delete = new CustomButton.VBButton();
            delete.Location = new Point(20, 210);
            delete.Size = new Size(65, 20);
            delete.BackColor = Color.Black;
            delete.Text = "delete";
            delete.ForeColor = Color.White;
            delete.BorderRadius = 0;
            delete.BorderColor = Color.White;
            delete.BorderSize = 1;
            delete.MouseDown += new MouseEventHandler(delete_MouseDown);
            
            panel3.Controls.Add(delete);

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

            //set size
            picSelect.Size = picCrop.Size = picRotate.Size = picFlip.Size = new Size(55, 55);

            //set sizemode
            picSelect.SizeMode = picCrop.SizeMode = picRotate.SizeMode = picFlip.SizeMode = PictureBoxSizeMode.StretchImage;

            //set position
            picSelect.Location = new Point(23, 20);
            panel3.Controls.Add(picSelect);
            picCrop.Location = new Point(120, 20);
            panel3.Controls.Add(picCrop);
            picRotate.Location = new Point(23, 115);
            panel3.Controls.Add(picRotate);
            picFlip.Location = new Point(120, 115);
            panel3.Controls.Add(picFlip);

            //set event
            picSelect.Click += new EventHandler(picSelect_Click);
            picCrop.Click += new EventHandler(picCrop_Click);
            picRotate.Click += new EventHandler(picRotate_Click);
            picFlip.Click += new EventHandler(picFlip_Click);

            picSelect.MouseEnter += new EventHandler(Select_Enter);
            picCrop.MouseEnter += new EventHandler(Select_Enter);
            picRotate.MouseEnter += new EventHandler(Select_Enter);
            picFlip.MouseEnter += new EventHandler(Select_Enter);
        }

        private void delete_MouseDown(object sender, MouseEventArgs e)
        {
            if (canDelete && e.Button == MouseButtons.Left)
            {
                Image temp = new Bitmap(imgNow);
                Graphics.FromImage(temp).FillRectangle(Brushes.White, crpX, crpY, rectW, rectH);
                imgNow = new Bitmap(temp);
                stackImage.Push(imgNow);
                pic.Image = imgNow;
                isSave = false;
                pic.Refresh();
                rectW = imgNow.Width;
                rectH = imgNow.Height;
                crpX = crpY = slX = slY = 0;
                canDelete = false;
            }    
        }

        private void Select_Enter(object sender, EventArgs e)
        {
            PictureBox pictmp = (PictureBox)sender;
            pictmp.Cursor = Cursors.Hand;
        }

        //select 
        private void pic_Select_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            if (isSelect == true)
            {
                Cursor = Cursors.Cross;
            }
        }

        private void pic_Select_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (isSelect == true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Cursor = Cursors.Cross;
                    crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    crpPen.Color = Color.Black;
                    crpPen.Width = 2;

                    crpX = slX = e.X;
                    crpY = slY = e.Y;
                }
            }
        }

        private void pic_Select_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isSelect == true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    pic.Refresh();
                    canDelete = true;

                    crpX = Math.Min(slX, e.X);
                    if (crpX < 0)
                        crpX = 0;
                    crpY = Math.Min(slY, e.Y);
                    if (crpY < 0)
                        crpY = 0;

                    rectW = Math.Abs(e.X - slX);
                    if (e.X < 0)
                        rectW = Math.Abs(0 - slX);
                    if (rectW + crpX >= imgNow.Width)
                        rectW = imgNow.Width - slX - 1;
                    rectH = Math.Abs(e.Y - slY);
                    if (e.Y < 0)
                        rectH = Math.Abs(0 - slY);
                    if (rectH + crpY >= imgNow.Height)
                        rectH = imgNow.Height - slY - 1;
                    Graphics select = pic.CreateGraphics();
                    select.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                    select.Dispose();
                }
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
            crpX = slX = 0;
            crpY = slY = 0;
            return (Image)crpImage;
        }

        private void picCrop_Click(object sender, EventArgs e)
        {
            canDelete = false;
            imgNow = CropImage(imgNow);
            resizePic(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
            isSave = false;
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
            canDelete = false;
            imgNow = Rotate_Image(imgNow);
            resizePic(imgNow);
            rectW = imgNow.Width;
            rectH = imgNow.Height;
            crpX = crpY = slX = slY = 0;
            pic.Image = imgNow;
            stackImage.Push(imgNow);
            isSave = false;
            resetSelect();
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
            imgNow = Flip_Image(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
            isSave = false;
            resetSelect();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }

        private void resetSelect()
        {
            canDelete = false;
            rectW = imgNow.Width;
            rectH = imgNow.Height;
            crpX = crpY = slX = slY = 0;
        }

        #endregion

        //Add image
        #region Add image
        Point imgLocation = new Point(0, 0);
        Image imgAddNow = null;
        Image imgSourceAdd = null;

        private void InitAddImage(Image img)
        {
            //resize img to draw on pic
            int tempW = imgNow.Width - img.Width;
            int tempH = imgNow.Height - img.Height;
            if (tempH > 0 && tempW < 0)
            {
                img = resizeImage(img, new Size(imgNow.Width, (int)(img.Height * ((float)imgNow.Width / img.Width) - 0.5)));
            }    
            else if (tempH < 0 && tempW > 0)
            {
                img = resizeImage(img, new Size((int)(img.Width * ((float)imgNow.Height / img.Height) - 0.5), imgNow.Height));
            }    
            else if (tempH > 0 && tempW > 0)
            {
                if (imgNow.Width - img.Width > imgNow.Height - img.Height)
                {
                    img = resizeImage(img, new Size((int)(img.Width * ((float)imgNow.Height / img.Height) - 0.5), imgNow.Height));
                }    
                else
                {
                    img = resizeImage(img, new Size(imgNow.Width, (int)(img.Height * ((float)imgNow.Width / img.Width) - 0.5)));
                }    
            }    
            else
            {
                if (imgNow.Width - img.Width > imgNow.Height - img.Height)
                {
                    img = resizeImage(img, new Size((int)(img.Width * ((float)imgNow.Height / img.Height) - 0.5), imgNow.Height));
                }
                else
                {
                    img = resizeImage(img, new Size(imgNow.Width, (int)(img.Height * ((float)imgNow.Width / img.Width) - 0.5)));
                }
            }
            imgAddNow = new Bitmap(img);
            pic.CreateGraphics().DrawImage(imgAddNow, imgLocation);
            pic.CreateGraphics().DrawRectangle(penRect, new Rectangle(imgLocation, imgAddNow.Size));
            //draw resizePoint
            resizePoint[0].X = imgLocation.X - 2;
            resizePoint[0].Y = imgLocation.Y - 2;
            resizePoint[1].X = imgLocation.X + (float)imgAddNow.Width / 2 - 2;
            resizePoint[1].Y = imgLocation.Y - 2;
            resizePoint[2].X = imgLocation.X + imgAddNow.Width - 2;
            resizePoint[2].Y = imgLocation.Y - 2;
            resizePoint[3].X = imgLocation.X + imgAddNow.Width - 2;
            resizePoint[3].Y = imgLocation.Y + (float)imgAddNow.Height / 2 - 2;
            resizePoint[4].X = imgLocation.X + imgAddNow.Width - 2;
            resizePoint[4].Y = imgLocation.Y + imgAddNow.Height - 2;
            resizePoint[5].X = resizePoint[1].X;
            resizePoint[5].Y = imgLocation.Y + imgAddNow.Height - 2;
            resizePoint[6].X = imgLocation.X - 2;
            resizePoint[6].Y = imgLocation.Y + imgAddNow.Height - 2;
            resizePoint[7].X = imgLocation.X - 2;
            resizePoint[7].Y = resizePoint[3].Y;
            for (int i = 0; i < 8; i++)
            {
                pic.CreateGraphics().FillRectangle(Brushes.White, resizePoint[i].X, resizePoint[i].Y, 5, 5);
                pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), resizePoint[i].X, resizePoint[i].Y, 5, 5);
            }

            penRect.DashStyle = DashStyle.DashDot;
            imgSourceAdd = new Bitmap(imgAddNow);
            imgTemp = new Bitmap(imgAddNow); ;
            canMove = true;
        }

        private void pic_AddImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                X = e.X;
                Y = e.Y;
                mouseDownLocation = e.Location;
                if (canMove == false)
                    return;
                    //pic.Refresh();
                isMouseDown = true;
                //if mouseDownLocation in rectNow, set canMove & canResize
                if ((e.X >= (imgLocation.X - penRect.Width)) && (e.X <= (imgLocation.X + imgAddNow.Width + penRect.Width))
                    && (e.Y >= (imgLocation.Y - penRect.Width)) && (e.Y <= (imgLocation.Y + imgAddNow.Height + penRect.Width)))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((e.X >= resizePoint[i].X) && (e.X <= resizePoint[i].X + 5)
                            && (e.Y >= resizePoint[i].Y) && (e.Y <= resizePoint[i].Y + 5))
                        {
                            resizePointNowIndex = i;
                            CanResize = true;
                            break;
                        }
                    }
                    CanMove = true;
                    startMouseMoveShape = e.Location;
                }
                else
                {
                    bool flag = true;
                    for (int i = 0; i < 8; i++)
                    {
                        if ((e.X >= resizePoint[i].X) && (e.X <= resizePoint[i].X + 5)
                            && (e.Y >= resizePoint[i].Y) && (e.Y <= resizePoint[i].Y + 5))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        canResize = false;
                    else
                    {
                        canMove = true;
                        canResize = true;
                    }    
                    if (!canResize)
                    {
                        CanMove = false;
                    }    
                    //if (canMove && !canResize)
                    //    CanMove = false;
                    //else if (canMove && canResize)
                    //    canMove = false;

                }
            }    
        }

        private void pic_AddImage_MouseMove(object sender, MouseEventArgs e)
        {
            //set pic.Cursor
            if (!canResize)
            {
                if (canMove)
                {
                    if ((e.X >= (imgLocation.X - penRect.Width)) && (e.X <= (imgLocation.X + imgAddNow.Width + penRect.Width))
                        && (e.Y >= (imgLocation.Y - penRect.Width)) && (e.Y <= (imgLocation.Y + imgAddNow.Height + penRect.Width)))
                    {
                        pic.Cursor = Cursors.SizeAll;
                        for (int i = 0; i < 8; i++)
                        {
                            if ((e.X >= resizePoint[i].X) && (e.X <= resizePoint[i].X + 5)
                                && (e.Y >= resizePoint[i].Y) && (e.Y <= resizePoint[i].Y + 5))
                            {
                                resizePointNowIndex = i;
                                switch (resizePointNowIndex)
                                {
                                    case 0:
                                        pic.Cursor = Cursors.SizeNWSE;
                                        break;
                                    case 1:
                                        pic.Cursor = Cursors.SizeNS;
                                        break;
                                    case 2:
                                        pic.Cursor = Cursors.SizeNESW;
                                        break;
                                    case 3:
                                        pic.Cursor = Cursors.SizeWE;
                                        break;
                                    case 4:
                                        pic.Cursor = Cursors.SizeNWSE;
                                        break;
                                    case 5:
                                        pic.Cursor = Cursors.SizeNS;
                                        break;
                                    case 6:
                                        pic.Cursor = Cursors.SizeNESW;
                                        break;
                                    case 7:
                                        pic.Cursor = Cursors.SizeWE;
                                        break;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        bool flag = true;
                        for (int i = 0; i < 8; i++)
                        {
                            if ((e.X >= resizePoint[i].X) && (e.X <= resizePoint[i].X + 5)
                                && (e.Y >= resizePoint[i].Y) && (e.Y <= resizePoint[i].Y + 5))
                            {
                                flag = false;
                                resizePointNowIndex = i;
                                switch (resizePointNowIndex)
                                {
                                    case 0:
                                        pic.Cursor = Cursors.SizeNWSE;
                                        break;
                                    case 1:
                                        pic.Cursor = Cursors.SizeNS;
                                        break;
                                    case 2:
                                        pic.Cursor = Cursors.SizeNESW;
                                        break;
                                    case 3:
                                        pic.Cursor = Cursors.SizeWE;
                                        break;
                                    case 4:
                                        pic.Cursor = Cursors.SizeNWSE;
                                        break;
                                    case 5:
                                        pic.Cursor = Cursors.SizeNS;
                                        break;
                                    case 6:
                                        pic.Cursor = Cursors.SizeNESW;
                                        break;
                                    case 7:
                                        pic.Cursor = Cursors.SizeWE;
                                        break;
                                }
                                break;
                            }
                        }
                        if (flag)
                            pic.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    pic.Cursor = Cursors.Default;
                }    
            }

            //MouseDown but cannot move and resize, MouseMove do nothing
            if (isMouseDown && !canMove && !canResize)
            {

            }
            //MouseDown, can move but cannot resize, relocation imgLocation when move
            else if (isMouseDown && canMove && !canResize)
            {
                pic.Refresh();
                pic.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                imgLocation = new Point((e.X - mouseDownLocation.X) + imgLocation.X, (e.Y - mouseDownLocation.Y) + imgLocation.Y);
                mouseDownLocation = e.Location;
                pic.CreateGraphics().DrawImage(imgAddNow, imgLocation);

            }
            //MouseDown and can resize, resize and relocation the imgAddNow, imgLocation
            else if (isMouseDown && canResize)
            {
                //set cursor and resize rectNow/line
                //resize and relocation rectNow
                //if (!shiftDown)
                {
                    switch (resizePointNowIndex)
                    {
                        case 0:
                            if (e.Y <= imgLocation.Y + imgTemp.Height - 2 && e.X <= imgLocation.X + imgTemp.Width - 2)
                            {
                                pic.Cursor = Cursors.SizeNWSE;
                                imgLocation = new Point(e.X, e.Y);
                                imgTemp = resizeImage(imgSourceAdd, new Size(imgTemp.Width + (mouseDownLocation.X - e.X), imgTemp.Height + (mouseDownLocation.Y - e.Y)));
                            }
                            break;
                        case 1:
                            if (e.Y <= imgLocation.Y + imgTemp.Height - 2)
                            {
                                pic.Cursor = Cursors.SizeNS;
                                imgLocation = new Point(imgLocation.X, e.Y);
                                imgTemp = resizeImage(imgSourceAdd, new Size(imgTemp.Width, imgTemp.Height + (mouseDownLocation.Y - e.Y)));
                            }
                            break;
                        case 2:
                            if (e.X >= (imgLocation.X + 2) && e.Y <= (imgLocation.Y + imgTemp.Height - 2))
                            {
                                pic.Cursor = Cursors.SizeNESW;
                                imgLocation = new Point(rectNow.Left, e.Y);
                                imgTemp = resizeImage(imgSourceAdd, new Size(imgTemp.Width - (mouseDownLocation.X - e.X), imgTemp.Height + (mouseDownLocation.Y - e.Y)));
                            }
                            break;
                        case 3:
                            if (e.X >= (imgLocation.X + 2))
                            {
                                pic.Cursor = Cursors.SizeWE;
                                imgTemp = resizeImage(imgSourceAdd, new Size(imgTemp.Width - (mouseDownLocation.X - e.X), imgTemp.Height));
                            }
                            break;
                        case 4:
                            if (e.X >= (imgLocation.X + 2) && e.Y >= (imgLocation.Y + 2))
                            {
                                pic.Cursor = Cursors.SizeNWSE;
                                imgTemp = resizeImage(imgSourceAdd, new Size(imgTemp.Width - (mouseDownLocation.X - e.X), imgTemp.Height - (mouseDownLocation.Y - e.Y)));
                            }
                            break;
                        case 5:
                            if (e.Y >= (imgLocation.Y + 2))
                            {
                                pic.Cursor = Cursors.SizeNS;
                                imgTemp = resizeImage(imgSourceAdd, new Size(imgTemp.Width, imgTemp.Height - (mouseDownLocation.Y - e.Y)));
                            }
                            break;
                        case 6:
                            if (e.X <= (imgLocation.X + imgTemp.Width - 2) && e.Y >= (imgLocation.Y + 2))
                            {
                                pic.Cursor = Cursors.SizeNESW;
                                imgLocation = new Point(e.X, imgLocation.Y);
                                imgTemp = resizeImage(imgSourceAdd, new Size(imgTemp.Width + (mouseDownLocation.X - e.X), imgTemp.Height - (mouseDownLocation.Y - e.Y)));
                            }
                            break;
                        case 7:
                            if (e.X <= (imgLocation.X + imgTemp.Width - 2))
                            {
                                pic.Cursor = Cursors.SizeWE;
                                imgLocation = new Point(e.X, imgLocation.Y);
                                imgTemp = resizeImage(imgSourceAdd, new Size(imgTemp.Width + (mouseDownLocation.X - e.X), imgTemp.Height));
                            }
                            break;
                    }
                }


                mouseDownLocation = e.Location;
                pic.Refresh();
                pic.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                //draw shape
                pic.CreateGraphics().DrawImage(imgTemp, imgLocation);
            }
        }

        private void pic_AddImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (canResize)
            {
                CanResize = false;
            }
            if (canMove)
            {
                resizePoint[0].X = imgLocation.X - 2;
                resizePoint[0].Y = imgLocation.Y - 2;
                resizePoint[1].X = imgLocation.X + (float)imgAddNow.Width / 2 - 2;
                resizePoint[1].Y = imgLocation.Y - 2;
                resizePoint[2].X = imgLocation.X + imgAddNow.Width - 2;
                resizePoint[2].Y = imgLocation.Y - 2;
                resizePoint[3].X = imgLocation.X + imgAddNow.Width - 2;
                resizePoint[3].Y = imgLocation.Y + (float)imgAddNow.Height / 2 - 2;
                resizePoint[4].X = imgLocation.X + imgAddNow.Width - 2;
                resizePoint[4].Y = imgLocation.Y + imgAddNow.Height - 2;
                resizePoint[5].X = resizePoint[1].X;
                resizePoint[5].Y = imgLocation.Y + imgAddNow.Height - 2;
                resizePoint[6].X = imgLocation.X - 2;
                resizePoint[6].Y = imgLocation.Y + imgAddNow.Height - 2;
                resizePoint[7].X = imgLocation.X - 2;
                resizePoint[7].Y = resizePoint[3].Y;
                for (int i = 0; i < 8; i++)
                {
                    pic.CreateGraphics().FillRectangle(Brushes.White, resizePoint[i].X, resizePoint[i].Y, 5, 5);
                    pic.CreateGraphics().DrawRectangle(new Pen(Color.Black, 1), resizePoint[i].X, resizePoint[i].Y, 5, 5);
                }
                pic.CreateGraphics().DrawRectangle(penRect, new Rectangle(imgLocation, imgTemp.Size));
            }    
            else
            {
                CanResize = false;
            }    
            
            isMouseDown = false;
            shiftDown = false;
        }

        private void addAddImageEvent()
        {
            this.KeyDown += this_Shapes_ShiftKey;
            this.KeyUp += this_Shapes_KeyUp;
            pic.MouseDown += new MouseEventHandler(pic_AddImage_MouseDown);
            pic.MouseMove += new MouseEventHandler(pic_AddImage_MouseMove);
            pic.MouseUp += new MouseEventHandler(pic_AddImage_MouseUp);
        }

        private void removeAddImageEvent()
        {
            this.KeyDown -= this_Shapes_ShiftKey;
            this.KeyUp -= this_Shapes_KeyUp;
            pic.MouseDown -= new MouseEventHandler(pic_AddImage_MouseDown);
            pic.MouseMove -= new MouseEventHandler(pic_AddImage_MouseMove);
            pic.MouseUp -= new MouseEventHandler(pic_AddImage_MouseUp);
        }

        #endregion

        //Text feature
        #region AddText
        private CustomTextbox txtAddText = new CustomTextbox();
        private Label lbAddText = new Label();
        private bool addtext = false;
        private bool isEnterText = false;
        private Font fontText = new Font("Arial", 13);
        private ComboBox cbFont = new ComboBox();
        private NumericUpDown numSize = new NumericUpDown();
        private GroupBox grpFontStyle = new GroupBox();
        private CheckBox cbItalic = new CheckBox();
        private CheckBox cbBold = new CheckBox();
        private CheckBox cbUnderline = new CheckBox();
        private Button btnCustom = new Button();

        private void InitTextDetails()
        {
            txtAddText.Font = fontText;

            foreach (FontFamily oneFontFamily in FontFamily.Families)
            {
                cbFont.Items.Add(oneFontFamily.Name);
            }
            cbFont.Location = new Point(10, 10);
            cbFont.Text = txtAddText.Font.Name.ToString();
            cbFont.SelectedIndexChanged += new EventHandler(cbFont_SelectedIndexChanged);
            panel3.Controls.Add(cbFont);

            numSize.Value = 13;
            numSize.Minimum = 1;
            numSize.Location = new Point(10, 50);
            numSize.ValueChanged += new EventHandler(numSize_ValueChanged);
            panel3.Controls.Add((NumericUpDown)numSize);

            grpFontStyle.Text = "Font Style";
            grpFontStyle.BackColor = SystemColors.Control;
            cbItalic.Checked = false;
            cbItalic.Text = "I";
            cbItalic.Font = new Font(Font, FontStyle.Italic);
            cbItalic.Location = new Point(10, 20);
            cbBold.Checked = false;
            cbBold.Text = "B";
            cbBold.Font = new Font(Font, FontStyle.Bold);
            cbBold.Location = new Point(10, 40);
            cbUnderline.Checked = false;
            cbUnderline.Text = "U";
            cbUnderline.Font = new Font(Font, FontStyle.Underline);
            cbUnderline.Location = new Point(10, 60);
            grpFontStyle.Controls.Add(cbBold);
            grpFontStyle.Controls.Add(cbItalic);
            grpFontStyle.Controls.Add(cbUnderline);
            grpFontStyle.AutoSize = false;
            grpFontStyle.Size = new Size(190, 12);
            grpFontStyle.Location = new Point(10, 90);
            grpFontStyle.AutoSize = true;
            cbBold.CheckedChanged += new EventHandler(FontStyle_Changed);
            cbItalic.CheckedChanged += new EventHandler(FontStyle_Changed);
            cbUnderline.CheckedChanged += new EventHandler(FontStyle_Changed);
            panel3.Controls.Add(grpFontStyle);

            btnCustom.Size = new Size(50, 25);
            btnCustom.BackColor = Color.LightGray;
            btnCustom.Text = "Font";
            btnCustom.Click += new EventHandler(btnCustom_Click);
            btnCustom.Location = new Point(140, 13);
            panel3.Controls.Add(btnCustom);

            InitAddTextColor();

            pic.MouseDown += pic_AddText_MouseDown;
            pic.MouseUp += pic_AddText_MouseUp;
            pic.MouseMove += pic_AddText_MouseMove;
            pic.MouseEnter += pic_AddText_MouseEnter;
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            DialogResult result = font.ShowDialog();
            fontText = font.Font;
            txtAddText.SelectionFont = font.Font;
        }

        //Init color for text
        #region ColorText
        private void InitAddTextColor()
        {
            CustomButton.VBButton selectedColor = new CustomButton.VBButton();
            selectedColor.Size = new Size(25, 25);
            selectedColor.Location = new Point(30, 680);
            selectedColor.BorderColor = Color.White;
            selectedColor.BorderSize = 1;
            selectedColor.BorderRadius = 0;
            panel3.Controls.Add(selectedColor);
            Label l = new Label();
            l.Text = "Selected Color";
            l.ForeColor = Color.White;
            l.Location = new Point(60, 685);
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
                    matrixColor[i, j].Location = new Point(30 + j * 34 + j * 25, 100 + grpFontStyle.Size.Height + i * 20 + i * 25);
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
            edit.Location = new Point(28, 610);
            edit.BackgroundImage = Image.FromFile(getFilePath(@"..\..\..\Roga\Assets\Images\EditColor.png"));
            edit.MouseDown += delegate (object sender, MouseEventArgs e) { editColor_MouseDown(sender, e, matrixColor); };
            panel3.Controls.Add(edit);
            selectedColor.BackColor = pen.Color;
        }
        #endregion

        //Set size of textbox
        #region size
        int tboxX = 0, tboxY = 0;
        int tboxWidth = 0, tboxHeight = 0;
        private int txtX = 0, txtY = 0;

        private void new_textbox()
        {
            tboxX = tboxY = txtX = txtY = 0;
        }
        private void pic_AddText_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            if (addtext == false)
            {
                Cursor = Cursors.Cross;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void pic_AddText_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                if (addtext == true)
                {
                    Draw_Text();
                    addtext = false;

                    tboxX = txtX = e.X;
                    tboxY = txtY = e.Y;
                    return;
                }
                Cursor = Cursors.Cross;
                crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                crpPen.Color = Color.Black;
                crpPen.Width = 2;

                tboxX = txtX = e.X;
                tboxY = txtY = e.Y;
                addtext = true;
            }
        }

        private void pic_AddText_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (addtext == true)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        pic.Refresh();

                        tboxX = Math.Min(txtX, e.X);
                        if (tboxX < 0)
                            tboxX = 0;
                        tboxY = Math.Min(txtY, e.Y);
                        if (tboxY < 0)
                            tboxY = 0;

                        tboxWidth = Math.Abs(e.X - txtX);
                        if (e.X < 0)
                            tboxWidth = Math.Abs(0 - txtX);
                        if (tboxWidth + tboxX >= imgNow.Width)
                            tboxWidth = imgNow.Width - txtX - 1;
                        tboxHeight = Math.Abs(e.Y - txtY);
                        if (e.Y < 0)
                            tboxHeight = Math.Abs(0 - txtY);
                        if (tboxHeight + tboxY >= imgNow.Height)
                            tboxHeight = imgNow.Height - txtY - 1;
                        Graphics select = pic.CreateGraphics();
                        select.DrawRectangle(crpPen, tboxX, tboxY, tboxWidth, tboxHeight);
                        select.Dispose();
                    }
                }
            }

        }

        private void pic_AddText_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);

            pic.Refresh();
            if (e.Button == MouseButtons.Left)
            {
                if (addtext == true)
                {
                    txtAddText.Text = "Add text";
                    txtAddText.AutoSize = true;
                    txtAddText.Size = new Size(tboxWidth, tboxHeight);
                    txtAddText.Location = new Point(tboxX, tboxY);
                    txtAddText.BorderStyle = BorderStyle.FixedSingle;
                    txtAddText.Enter += new EventHandler(txt_Enter);
                    txtAddText.TextChanged += new EventHandler(txtAddText_TextChanged);
                    pic.Controls.Add(txtAddText);

                    lbAddText.Text = "Add text";
                    lbAddText.Font = txtAddText.Font;
                    lbAddText.Location = new Point(0, 0);
                    lbAddText.BackColor = Color.Transparent;
                    lbAddText.AutoSize = true;
                    lbAddText.BringToFront();
                    txtAddText.Controls.Add(lbAddText);
                    addtext = true;
                    Cursor = Cursors.Default;
                }
            }
        }

        private void txtAddText_TextChanged(object sender, EventArgs e)
        {
            lbAddText.Text = txtAddText.Text;
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            if (txtAddText.Text == "Add text")
            {
                txtAddText.Text = "";
            }
            isEnterText = true;
        }
        #endregion

        private void Draw_Text()
        {
            if (isEnterText == true)
            {
                Bitmap bit = new Bitmap(pic.Image);
                Graphics g = Graphics.FromImage(bit);
                g.DrawString(txtAddText.Text, fontText, new SolidBrush(txtAddText.ForeColor), txtAddText.Location.X, txtAddText.Location.Y);
                txtAddText.Text = "";
                imgNow = new Bitmap(bit);
                pic.Image = imgNow;
                stackImage.Push(imgNow);
                isSave = false;
            }
            pic.Controls.Remove(txtAddText);
            
        }

        private void cbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            fontText = new Font(cbFont.Text, fontText.Size);
            txtAddText.SelectionFont = lbAddText.Font = fontText;
        }

        private void numSize_ValueChanged(object sender, EventArgs e)
        {
            fontText = new Font(fontText.FontFamily.Name, (float)numSize.Value);
            txtAddText.SelectionFont = lbAddText.Font = fontText;
        }

        private void FontStyle_Changed(object sender, EventArgs e)
        {
            if (cbBold.Checked == true)
            {
                if (cbItalic.Checked == true)
                {
                    if (cbUnderline.Checked == true)
                    {
                        Font newFont = new Font(fontText.FontFamily.Name, fontText.Size, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
                        fontText = newFont;

                    }
                    else
                    {
                        Font newFont1 = new Font(fontText.FontFamily.Name, fontText.Size, FontStyle.Bold | FontStyle.Italic);
                        fontText = newFont1;

                    }
                }
                else
                {
                    if (cbUnderline.Checked == true)
                    {
                        Font newFont2 = new Font(fontText.FontFamily.Name, fontText.Size, FontStyle.Bold | FontStyle.Underline);
                        fontText = newFont2;

                    }
                    else
                    {
                        Font newFont1 = new Font(fontText.FontFamily.Name, fontText.Size, FontStyle.Bold);
                        fontText = newFont1;

                    }
                }

            }
            else
            {
                if (cbItalic.Checked == true)
                {
                    if (cbUnderline.Checked == true)
                    {
                        Font newFont = new Font(fontText.FontFamily.Name, fontText.Size, FontStyle.Italic | FontStyle.Underline);
                        fontText = newFont;

                    }
                    else
                    {
                        Font newFont1 = new Font(fontText.FontFamily.Name, fontText.Size, FontStyle.Italic);
                        fontText = newFont1;

                    }
                }
                else
                {
                    if (cbUnderline.Checked == true)
                    {
                        Font newFont2 = new Font(fontText.FontFamily.Name, fontText.Size, FontStyle.Underline);
                        fontText = newFont2;

                    }
                    else
                    {
                        Font newFont1 = new Font(fontText.FontFamily.Name, fontText.Size, FontStyle.Regular);
                        fontText = newFont1;

                    }
                }
            }
            txtAddText.SelectionFont = lbAddText.Font = fontText;
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            Draw_Text();
        }

        private void Remove_AddText()
        {
            pic.MouseDown -= pic_AddText_MouseDown;
            pic.MouseUp -= pic_AddText_MouseUp;
            pic.MouseMove -= pic_AddText_MouseMove;
            pic.MouseEnter -= pic_AddText_MouseEnter;
        }
        #endregion

        //Color Channel feature
        #region ColorRGB
        private Image imgNowFake;
        private Image imgProcess;
        private int _typeRGB;
        private bool isChanged = false;
        public int TypeRGB
        {
            get
            {
                return _typeRGB;
            }
            set
            {
                isChanged = true;
                if (_typeRGB != value)
                {
                    imgNowFake = imgProcess;
                    //stackImage.Push(imgNowFake);
                }
                _typeRGB = value;
            }
        }
        public void Add_ColorRGB_Channel()
        {
            isChanged = false;
            imgProcess = imgNow;
            imgNowFake = imgNow;
        }
        public void Remove_ColorRGB_Channel()
        {
            imgNow = imgProcess;
            pic.Image = new Bitmap(imgNow);
            if (isChanged == true)
            {
                stackImage.Push(imgProcess);
                isSave = false;
            }
        }
        public void InitColorRGB()
        {
            // 
            // Green_Trackbar
            // 
            CustomTrackbar Green_Trackbar = new CustomTrackbar();
            Green_Trackbar.BackColor = Color.Transparent;
            Green_Trackbar.BarInnerColor = System.Drawing.Color.Green;
            Green_Trackbar.BarOuterColor = System.Drawing.Color.Green;
            Green_Trackbar.BarPenColor = System.Drawing.Color.White;
            Green_Trackbar.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            Green_Trackbar.ElapsedInnerColor = Color.Black;
            Green_Trackbar.ElapsedOuterColor = Color.Black;
            Green_Trackbar.LargeChange = ((uint)(5u));
            Green_Trackbar.Location = new System.Drawing.Point(70, 16);
            Green_Trackbar.Maximum = 100;
            Green_Trackbar.Minimum = -100;
            Green_Trackbar.Name = "Green_Trackbar";
            Green_Trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            Green_Trackbar.Size = new System.Drawing.Size(56, 406);
            Green_Trackbar.SmallChange = ((uint)(1u));
            Green_Trackbar.TabIndex = 5;
            Green_Trackbar.Text = "customTrackbar1";
            Green_Trackbar.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            Green_Trackbar.ThumbSize = 2;
            Green_Trackbar.Value = 0;
            Green_Trackbar.ValueChanged += Green_Trackbar_ValueChanged;
            panel3.Controls.Add(Green_Trackbar);
            // 
            // Red_Trackbar
            // 
            CustomTrackbar Red_Trackbar = new CustomTrackbar();
            Red_Trackbar.BackColor = System.Drawing.Color.Transparent;
            Red_Trackbar.BarInnerColor = System.Drawing.Color.Maroon;
            Red_Trackbar.BarOuterColor = System.Drawing.Color.Maroon;
            Red_Trackbar.BarPenColor = System.Drawing.SystemColors.ButtonHighlight;
            Red_Trackbar.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            Red_Trackbar.ElapsedInnerColor = System.Drawing.SystemColors.ActiveCaptionText;
            Red_Trackbar.ElapsedOuterColor = System.Drawing.SystemColors.ActiveCaptionText;
            Red_Trackbar.LargeChange = ((uint)(5u));
            Red_Trackbar.Location = new System.Drawing.Point(17, 16);
            Red_Trackbar.Maximum = 100;
            Red_Trackbar.Minimum = -100;
            Red_Trackbar.Name = "Red_Trackbar";
            Red_Trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            Red_Trackbar.Size = new System.Drawing.Size(56, 406);
            Red_Trackbar.SmallChange = ((uint)(1u));
            Red_Trackbar.TabIndex = 7;
            Red_Trackbar.Text = "customTrackbar1";
            Red_Trackbar.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            Red_Trackbar.ThumbSize = 2;
            Red_Trackbar.Value = 0;
            Red_Trackbar.ValueChanged += Red_Trackbar_ValueChanged;
            panel3.Controls.Add(Red_Trackbar);
            // 
            // Blue_Trackbar
            // 
            CustomTrackbar Blue_Trackbar = new CustomTrackbar();
            Blue_Trackbar.BackColor = System.Drawing.Color.Transparent;
            Blue_Trackbar.BarInnerColor = System.Drawing.Color.Navy;
            Blue_Trackbar.BarOuterColor = System.Drawing.Color.Navy;
            Blue_Trackbar.BarPenColor = System.Drawing.SystemColors.ButtonHighlight;
            Blue_Trackbar.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            Blue_Trackbar.ElapsedInnerColor = System.Drawing.SystemColors.ActiveCaptionText;
            Blue_Trackbar.ElapsedOuterColor = System.Drawing.SystemColors.ActiveCaptionText;
            Blue_Trackbar.LargeChange = ((uint)(5u));
            Blue_Trackbar.Location = new System.Drawing.Point(125, 16);
            Blue_Trackbar.Maximum = 100;
            Blue_Trackbar.Minimum = -100;
            Blue_Trackbar.Name = "Blue_Trackbar";
            Blue_Trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            Blue_Trackbar.Size = new System.Drawing.Size(56, 406);
            Blue_Trackbar.SmallChange = ((uint)(5u));
            Blue_Trackbar.TabIndex = 9;
            Blue_Trackbar.Text = "customTrackbar2";
            Blue_Trackbar.ThumbInnerColor = System.Drawing.Color.WhiteSmoke;
            Blue_Trackbar.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            Blue_Trackbar.ThumbSize = 2;
            Blue_Trackbar.Value = 0;
            Blue_Trackbar.ValueChanged += Blue_Trackbar_ValueChanged;
            panel3.Controls.Add(Blue_Trackbar);
            //
            // Label
            // 
            Label lb = new Label();
            lb.AutoSize = true;
            lb.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            lb.Location = new System.Drawing.Point(40, 451);
            lb.Name = "lb";
            lb.Size = new System.Drawing.Size(148, 26);
            lb.TabIndex = 0;
            lb.Text = "Color Channel";
            lb.BringToFront();
            panel3.Controls.Add(lb);
        }

        private void Blue_Trackbar_ValueChanged(object sender, EventArgs e)
        {
            TypeRGB = 3;
            CustomTrackbar bar = (CustomTrackbar)sender;
            float trueValue = 0 - bar.Value;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1,0,0,0,0},
                new float[] {0,1,0,0,0},
                new float[] {0,0,1f + (trueValue / 200),0,0},
                new float[] {0,0,0,1,0},
                new float[] {0,0,0,0,1},
            });
            Bitmap bitmap = new Bitmap(ApplyColorMatrix(imgNowFake, colorMatrix));
            imgProcess = bitmap;
            pic.Image = new Bitmap(imgProcess);
        }

        private void Red_Trackbar_ValueChanged(object sender, EventArgs e)
        {
            TypeRGB = 1;
            CustomTrackbar bar = (CustomTrackbar)sender;
            float trueValue = 0 - bar.Value;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1f + (trueValue / 200),0,0,0,0},
                new float[] {0,1, 0,0,0},
                new float[] {0,0,1,0,0},
                new float[] {0,0,0,1,0},
                new float[] {0,0,0,0,1},
            });
            Bitmap bitmap = new Bitmap(ApplyColorMatrix(imgNowFake, colorMatrix));
            imgProcess = bitmap;
            pic.Image = new Bitmap(imgProcess);
        }

        private void Green_Trackbar_ValueChanged(object sender, EventArgs e)
        {
            TypeRGB = 2;
            CustomTrackbar bar = (CustomTrackbar)sender;
            float trueValue = 0 - bar.Value;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0,0,0,0},
                new float[] {0, 1f + (trueValue / 200), 0,0,0},
                new float[] {0,0,1,0,0},
                new float[] {0,0,0,1,0},
                new float[] {0,0,0,0,1},
            });
            Bitmap bitmap = new Bitmap(ApplyColorMatrix(imgNowFake, colorMatrix));
            imgProcess = bitmap;
            pic.Image = new Bitmap(imgProcess);
        }
        #endregion

        //Brightness & Contrast feature
        #region BrighnessAndContrast
        public void Add_BrighnessAndContrast()
        {
            isChanged = false;
            imgProcess = imgNow;
            imgNowFake = imgNow;
        }
        public void Remove_BrighnessAndContrast()
        {
            imgNow = imgProcess;
            pic.Image = new Bitmap(imgNow);
            if (isChanged == true)
            {
                isSave = false;
                stackImage.Push(imgProcess);
            }
        }

        public void InitBrighnessAndContrast()
        {
            // 
            // Brighness_Trackbar
            // 
            CustomTrackbar Brighness_Trackbar = new CustomTrackbar();
            Brighness_Trackbar.BackColor = Color.Transparent;
            Brighness_Trackbar.BarInnerColor = System.Drawing.Color.FromArgb(40, 51, 90);
            Brighness_Trackbar.BarOuterColor = System.Drawing.Color.FromArgb(40, 51, 90);
            Brighness_Trackbar.BarPenColor = System.Drawing.Color.White;
            Brighness_Trackbar.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            Brighness_Trackbar.ElapsedInnerColor = Color.Black;
            Brighness_Trackbar.ElapsedOuterColor = Color.Black;
            Brighness_Trackbar.LargeChange = ((uint)(5u));
            Brighness_Trackbar.Location = new System.Drawing.Point(35, 16);
            Brighness_Trackbar.Maximum = 100;
            Brighness_Trackbar.Minimum = -100;
            Brighness_Trackbar.Name = "Green_Trackbar";
            Brighness_Trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            Brighness_Trackbar.Size = new System.Drawing.Size(56, 406);
            Brighness_Trackbar.SmallChange = ((uint)(1u));
            Brighness_Trackbar.TabIndex = 5;
            Brighness_Trackbar.Text = "customTrackbar1";
            Brighness_Trackbar.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            Brighness_Trackbar.ThumbSize = 2;
            Brighness_Trackbar.Value = 0;
            Brighness_Trackbar.ValueChanged += Brighness_Trackbar_ValueChanged;
            panel3.Controls.Add(Brighness_Trackbar);

            // 
            // Contrast_Trackbar
            // 
            CustomTrackbar Contrast_Trackbar = new CustomTrackbar();
            Contrast_Trackbar.BackColor = System.Drawing.Color.Transparent;
            Contrast_Trackbar.BarInnerColor = System.Drawing.Color.FromArgb(40, 51, 90);
            Contrast_Trackbar.BarOuterColor = System.Drawing.Color.FromArgb(40, 51, 90);
            Contrast_Trackbar.BarPenColor = System.Drawing.SystemColors.ButtonHighlight;
            Contrast_Trackbar.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            Contrast_Trackbar.ElapsedInnerColor = System.Drawing.SystemColors.ActiveCaptionText;
            Contrast_Trackbar.ElapsedOuterColor = System.Drawing.SystemColors.ActiveCaptionText;
            Contrast_Trackbar.LargeChange = ((uint)(5u));
            Contrast_Trackbar.Location = new System.Drawing.Point(110, 16);
            Contrast_Trackbar.Maximum = 100;
            Contrast_Trackbar.Minimum = -100;
            Contrast_Trackbar.Name = "Red_Trackbar";
            Contrast_Trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            Contrast_Trackbar.Size = new System.Drawing.Size(56, 406);
            Contrast_Trackbar.SmallChange = ((uint)(1u));
            Contrast_Trackbar.TabIndex = 7;
            Contrast_Trackbar.Text = "customTrackbar1";
            Contrast_Trackbar.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            Contrast_Trackbar.ThumbSize = 2;
            Contrast_Trackbar.Value = 0;
            Contrast_Trackbar.ValueChanged += Contrast_Trackbar_ValueChanged;
            panel3.Controls.Add(Contrast_Trackbar);
            //
            // Label
            // 
            Label lb = new Label();
            lb.AutoSize = true;
            lb.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            lb.Location = new System.Drawing.Point(20, 451);
            lb.Name = "lb";
            lb.Size = new System.Drawing.Size(148, 26);
            lb.TabIndex = 0;
            lb.Text = "Brighness   Contrast";
            lb.BringToFront();
            panel3.Controls.Add(lb);

        }

        private void Contrast_Trackbar_ValueChanged(object sender, EventArgs e)
        {
            TypeRGB = 5;
            CustomTrackbar bar = (CustomTrackbar)sender;
            float c = 1 + -bar.Value * 0.01f;
            float t = (1.0f - c) / 2.0f;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {c,0,0,0,0},
                new float[] {0,c,0,0,0},
                new float[] {0,0,c,0,0},
                new float[] {0,0,0,1,0},
                new float[] {t,t,t,0,1},
            });
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Image _img = imgNowFake;

            //PictureBox1.Image

            Graphics _g = default(Graphics);

            Bitmap bm_dest = new Bitmap(Convert.ToInt32(_img.Width), Convert.ToInt32(_img.Height));

            _g = Graphics.FromImage(bm_dest);

            _g.DrawImage(_img, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel, imageAttributes);

            imgProcess = bm_dest;
            pic.Image = new Bitmap(imgProcess);
        }

        private void Brighness_Trackbar_ValueChanged(object sender, EventArgs e)
        {
            TypeRGB = 4;
            CustomTrackbar bar = (CustomTrackbar)sender;
            float c = -bar.Value * 0.001f;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1,0,0,0,0},
                new float[] {0,1,0,0,0},
                new float[] {0,0,1,0,0},
                new float[] {0,0,0,1,0},
                new float[] {c,c,c,0,1},
            });
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Image _img = imgNowFake;

            //PictureBox1.Image

            Graphics _g = default(Graphics);

            Bitmap bm_dest = new Bitmap(Convert.ToInt32(_img.Width), Convert.ToInt32(_img.Height));

            _g = Graphics.FromImage(bm_dest);

            _g.DrawImage(_img, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel, imageAttributes);

            imgProcess = bm_dest;
            pic.Image = new Bitmap(imgProcess);
        }
        #endregion

        //Saturation feature
        #region Saturation
        const float rwgt = 0.3086f;
        const float gwgt = 0.6094f;
        const float bwgt = 0.0820f;
        static private float saturation = 1.0f;

        public void Add_Saturation()
        {
            isChanged = false;
            imgProcess = imgNow;
            imgNowFake = imgNow;
        }
        public void Remove_Saturation()
        {
            imgNow = imgProcess;
            pic.Image = new Bitmap(imgNow);
            if(isChanged == true)
            {
                isSave = false;
                stackImage.Push(imgProcess);
            }
        }

        private void InitSaturation()
        {
            //Saturation trackbar
            CustomTrackbar Saturation_Trackbar = new CustomTrackbar();
            Saturation_Trackbar.BackColor = Color.Transparent;
            Saturation_Trackbar.BarInnerColor = System.Drawing.Color.FromArgb(40, 51, 90);
            Saturation_Trackbar.BarOuterColor = System.Drawing.Color.FromArgb(40, 51, 90);
            Saturation_Trackbar.BarPenColor = System.Drawing.Color.White;
            Saturation_Trackbar.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            Saturation_Trackbar.ElapsedInnerColor = Color.Black;
            Saturation_Trackbar.ElapsedOuterColor = Color.Black;
            Saturation_Trackbar.LargeChange = ((uint)(5u));
            Saturation_Trackbar.Location = new System.Drawing.Point(75, 16);
            Saturation_Trackbar.Maximum = 100;
            Saturation_Trackbar.Minimum = -100;
            Saturation_Trackbar.Name = "Green_Trackbar";
            Saturation_Trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            Saturation_Trackbar.Size = new System.Drawing.Size(56, 406);
            Saturation_Trackbar.SmallChange = ((uint)(1u));
            Saturation_Trackbar.TabIndex = 5;
            Saturation_Trackbar.Text = "customTrackbar1";
            Saturation_Trackbar.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            Saturation_Trackbar.ThumbSize = 2;
            Saturation_Trackbar.Value = 0;
            Saturation_Trackbar.ValueChanged += Saturation_Trackbar_ValueChanged;
            panel3.Controls.Add(Saturation_Trackbar);

            Label lb = new Label();
            lb.AutoSize = true;
            lb.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            lb.Location = new System.Drawing.Point(63, 451);
            lb.Name = "lb";
            lb.Size = new System.Drawing.Size(148, 26);
            lb.TabIndex = 0;
            lb.Text = "Saturation";
            lb.BringToFront();
            panel3.Controls.Add(lb);
        }

        private void Saturation_Trackbar_ValueChanged(object sender, EventArgs e)
        {
            TypeRGB = 6;
            CustomTrackbar bar = (CustomTrackbar)sender;
            saturation = 1f - (bar.Value / 100f);
            float baseSat = 1.0f - saturation;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {baseSat * rwgt + saturation, baseSat * rwgt, baseSat * rwgt, 0,0},
                new float[] {baseSat * gwgt, baseSat * gwgt + saturation, baseSat * gwgt, 0,0},
                new float[] {baseSat * bwgt, baseSat * bwgt, baseSat * bwgt + saturation, 0,0},
                new float[] {0,0,0,1,0},
                new float[] {0,0,0,0,1},
            });
            Bitmap bitmap = new Bitmap(ApplyColorMatrix(imgNowFake, colorMatrix));
            imgProcess = bitmap;
            pic.Image = new Bitmap(imgProcess);
        }
        #endregion

        #region ConvertIMG
        public static byte[] ConvertImgToBinary(Image img)
        {
            Image temp = new Bitmap(img);
            using (MemoryStream ms = new MemoryStream())
            {
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public static Image ConvertBinaryToImg(byte[] bi)
        {
            using (MemoryStream ms = new MemoryStream(bi))
            {
                return Image.FromStream(ms);
            }
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

        private void Back_Button_Click(object sender, EventArgs e)
        {
            backImage();
        }

        private void createBlankForNew()
        {
            stackImage.Clear();
            Controls.Remove(pic);
            pic = new PictureBox();

            pic.Anchor = 0;
            pic.Location = new Point(400, 260);
            pic.Name = "pictureBox1";
            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.SendToBack();

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
            setLocation(imgNow);
            stackImage.Push(imgNow);
            isSave = true;

            Controls.Add(pic);
            pic.BringToFront();
        }

        private void newToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseType = "";
                DialogResult result = MessageBox.Show("Do you want to save the current image?", "Roga", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (!isSave && result == DialogResult.Yes)
                {
                    if (!saveImage())
                    {
                        return;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }    
                createBlankForNew();
            }    
        }

        private void openToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "(BMP, PNG, JPG, JPEG Files)|*.bmp;*.png; *.jpg; *.jpeg";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    MouseType = "";
                    DialogResult result = MessageBox.Show("Do you want to save the current image?", "Roga", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (!isSave && result == DialogResult.Yes)
                    {
                        if (fileNameNow != "")
                        {
                            if (File.Exists(fileNameNow))
                            {
                                imgNow.Save(fileNameNow);
                                isSave = true;
                            }
                            else if (!saveImage())
                            {
                                return;
                            }
                            else
                            {
                                if (Program.loginState)
                                {
                                    IMAGE_ newImg = new IMAGE_ { userid = LoginScreen.userNow.id, img = ConvertImgToBinary(imgNow) };
                                    using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                                    {
                                        data.IMAGE_.Add(newImg);
                                        data.SaveChanges();
                                    }
                                }
                            }
                        }
                        else if (!saveImage())
                        {
                            return;
                        }
                        else
                        {
                            if (Program.loginState)
                            {
                                IMAGE_ newImg = new IMAGE_ { userid = LoginScreen.userNow.id, img = ConvertImgToBinary(imgNow) };
                                using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                                {
                                    data.IMAGE_.Add(newImg);
                                    data.SaveChanges();
                                }
                            }
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    fileNameNow = open.FileName;
                    Image tempp = Image.FromFile(open.FileName);
                    Image img = new Bitmap(tempp);
                    tempp.Dispose();

                    MouseType = "";
                    stackImage.Clear();
                    Controls.Remove(pic);
                    pic = new PictureBox();

                    pic.Anchor = 0;
                    pic.Location = new Point(400, 260);
                    pic.Name = "pictureBox1";
                    pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    pic.TabIndex = 0;
                    pic.TabStop = false;
                    pic.SendToBack();

                    if (img.Width > Width || img.Height > Height)
                    {
                        int newHeight, newWidth;
                        if (img.Width > img.Height)
                        {
                            newWidth = Width - 225 * 2;
                            newHeight = (int)(img.Height * ((float)newWidth / img.Width));
                        }
                        else
                        {
                            newHeight = Height - 50;
                            newWidth = (int)(img.Width * ((float)newHeight / img.Height));
                        }
                        pic.Size = new Size(newWidth, newHeight);
                        Image temp = img;
                        temp = resizeImage(temp, pic.Size);
                        pic.Image = temp;
                        imgNow = pic.Image;
                        setLocation(imgNow);
                        stackImage.Push(imgNow);
                    }
                    else
                    {
                        int newHeight, newWidth;
                        newWidth = img.Width;
                        newHeight = img.Height;
                        pic.Size = new Size(newWidth, newHeight);
                        pic.Image = img;
                        imgNow = pic.Image;
                        setLocation(imgNow);
                        stackImage.Push(imgNow);
                    }
                    isSave = true;
                    Controls.Add(pic);
                    pic.BringToFront();
                }
            }
        }
        private void saveToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseType = "";
                if (File.Exists(fileNameNow))
                {
                    imgNow.Save(fileNameNow);
                    isSave = true;
                }    
                else
                {
                    saveImage();
                }    
            }
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            MouseType = "";
            if (isSave == false)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save the current image?", "Roga", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (File.Exists(fileNameNow))
                    {
                        imgNow.Save(fileNameNow);
                        if (Program.loginState)
                        {
                            IMAGE_ newImg = new IMAGE_ { userid = LoginScreen.userNow.id, img = ConvertImgToBinary(imgNow) };
                            using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                            {
                                data.IMAGE_.Add(newImg);
                                data.SaveChanges();
                            }
                        }
                        isSave = true;
                    }
                    else if (!saveImage())
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        if (Program.loginState)
                        {
                            IMAGE_ newImg = new IMAGE_ { userid = LoginScreen.userNow.id, img = ConvertImgToBinary(imgNow) };
                            using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                            {
                                data.IMAGE_.Add(newImg);
                                data.SaveChanges();
                            }
                        }
                    }    
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (Program.loginState)
                {
                    IMAGE_ newImg = new IMAGE_ { userid = LoginScreen.userNow.id, img = ConvertImgToBinary(imgNow) };
                    using (RogaDatabaseEntities data = new RogaDatabaseEntities())
                    {
                        data.IMAGE_.Add(newImg);
                        data.SaveChanges();
                    }
                }
            }    
        }

        private void saveAsToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                saveImage();
            }
        }

        private void exitToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseType = "";
                DialogResult result = MessageBox.Show("Do you want to save the current image?", "Roga", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (!isSave && result == DialogResult.Yes)
                {
                    if (!saveImage())
                        return;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }    
                Application.Exit();
            }
        }

        private bool saveImage()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = " Save file...";
            saveFileDialog1.InitialDirectory = "D:\\";
            saveFileDialog1.DefaultExt = "jpg";
            saveFileDialog1.Filter = " Image file (*.BMP,*.JPG,*.JPEG, *.PNG)|*.bmp;*.jpg;*.jpeg;*.png";
            saveFileDialog1.OverwritePrompt = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MouseType = "";
                    pic.Image.Save(saveFileDialog1.FileName);
                    isSave = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Roga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            return false;
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            MouseType = "";
            if (File.Exists(fileNameNow))
            {
                imgNow.Save(fileNameNow);
            }
            else
            {
                saveImage();
            }
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
            MouseType = "saturation";
            LastMouseType = MouseType;
        }

        private void AddPicture_Button_Click(object sender, EventArgs e)
        {
            MouseType = "addPicture";
            LastMouseType = MouseType;
            Thread t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Title = " Open file...";
                open.InitialDirectory = "D:\\";
                open.DefaultExt = "jpg";
                open.Filter = " Image file (*.BMP,*.JPG,*.JPEG,*.PNG)|*.bmp;*.jpg;*.jpeg;*.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Image temp = Image.FromFile(open.FileName);
                    Image img = new Bitmap(temp);
                    temp.Dispose();
                    BeforeAddImage newForm = new BeforeAddImage(img);
                    newForm.ShowDialog();
                    if (newForm.getImageToAdd() != null)
                    {
                        InitAddImage(newForm.getImageToAdd());
                    }
                }    
            }));

            // Run your code from a thread that joins the STA Thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
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
