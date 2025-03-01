﻿namespace Ratr
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
            userlabel = new Label();
            pswdlabel = new Label();
            icon = new PictureBox();
            model = new ComboBox();
            menu = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            howToExportMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            aboutMenuItem = new ToolStripMenuItem();
            resultGroup = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)icon).BeginInit();
            menu.SuspendLayout();
            resultGroup.SuspendLayout();
            SuspendLayout();
            // 
            // upload
            // 
            upload.Enabled = false;
            upload.Location = new Point(67, 153);
            upload.Name = "upload";
            upload.Size = new Size(189, 23);
            upload.TabIndex = 0;
            upload.Text = "Choose file";
            upload.UseVisualStyleBackColor = true;
            upload.Click += UploadClick;
            // 
            // username
            // 
            username.Enabled = false;
            username.Location = new Point(17, 49);
            username.Name = "username";
            username.ReadOnly = true;
            username.Size = new Size(276, 23);
            username.TabIndex = 1;
            // 
            // password
            // 
            password.Enabled = false;
            password.Location = new Point(17, 97);
            password.Name = "password";
            password.ReadOnly = true;
            password.Size = new Size(276, 23);
            password.TabIndex = 2;
            // 
            // userlabel
            // 
            userlabel.AutoSize = true;
            userlabel.ImageAlign = ContentAlignment.MiddleLeft;
            userlabel.Location = new Point(17, 28);
            userlabel.Name = "userlabel";
            userlabel.Size = new Size(60, 15);
            userlabel.TabIndex = 3;
            userlabel.Text = "Username";
            // 
            // pswdlabel
            // 
            pswdlabel.AutoSize = true;
            pswdlabel.Location = new Point(17, 79);
            pswdlabel.Name = "pswdlabel";
            pswdlabel.Size = new Size(57, 15);
            pswdlabel.TabIndex = 4;
            pswdlabel.Text = "Password";
            // 
            // icon
            // 
            icon.Image = Properties.Resources.router;
            icon.Location = new Point(111, 27);
            icon.Name = "icon";
            icon.Size = new Size(98, 91);
            icon.SizeMode = PictureBoxSizeMode.CenterImage;
            icon.TabIndex = 8;
            icon.TabStop = false;
            // 
            // model
            // 
            model.DropDownStyle = ComboBoxStyle.DropDownList;
            model.Items.AddRange(new object[] { "Select Model", "ZTE (ZXHN H267N)", "Huawei (DG8245V-10)" });
            model.Location = new Point(67, 124);
            model.Name = "model";
            model.Size = new Size(189, 23);
            model.TabIndex = 11;
            model.SelectedIndexChanged += ModelSelectedIndexChanged;
            // 
            // menu
            // 
            menu.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });
            menu.Location = new Point(0, 0);
            menu.Name = "menu";
            menu.Size = new Size(334, 24);
            menu.TabIndex = 12;
            menu.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { howToExportMenuItem, toolStripSeparator1, aboutMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(50, 20);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // howToExportMenuItem
            // 
            howToExportMenuItem.Name = "howToExportMenuItem";
            howToExportMenuItem.Size = new Size(219, 22);
            howToExportMenuItem.Text = "How to export router data ?";
            howToExportMenuItem.Click += HowToExportMenuItemClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(216, 6);
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new Size(219, 22);
            aboutMenuItem.Text = "About";
            aboutMenuItem.Click += AboutMenuItemClick;
            // 
            // resultGroup
            // 
            resultGroup.Controls.Add(username);
            resultGroup.Controls.Add(password);
            resultGroup.Controls.Add(userlabel);
            resultGroup.Controls.Add(pswdlabel);
            resultGroup.Location = new Point(12, 211);
            resultGroup.Name = "resultGroup";
            resultGroup.Size = new Size(310, 136);
            resultGroup.TabIndex = 13;
            resultGroup.TabStop = false;
            resultGroup.Text = "Result (PPP decoded data)";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 359);
            Controls.Add(resultGroup);
            Controls.Add(model);
            Controls.Add(icon);
            Controls.Add(upload);
            Controls.Add(menu);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = menu;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "Main";
            Text = "Ratr [Router Config Extractor]";
            TopMost = true;
            FormClosed += FormClose;
            Load += FormLoad;
            ((System.ComponentModel.ISupportInitialize)icon).EndInit();
            menu.ResumeLayout(false);
            menu.PerformLayout();
            resultGroup.ResumeLayout(false);
            resultGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button upload;
        private TextBox username;
        private TextBox password;
        private Label userlabel;
        private Label pswdlabel;
        private PictureBox icon;
        private ComboBox model;
        private MenuStrip menu;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem howToExportMenuItem;
        private ToolStripMenuItem aboutMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private GroupBox resultGroup;
    }
}