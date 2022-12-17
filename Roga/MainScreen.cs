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
                        //remove 
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

        //constructor
        //
        public MainScreen()
        {
            InitializeComponent();
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\pencil.jpg");
            string sFilePath = Path.GetFullPath(sFile);
            Pen_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\back.png");
            sFilePath = Path.GetFullPath(sFile);
            Back_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\hand.png");
            sFilePath = Path.GetFullPath(sFile);
            Hand_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\save.png");
            sFilePath = Path.GetFullPath(sFile);
            Save_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\eraser.png");
            sFilePath = Path.GetFullPath(sFile);
            Eraser_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\brightness.png");
            sFilePath = Path.GetFullPath(sFile);
            BrightnessAndContrast_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\color.png");
            sFilePath = Path.GetFullPath(sFile);
            ColorChannel_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\exposure.png");
            sFilePath = Path.GetFullPath(sFile);
            Exposure_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\addpicture.png");
            sFilePath = Path.GetFullPath(sFile);
            AddPicture_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\text.png");
            sFilePath = Path.GetFullPath(sFile);
            AddText_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\crop.png");
            sFilePath = Path.GetFullPath(sFile);
            Crop_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\shape.png");
            sFilePath = Path.GetFullPath(sFile);
            Shape_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\filter.png");
            sFilePath = Path.GetFullPath(sFile);
            Filter_Button.BackgroundImage = Image.FromFile(sFilePath);


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
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\pencil.jpg");
            string sFilePath = Path.GetFullPath(sFile);
            Pen_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\back.png");
            sFilePath = Path.GetFullPath(sFile);
            Back_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\hand.png");
            sFilePath = Path.GetFullPath(sFile);
            Hand_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\save.png");
            sFilePath = Path.GetFullPath(sFile);
            Save_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\eraser.png");
            sFilePath = Path.GetFullPath(sFile);
            Eraser_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\brightness.png");
            sFilePath = Path.GetFullPath(sFile);
            BrightnessAndContrast_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\color.png");
            sFilePath = Path.GetFullPath(sFile);
            ColorChannel_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\exposure.png");
            sFilePath = Path.GetFullPath(sFile);
            Exposure_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\addpicture.png");
            sFilePath = Path.GetFullPath(sFile);
            AddPicture_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\text.png");
            sFilePath = Path.GetFullPath(sFile);
            AddText_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\crop.png");
            sFilePath = Path.GetFullPath(sFile);
            Crop_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\shape.png");
            sFilePath = Path.GetFullPath(sFile);
            Shape_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\filter.png");
            sFilePath = Path.GetFullPath(sFile);
            Filter_Button.BackgroundImage = Image.FromFile(sFilePath);

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
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\pencil.jpg");
            string sFilePath = Path.GetFullPath(sFile);
            Pen_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\back.png");
            sFilePath = Path.GetFullPath(sFile);
            Back_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\hand.png");
            sFilePath = Path.GetFullPath(sFile);
            Hand_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\save.png");
            sFilePath = Path.GetFullPath(sFile);
            Save_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\eraser.png");
            sFilePath = Path.GetFullPath(sFile);
            Eraser_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\brightness.png");
            sFilePath = Path.GetFullPath(sFile);
            BrightnessAndContrast_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\color.png");
            sFilePath = Path.GetFullPath(sFile);
            ColorChannel_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\exposure.png");
            sFilePath = Path.GetFullPath(sFile);
            Exposure_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\addpicture.png");
            sFilePath = Path.GetFullPath(sFile);
            AddPicture_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\text.png");
            sFilePath = Path.GetFullPath(sFile);
            AddText_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\crop.png");
            sFilePath = Path.GetFullPath(sFile);
            Crop_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\shape.png");
            sFilePath = Path.GetFullPath(sFile);
            Shape_Button.BackgroundImage = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\icons\filter.png");
            sFilePath = Path.GetFullPath(sFile);
            Filter_Button.BackgroundImage = Image.FromFile(sFilePath);

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
        //
        static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
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
            btnLine1.BorderColor = btnLine2.BorderColor = btnLine3.BorderColor = Color.FromArgb(217, 217, 217);
            btnLine1.BorderRadius = btnLine2.BorderRadius = btnLine3.BorderRadius = 0;
            btnLine1.Cursor = btnLine2.Cursor = btnLine3.Cursor = Cursors.Hand;
            btnLine1.Click += new EventHandler(SetWidth_10);
            btnLine2.Click += new EventHandler(SetWidth_5);
            btnLine3.Click += new EventHandler(SetWidth_3);
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
                matrixColor[i, 0].Click += delegate (object sender, EventArgs e) { SetCustomColor(sender, e, selectedColor); };
                matrixColor[i, 1].Click += delegate (object sender, EventArgs e) { SetColor(sender, e, selectedColor); };
                matrixColor[i, 2].Click += delegate (object sender, EventArgs e) { SetColor(sender, e, selectedColor); };
            }
            //Init EditColor
            Panel edit = new Panel();
            edit.Size = new Size(147, 56);
            edit.Location = new Point(28, 500);
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\EditColor.png");
            string sFilePath = Path.GetFullPath(sFile);
            edit.BackgroundImage = Image.FromFile(sFilePath);
            edit.Click += delegate (object sender, EventArgs e) { editColor_Click(sender, e, matrixColor); };
            panel3.Controls.Add(edit);
            selectedColor.BackColor = Color.Black;
        }

        private void SetWidth_10(object sender, EventArgs e)
        {
            pen.Width = 10;
        }
        private void SetWidth_5(object sender, EventArgs e)
        {
            pen.Width = 5;
        }
        private void SetWidth_3(object sender, EventArgs e)
        {
            pen.Width = 3;
        }

        private void SetColor(object sender, EventArgs e, CustomButton.VBButton b)
        {
            CustomButton.VBButton button = (CustomButton.VBButton)sender;
            b.BackColor = button.BackColor;
            pen.Color = button.BackColor;
        }
        private void SetCustomColor(object sender, EventArgs e, CustomButton.VBButton b)
        {
            CustomButton.VBButton button = (CustomButton.VBButton)sender;
            if (button.ForeColor != Color.Red)
            {
                b.BackColor = button.BackColor;
                pen.Color = button.BackColor;
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
        private void editColor_Click(object sender, EventArgs e, CustomButton.VBButton[,] matrixColor)
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
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Filter\GrayImg.png");
            string sFilePath = Path.GetFullPath(sFile);
            picGray.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Filter\NegativeImg.png");
            sFilePath = Path.GetFullPath(sFile);
            picNegative.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Filter\TransparencyImg.png");
            sFilePath = Path.GetFullPath(sFile);
            picTrans.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Filter\SepiaImg.png");
            sFilePath = Path.GetFullPath(sFile);
            picSepiaTone.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Filter\RedImg.png");
            sFilePath = Path.GetFullPath(sFile);
            picRed.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Filter\GreenImg.png");
            sFilePath = Path.GetFullPath(sFile);
            picGreen.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Filter\BlueImg.png");
            sFilePath = Path.GetFullPath(sFile);
            picBlue.Image = Image.FromFile(sFilePath);

            //setSize
            picGray.Size = picNegative.Size = picTrans.Size = picSepiaTone.Size = picRed.Size = picGreen.Size = picBlue.Size = new Size(80, 80);

            //setSizeMode
            picGray.SizeMode = picNegative.SizeMode = picTrans.SizeMode = picSepiaTone.SizeMode = picRed.SizeMode = picGreen.SizeMode = picBlue.SizeMode = PictureBoxSizeMode.StretchImage;

            //setEvent
            picGray.Click += new EventHandler(picGray_Click);
            picNegative.Click += new EventHandler(picNegative_Click);
            picTrans.Click += new EventHandler(picTrans_Click);
            picSepiaTone.Click += new EventHandler(picSepiaTone_Click);
            picRed.Click += new EventHandler(picRed_Click);
            picGreen.Click += new EventHandler(picGreen_Click);
            picBlue.Click += new EventHandler(picBlue_Click);

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
        private void picGray_Click(object sender, EventArgs e)
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

        private void picNegative_Click(object sender, EventArgs e)
        {
            imgNow = ImageWithNegative(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
        }

        private void picTrans_Click(object sender, EventArgs e)
        {
            imgNow = ImageWithTransparency(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
        }

        private void picSepiaTone_Click(object sender, EventArgs e)
        {
            imgNow = ImageWithSepiaTone(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
        }

        private void picRed_Click(object sender, EventArgs e)
        {
            imgNow = ImageWithRed(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
        }

        private void picGreen_Click(object sender, EventArgs e)
        {
            imgNow = ImageWithGreen(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
        }

        private void picBlue_Click(object sender, EventArgs e)
        {
            imgNow = ImageWithBlue(imgNow);
            pic.Image = imgNow;
            stackImage.Push(imgNow);
        }

        #endregion

        //Add Shapes feature
        #region Shapes
        private int width, height, X, Y;

        private void pic_Shapes_MouseDown(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
            pic.Refresh();
            isMouseDown = true;
        }

        private void pic_Shapes_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                pic.Refresh();
                pic.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                width = Math.Abs(e.X - X);
                height = Math.Abs(e.Y - Y);
                pic.CreateGraphics().DrawRectangle(pen, Math.Min(e.X, X), Math.Min(e.Y, Y), width, height);
            }
        }

        private void pic_Shapes_MouseUp(object sender, MouseEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(imgNow))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawRectangle(pen, Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y), Math.Abs(e.X - X), Math.Abs(e.Y - Y));
                //hasObject = true;
                //leftTop = new Point(Math.Min(X, e.Location.X), Math.Min(Y, e.Location.Y));
                //rightBottom = new Point(leftTop.X + Math.Abs(e.X - X), leftTop.Y + Math.Abs(e.Y - Y));
            }
            isMouseDown = false;
            pic.Invalidate();
        }

        private void addShapesEvent()
        {
            pic.MouseDown += new MouseEventHandler(pic_Shapes_MouseDown);
            pic.MouseMove += new MouseEventHandler(pic_Shapes_MouseMove);
            pic.MouseUp += new MouseEventHandler(pic_Shapes_MouseUp);
        }

        private void removeShapesEvent()
        {
            pic.MouseDown -= new MouseEventHandler(pic_Shapes_MouseDown);
            pic.MouseMove -= new MouseEventHandler(pic_Shapes_MouseMove);
            pic.MouseUp -= new MouseEventHandler(pic_Shapes_MouseUp);
        }

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
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Crop and rotate\select.png");
            string sFilePath = Path.GetFullPath(sFile);
            picSelect.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Crop and rotate\crop.png");
            sFilePath = Path.GetFullPath(sFile);
            picCrop.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Crop and rotate\rotate.png");
            sFilePath = Path.GetFullPath(sFile);
            picRotate.Image = Image.FromFile(sFilePath);

            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\Crop and rotate\flip.png");
            sFilePath = Path.GetFullPath(sFile);
            picFlip.Image = Image.FromFile(sFilePath);

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
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
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


        //button onclick
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
        //
    }
}
