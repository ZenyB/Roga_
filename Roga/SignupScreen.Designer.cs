namespace Roga
{
    partial class SignupScreen
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
            this.pbBackground = new System.Windows.Forms.PictureBox();
            this.lbSignup = new System.Windows.Forms.Label();
            this.lbUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.btnSignup = new CustomButton.VBButton();
            this.lbLogin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBackground
            // 
            this.pbBackground.Location = new System.Drawing.Point(0, 0);
            this.pbBackground.Name = "pbBackground";
            this.pbBackground.Size = new System.Drawing.Size(514, 363);
            this.pbBackground.TabIndex = 1;
            this.pbBackground.TabStop = false;
            // 
            // lbSignup
            // 
            this.lbSignup.AutoSize = true;
            this.lbSignup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbSignup.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lbSignup.Location = new System.Drawing.Point(520, 60);
            this.lbSignup.Name = "lbSignup";
            this.lbSignup.Size = new System.Drawing.Size(123, 41);
            this.lbSignup.TabIndex = 2;
            this.lbSignup.Text = "Sign Up";
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbUsername.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbUsername.Location = new System.Drawing.Point(520, 101);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(81, 20);
            this.lbUsername.TabIndex = 4;
            this.lbUsername.Text = "Username*";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUsername.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtUsername.Location = new System.Drawing.Point(520, 124);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(195, 27);
            this.txtUsername.TabIndex = 6;
            this.txtUsername.Text = "Please Enter Your Name";
            this.txtUsername.Enter += new System.EventHandler(this.txtUsername_Enter);
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbPassword.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbPassword.Location = new System.Drawing.Point(520, 154);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(76, 20);
            this.lbPassword.TabIndex = 7;
            this.lbPassword.Text = "Password*";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtPassword.Location = new System.Drawing.Point(520, 177);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(195, 27);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.Text = "Enter Password";
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbName.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbName.Location = new System.Drawing.Point(520, 207);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(135, 20);
            this.lbName.TabIndex = 9;
            this.lbName.Text = "Confirm password*";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtConfirm.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtConfirm.Location = new System.Drawing.Point(520, 230);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(195, 27);
            this.txtConfirm.TabIndex = 10;
            this.txtConfirm.Text = "Confirm Password";
            this.txtConfirm.Enter += new System.EventHandler(this.txtConfirm_Enter);
            this.txtConfirm.Leave += new System.EventHandler(this.txtConfirm_Leave);
            // 
            // btnSignup
            // 
            this.btnSignup.BackColor = System.Drawing.Color.White;
            this.btnSignup.BackgroundColor = System.Drawing.Color.White;
            this.btnSignup.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnSignup.BorderRadius = 10;
            this.btnSignup.BorderSize = 2;
            this.btnSignup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSignup.FlatAppearance.BorderSize = 0;
            this.btnSignup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSignup.ForeColor = System.Drawing.Color.Black;
            this.btnSignup.Location = new System.Drawing.Point(520, 272);
            this.btnSignup.Name = "btnSignup";
            this.btnSignup.Size = new System.Drawing.Size(195, 27);
            this.btnSignup.TabIndex = 11;
            this.btnSignup.Text = "Sign Up";
            this.btnSignup.TextColor = System.Drawing.Color.Black;
            this.btnSignup.UseVisualStyleBackColor = false;
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbLogin.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbLogin.Location = new System.Drawing.Point(669, 332);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(46, 20);
            this.lbLogin.TabIndex = 12;
            this.lbLogin.Text = "Login";
            this.lbLogin.Click += new System.EventHandler(this.lbLogin_Click);
            // 
            // SignupScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 361);
            this.Controls.Add(this.lbLogin);
            this.Controls.Add(this.btnSignup);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.lbSignup);
            this.Controls.Add(this.pbBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignupScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roga";
            ((System.ComponentModel.ISupportInitialize)(this.pbBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBackground;
        private System.Windows.Forms.Label lbSignup;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox txtConfirm;
        private CustomButton.VBButton btnSignup;
        private System.Windows.Forms.Label lbLogin;
    }
}