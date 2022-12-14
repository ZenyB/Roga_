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
        public string MouseType
        {
            get { return _mouseType; }
            set
            {
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
                        //remove
                        break;
                    case "filter":
                        //remove
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
                        //add 
                        break;
                    case "shape":
                        //add
                        break;
                    case "filter":
                        //add
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
            Back_Button.BackgroundImage= Image.FromFile(sFilePath);

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
        //

        //button onclick
        //
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
            Thread t = new Thread((ThreadStart)(() => {
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
