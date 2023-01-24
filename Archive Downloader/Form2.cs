using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using System.Threading;

namespace Archive_Downloader
{
    public partial class Form2 : Form
    {
        //Imports and settings for mouse drag
        public const int WM_NCLBUTTONDOWN = 0xA1;//Constant for mouse drag
        public const int HT_CAPTION = 0x2;//Other constant for mouse drag
        [DllImportAttribute("user32.dll")]//Import DLL for the aboility for dragging
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);//When mouse is held window will follow mouse
        [DllImportAttribute("user32.dll")]//Import DLL for the aboility for dragging
        public static extern bool ReleaseCapture();//release the window after mouse click is over

        public Form2()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //Link = String.Format(lblWarning.Text);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.pictureBox1.MouseMove += new MouseEventHandler(drag_MouseMove);//Allow Drag From Tool Bar
            SoftBlink(lblWarning, Color.FromArgb(30, 30, 30), Color.Green, 2000, false);
            SoftBlink(lblSoftBlink, Color.FromArgb(30, 30, 30), Color.Green, 2000, true);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            threadedSnd();
        }

        private void threadedSnd()
        {
            ThreadPool.QueueUserWorkItem(ignoredState =>
            {
                using (var audioMemory = Properties.Resources.voices_Form2)
                {
                    using (var player = new SoundPlayer(audioMemory))
                    {
                        player.PlaySync();
                        player.Dispose();
                    }
                }
            });
        }

        private async void SoftBlink(Control ctrl, Color c1, Color c2, short CycleTime_ms, bool BkClr)
        {
            var sw = new Stopwatch(); sw.Start();
            short halfCycle = (short)Math.Round(CycleTime_ms * 0.5);
            while (true)
            {
                await Task.Delay(1);
                var n = sw.ElapsedMilliseconds % CycleTime_ms;
                var per = (double)Math.Abs(n - halfCycle) / halfCycle;
                var red = (short)Math.Round((c2.R - c1.R) * per) + c1.R;
                var grn = (short)Math.Round((c2.G - c1.G) * per) + c1.G;
                var blw = (short)Math.Round((c2.B - c1.B) * per) + c1.B;
                var clr = Color.FromArgb(red, grn, blw);
                if (BkClr) ctrl.BackColor = clr; 
                else ctrl.ForeColor = clr;
            }
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
    }
}
