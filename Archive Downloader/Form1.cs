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
using System.Runtime.InteropServices;
using System.Media;

namespace Archive_Downloader
{
    public partial class Form1 : Form
    {
        String SW0 = "";// -d The directory to store the downloaded file
        String SW1 = "";// -x The maximum number of connections to one server for each download.
        String SW2 = "";// -i Use link list file to download a list of links
        String SW3 = "";// -c, --continue [true|false]
        String SW4 = "";// -o, --out=<FILE>  The file name of the downloaded file. It is always relative to the directory given in --dir option
        String SW5 = "";// -h, Help menu
        String SPC = "";// This will be the space variable
        String Linkfile = "";// sets the text of linkTextBox as the link var
        String Link = "";// sets the text of linkTextBox as the link var
        String SAVEDIR = "";// The Save Directory for the downloads
        String CONS = "";// takes the selection from the combobox and converts it to a string to use with aria2
        String MaxCons = "";//--max-connection-per-server=4 --min-split-size=1M
        String LOGDIR = @"LOGS";
        Form2 f2 = new Form2();
        

        public Form1()
        {
            InitializeComponent();
            multiDLButton.Hide();//Start by hiding the multi download button
            downloadButton.Show();//Start by showing single link download button
        }

        //Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            
            settingsPanel.Height = 25;//Set height for settings bar
            this.panel1.MouseMove += new MouseEventHandler(drag_MouseMove);//Allow Drag From Tool Bar
            this.titleLabel.MouseMove += new MouseEventHandler(drag_MouseMove);//Allow Drag from App Title
            this.iconPictureBox.MouseMove += new MouseEventHandler(drag_MouseMove);//Allow Drag from App Icon
            //Print About Message in the log window
            logRichTextBox.Text = "Welcome to Archive Downloader Powered By Aria2. Please enter your link and number of connections to download a single file. To download multiple files please create a list of links, one link per line and select number of connections. Then click the download button to download your list of links.";

            if (maxSpeedCheckBox.Checked)
            {
                String MaxCons = "--max-connection-per-server=16 --min-split-size=1M";//--max-connection-per-server=4 --min-split-size=1M
            }
            else
            {
                String MaxCons = $"{SW1} {CONS}";//else will allow you to set the num of connections
            }
        }

        private void helpOptions()
        {
            String SW5 = "-h";// -h, Help menu
            this.Invoke((MethodInvoker)delegate
            {
                //Show messgae box when download fineshes
                // Use ProcessStartInfo class
                ProcessStartInfo startDL = new ProcessStartInfo();//Create process StartDL
                startDL.CreateNoWindow = true;//Do not create new terminal window for the task
                startDL.UseShellExecute = false;//Do not use shell to execute command
                startDL.RedirectStandardOutput = true;//Redirect terminal window to rich text box
                startDL.FileName = "Aria2/aria2c.exe";// Location and aria2.exe
                startDL.WindowStyle = ProcessWindowStyle.Hidden;//Hides command window
                startDL.Arguments = $"{SW5}";//Show help menu
                try
                {
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process dlProcess = Process.Start(startDL))
                    {
                        string Output = dlProcess.StandardOutput.ReadToEnd();//String for redirect of log output
                        dlProcess.WaitForExit();//Wait for DL to finish before exit
                        logRichTextBox.Text = Output;//Print output string redirect to the rich text box
                    }
                }

                catch
                {
                    //Catch error and create popup saying there was a download error.
                    System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
        }

        //Imports and settings for mouse drag
        public const int WM_NCLBUTTONDOWN = 0xA1;//Constant for mouse drag
        public const int HT_CAPTION = 0x2;//Other constant for mouse drag
        [DllImportAttribute("user32.dll")]//Import DLL for the aboility for dragging
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);//When mouse is held window will follow mouse
        [DllImportAttribute("user32.dll")]//Import DLL for the aboility for dragging
        public static extern bool ReleaseCapture();//release the window after mouse click is over

        //Drag function for moving form
        private void drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();//release the window after mouse click is over
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //Link label and Link for github page
        private void gitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/W-KNOTTS/Archive-Downloader");
        }

        //Settings button
        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (settingsPanel.Height == 100)//if the settings panel is expanded 
            {
                settingsPanel.Height = 25;//Shrink settings window
            }
            else
            {
                settingsPanel.Height = 100;//else grow the settings window
            }
        }

        //Close Button
        private void closeButton_Click(object sender, EventArgs e)
        {
            f2.Close();
            this.Close();//Closes out the program
        }

        //Quit Button
        private void quitButton_Click(object sender, EventArgs e)
        {
            f2.Close();
            this.Close();//Closes out the program
        }

        //FTP Sttings button
        private void ftpSettingsButton_Click(object sender, EventArgs e)
        {
            logRichTextBox.Text = "";//Clear log box
            settingsPanel.Height = 25;//Shrink settings
            Hideparts();//Hide parts function to hide main window items
            userNamelabel.Show();//Show username label 
            passwordLabel.Show();//show password label
            userNameTextBox.Show();//show username text box //////// PLACE HOLDER - Currently does nothing!!!
            passwordTextBox.Show();//show Password text box //////// PLACE HOLDER - Currently does nothing!!!
            saveButton.Show();//Show the save ftp user settings.
        }

        //Hideparts function to hide form items
        private void Hideparts()
        {
            multiDLButton.Hide();//Hide the multi download button
            downloadButton.Hide();//Hide the download button
            linkLabel.Hide();//Hide link label 
            linkTextBox.Hide();//Hide link text box
            conLabel.Hide();//Hide num of connections label
            connectionComboBox.Hide();//Hides cons combo boc
            linkFileButton.Hide();// Hide the link file button
            linkFileLabel.Hide();// hide link label
            resumeButton.Hide();// hide download resume button //////// DOES NOT FUNCTION YET - Place holder!!
        }

        //Shows hidden Items
        private void ShowParts()
        {
            downloadButton.Show();//Show download button
            linkLabel.Show();//show link label 
            linkTextBox.Show();//show link text box
            conLabel.Show();//show connection labels 
            connectionComboBox.Show();//show the combo box
            linkFileButton.Show();//show link file button
            linkFileLabel.Show();//show link file label
            resumeButton.Show();//show the resume button
        }

        //Save file button 
        private void saveButton_Click(object sender, EventArgs e)
        {
            userNamelabel.Hide();//Hide username label
            passwordLabel.Hide();//Hide Password Label
            userNameTextBox.Hide();//Hide ftp username box
            passwordTextBox.Hide();//Hide ftp password box
            saveButton.Hide();//Hide the save button
            ShowParts();//Run the show forms items function
        }

        //About Button
        private void button1_Click(object sender, EventArgs e)
        {
            settingsPanel.Height = 25;//Set settings bar size after selecting about
            logRichTextBox.Text = "";//Clear Log window
            logRichTextBox.Text = "CST-326 Milestone Project Created By:\n William Knotts,\n Antowan Graham,\n Bradley Austin,\n Joseph Carrillo,\n Mara Munoz";//Credits as a place holder
        }

        //Error Catch
        private static void process_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);//Show popup when an error is caught
        }

        //Jingle function to play an audio stream
        private void jingle()
        {
            SoundPlayer snd = new SoundPlayer(Properties.Resources.Rumper_Stumper);//new sound stream from resource file
            snd.PlaySync();//PlaySync forces audio stream to play until the end
        }

        //Show form2 loading gif
        private void showF2()
        {
            Thread t = new Thread(threadedLoad);// run form2 threaded        
            t.SetApartmentState(ApartmentState.MTA);
            t.Start(ApartmentState.MTA);
        }

        private void threadedLoad(object arg)
        {
            Application.Run(f2);//Run Form 2
        }

        //close form2 loading gif
        private void hideF2()
        {
            f2.Close();//close downloadin animation
            jingle();//Play sound until the end of the sound stream
        }

        //Download button
        private void downloadButton_Click(object sender, EventArgs e)
        {
            logRichTextBox.Text = "";//Clear rich text box log window
            downloadButton.Enabled = false; // disable button while saving report                  
            StartDL();//Run start download function
        }

        //Start Download Function
        private void StartDL()
        {
            makeSaveDir();//make save directory
            selSaveDir();//User Selected Save Directory
            showF2();//show form2

            // Variables, Strings, ect
            // Variables, Strings, ect
            String SW0 = "-d";// -d The directory to store the downloaded file
            String Link = linkTextBox.Text;// sets the text of linkTextBox as the link var
            String CONS = connectionComboBox.Text;// takes the selection from the combobox and converts it to a string to use with aria2

            //Invoker for downloading using ARIA2c
            this.Invoke((MethodInvoker)delegate
            {
                try
                {
                    // Use ProcessStartInfo class
                    ProcessStartInfo startDL = new ProcessStartInfo();//Create process StartDL
                    startDL.CreateNoWindow = true;//Do not create a new terminal window
                    startDL.UseShellExecute = false;//Do not use shell 
                    startDL.RedirectStandardOutput = true;//Redirect output to rich text box
                    startDL.FileName = "Aria2/aria2c.exe";//Location of Aria binary
                    startDL.WindowStyle = ProcessWindowStyle.Hidden;//Hide cmd window
                    startDL.Arguments = MaxCons + " " + Link + " " + SW0 + " " + SAVEDIR + "/";// Looks Like
                    //Command Example:  "aria2c.exe -x 16 www.site.com/download.zip -d Downloads"
                    // Call WaitForExit and then the using statement will close.
                    using (Process dlProcess = Process.Start(startDL))
                    {
                        string Output = dlProcess.StandardOutput.ReadToEnd();
                        dlProcess.WaitForExit();
                        logRichTextBox.Text = Output;
                    }
                    hideF2();//hide form 2
                }

                catch
                {
                    hideF2();//hide form 2
                    //Catch error and create popup saying there was a download error.
                    System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, (MessageBoxDefaultButton)MessageBoxOptions.DefaultDesktopOnly);
                    downloadButton.Enabled = true;//Reenable the download button
                    multiDLButton.Enabled = true;//Reenable the download button
                    multiDLButton.Hide();//Hide multidownload Button
                    downloadButton.Show();//Show single link DL Button
                }
            });

            string dt = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");//Create DT string to hold current date and time
            logRichTextBox.SaveFile($"{LOGDIR}/{dt}_Logs.txt", RichTextBoxStreamType.RichText);//Output log window to log file
            downloadButton.Enabled = true;//Reinable download button after download finished
            Application.Restart();
        }

        private void multiDLButton_Click(object sender, EventArgs e)
        {
            logRichTextBox.Text = "";//Clear rich text box log window
            downloadButton.Enabled = false; // disable button while saving report
            multiDLButton.Enabled = false;//disable button while saving report
            StartMultiDL();// Run Multidownload function from clicking the Multi download button.
        }

        //Mutli Download Function 
        private void StartMultiDL()
        {
            makeSaveDir();//make a save directory in the working dir
            selSaveDir();//use set download dir
            showF2();//show form2

            // Variables, Strings, ect
            String SW0 = "-d";// -d The directory to store the downloaded file
            String SW2 = "-i";// -i Use link list file to download a list of links
            String Linkfile = linkTextFileLabel.Text;// sets the text of linkTextBox as the link var
            String CONS = connectionComboBox.Text;// takes the selection from the combobox and converts it to a string to use with aria2

            this.Invoke((MethodInvoker)delegate
            {          
                try
                {
                    ProcessStartInfo startDL = new ProcessStartInfo();//Create process StartDL
                    startDL.CreateNoWindow = true;//Do not create new terminal window for the task
                    startDL.UseShellExecute = false;//Do not use shell to execute command
                    startDL.RedirectStandardOutput = true;//Redirect terminal window to rich text box
                    startDL.FileName = "Aria2/aria2c.exe";// Location and aria2.exe
                    startDL.WindowStyle = ProcessWindowStyle.Hidden;//Hides command window
                    startDL.Arguments = $"{SW2} {Linkfile} {MaxCons} {SW0} {SAVEDIR}/";//Example of link: "aria2c.exe -i link.txt -x 16 -d Downloads"
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process dlProcess = Process.Start(startDL))
                    {
                        string Output = dlProcess.StandardOutput.ReadToEnd();//String for redirect of log output
                        dlProcess.WaitForExit();//Wait for DL to finish before exit
                        logRichTextBox.Text = Output;//Print output string redirect to the rich text box
                    }
                    hideF2();//hide form 2
                }
                catch
                {
                    //Catch error and create popup saying there was a download error.
                    hideF2();//hide form 2
                    System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    downloadButton.Enabled = true;//Reenable the download button
                    multiDLButton.Enabled = true;//Reenable the download button
                    multiDLButton.Hide();//Hide multidownload Button
                    downloadButton.Show();//Show single link DL Button
                }
            });
            string dt = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");//Create DT string to hold current date and time
            logRichTextBox.SaveFile($"{LOGDIR}/{dt}_Logs.txt", RichTextBoxStreamType.RichText);//Output log window to log file
            downloadButton.Enabled = true;//Reenable the download button
            multiDLButton.Enabled = true;//Reenable the download button
            multiDLButton.Hide();//Hide multidownload Button
            downloadButton.Show();//Show single link DL Button
            Application.Restart();
        }

        //linkTextBox
        private void linkTextBox_TextChanged(object sender, EventArgs e)
        {
            //Nothing here to see at the time
        }

        //Help Button****************************************
        private void logButton_Click(object sender, EventArgs e)//Log Button is now the Help button
        {
            helpOptions();//Prints aria2 help information to the log window on load
        }

        //Home button in the settings 
        private void homeButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Height = 25;//Shrink settings option
            userNamelabel.Hide();//Hide FTP Username label
            passwordLabel.Hide();//Hide password label
            userNameTextBox.Hide();//Hide Username Text Box
            passwordTextBox.Hide();//Hide Password Text Box
            saveButton.Hide();//Hide Save Button
            ShowParts();//Run Show Parts function
        }

        //Link File Button
        private void linkFileButton_Click(object sender, EventArgs e)
        {
            downloadButton.Hide();//Hide the download button 
            multiDLButton.Show();//show the multidownload button
            OpenFileDialog resumeFile = new OpenFileDialog();//Open file search for text file
            resumeFile.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";//Only show .txt or all files when searching for a link list

            if (resumeFile.ShowDialog() == DialogResult.OK)//File added = OK ///////////////// NEED TO ADD RELITIVE AND ABSOLUTE PATHS
            {
                //Currently only works with TEXT FILE in the working Directory
                pathLabel.Text = "Link File Added: ";//Print Link File Added to the label to the right of the button
                linkTextFileLabel.Text = Path.GetFileName(resumeFile.FileName);// Print File Name in the log window
            }
        }

        private void selSaveDir()
        {
            saveDirLabel.Text = "";// Clear label text
            FolderBrowserDialog fbd = new FolderBrowserDialog();//New Open folder window
            fbd.Description = "Select Your Download(s) Save Location";//File Browser info
            if (fbd.ShowDialog() == DialogResult.OK)//If folder selection is OK
            {
                string sSelectedPath = fbd.SelectedPath;//String selected dir path
                saveDirLabel.Text = $"{fbd.SelectedPath}".ToString();//store path in directory label
                SAVEDIR = saveDirLabel.Text;
            }
        }

        //User selected save folder
        private void makeSaveDir()
        {
            DirectoryInfo di = Directory.CreateDirectory(LOGDIR);//Create directory
            if (Directory.Exists(LOGDIR))//If download dir is there, say its there
            {
                logRichTextBox.Text = ("Log Directory Exists: " + LOGDIR);//Show info in log box
            }

            else//If its not there say its not there, print in log window and create the dir
            {
                logRichTextBox.Text = ("Log Directory Does Not Exist Creating Directory: " + LOGDIR);// Show message in the log box
            }
        }

        private void maxSpeedCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
