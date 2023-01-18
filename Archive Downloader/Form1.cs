using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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
            System.Diagnostics.Process.Start("https://github.com/W-KNOTTS/Archive-Downloader");
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (settingsPanel.Height == 100)
            {
                settingsPanel.Height = 25;
            }
            else
            {
                settingsPanel.Height = 100;
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
            logRichTextBox.Text = "";
            settingsPanel.Height = 25;
            Hideparts();
            userNamelabel.Show();
            passwordLabel.Show();
            userNameTextBox.Show();
            passwordTextBox.Show();
            saveButton.Show();
        }

        private void Hideparts()
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

        private void ShowParts()
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
            ShowParts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            settingsPanel.Height = 25;
            logRichTextBox.Text = "";
            logRichTextBox.Text = "CST-326 Milestone Project Created By:\n William Knotts,\n Antowan Graham,\n Bradley Austin,\n Joseph Carrillo,\n Mara Munoz";
        }

        private static void process_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } 

        private void downloadButton_Click(object sender, EventArgs e)
        {

            logRichTextBox.Text = "";
            downloadButton.Enabled = false; // disable button while saving report                  
            StartDL();
        }

       

        private void StartDL()
        {


            // Variables, Strings, ect
            string link = linkTextBox.Text;// sets the text of linkTextBox as the link var
            string cons = connectionComboBox.Text;// takes the selection from the combobox and converts it to a string to use with aria2
            string saveDir = "-d";//The directory to store the downloaded file
            string sw1 = "-x";//The maximum number of connections to one server for each download.
            var dirName = $@"Downloads";

            //For Testing commands
            DirectoryInfo di = Directory.CreateDirectory(dirName);
            if (Directory.Exists(dirName))//If download dir is there, say its there
            {
                logRichTextBox.Text = ("Download Directory Exists\nNow Downloading, Please Wait. \nA full Log Will Be Shown Here.");

            }

            else//If its not there say its not there, print in log window and create the dir
            {
                logRichTextBox.Text = ("Download Directory Does Not Exist\nCreating Directory\nNow Downloading, Please Wait. \nA full Log Will Be Shown Here.");

            }

            this.Invoke((MethodInvoker)delegate
            {
                //loadingLabel.Text = "Now Downloading, Please Wait. A full Log Will Be Shown Here.";
                System.Windows.Forms.MessageBox.Show("Download Started\nClick OK To Continue", "Download Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Use ProcessStartInfo class
                ProcessStartInfo startDL = new ProcessStartInfo();//Create process StartDL
                startDL.CreateNoWindow = true;
                startDL.UseShellExecute = false;
                startDL.RedirectStandardOutput = true;
                startDL.FileName = "Aria2/aria2c.exe";
                startDL.WindowStyle = ProcessWindowStyle.Hidden;
                startDL.Arguments = sw1 + " " + cons + " " + link + " " + saveDir + " " + dirName;
                try
                {            
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process dlProcess = Process.Start(startDL))
                    {
                        string Output = dlProcess.StandardOutput.ReadToEnd();
                        dlProcess.WaitForExit();
                        logRichTextBox.Text = Output;
                        System.Windows.Forms.MessageBox.Show("Download Finished", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                catch
                {
                    System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
            string dt = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
            logRichTextBox.SaveFile($"Downloads/{dt}_Logs.txt", RichTextBoxStreamType.RichText);
            downloadButton.Enabled = true;
        }

        

        private void linkTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void logButton_Click(object sender, EventArgs e)
        {
            logRichTextBox.Text = "Welcome to Archive Downloader Powered By Aria2. Please enter your link and number of connections to download a single file. To download multiple files please create a list of links, one link per line and select number of connections. Then click the download button to download your list of links.";
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Height = 25;
            userNamelabel.Hide();
            passwordLabel.Hide();
            userNameTextBox.Hide();
            passwordTextBox.Hide();
            saveButton.Hide();
            ShowParts();
        }

        private void loadPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void linkFileButton_Click(object sender, EventArgs e)
        {
            downloadButton.Hide();
            multiDLButton.Show();
            OpenFileDialog resumeFile = new OpenFileDialog();
            resumeFile.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";

            if (resumeFile.ShowDialog() == DialogResult.OK)
            {
                pathLabel.Text = "Link File Added";
                logRichTextBox.Text = Path.GetFileName(resumeFile.FileName);
            }

        }

        private void StartMultiDL()
        {


            // Variables, Strings, ect
            string linkfile = logRichTextBox.Text;// sets the text of linkTextBox as the link var
            string cons = connectionComboBox.Text;// takes the selection from the combobox and converts it to a string to use with aria2
            string saveDir = "-d";//The directory to store the downloaded file
            string sw1 = "-x";//The maximum number of connections to one server for each download.
            string sw2 = "-i";
            var dirName = $@"Downloads";

            //For Testing commands
            DirectoryInfo di = Directory.CreateDirectory(dirName);
            if (Directory.Exists(dirName))//If download dir is there, say its there
            {
                logRichTextBox.Text = ("Download Directory Exists\nNow Downloading, Please Wait. \nA full Log Will Be Shown Here.");
            }

            else//If its not there say its not there, print in log window and create the dir
            {
                logRichTextBox.Text = ("Download Directory Does Not Exist\nCreating Directory\nNow Downloading, Please Wait. \nA full Log Will Be Shown Here.");

            }

            this.Invoke((MethodInvoker)delegate
            {
                //loadingLabel.Text = "Now Downloading, Please Wait. A full Log Will Be Shown Here.";
                System.Windows.Forms.MessageBox.Show("Download Finished", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Use ProcessStartInfo class
                ProcessStartInfo startDL = new ProcessStartInfo();//Create process StartDL
                startDL.CreateNoWindow = true;
                startDL.UseShellExecute = false;
                startDL.RedirectStandardOutput = true;
                startDL.FileName = "Aria2/aria2c.exe";
                startDL.WindowStyle = ProcessWindowStyle.Hidden;
                startDL.Arguments = $"{sw2} {linkfile} {sw1} {cons} {saveDir} {dirName}";
                try
                {
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process dlProcess = Process.Start(startDL))
                    {
                        string Output = dlProcess.StandardOutput.ReadToEnd();
                        dlProcess.WaitForExit();
                        logRichTextBox.Text = Output;
                        System.Windows.Forms.MessageBox.Show("Download Finished", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                catch
                {
                    System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
            string dt = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
            logRichTextBox.SaveFile($"Downloads/{dt}_Logs.txt", RichTextBoxStreamType.RichText);
            downloadButton.Enabled = true;
            multiDLButton.Enabled = true;
            multiDLButton.Hide();
            downloadButton.Show();
            pathLabel.Text = "";

        }

        private void multiDLButton_Click(object sender, EventArgs e)
        {
            StartMultiDL();

        }
    }
}
