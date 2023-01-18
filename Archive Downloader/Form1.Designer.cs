
namespace Archive_Downloader
{
    partial class Form1
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
            this.linkTextBox = new System.Windows.Forms.TextBox();
            this.linkLabel = new System.Windows.Forms.Label();
            this.conLabel = new System.Windows.Forms.Label();
            this.connectionComboBox = new System.Windows.Forms.ComboBox();
            this.downloadButton = new System.Windows.Forms.Button();
            this.linkFileButton = new System.Windows.Forms.Button();
            this.linkFileLabel = new System.Windows.Forms.Label();
            this.logButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.gitLinkLabel = new System.Windows.Forms.LinkLabel();
            this.resumeButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.aboutButton = new System.Windows.Forms.Button();
            this.ftpSettingsButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.userNamelabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.multiDLButton = new System.Windows.Forms.Button();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // linkTextBox
            // 
            this.linkTextBox.Location = new System.Drawing.Point(111, 37);
            this.linkTextBox.Name = "linkTextBox";
            this.linkTextBox.Size = new System.Drawing.Size(228, 20);
            this.linkTextBox.TabIndex = 0;
            this.linkTextBox.Text = "https://archive.org/download/genpsp0_01_11.7z/genpsp0_01_11.zip";
            this.linkTextBox.TextChanged += new System.EventHandler(this.linkTextBox_TextChanged);
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Location = new System.Drawing.Point(27, 40);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(84, 13);
            this.linkLabel.TabIndex = 1;
            this.linkLabel.Text = "Download Link: ";
            // 
            // conLabel
            // 
            this.conLabel.AutoSize = true;
            this.conLabel.Location = new System.Drawing.Point(343, 40);
            this.conLabel.Name = "conLabel";
            this.conLabel.Size = new System.Drawing.Size(126, 13);
            this.conLabel.TabIndex = 2;
            this.conLabel.Text = "Number Of Connections: ";
            // 
            // connectionComboBox
            // 
            this.connectionComboBox.FormattingEnabled = true;
            this.connectionComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.connectionComboBox.Location = new System.Drawing.Point(468, 37);
            this.connectionComboBox.Name = "connectionComboBox";
            this.connectionComboBox.Size = new System.Drawing.Size(48, 21);
            this.connectionComboBox.TabIndex = 3;
            this.connectionComboBox.Text = "16";
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(441, 75);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 23);
            this.downloadButton.TabIndex = 4;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // linkFileButton
            // 
            this.linkFileButton.Location = new System.Drawing.Point(155, 75);
            this.linkFileButton.Name = "linkFileButton";
            this.linkFileButton.Size = new System.Drawing.Size(90, 23);
            this.linkFileButton.TabIndex = 5;
            this.linkFileButton.Text = "Select Link File";
            this.linkFileButton.UseVisualStyleBackColor = true;
            this.linkFileButton.Click += new System.EventHandler(this.linkFileButton_Click);
            // 
            // linkFileLabel
            // 
            this.linkFileLabel.AutoSize = true;
            this.linkFileLabel.Location = new System.Drawing.Point(27, 80);
            this.linkFileLabel.Name = "linkFileLabel";
            this.linkFileLabel.Size = new System.Drawing.Size(128, 13);
            this.linkFileLabel.TabIndex = 6;
            this.linkFileLabel.Text = "Download Multiple Links: ";
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(30, 400);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(75, 23);
            this.logButton.TabIndex = 8;
            this.logButton.Text = "Help";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(441, 400);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 23);
            this.quitButton.TabIndex = 9;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // gitLinkLabel
            // 
            this.gitLinkLabel.AutoSize = true;
            this.gitLinkLabel.Location = new System.Drawing.Point(222, 428);
            this.gitLinkLabel.Name = "gitLinkLabel";
            this.gitLinkLabel.Size = new System.Drawing.Size(94, 13);
            this.gitLinkLabel.TabIndex = 10;
            this.gitLinkLabel.TabStop = true;
            this.gitLinkLabel.Text = "Link to our Github ";
            this.gitLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.gitLinkLabel_LinkClicked);
            // 
            // resumeButton
            // 
            this.resumeButton.Location = new System.Drawing.Point(206, 400);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(126, 23);
            this.resumeButton.TabIndex = 11;
            this.resumeButton.Text = "Resume Download";
            this.resumeButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 25);
            this.panel1.TabIndex = 12;
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = global::Archive_Downloader.Properties.Resources.close;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeButton.Location = new System.Drawing.Point(521, -2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(30, 29);
            this.closeButton.TabIndex = 3;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // settingsPanel
            // 
            this.settingsPanel.BackColor = System.Drawing.Color.Silver;
            this.settingsPanel.Controls.Add(this.aboutButton);
            this.settingsPanel.Controls.Add(this.ftpSettingsButton);
            this.settingsPanel.Controls.Add(this.homeButton);
            this.settingsPanel.Controls.Add(this.settingsButton);
            this.settingsPanel.Location = new System.Drawing.Point(410, 0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(114, 100);
            this.settingsPanel.TabIndex = 13;
            // 
            // aboutButton
            // 
            this.aboutButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.aboutButton.FlatAppearance.BorderSize = 0;
            this.aboutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aboutButton.Location = new System.Drawing.Point(0, 75);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(114, 25);
            this.aboutButton.TabIndex = 2;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ftpSettingsButton
            // 
            this.ftpSettingsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ftpSettingsButton.FlatAppearance.BorderSize = 0;
            this.ftpSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ftpSettingsButton.Location = new System.Drawing.Point(0, 50);
            this.ftpSettingsButton.Name = "ftpSettingsButton";
            this.ftpSettingsButton.Size = new System.Drawing.Size(114, 25);
            this.ftpSettingsButton.TabIndex = 1;
            this.ftpSettingsButton.Text = "FTP Settings";
            this.ftpSettingsButton.UseVisualStyleBackColor = true;
            this.ftpSettingsButton.Click += new System.EventHandler(this.ftpSettingsButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.homeButton.FlatAppearance.BorderSize = 0;
            this.homeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homeButton.Location = new System.Drawing.Point(0, 25);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(114, 25);
            this.homeButton.TabIndex = 3;
            this.homeButton.Text = "Home";
            this.homeButton.UseVisualStyleBackColor = true;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingsButton.FlatAppearance.BorderSize = 0;
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.Location = new System.Drawing.Point(0, 0);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(114, 25);
            this.settingsButton.TabIndex = 0;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // userNamelabel
            // 
            this.userNamelabel.AutoSize = true;
            this.userNamelabel.Location = new System.Drawing.Point(27, 40);
            this.userNamelabel.Name = "userNamelabel";
            this.userNamelabel.Size = new System.Drawing.Size(61, 13);
            this.userNamelabel.TabIndex = 14;
            this.userNamelabel.Text = "Username: ";
            this.userNamelabel.Visible = false;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(27, 75);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 13);
            this.passwordLabel.TabIndex = 15;
            this.passwordLabel.Text = "Password: ";
            this.passwordLabel.Visible = false;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(94, 37);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.userNameTextBox.TabIndex = 16;
            this.userNameTextBox.Visible = false;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(94, 72);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 17;
            this.passwordTextBox.Visible = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(223, 52);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 18;
            this.saveButton.Text = "Save User";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Visible = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Location = new System.Drawing.Point(30, 129);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.Size = new System.Drawing.Size(486, 240);
            this.logRichTextBox.TabIndex = 19;
            this.logRichTextBox.Text = "";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(251, 80);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(0, 13);
            this.pathLabel.TabIndex = 20;
            // 
            // multiDLButton
            // 
            this.multiDLButton.Location = new System.Drawing.Point(441, 75);
            this.multiDLButton.Name = "multiDLButton";
            this.multiDLButton.Size = new System.Drawing.Size(75, 23);
            this.multiDLButton.TabIndex = 21;
            this.multiDLButton.Text = "Download";
            this.multiDLButton.UseVisualStyleBackColor = true;
            this.multiDLButton.Click += new System.EventHandler(this.multiDLButton_Click);
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox.BackgroundImage = global::Archive_Downloader.Properties.Resources.aria2DL;
            this.iconPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.iconPictureBox.ErrorImage = global::Archive_Downloader.Properties.Resources.loading;
            this.iconPictureBox.InitialImage = null;
            this.iconPictureBox.Location = new System.Drawing.Point(-1, 0);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(26, 25);
            this.iconPictureBox.TabIndex = 22;
            this.iconPictureBox.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.titleLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.Desktop;
            this.titleLabel.Location = new System.Drawing.Point(28, 3);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(283, 19);
            this.titleLabel.TabIndex = 23;
            this.titleLabel.Text = "Archive Downloader: Powered By - ARIA2c";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 450);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.multiDLButton);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.logRichTextBox);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userNamelabel);
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.gitLinkLabel);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.linkFileLabel);
            this.Controls.Add(this.linkFileButton);
            this.Controls.Add(this.connectionComboBox);
            this.Controls.Add(this.conLabel);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.linkTextBox);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archive Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.settingsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox linkTextBox;
        private System.Windows.Forms.Label linkLabel;
        private System.Windows.Forms.Label conLabel;
        private System.Windows.Forms.ComboBox connectionComboBox;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button linkFileButton;
        private System.Windows.Forms.Label linkFileLabel;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.LinkLabel gitLinkLabel;
        private System.Windows.Forms.Button resumeButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button ftpSettingsButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label userNamelabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Button multiDLButton;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.Label titleLabel;
    }
}

