namespace Roga
{
    partial class MainScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactUsRogagmailcomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Shape_Button = new System.Windows.Forms.Button();
            this.Crop_Button = new System.Windows.Forms.Button();
            this.Filter_Button = new System.Windows.Forms.Button();
            this.AddText_Button = new System.Windows.Forms.Button();
            this.AddPicture_Button = new System.Windows.Forms.Button();
            this.Saturation_Button = new System.Windows.Forms.Button();
            this.ColorChannel_Button = new System.Windows.Forms.Button();
            this.BrightnessAndContrast_Button = new System.Windows.Forms.Button();
            this.Pen_Button = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Back_Button = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Hand_Button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Eraser_Button = new System.Windows.Forms.Button();
            this.pic = new System.Windows.Forms.PictureBox();
            this.picturePanel2 = new PicturePanel();
            this.picturePanel1 = new PicturePanel();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1162, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.newToolStripMenuItem_MouseDown);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openToolStripMenuItem_MouseDown);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.saveToolStripMenuItem_MouseDown);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.saveAsToolStripMenuItem_MouseDown);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.exitToolStripMenuItem_MouseDown);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contactUsRogagmailcomToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // contactUsRogagmailcomToolStripMenuItem
            // 
            this.contactUsRogagmailcomToolStripMenuItem.Enabled = false;
            this.contactUsRogagmailcomToolStripMenuItem.Name = "contactUsRogagmailcomToolStripMenuItem";
            this.contactUsRogagmailcomToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
            this.contactUsRogagmailcomToolStripMenuItem.Text = "Contact us: Roga@gmail.com";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 730);
            this.panel2.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel5.Controls.Add(this.Shape_Button);
            this.panel5.Controls.Add(this.Crop_Button);
            this.panel5.Controls.Add(this.Filter_Button);
            this.panel5.Controls.Add(this.AddText_Button);
            this.panel5.Controls.Add(this.AddPicture_Button);
            this.panel5.Controls.Add(this.Saturation_Button);
            this.panel5.Controls.Add(this.ColorChannel_Button);
            this.panel5.Controls.Add(this.BrightnessAndContrast_Button);
            this.panel5.Controls.Add(this.Pen_Button);
            this.panel5.Location = new System.Drawing.Point(12, 95);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(174, 390);
            this.panel5.TabIndex = 1;
            // 
            // Shape_Button
            // 
            this.Shape_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Shape_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Shape_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Shape_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Shape_Button.Location = new System.Drawing.Point(98, 319);
            this.Shape_Button.Name = "Shape_Button";
            this.Shape_Button.Size = new System.Drawing.Size(55, 55);
            this.Shape_Button.TabIndex = 9;
            this.Shape_Button.UseVisualStyleBackColor = true;
            this.Shape_Button.Click += new System.EventHandler(this.Shape_Button_Click);
            // 
            // Crop_Button
            // 
            this.Crop_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Crop_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Crop_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Crop_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Crop_Button.Location = new System.Drawing.Point(18, 319);
            this.Crop_Button.Name = "Crop_Button";
            this.Crop_Button.Size = new System.Drawing.Size(55, 55);
            this.Crop_Button.TabIndex = 8;
            this.Crop_Button.UseVisualStyleBackColor = true;
            this.Crop_Button.Click += new System.EventHandler(this.Crop_Button_Click);
            // 
            // Filter_Button
            // 
            this.Filter_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Filter_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Filter_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Filter_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Filter_Button.Location = new System.Drawing.Point(98, 249);
            this.Filter_Button.Name = "Filter_Button";
            this.Filter_Button.Size = new System.Drawing.Size(55, 55);
            this.Filter_Button.TabIndex = 7;
            this.Filter_Button.UseVisualStyleBackColor = true;
            this.Filter_Button.Click += new System.EventHandler(this.Filter_Button_Click);
            // 
            // AddText_Button
            // 
            this.AddText_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddText_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddText_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddText_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddText_Button.Location = new System.Drawing.Point(18, 249);
            this.AddText_Button.Name = "AddText_Button";
            this.AddText_Button.Size = new System.Drawing.Size(55, 55);
            this.AddText_Button.TabIndex = 6;
            this.AddText_Button.UseVisualStyleBackColor = true;
            this.AddText_Button.Click += new System.EventHandler(this.AddText_Button_Click);
            // 
            // AddPicture_Button
            // 
            this.AddPicture_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddPicture_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddPicture_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddPicture_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddPicture_Button.Location = new System.Drawing.Point(98, 173);
            this.AddPicture_Button.Name = "AddPicture_Button";
            this.AddPicture_Button.Size = new System.Drawing.Size(55, 55);
            this.AddPicture_Button.TabIndex = 5;
            this.AddPicture_Button.UseVisualStyleBackColor = true;
            this.AddPicture_Button.Click += new System.EventHandler(this.AddPicture_Button_Click);
            // 
            // Saturation_Button
            // 
            this.Saturation_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Saturation_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Saturation_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Saturation_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Saturation_Button.Location = new System.Drawing.Point(18, 173);
            this.Saturation_Button.Name = "Saturation_Button";
            this.Saturation_Button.Size = new System.Drawing.Size(55, 55);
            this.Saturation_Button.TabIndex = 4;
            this.Saturation_Button.UseVisualStyleBackColor = true;
            this.Saturation_Button.Click += new System.EventHandler(this.Exposure_Button_Click);
            // 
            // ColorChannel_Button
            // 
            this.ColorChannel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorChannel_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ColorChannel_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorChannel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColorChannel_Button.Location = new System.Drawing.Point(98, 98);
            this.ColorChannel_Button.Name = "ColorChannel_Button";
            this.ColorChannel_Button.Size = new System.Drawing.Size(55, 55);
            this.ColorChannel_Button.TabIndex = 3;
            this.ColorChannel_Button.UseVisualStyleBackColor = true;
            this.ColorChannel_Button.Click += new System.EventHandler(this.ColorChannel_Button_Click);
            // 
            // BrightnessAndContrast_Button
            // 
            this.BrightnessAndContrast_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BrightnessAndContrast_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BrightnessAndContrast_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrightnessAndContrast_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrightnessAndContrast_Button.Location = new System.Drawing.Point(18, 98);
            this.BrightnessAndContrast_Button.Name = "BrightnessAndContrast_Button";
            this.BrightnessAndContrast_Button.Size = new System.Drawing.Size(55, 55);
            this.BrightnessAndContrast_Button.TabIndex = 2;
            this.BrightnessAndContrast_Button.UseVisualStyleBackColor = true;
            this.BrightnessAndContrast_Button.Click += new System.EventHandler(this.BrightnessAndContrast_Button_Click);
            // 
            // Pen_Button
            // 
            this.Pen_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Pen_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pen_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pen_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pen_Button.Location = new System.Drawing.Point(18, 23);
            this.Pen_Button.Name = "Pen_Button";
            this.Pen_Button.Size = new System.Drawing.Size(55, 55);
            this.Pen_Button.TabIndex = 0;
            this.Pen_Button.UseVisualStyleBackColor = true;
            this.Pen_Button.Click += new System.EventHandler(this.Pen_Button_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel4.Controls.Add(this.Back_Button);
            this.panel4.Controls.Add(this.Save_Button);
            this.panel4.Controls.Add(this.Hand_Button);
            this.panel4.Location = new System.Drawing.Point(12, 15);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(174, 59);
            this.panel4.TabIndex = 0;
            // 
            // Back_Button
            // 
            this.Back_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Back_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Back_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Back_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back_Button.Location = new System.Drawing.Point(1, 1);
            this.Back_Button.Name = "Back_Button";
            this.Back_Button.Size = new System.Drawing.Size(55, 55);
            this.Back_Button.TabIndex = 3;
            this.Back_Button.UseVisualStyleBackColor = true;
            this.Back_Button.Click += new System.EventHandler(this.Back_Button_Click);
            // 
            // Save_Button
            // 
            this.Save_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Save_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Save_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_Button.Location = new System.Drawing.Point(117, 1);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(55, 55);
            this.Save_Button.TabIndex = 2;
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Hand_Button
            // 
            this.Hand_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Hand_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Hand_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Hand_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Hand_Button.Location = new System.Drawing.Point(59, 1);
            this.Hand_Button.Name = "Hand_Button";
            this.Hand_Button.Size = new System.Drawing.Size(55, 55);
            this.Hand_Button.TabIndex = 1;
            this.Hand_Button.UseVisualStyleBackColor = true;
            this.Hand_Button.Click += new System.EventHandler(this.Hand_Button_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(959, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(203, 728);
            this.panel3.TabIndex = 3;
            // 
            // Eraser_Button
            // 
            this.Eraser_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Eraser_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Eraser_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Eraser_Button.Location = new System.Drawing.Point(110, 146);
            this.Eraser_Button.Name = "Eraser_Button";
            this.Eraser_Button.Size = new System.Drawing.Size(55, 55);
            this.Eraser_Button.TabIndex = 1;
            this.Eraser_Button.UseVisualStyleBackColor = true;
            this.Eraser_Button.Click += new System.EventHandler(this.Eraser_Button_Click);
            // 
            // pic
            // 
            this.pic.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic.Location = new System.Drawing.Point(7, 68);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(736, 424);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // picturePanel2
            // 
            this.picturePanel2.AutoScroll = true;
            this.picturePanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picturePanel2.Location = new System.Drawing.Point(218, 37);
            this.picturePanel2.Name = "picturePanel2";
            this.picturePanel2.Size = new System.Drawing.Size(729, 510);
            this.picturePanel2.TabIndex = 4;
            // 
            // picturePanel1
            // 
            this.picturePanel1.AutoScroll = true;
            this.picturePanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picturePanel1.Location = new System.Drawing.Point(0, 0);
            this.picturePanel1.Name = "picturePanel1";
            this.picturePanel1.Size = new System.Drawing.Size(200, 100);
            this.picturePanel1.TabIndex = 0;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(1162, 753);
            this.Controls.Add(this.Eraser_Button);
            this.Controls.Add(this.picturePanel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainScreen";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainScreen_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private PicturePanel picturePanel1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button Pen_Button;
        private System.Windows.Forms.Button Back_Button;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button Hand_Button;
        private PicturePanel picturePanel2;
        private System.Windows.Forms.Button Eraser_Button;
        private System.Windows.Forms.Button Filter_Button;
        private System.Windows.Forms.Button AddText_Button;
        private System.Windows.Forms.Button AddPicture_Button;
        private System.Windows.Forms.Button Saturation_Button;
        private System.Windows.Forms.Button ColorChannel_Button;
        private System.Windows.Forms.Button BrightnessAndContrast_Button;
        private System.Windows.Forms.Button Shape_Button;
        private System.Windows.Forms.Button Crop_Button;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contactUsRogagmailcomToolStripMenuItem;
    }
}