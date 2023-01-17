using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archive_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settingsPanel.Height = 25;
        }

        private void gitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/W-KNOTTS/CST326-Milestone-Archive-Downloader");
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (settingsPanel.Height == 81)
            {
                settingsPanel.Height = 25;
            }
            else
            {
                settingsPanel.Height = 81;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ftpSettingsButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Height = 25;
            hideparts();
            userNamelabel.Show();
            passwordLabel.Show();
            userNameTextBox.Show();
            passwordTextBox.Show();
            saveButton.Show();
        }

        private void hideparts()
        {
            downloadButton.Hide();
            linkLabel.Hide();
            linkTextBox.Hide();
            conLabel.Hide();
            connectionComboBox.Hide();
            linkFileButton.Hide();
            linkFileLabel.Hide();
            resumeButton.Hide();
        }

        private void showParts()
        {
            downloadButton.Show();
            linkLabel.Show();
            linkTextBox.Show();
            conLabel.Show();
            connectionComboBox.Show();
            linkFileButton.Show();
            linkFileLabel.Show();
            resumeButton.Show();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            userNamelabel.Hide();
            passwordLabel.Hide();
            userNameTextBox.Hide();
            passwordTextBox.Hide();
            saveButton.Hide();
            showParts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            settingsPanel.Height = 25;
            logTextBox.Text = "";
            logTextBox.Text = "CST-326 Milestone Project Created By:\t William Knotts, Antowan Graham, Bradley Austin, Joseph Carrillo, Mara Munoz";
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            logTextBox.Text = "";
        }
    }
}
