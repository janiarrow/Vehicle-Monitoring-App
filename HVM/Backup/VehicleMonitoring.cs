using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;

using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge;
using AForge.Video.DirectShow;
using AForge.Video;
using AForge.Vision.Motion;
using System.Media;

namespace HVM
{
    public partial class txtDate : Form
    {
        private IVideoSource _videoSource = null;
        MotionDetector _detector = new MotionDetector(new SimpleBackgroundModelingDetector(), new BlobCountingObjectsProcessing());

        private const int _statLength = 15;
        private int _statIndex = 0;
        private int _statReady = 0;
        private int _flash = 0;
        private float _motionAlarmLevel = 0.015f;
        private int _detectedObjectsCount = -1;

        private int[] _statCount = new int[_statLength];
        private List<float> _motionHistory = new List<float>();

        private FilterInfoCollection _videoDevices;
        private EuclideanColorFiltering _filter = new EuclideanColorFiltering();
        private Color _color = Color.Black;
        private Grayscale _grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
        private int _range;
        private Bitmap _image;
        private Pen _pen;
        private Graphics _g2;

        public txtDate()
        {
            InitializeComponent();
            Initialize();
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void Initialize()
        {
            _image = new Bitmap(355, 300);

            _pen = new Pen(Color.FromArgb(160, 255, 160), 3);
            _g2 = Graphics.FromImage(_image);

            _pen = new Pen(Color.FromArgb(255, 0, 0), 3);
            _g2.Clear(Color.White);
            //_g2.DrawLine(_pen, _image.Width / 2, 0, _image.Width / 2, _image.Width);
            //_g2.DrawLine(_pen, _image.Width, _image.Height / 2, 0, _image.Height / 2);

            pictureBox1.Image = (System.Drawing.Image)_image;
            pictureBox2.Image = (System.Drawing.Image)_image;
            listBox1.Hide();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileVideoSource fileSource = new FileVideoSource(openFileDialog.FileName);
                timer1.Start();
                OpenVideoSource(fileSource);
                
            }
            
            SetMotionDetectionAlgorithm(new SimpleBackgroundModelingDetector(true, true));
            SetMotionProcessingAlgorithm(new BlobCountingObjectsProcessing());
        }

        ///////////////////////////////////////////////////
        // massagin function...
        int state = 0;
        private void listError() {

            if (state == 2)
            {
                timer1.Stop();
                timer2.Stop();
                return;
            }

            txtIncident.Text += "Insident Start \n";
            state++;

            if (state == 1)
            {
                timer1.Stop();
                timer2.Start();
            }
            
            timer1.Start();
            

        }
        /////////////////////////////////////////////////////

        private void timer1_Tick(object sender, EventArgs e)
        {
            listError();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            listError();
        }

        /// <summary>
        /// ////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void VehicleMonitoring_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseVideoSource();
        }

        private void OpenVideoSource(IVideoSource source)
        {
            this.Cursor = Cursors.WaitCursor;

            CloseVideoSource();

            //videoSourcePlayer.VideoSource = new AsyncVideoSource(source);
            //videoSourcePlayer.Start();

            videoSourcePlayer.VideoSource = source;
            videoSourcePlayer.Start();

            _statIndex = _statReady = 0;

            timer.Start();
            alarmTimer.Start();

            _videoSource = source;
            this.Cursor = Cursors.Default;
        }

        private void CloseVideoSource()
        {
            this.Cursor = Cursors.WaitCursor;

            videoSourcePlayer.SignalToStop();

            for (int i = 0; (i < 50) && (videoSourcePlayer.IsRunning); i++)
            {
                Thread.Sleep(100);
            }
            if (videoSourcePlayer.IsRunning)
                videoSourcePlayer.Stop();

            timer.Stop();
            alarmTimer.Stop();

            _motionHistory.Clear();

            if (_detector != null)
                _detector.Reset();

            videoSourcePlayer.BorderColor = Color.Black;
            this.Cursor = Cursors.Default;
        }

        private void videoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            lock (this)
            {
                if (_detector != null)
                {
                    float motionLevel = _detector.ProcessFrame(image);

                    if (motionLevel > _motionAlarmLevel)
                    {
                        _flash = (int)(2 * (1000 / alarmTimer.Interval));
                    }

                    if (_detector.MotionProcessingAlgorithm is BlobCountingObjectsProcessing)
                    {
                        BlobCountingObjectsProcessing countingDetector = (BlobCountingObjectsProcessing)_detector.MotionProcessingAlgorithm;
                        _detectedObjectsCount = countingDetector.ObjectsCount;

                        Rectangle[] rects = countingDetector.ObjectRectangles;
                        if (rects.Length > 0)
                        {

                            Rectangle objectRect = rects[0];

                            ParameterizedThreadStart t = new ParameterizedThreadStart(WriteStatus);
                            Thread aa = new Thread(t);
                            aa.Start(rects[0]);
                        }
                    }
                    else
                    {
                        _detectedObjectsCount = -1;
                    }

                    _motionHistory.Add(motionLevel);
                    if (_motionHistory.Count > 300)
                    {
                        _motionHistory.RemoveAt(0);
                    }
                }
            }
        }


        int gX , gY = 0;

        //int breakX, breakY = 0;

        int moveNo = 0;

        private void WriteStatus(object r)
        {
            try
            {
                Bitmap b = new Bitmap(pictureBox1.Image);
                Rectangle a = (Rectangle)r;
                Pen pen1 = new Pen(Color.Red , 7);
                Graphics g2 = Graphics.FromImage(b);
                pen1 = new Pen(_color, 3);
                // Brush b5 = null;
                SolidBrush b5 = new SolidBrush(_color);
                //   g2.Clear(Color.Black);


                Font f = new Font(Font, FontStyle.Bold);


                if (moveNo >= 25)
                {
                    if (gX == a.Location.X && gY == a.Location.Y)
                    {
                        try
                        {
                            //txtIncident.Text = "Non moving vehicle identified";
                            //txtMessage.Text = "Caution situation";
                            //SoundPlayer p = new SoundPlayer(@"C:\Documents and Settings\Arosha\Desktop\jili\V 3.2\HVM\whoop.wav");
                            //p.Play();

                            pictureBox2.Image = videoSourcePlayer.GetCurrentVideoFrame();
                            MessageBox.Show("Caution!");
                            //p.Stop();
                            moveNo = 0;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        return;
                    }

                    moveNo = 0;
                }

                moveNo++;

                g2.DrawString("o", f, b5, a.Location);

                gX  = a.Location.X ;
                gY = a.Location.Y;

                g2.Dispose();
                pictureBox1.Image = (System.Drawing.Image)b;
                this.Invoke((MethodInvoker)delegate
                {
                    textStatus.Text = a.Location.ToString() + "\n" + textStatus.Text + "\n"; ;
                    //DetectNonMovingVehicles();
                });
            }
            catch (Exception)
            {
                Thread.CurrentThread.Abort();
            }

            Thread.CurrentThread.Abort();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            objectsCountLabel.Text = (_detectedObjectsCount < 0) ? string.Empty : "Objects: " + _detectedObjectsCount;
        }

        private void SetMotionDetectionAlgorithm(IMotionDetector detectionAlgorithm)
        {
            lock (this)
            {
                _detector.MotionDetectionAlgorithm = detectionAlgorithm;
                _motionHistory.Clear();

                if (detectionAlgorithm is TwoFramesDifferenceDetector)
                {
                    if (
                        (_detector.MotionProcessingAlgorithm is MotionBorderHighlighting) ||
                        (_detector.MotionProcessingAlgorithm is BlobCountingObjectsProcessing))
                    {
                        SetMotionProcessingAlgorithm(new MotionAreaHighlighting());
                    }
                }
            }
        }

        private void SetMotionProcessingAlgorithm(IMotionProcessing processingAlgorithm)
        {
            lock (this)
            {
                _detector.MotionProcessingAlgorithm = processingAlgorithm;
            }
        }

        private void VehicleMonitoring_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            txtToday.Text = today.ToString();

            DateTime time = DateTime.Now;
            txtTime.Text = time.ToString();

            txtCamera.Text = "Camera 01";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            videoSourcePlayer.SignalToStop();
            videoSourcePlayer.WaitForStop();
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            string location = textStatus.Text.Trim();


            string[] cordinates = location.Split(new char[] { '{', '}', '=', ' ', ',' });

            string previousX = "";
            string previousY = "";
            string currentX = "";
            string currentY = "";
            bool X_flag = false;
            bool Y_flag = false;
            bool sameX = false;
            bool sameY = false;


            previousX = cordinates[2];
            previousY = cordinates[4];

            for (int i = 1; i < cordinates.Length; i++)
            {


                if (cordinates[i].Trim().Equals("X"))
                {
                    // Console.WriteLine("found X");
                    X_flag = true;
                    Y_flag = false;

                }
                else if (X_flag)
                {
                    Console.WriteLine(cordinates[i]);

                    previousX = currentX;
                    currentX = cordinates[i];
                    if (previousX.Trim().Equals(currentX))
                    {
                        //  Console.WriteLine("same X" + previousX + currentX);
                        sameX = true;
                    }

                    X_flag = false;

                }
                else if (cordinates[i].Trim().Equals("Y"))
                {
                    //Console.WriteLine("found y");
                    X_flag = false;
                    Y_flag = true;

                }
                else if (Y_flag)
                {
                    Console.WriteLine(cordinates[i]);
                    previousY = currentY;
                    currentY = cordinates[i];
                    if (previousY.Trim().Equals(currentY))
                    {
                        // Console.WriteLine("same Y" + previousY + currentY);
                        sameY = true;

                        if (sameX && sameY)
                        {
                            //Console.WriteLine("Non moving at X" +currentX+" and Y "+currentY);
                            richTextBox1.Text = "Non moving at X" + currentX + " and Y " + currentY + "\n" + richTextBox1.Text + "\n";
                            sameX = false;
                            sameY = false;
                        }
                    }

                    Y_flag = false;

                }


            }
        }

        #region DetectNonMovingVehicles Method 
        private void DetectNonMovingVehicles()
        {
            string location = textStatus.Text.Trim();


            string[] cordinates = location.Split(new char[] { '{', '}', '=', ' ', ',' });

            string previousX = "";
            string previousY = "";
            string currentX = "";
            string currentY = "";
            bool X_flag = false;
            bool Y_flag = false;
            bool sameX = false;
            bool sameY = false;


            previousX = cordinates[2];
            previousY = cordinates[4];

            for (int i = 1; i < cordinates.Length; i++)
            {


                if (cordinates[i].Trim().Equals("X"))
                {
                    // Console.WriteLine("found X");
                    X_flag = true;
                    Y_flag = false;

                }
                else if (X_flag)
                {
                    Console.WriteLine(cordinates[i]);

                    previousX = currentX;
                    currentX = cordinates[i];
                    if (previousX.Trim().Equals(currentX))
                    {
                        //  Console.WriteLine("same X" + previousX + currentX);
                        sameX = true;
                    }

                    X_flag = false;

                }
                else if (cordinates[i].Trim().Equals("Y"))
                {
                    //Console.WriteLine("found y");
                    X_flag = false;
                    Y_flag = true;

                }
                else if (Y_flag)
                {
                    Console.WriteLine(cordinates[i]);
                    previousY = currentY;
                    currentY = cordinates[i];
                    if (previousY.Trim().Equals(currentY))
                    {
                        // Console.WriteLine("same Y" + previousY + currentY);
                        sameY = true;

                        if (sameX && sameY)
                        {
                            //Console.WriteLine("Non moving at X" +currentX+" and Y "+currentY);
                            richTextBox1.Text = "Non moving at X" + currentX + " and Y " + currentY + "\n" + richTextBox1.Text + "\n";
                            sameX = false;
                            sameY = false;
                        }
                    }

                    Y_flag = false;
                }
            }
        }

        # endregion 

        private void btnClear_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        private void txtIncident_TextChanged(object sender, EventArgs e)
        {

        }

        private void videoSourcePlayer_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CameraSettings(videoSourcePlayer.GetCurrentVideoFrame()).Show();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("X = "+ e.X + " Y ="+e.Y  );
        }

      

       
    }
}
