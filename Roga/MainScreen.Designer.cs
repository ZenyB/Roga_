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
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Pen_Button = new System.Windows.Forms.Button();
            this.Back_Button = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Hand_Button = new System.Windows.Forms.Button();
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
            this.menuStrip1.Size = new System.Drawing.Size(1162, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.helpToolStripMenuItem.Text = "Help";
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
            this.panel2.Size = new System.Drawing.Size(208, 548);
            this.panel2.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel5.Controls.Add(this.Pen_Button);
            this.panel5.Location = new System.Drawing.Point(12, 95);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(174, 374);
            this.panel5.TabIndex = 1;
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
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(959, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(203, 546);
            this.panel3.TabIndex = 3;
            // 
            // Pen_Button
            // 
            this.Pen_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pen_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pen_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pen_Button.Location = new System.Drawing.Point(18, 23);
            this.Pen_Button.Name = "Pen_Button";
            this.Pen_Button.Size = new System.Drawing.Size(55, 55);
            this.Pen_Button.TabIndex = 0;
            this.Pen_Button.UseVisualStyleBackColor = true;
            this.Pen_Button.Click += new System.EventHandler(this.Pen_Button_Click);
            // 
            // Back_Button
            // 
            this.Back_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Back_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Back_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back_Button.Location = new System.Drawing.Point(1, 1);
            this.Back_Button.Name = "Back_Button";
            this.Back_Button.Size = new System.Drawing.Size(55, 55);
            this.Back_Button.TabIndex = 3;
            this.Back_Button.UseVisualStyleBackColor = true;
            // 
            // Save_Button
            // 
            this.Save_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Save_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
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
            this.Hand_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Hand_Button.Location = new System.Drawing.Point(59, 1);
            this.Hand_Button.Name = "Hand_Button";
            this.Hand_Button.Size = new System.Drawing.Size(55, 55);
            this.Hand_Button.TabIndex = 1;
            this.Hand_Button.UseVisualStyleBackColor = true;
            this.Hand_Button.Click += new System.EventHandler(this.Hand_Button_Click);
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
            this.ClientSize = new System.Drawing.Size(1162, 571);
            this.Controls.Add(this.picturePanel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimizeBox = false;
            this.Name = "MainScreen";
            this.Text = "MainScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainScreen_Load);
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
    }
}