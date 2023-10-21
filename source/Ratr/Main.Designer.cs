namespace Ratr
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            upload = new Button();
            username = new TextBox();
            password = new TextBox();
            label1 = new Label();
            label2 = new Label();
            icon = new PictureBox();
            author = new LinkLabel();
            about = new Label();
            how = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)icon).BeginInit();
            SuspendLayout();
            // 
            // upload
            // 
            upload.Location = new Point(111, 122);
            upload.Name = "upload";
            upload.Size = new Size(108, 23);
            upload.TabIndex = 0;
            upload.Text = "Upload .bin";
            upload.UseVisualStyleBackColor = true;
            upload.Click += UploadClick;
            // 
            // username
            // 
            username.Location = new Point(12, 171);
            username.Name = "username";
            username.ReadOnly = true;
            username.Size = new Size(307, 23);
            username.TabIndex = 1;
            // 
            // password
            // 
            password.Location = new Point(12, 219);
            password.Name = "password";
            password.ReadOnly = true;
            password.Size = new Size(307, 23);
            password.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(12, 150);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 3;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 201);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // icon
            // 
            icon.Image = Properties.Resources.router;
            icon.Location = new Point(111, 10);
            icon.Name = "icon";
            icon.Size = new Size(108, 106);
            icon.TabIndex = 8;
            icon.TabStop = false;
            // 
            // author
            // 
            author.AutoSize = true;
            author.LinkBehavior = LinkBehavior.AlwaysUnderline;
            author.LinkColor = Color.MediumSlateBlue;
            author.Location = new Point(12, 248);
            author.Name = "author";
            author.Size = new Size(46, 15);
            author.TabIndex = 7;
            author.TabStop = true;
            author.Text = "Jakiboy";
            author.LinkClicked += AuthorClicked;
            // 
            // about
            // 
            about.AutoSize = true;
            about.Enabled = false;
            about.Location = new Point(56, 248);
            about.Name = "about";
            about.Size = new Size(211, 15);
            about.TabIndex = 9;
            about.Text = "| For educational purposes only | v0.1.0";
            // 
            // how
            // 
            how.AutoSize = true;
            how.LinkColor = Color.MediumSlateBlue;
            how.Location = new Point(228, 127);
            how.Name = "how";
            how.Size = new Size(37, 15);
            how.TabIndex = 10;
            how.TabStop = true;
            how.Text = "How?";
            how.LinkClicked += HowClicked;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 272);
            Controls.Add(icon);
            Controls.Add(how);
            Controls.Add(about);
            Controls.Add(author);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(password);
            Controls.Add(username);
            Controls.Add(upload);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "Main";
            Text = "Ratr [ZTE Router Config Extractor]";
            TopMost = true;
            FormClosed += FormClose;
            Load += FormLoad;
            ((System.ComponentModel.ISupportInitialize)icon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button upload;
        private TextBox username;
        private TextBox password;
        private Label label1;
        private Label label2;
        private PictureBox icon;
        private LinkLabel author;
        private Label about;
        private LinkLabel how;
    }
}