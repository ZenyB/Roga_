namespace Roga
{
    partial class LoginScreen
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
            this.lbLogin = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pbBackground = new System.Windows.Forms.PictureBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lbSignup = new System.Windows.Forms.Label();
            this.vbButton1 = new CustomButton.VBButton();
            this.btnLogin = new CustomButton.VBButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbLogin.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lbLogin.Location = new System.Drawing.Point(516, 75);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(103, 46);
            this.lbLogin.TabIndex = 1;
            this.lbLogin.Text = "Login";
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(563, 12);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(124, 68);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 2;
            this.pbLogo.TabStop = false;
            // 
            // pbBackground
            // 
            this.pbBackground.Location = new System.Drawing.Point(0, 0);
            this.pbBackground.Name = "pbBackground";
            this.pbBackground.Size = new System.Drawing.Size(514, 363);
            this.pbBackground.TabIndex = 0;
            this.pbBackground.TabStop = false;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbUsername.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbUsername.Location = new System.Drawing.Point(520, 125);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(81, 20);
            this.lbUsername.TabIndex = 3;
            this.lbUsername.Text = "Username*";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbPassword.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbPassword.Location = new System.Drawing.Point(520, 178);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(76, 20);
            this.lbPassword.TabIndex = 4;
            this.lbPassword.Text = "Password*";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUsername.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtUsername.Location = new System.Drawing.Point(520, 148);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(195, 27);
            this.txtUsername.TabIndex = 5;
            this.txtUsername.Text = "Please Enter Your Name";
            this.txtUsername.Enter += new System.EventHandler(this.txtUsername_Enter);
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtPassword.Location = new System.Drawing.Point(520, 201);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(195, 27);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Text = "Please Enter Password";
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // lbSignup
            // 
            this.lbSignup.AutoSize = true;
            this.lbSignup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbSignup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbSignup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbSignup.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbSignup.Location = new System.Drawing.Point(520, 268);
            this.lbSignup.Name = "lbSignup";
            this.lbSignup.Size = new System.Drawing.Size(135, 20);
            this.lbSignup.TabIndex = 8;
            this.lbSignup.Text = "Create an account?";
            this.lbSignup.Click += new System.EventHandler(this.lbSignup_Click);
            // 
            // vbButton1
            // 
            this.vbButton1.BackColor = System.Drawing.Color.White;
            this.vbButton1.BackgroundColor = System.Drawing.Color.White;
            this.vbButton1.BorderColor = System.Drawing.Color.SteelBlue;
            this.vbButton1.BorderRadius = 10;
            this.vbButton1.BorderSize = 2;
            this.vbButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.vbButton1.FlatAppearance.BorderSize = 0;
            this.vbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton1.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.vbButton1.ForeColor = System.Drawing.Color.Black;
            this.vbButton1.Location = new System.Drawing.Point(520, 291);
            this.vbButton1.Name = "vbButton1";
            this.vbButton1.Size = new System.Drawing.Size(195, 27);
            this.vbButton1.TabIndex = 9;
            this.vbButton1.Text = "Continue without account";
            this.vbButton1.TextColor = System.Drawing.Color.Black;
            this.vbButton1.UseVisualStyleBackColor = false;
            this.vbButton1.Click += new System.EventHandler(this.vbButton1_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.White;
            this.btnLogin.BackgroundColor = System.Drawing.Color.White;
            this.btnLogin.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnLogin.BorderRadius = 10;
            this.btnLogin.BorderSize = 2;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.Location = new System.Drawing.Point(520, 234);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(195, 27);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Login";
            this.btnLogin.TextColor = System.Drawing.Color.Black;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 361);
            this.Controls.Add(this.vbButton1);
            this.Controls.Add(this.lbSignup);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.lbLogin);
            this.Controls.Add(this.pbBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roga";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBackground;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private CustomButton.VBButton btnLogin;
        private System.Windows.Forms.Label lbSignup;
        private CustomButton.VBButton vbButton1;
    }
}