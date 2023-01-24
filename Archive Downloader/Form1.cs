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
using System.Data.OleDb;
using Archive_Downloader.Properties;

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
        String FS = "/";
        String LOGDIR = @"LOGS";
        String NOALO = "--file - allocation = none";
        String FTPU = "";
        String FTPP = "";
        String HTTPU = "";
        String HTTPP = "";
        String acctFile = @"DB/acct.accdb";
        Form2 f2 = new Form2();

        //Imports and settings for mouse drag
        public const int WM_NCLBUTTONDOWN = 0xA1;//Constant for mouse drag
        public const int HT_CAPTION = 0x2;//Other constant for mouse drag
        [DllImportAttribute("user32.dll")]//Import DLL for the aboility for dragging
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);//When mouse is held window will follow mouse
        [DllImportAttribute("user32.dll")]//Import DLL for the aboility for dragging
        public static extern bool ReleaseCapture();//release the window after mouse click is over

        public Form1()
        {
            InitializeComponent();
            showDLlinks();
            multiDLButton.Hide();//Start by hiding the multi download button
            downloadButton.Show();//Start by showing single link download button
            Link = String.Format(linkTextBox.Text);// sets the text of linkTextBox as the link var
        }

        //Form Load
        private void Form1_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'acctDataSet.Links' table. You can move, or remove it, as needed.
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            this.linksTableAdapter.Fill(this.acctDataSet.Links);

            LastLog();
            settingsPanel.Height = 25;//Set height for settings bar
            this.panel1.MouseMove += new MouseEventHandler(drag_MouseMove);//Allow Drag From Tool Bar
            this.titleLabel.MouseMove += new MouseEventHandler(drag_MouseMove);//Allow Drag from App Title
            this.iconPictureBox.MouseMove += new MouseEventHandler(drag_MouseMove);//Allow Drag from App Icon
        }

        //Drag function for moving form
        private void drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();//release the window after mouse click is over
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //Load Last Log File
        private void LastLog()
        {
            //Looks for last created Log File in the LOGS directory
            var directory = new DirectoryInfo("LOGS");
            var myLog = (from f in directory.GetFiles()
                         orderby f.LastWriteTime descending
                         select f).First();

            //Copies log file name to LogRichTextBox
            logRichTextBox.Text = String.Format(myLog.ToString());
            string LOGFILES = logRichTextBox.Text;
            string LOGFILESp = @"LOGS/";
            string LLFp = LOGFILESp + LOGFILES;//Adding strings to create Path plus Logfile name
            if (File.Exists(LLFp))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(LLFp);//Create ne file stream to read last log file
                logRichTextBox.Text = sr.ReadToEnd();//Print stream of log file to rich text box
                sr.Close();//Close file stream
            }
            else
            {
                logRichTextBox.Text = "No Log Files At This Time";
            }
        }

        private void showDLlinks()
        {
            //dataGridView1.Refresh();
            // Simplify object initialization
            System.Data.OleDb.OleDbConnection conn = new OleDbConnection();
            // Simplify object initialization
            //var MyXML = "Scores.xml";
            var db = "acct.accdb";
            var db2 = "DB/acct.accdb";
            var Table1 = "Links";
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data source=DB/" + db;
            string strDSN = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data source=DB/acct.accdb";
            string strSQL = "SELECT * FROM Links";


            OleDbConnection myConn = new OleDbConnection(strDSN);
            OleDbDataAdapter myCmd = new OleDbDataAdapter(strSQL, myConn);
            try
            {
                if (File.Exists(db2))//If file exists read and output to the data grid
                {

                    myConn.Open();//opens file stream
                    DataSet dtSet = new DataSet();
                    myCmd.Fill(dtSet, Table1);//Copies table scores to the data grid
                    DataTable dTable = dtSet.Tables[0];
                    dataGridView1.DataSource = dtSet.Tables[Table1].DefaultView;//Set the data grid source as the accdb file 


                    //Below is an example on how to write it as XML instead of the .accdb
                    //dtSet.Tables[Table1].WriteXml(@"Links.xml");//write xml from datagrid
                                                                 //dtSet.ReadXml(@"Scores.xml");//read from xml to data grid
                                                                 //dataGridView1.DataSource = dtSet.Tables[0];

                }
                else if (!File.Exists(db2))//If it does not exist copy from resource files
                {

                    using (var save = new SaveFileDialog())
                    {
                        save.Title = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);//sets path of db file to the same as the exe
                        save.FileName = "acct.accdb";//sets name 
                        save.Filter = "Access files|*.accdb";


                        var dialog = save.ShowDialog();
                        if (dialog != DialogResult.OK)
                            return;
                        System.IO.File.WriteAllBytes(save.FileName, Resources.acct);
                    }
                }
                dataGridView1.Columns[0].Visible = false;
            }


            catch (Exception ex)
            {
                MessageBox.Show("Failed due to" + ex.Message);
            }
            finally
            {
                myConn.Close();
                conn.Close();
            }
        }
 
        //Save Links
        private void saveLinks()
        {
            string myLink = linkTextBox.Text;

            using (OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", acctFile)))
            {
                using (OleDbCommand insertCommand = new OleDbCommand("INSERT INTO Links ([Downloads]) VALUES (?)", connection))
                {
                    connection.Open();

                    insertCommand.Parameters.AddWithValue(@"Downloads", $"{myLink}");

                    insertCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        //Save credentials to acct.accdb
        private void SaveFTPAcct()
        {
            //var acctFile2 = Properties.Resources.acct;
            String FTPU = String.Format(userNameTextBox.Text);
            String FTPP = String.Format(passwordTextBox.Text);

            using (OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", acctFile)))
            {
                using (OleDbCommand updateCommand = new OleDbCommand("UPDATE FTP SET [Name] = ?, [Password] = ? WHERE [ID] = ?", connection))
                {
                    connection.Open();

                    updateCommand.Parameters.AddWithValue("@Name", $"{FTPU}");
                    updateCommand.Parameters.AddWithValue("@Password", $"{FTPP}");
                    updateCommand.Parameters.AddWithValue("@ID", 1);
                    updateCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        //Save http credentials to acct.accdb
        private void SaveHTTPAcct()
        {
            //var acctFile2 = Properties.Resources.acct;
            String HTTPPU = String.Format(userNameTextBox.Text);
            String HTTPP = String.Format(passwordTextBox.Text);

            using (OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", acctFile)))
            {
                using (OleDbCommand updateCommand = new OleDbCommand("UPDATE HTTP SET [Name] = ?, [Password] = ? WHERE [ID] = ?", connection))
                {
                    connection.Open();

                    updateCommand.Parameters.AddWithValue("@Name", $"{HTTPPU}");
                    updateCommand.Parameters.AddWithValue("@Password", $"{HTTPP}");
                    updateCommand.Parameters.AddWithValue("@ID", 1);
                    updateCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private void accessHttpDB()
        {
            try
            {
                if (File.Exists(acctFile))
                {
                    using (OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", acctFile)))
                    {
                        using (OleDbCommand selectCommand = new OleDbCommand("SELECT TOP 1 * FROM HTTP", connection))
                        {
                            connection.Open();

                            DataTable table = new DataTable();
                            OleDbDataAdapter adapter = new OleDbDataAdapter();
                            adapter.SelectCommand = selectCommand;
                            adapter.Fill(table);

                            foreach (DataRow row in table.Rows)
                            {
                                object NameValue = row["Name"];
                                object PasswordValue = row["Password"];

                                userNameTextBox.Text = String.Format(NameValue.ToString());
                                passwordTextBox.Text = String.Format(PasswordValue.ToString());
                            }
                        }
                        connection.Close();
                    }
                }
                else
                {
                    userNameTextBox.Text = String.Format("Enter User");
                    passwordTextBox.Text = String.Format("Enter Pass");
                    File.WriteAllBytes(acctFile, Properties.Resources.acct);
                    httpSettingsButton.PerformClick();
                }
            }
            catch
            {
                userNameTextBox.Text = String.Format("Enter User");
                passwordTextBox.Text = String.Format("Enter Pass");
            }
        }

        private void accessFtpDB()
        {
            try
            {
                if (File.Exists(acctFile))
                {
                    using (OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", acctFile)))
                    {
                        using (OleDbCommand selectCommand = new OleDbCommand("SELECT TOP 1 * FROM FTP", connection))
                        {
                            connection.Open();

                            DataTable table = new DataTable();
                            OleDbDataAdapter adapter = new OleDbDataAdapter();
                            adapter.SelectCommand = selectCommand;
                            adapter.Fill(table);

                            foreach (DataRow row in table.Rows)
                            {
                                object NameValue = row["Name"];
                                object PasswordValue = row["Password"];

                                userNameTextBox.Text = String.Format(NameValue.ToString());
                                passwordTextBox.Text = String.Format(PasswordValue.ToString());
                            }
                        }
                        connection.Close();
                    }
                }
                else
                {
                    userNameTextBox.Text = String.Format("Enter User");
                    passwordTextBox.Text = String.Format("Enter Pass");
                    File.WriteAllBytes(acctFile, Properties.Resources.acct);
                    ftpSettingsButton.PerformClick();
                }
            }
            catch
            {
                userNameTextBox.Text = String.Format("Enter User");
                passwordTextBox.Text = String.Format("Enter Pass");
            }
        }

        //HTTP Settings Button
        private void httpSettingsButton_Click_1(object sender, EventArgs e)
        {
            logRichTextBox.Text = "";//Clear log box
            settingsPanel.Height = 25;//Shrink settings
            Hideparts();//Hide parts function to hide main window items
            userNamelabel.Show();//Show username label 
            passwordLabel.Show();//show password label
            userNameTextBox.Show();//show username text box //////// PLACE HOLDER - Currently does nothing!!!
            passwordTextBox.Show();//show Password text box //////// PLACE HOLDER - Currently does nothing!!!
            httpSaveButton.Show();//Show the save ftp user settings.
            accessHttpDB();//Load saved user in DB
        }

        //FTP Settings button
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
            accessFtpDB();//Load saved user in DB
        }

        //Hideparts function to hide form items
        private void Hideparts()
        {
            httpSaveButton.Hide();
            maxSpeedCheckBox.Hide();
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
            maxSpeedCheckBox.Show();
        }

        //Save HTTP Account
        private void httpSaveButton_Click_1(object sender, EventArgs e)
        {
            SaveHTTPAcct();
            userNameTextBox.Text = "";//Hide ftp username box
            passwordTextBox.Text = "";//Hide ftp password box
            settingsPanel.Height = 25;//Shrink settings option
            userNamelabel.Hide();//Hide username label
            passwordLabel.Hide();//Hide Password Label
            userNameTextBox.Hide();//Hide ftp username box
            passwordTextBox.Hide();//Hide ftp password box
            httpSaveButton.Hide();//Hide the save button
            ShowParts();//Run the show forms items function
        }

        //Save FTP Account button 
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFTPAcct();
            userNameTextBox.Text = "";//Hide ftp username box
            passwordTextBox.Text = "";//Hide ftp password box
            settingsPanel.Height = 25;//Shrink settings option
            userNamelabel.Hide();//Hide username label
            passwordLabel.Hide();//Hide Password Label
            userNameTextBox.Hide();//Hide ftp username box
            passwordTextBox.Hide();//Hide ftp password box
            saveButton.Hide();//Hide the save button
            ShowParts();//Run the show forms items function
        }

        //Home button in the settings 
        private void homeButton_Click(object sender, EventArgs e)
        {
            LastLog();
            httpSaveButton.Hide();
            settingsPanel.Height = 25;//Shrink settings option
            userNamelabel.Hide();//Hide FTP Username label
            passwordLabel.Hide();//Hide password label
            userNameTextBox.Hide();//Hide Username Text Box
            passwordTextBox.Hide();//Hide Password Text Box
            saveButton.Hide();//Hide Save Button
            ShowParts();//Run Show Parts function
        }

        //About Button
        private void button1_Click(object sender, EventArgs e)
        {
            settingsPanel.Height = 25;//Set settings bar size after selecting about
            logRichTextBox.Text = "";//Clear Log window
            logRichTextBox.Text = "\tWelcome to Archive Downloader Powered By Aria2. Please enter your link and number of connections to download a single file. To download multiple files please create a list of links, one link per line and select number of connections. Then click the download button to download your list of links.\nCST-326 Milestone Project Created By:\n William Knotts,\n Antowan Graham,\n Bradley Austin,\n Joseph Carrillo,\n Mara Munoz";//Credits as a place holder
        }

        //Error Catch
        private static void process_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);//Show popup when an error is caught
        }

        //Link label and Link for github page
        private void gitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/W-KNOTTS/Archive-Downloader");
        }

        //Settings button
        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (settingsPanel.Height == 130)//if the settings panel is expanded 
            {
                settingsPanel.Height = 25;//Shrink settings window
            }
            else
            {
                settingsPanel.Height = 130;//else grow the settings window
            }
        }

        //Close Button
        private void closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch
            {
                Application.Exit();
            }
        }

        //Quit Button
        private void quitButton_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch
            {
                Application.Exit();
            }
        }

        //Jingle function to play an audio stream
        private void jingle()
        {
            SoundPlayer snd = new SoundPlayer(Properties.Resources.Rumper_Stumper);//new sound stream from resource file
            snd.PlaySync();//PlaySync forces audio stream to play until the end
            snd.Dispose();
        }

        //Show form2 loading gif
        private void showF2()
        {
            this.Hide();//hides form 1

            //Application.Run(f2);
            Thread t = new Thread(threadedLoad);// run form2 threaded  
            t.SetApartmentState(ApartmentState.MTA);
            t.Start(ApartmentState.MTA);
        }

        private void threadedLoad(object arg)
        {
            Application.Run(f2);
        }

        //close form2 loading gif
        private void hideF2()
        {
            this.Show();
            f2.Close();//close downloadin animation
            jingle();//Play sound until the end of the sound stream
        }

        private void CatchError()
        {
            hideF2();//hide form 2
            //Catch error and create popup saying there was a download error.
            System.Windows.Forms.MessageBox.Show("Download Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, (MessageBoxDefaultButton)MessageBoxOptions.DefaultDesktopOnly);
            downloadButton.Enabled = true;//Reenable the download button
            multiDLButton.Enabled = true;//Reenable the download button
            multiDLButton.Hide();//Hide multidownload Button
            downloadButton.Show();//Show single link DL Button
        }

        private void resumeDL()
        {
            makeSaveDir();//make save directory
            selSaveDir();//User Selected Save Directory
            showF2();//show form2

            // Variables, Strings, ect
            String SW0 = "-d";// -d The directory to store the downloaded file
            String SW6 = "--continue=true";// -d The directory to store the downloaded file
            String Link = linkTextBox.Text;// sets the text of linkTextBox as the link var
            String CONS = String.Format(connectionComboBox.Text);// takes the selection from the combobox and converts it to a string to use with aria2

            //Invoker for downloading using ARIA2c
            this.Invoke((MethodInvoker)delegate
            {
                Form2.CheckForIllegalCrossThreadCalls = false;
                try
                {
                    // Use ProcessStartInfo class
                    ProcessStartInfo startDL = new ProcessStartInfo();//Create process StartDL
                    startDL.CreateNoWindow = true;//Do not create a new terminal window
                    startDL.UseShellExecute = false;//Do not use shell 
                    startDL.RedirectStandardOutput = true;//Redirect output to rich text box
                    startDL.FileName = "Aria2/aria2c.exe";//Location of Aria binary
                    startDL.WindowStyle = ProcessWindowStyle.Hidden;//Hide cmd window
                    startDL.Arguments = $"{MaxCons} {SW6} {Link} {SW0} {SAVEDIR}{FS}";// Looks Like
                    //Command Example:  "aria2c.exe -x 16 www.site.com/download.zip -d Downloads"

                    // Call WaitForExit and then the using statement will close.
                    using (Process dlProcess = Process.Start(startDL))
                    {
                        string Output = dlProcess.StandardOutput.ReadToEnd();
                        dlProcess.WaitForExit();
                        logRichTextBox.Text = Output;//Print output string redirect to the rich text box

                    }
                    hideF2();//hide form 2
                }

                catch
                {
                    CatchError();
                }
            });
            string dt = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");//Create DT string to hold current date and time
            logRichTextBox.SaveFile($"{LOGDIR}/{dt}_Logs.txt", RichTextBoxStreamType.RichText);//Output log window to log file
            downloadButton.Enabled = true;//Reinable download button after download finished
            Application.Restart();
        }

        private void helpOptions()
        {
            String SW5 = "-h";// -h, Help menu
            this.Invoke((MethodInvoker)delegate
            {
                try
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
                    CatchError();
                }
            });
        }

        //Start Download Function
        private void StartDL()
        {
            saveLinks();//Saves links to DB Table
            makeSaveDir();//make save directory
            selSaveDir();//User Selected Save Directory
            showF2();//show form2

            // Variables, Strings, ect
            String SW0 = "-d";// -d The directory to store the downloaded file
            String Link = linkTextBox.Text;// sets the text of linkTextBox as the link var
            String CONS = String.Format(connectionComboBox.Text);// takes the selection from the combobox and converts it to a string to use with aria2
            String SPC = " ";// This will be the space variable

            //Invoker for downloading using ARIA2c
            this.Invoke((MethodInvoker)delegate
            {
                Form2.CheckForIllegalCrossThreadCalls = false;
                try
                {
                    // Use ProcessStartInfo class
                    ProcessStartInfo startDL = new ProcessStartInfo();//Create process StartDL
                    startDL.CreateNoWindow = true;//Do not create a new terminal window
                    startDL.UseShellExecute = false;//Do not use shell 
                    startDL.RedirectStandardOutput = true;//Redirect output to rich text box
                    startDL.FileName = "Aria2/aria2c.exe";//Location of Aria binary
                    startDL.WindowStyle = ProcessWindowStyle.Hidden;//Hide cmd window
                    startDL.Arguments = $"{MaxCons} {Link} {SW0} {SAVEDIR}{FS}";// Looks Like
                    //Command Example:  "aria2c.exe -x 16 www.site.com/download.zip -d Downloads"

                    // Call WaitForExit and then the using statement will close.
                    using (Process dlProcess = Process.Start(startDL))
                    {
                        string Output = dlProcess.StandardOutput.ReadToEnd();
                        dlProcess.WaitForExit();
                        logRichTextBox.Text = Output;//Print output string redirect to the rich text box

                    }
                    hideF2();//hide form 2
                }

                catch
                {
                    CatchError();
                }
            });
            string dt = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");//Create DT string to hold current date and time
            logRichTextBox.SaveFile($"{LOGDIR}/{dt}_Logs.txt", RichTextBoxStreamType.RichText);//Output log window to log file
            downloadButton.Enabled = true;//Reinable download button after download finished
            Application.Restart();
        }

        //Mutli Download Function 
        private void StartMultiDL()
        {
            makeSaveDir();//make a log directory in the working dir
            selSaveDir();//use set download dir
            showF2();//show form2

            // Variables, Strings, ect
            String SW0 = "-d";// -d The directory to store the downloaded file
            String SW2 = "-i";// -i Use link list file to download a list of links
            String Linkfile = linkTextFileLabel.Text;// sets the text of linkTextBox as the link var
            String CONS = String.Format(connectionComboBox.Text);// takes the selection from the combobox and converts it to a string to use with aria2
            Form2.CheckForIllegalCrossThreadCalls = false;
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
                    startDL.Arguments = $"{SW2} {Linkfile} {MaxCons} {SW0} {SAVEDIR}{FS}";//Example of link: "aria2c.exe -i link.txt -x 16 -d Downloads"
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    
                    using (Process dlProcess = Process.Start(startDL))
                    {
                        string Output = dlProcess.StandardOutput.ReadToEnd();
                        dlProcess.WaitForExit();//Wait for DL to finish before exit
                        logRichTextBox.Text = Output;//Print output string redirect to the rich text box

                    }
                    hideF2();//hide form 2
                }
                catch
                {
                    CatchError();
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

        private void multiDLButton_Click(object sender, EventArgs e)
        {
            logRichTextBox.Text = "";//Clear rich text box log window
            downloadButton.Enabled = false; // disable button while saving report
            multiDLButton.Enabled = false;//disable button while saving report
            StartMultiDL();// Run Multidownload function from clicking the Multi download button.
        }

        //Download button
        private void downloadButton_Click(object sender, EventArgs e)
        {
            //
            //saveLinks();
            logRichTextBox.Text = "";//Clear rich text box log window
            downloadButton.Enabled = false; // disable button while saving report                  
            StartDL();//Run start download function
        }

        //Help Button****************************************
        private void logButton_Click(object sender, EventArgs e)//Log Button is now the Help button
        {
            helpOptions();//Prints aria2 help information to the log window on load
            //saveLinks();
            //showDLlinks();
        }

        //Link File Button
        private void linkFileButton_Click(object sender, EventArgs e)
        {
            downloadButton.Hide();//Hide the download button 
            multiDLButton.Show();//show the multidownload button
            OpenFileDialog resumeFile = new OpenFileDialog();//Open file search for text file
            resumeFile.Filter = "Text file for Multi-Link Downloads (*.txt)|*.txt|All files (*.*)|*.*";//Only show .txt or all files when searching for a link list

            if (resumeFile.ShowDialog() == DialogResult.OK)//File added = OK ///////////////// NEED TO ADD RELITIVE AND ABSOLUTE PATHS
            {
                //Currently only works with TEXT FILE in the working Directory
                pathLabel.Text = "Link File Added: ";//Print Link File Added to the label to the right of the button
                linkTextFileLabel.Text = Path.GetFileName(resumeFile.FileName);// Print File Name in the log window
            }
        }

        //linkTextBox
        private void linkTextBox_TextChanged(object sender, EventArgs e)
        {
            String Link = String.Format(linkTextBox.Text);
        }

        private void maxSpeedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Setting variable for the check box
            if (maxSpeedCheckBox.Checked)
            {
                String MaxCons = String.Format("--max-connection-per-server=16 --min-split-size=4M");//--max-connection-per-server=4 --min-split-size=1M
            }
            else
            {
                String MaxCons = $"{SW1} {CONS}";//else will allow you to set the num of connections
            }
        }

        private void connectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String CONS = connectionComboBox.Text;//dynamically set the valu box
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            resumeButton.Enabled = false;
            multiDLButton.Enabled = false;
            downloadButton.Enabled = false;
            resumeDL();
            resumeButton.Enabled = true;
            multiDLButton.Enabled = false;
            downloadButton.Enabled = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string fill = dataGridView1.CurrentCell.Value.ToString();
            linkTextBox.Text = fill;
        }

        private void pastCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(pastCheckBox.Checked == true)
            {
                dataGridView1.Show();
                resumeButton.Enabled = true;
                downloadButton.Enabled = false;
                multiDLButton.Enabled = false;
                linkFileButton.Enabled = false;
                logButton.Enabled = false;
            }
            else
            {
                dataGridView1.Hide();
                resumeButton.Enabled = false;
                downloadButton.Enabled = true;
                multiDLButton.Enabled = true;
                linkFileButton.Enabled = true;
                logButton.Enabled = true;

            }
        }
    }
}
