
namespace Roga
{
    partial class HomeScreen
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkLabelLogin = new System.Windows.Forms.LinkLabel();
            this.loginLabel = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBlank = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.ptbAvt = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRename = new CustomButton.VBButton();
            this.btnLogout = new CustomButton.VBButton();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(46, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1343, 666);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(494, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(840, 62);
            this.panel4.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(365, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Storage";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSize = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.linkLabelLogin);
            this.panel3.Controls.Add(this.loginLabel);
            this.panel3.Controls.Add(this.listView1);
            this.panel3.Location = new System.Drawing.Point(494, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(840, 599);
            this.panel3.TabIndex = 1;
            // 
            // linkLabelLogin
            // 
            this.linkLabelLogin.AutoSize = true;
            this.linkLabelLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelLogin.Location = new System.Drawing.Point(188, 270);
            this.linkLabelLogin.Name = "linkLabelLogin";
            this.linkLabelLogin.Size = new System.Drawing.Size(96, 38);
            this.linkLabelLogin.TabIndex = 2;
            this.linkLabelLogin.TabStop = true;
            this.linkLabelLogin.Text = "Login";
            this.linkLabelLogin.Visible = false;
            this.linkLabelLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLogin_LinkClicked);
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginLabel.Location = new System.Drawing.Point(276, 270);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(439, 38);
            this.loginLabel.TabIndex = 1;
            this.loginLabel.Text = " to unlock the storage feature";
            this.loginLabel.Visible = false;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(7, 7);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(825, 583);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.btnLogout);
            this.panel2.Controls.Add(this.btnRename);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnBlank);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Controls.Add(this.labelName);
            this.panel2.Controls.Add(this.ptbAvt);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(-1, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(501, 662);
            this.panel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(299, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Blank Project";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(65, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Import Image";
            // 
            // btnBlank
            // 
            this.btnBlank.BackColor = System.Drawing.Color.LightGray;
            this.btnBlank.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBlank.FlatAppearance.BorderSize = 0;
            this.btnBlank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlank.Location = new System.Drawing.Point(292, 368);
            this.btnBlank.Name = "btnBlank";
            this.btnBlank.Size = new System.Drawing.Size(145, 115);
            this.btnBlank.TabIndex = 4;
            this.btnBlank.UseVisualStyleBackColor = false;
            this.btnBlank.Click += new System.EventHandler(this.btnBlank_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.LightGray;
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Location = new System.Drawing.Point(57, 368);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(145, 115);
            this.btnImport.TabIndex = 3;
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // labelName
            // 
            this.labelName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.White;
            this.labelName.Font = new System.Drawing.Font("Corbel", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(109, 205);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(238, 40);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Hello, username";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelName.TextChanged += new System.EventHandler(this.labelName_TextChanged);
            // 
            // ptbAvt
            // 
            this.ptbAvt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptbAvt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptbAvt.Location = new System.Drawing.Point(189, 112);
            this.ptbAvt.Name = "ptbAvt";
            this.ptbAvt.Size = new System.Drawing.Size(90, 90);
            this.ptbAvt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ptbAvt.TabIndex = 1;
            this.ptbAvt.TabStop = false;
            this.ptbAvt.Click += new System.EventHandler(this.ptbAvt_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnRename
            // 
            this.btnRename.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnRename.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnRename.BorderColor = System.Drawing.Color.Gray;
            this.btnRename.BorderRadius = 18;
            this.btnRename.BorderSize = 1;
            this.btnRename.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRename.FlatAppearance.BorderSize = 0;
            this.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRename.ForeColor = System.Drawing.Color.White;
            this.btnRename.Location = new System.Drawing.Point(439, 200);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(50, 45);
            this.btnRename.TabIndex = 8;
            this.btnRename.TextColor = System.Drawing.Color.White;
            this.btnRename.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnRename.UseVisualStyleBackColor = false;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLogout.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLogout.BorderColor = System.Drawing.Color.Gray;
            this.btnLogout.BorderRadius = 18;
            this.btnLogout.BorderSize = 1;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(23, 592);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(50, 45);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.TextColor = System.Drawing.Color.White;
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1422, 753);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1440, 800);
            this.MinimumSize = new System.Drawing.Size(1440, 800);
            this.Name = "HomeScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HomeScreen_FormClosed);
            this.SizeChanged += new System.EventHandler(this.Form1_Resize);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HomeScreen_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.PictureBox ptbAvt;
        private System.Windows.Forms.Button btnBlank;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.LinkLabel linkLabelLogin;
        private CustomButton.VBButton btnRename;
        private CustomButton.VBButton btnLogout;
    }
}