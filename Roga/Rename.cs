using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Roga
{
    public partial class Rename : Form
    {
        HomeScreen parentForm;
        public Rename(HomeScreen homeScreen)
        {
            InitializeComponent();
            parentForm = homeScreen;
            //Insert Logo
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Roga\Assets\Images\logo.png");
            string sFilePath = Path.GetFullPath(sFile);
            pictureBox1.Image = Image.FromFile(sFilePath);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel the renaming process", "Notify", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void setName()
        {
            if (textBox1.Text != "" && textBox1.Text.Trim() != "")
            {
                parentForm.Rename(textBox1.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Name cannot be empty", "Notify");
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                setName();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setName();
        }

        //Create Gradient Background
        private void Rename_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush linear = new LinearGradientBrush(r, Color.FromArgb(217, 165, 179), Color.FromArgb(24, 104, 174), LinearGradientMode.Horizontal);
            g.FillRectangle(linear, r);
        }
    }
}
