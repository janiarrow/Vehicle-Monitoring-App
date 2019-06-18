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
using System.Reflection;

namespace HVM
{
    public partial class SpeedDetection : Form
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
        private Bitmap _image2;
        private Pen _pen;
        private Graphics _g2;

        public SpeedDetection()
        {
            InitializeComponent();
            Initialize();
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void Initialize()
        {
            _image = new Bitmap(355, 300);
            _image2 = new Bitmap(10, 10);

            _pen = new Pen(Color.FromArgb(160, 255, 160), 3);
            _g2 = Graphics.FromImage(_image);
            _g2 = Graphics.FromImage(_image2);

            _pen = new Pen(Color.FromArgb(255, 0, 0), 3);
            _g2.Clear(Color.White);
            _g2.DrawLine(_pen, _image.Width / 2, 0, _image.Width / 2, _image.Width);
            _g2.DrawLine(_pen, _image.Width, _image.Height / 2, 0, _image.Height / 2);

            pictureBox1.Image = (System.Drawing.Image)_image;
        }

        private void SpeedDetection_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseVideoSource();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileVideoSource fileSource = new FileVideoSource(openFileDialog.FileName);
                OpenVideoSource(fileSource);
            }

            SetMotionDetectionAlgorithm(new SimpleBackgroundModelingDetector(true, true));
            SetMotionProcessingAlgorithm(new BlobCountingObjectsProcessing());
        }

        private void OpenVideoSource(IVideoSource source)
        {
            this.Cursor = Cursors.WaitCursor;

            CloseVideoSource();

            //videoSourcePlayer.VideoSource = new AsyncVideoSource(source);
            //videoSourcePlayer.Start();

            videoSourcePlayer.VideoSource = source;
            videoSourcePlayer.Start();

            videoSourceHistory.VideoSource = source;
            videoSourceHistory.Start();

            _statIndex = _statReady = 0;

            timer.Start();
            alarmTimer.Start();

            _videoSource = source;
            this.Cursor = Cursors.Default;

            //txtFrameRate.Text = "29.0";
        }

        private void CloseVideoSource()
        {
            this.Cursor = Cursors.WaitCursor;

            videoSourcePlayer.SignalToStop();
            videoSourceHistory.SignalToStop();

            for (int i = 0; (i < 50) && (videoSourcePlayer.IsRunning); i++)
            {
                Thread.Sleep(100);
            }
            if (videoSourcePlayer.IsRunning)
                videoSourcePlayer.Stop();

            for (int i = 0; (i < 50) && (videoSourceHistory.IsRunning); i++)
            {
                Thread.Sleep(100);
            }
            if (videoSourceHistory.IsRunning)
                videoSourceHistory.Stop();

            timer.Stop();
            alarmTimer.Stop();

            _motionHistory.Clear();

            if (_detector != null)
                _detector.Reset();

            videoSourcePlayer.BorderColor = Color.Black;
            videoSourceHistory.BorderColor = Color.Black;
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

                            Graphics g = Graphics.FromImage(image);
                            using (Pen pen = new Pen(Color.Red, 1))
                            {
                                int n = 1;
                                SolidBrush b5 = new SolidBrush(_color);
                                Font f = new Font(Font, FontStyle.Bold);

                                // draw each rectangle
                                foreach (Rectangle rc in rects)
                                {
                                    g.DrawRectangle(pen, rc);

                                    if ((n < 10) && (rc.Width > 15) && (rc.Height > 15))
                                    {
                                        g.DrawString(n.ToString(), f, b5, rc.Location); 
                                        //g.DrawImage(_image2, rc.Left, rc.Top, 5, 5);
                                        n++;
                                    }                                   
                                }
                            }
                            g.Dispose();
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

        private void Application_Idle(object sender, EventArgs e)
        {
            objectsCountLabel.Text = (_detectedObjectsCount < 0) ? string.Empty : "Objects: " + _detectedObjectsCount;
        }

        private void DrawMotionHistory(Bitmap image)
        {
            Color greenColor = Color.FromArgb(128, 0, 255, 0);
            Color yellowColor = Color.FromArgb(128, 255, 255, 0);
            Color redColor = Color.FromArgb(128, 255, 0, 0);

            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite, image.PixelFormat);

            int t1 = (int)(_motionAlarmLevel * 500);
            int t2 = (int)(0.075 * 500);

            for (int i = 1, n = _motionHistory.Count; i <= n; i++)
            {
                int motionBarLength = (int)(_motionHistory[n - i] * 500);

                if (motionBarLength == 0)
                    continue;

                if (motionBarLength > 50)
                    motionBarLength = 50;

                Drawing.Line(bitmapData,
                    new IntPoint(image.Width - i, image.Height - 1),
                    new IntPoint(image.Width - i, image.Height - 1 - motionBarLength),
                    greenColor);

                if (motionBarLength > t1)
                {
                    Drawing.Line(bitmapData,
                        new IntPoint(image.Width - i, image.Height - 1 - t1),
                        new IntPoint(image.Width - i, image.Height - 1 - motionBarLength),
                        yellowColor);
                }

                if (motionBarLength > t2)
                {
                    Drawing.Line(bitmapData,
                        new IntPoint(image.Width - i, image.Height - 1 - t2),
                        new IntPoint(image.Width - i, image.Height - 1 - motionBarLength),
                        redColor);
                }
            }
            image.UnlockBits(bitmapData);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            IVideoSource videoSource = videoSourcePlayer.VideoSource;
            if (videoSource != null)
            {
                _statCount[_statIndex] = videoSource.FramesReceived;

                if (++_statIndex >= _statLength)
                    _statIndex = 0;
                if (_statReady < _statLength)
                    _statReady++;

                float fps = 0;

                for (int i = 0; i < _statReady; i++)
                {
                    fps += _statCount[i];
                }
                fps /= _statReady;

                _statCount[_statIndex] = 0;

                txtFrameRate.Text = fps.ToString("F2") + " fps"; 

            }
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

        private void videoSourceHistory_NewFrame(object sender, ref Bitmap image)
        {
            #region Make the Video GrayScale

            //_range = 120;
            //Bitmap objectsImage = null;

            //// set center colol and radius
            //_filter.CenterColor = new RGB(Color.FromArgb(_color.ToArgb()));
            //_filter.Radius = (short)_range;
            //// apply the filter
            //objectsImage = image;
            //_filter.ApplyInPlace(image);

            //// lock image for further processing
            //BitmapData objectsData = objectsImage.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

            //// grayscaling
            //UnmanagedImage grayImage = _grayscaleFilter.Apply(new UnmanagedImage(objectsData));

            //// unlock image
            //objectsImage.UnlockBits(objectsData);

            #endregion

            //DrawMotionHistory(image);
        }

        private void WriteStatus(object r)
        {
            try
            {
                Bitmap b = new Bitmap(pictureBox1.Image);
                Rectangle a = (Rectangle)r;
                Pen pen1 = new Pen(Color.FromArgb(160, 255, 160), 3);
                Graphics g2 = Graphics.FromImage(b);
                pen1 = new Pen(_color, 3);
                // Brush b5 = null;
                SolidBrush b5 = new SolidBrush(_color);
                //   g2.Clear(Color.Black);


                Font f = new Font(Font, FontStyle.Bold);

                g2.DrawString("X", f, b5, a.Location);
                g2.Dispose();
                pictureBox1.Image = (System.Drawing.Image)b;
                this.Invoke((MethodInvoker)delegate
                {
                    textStatus.Text = a.Location.ToString() + "\n" + textStatus.Text + "\n"; ;
                    SpeedCalc();
                });
            }
            catch (Exception)
            {
                Thread.CurrentThread.Abort();
            }

            Thread.CurrentThread.Abort();
        }

        private void videoSourceHistory_NewFrame_1(object sender, ref Bitmap image)
        {
            #region Make the Video GrayScale

            _range = 120;
            Bitmap objectsImage = null;

            // set center colol and radius
            _filter.CenterColor = new RGB(Color.FromArgb(_color.ToArgb()));
            _filter.Radius = (short)_range;
            // apply the filter
            objectsImage = image;
            _filter.ApplyInPlace(image);

            // lock image for further processing
            BitmapData objectsData = objectsImage.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

            // grayscaling
            UnmanagedImage grayImage = _grayscaleFilter.Apply(new UnmanagedImage(objectsData));

            // unlock image
            objectsImage.UnlockBits(objectsData);

            #endregion

            DrawMotionHistory(image);

        }

        private void SpeedDetection_Load(object sender, EventArgs e)
        {
            txtSpeedLimit.Text = "100.0";
            objectsCountLabel.Text = "0";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            videoSourcePlayer.SignalToStop();
            videoSourcePlayer.WaitForStop();

            videoSourceHistory.SignalToStop();
            videoSourceHistory.WaitForStop();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textStatus.Text = "";
            objectsCountLabel.Text = "0";
            Initialize();
        }

        private void SpeedCalc()
        {
            string previousX = "";
            string previousY = "";
            string lastX = "";
            string lastY = "";
            double distance = 0.0;
            double speed = 0.0;
            double speedInKmph = 0.0;
            double output = 0.0;
            double frames = 0.0;

            string location = textStatus.Text.Trim();
            string[] cordinates = location.Split(new char[] { '{', '}', '=', ' ', ',' });

            previousX = cordinates[2];
            previousY = cordinates[4];
            lastX = "298";
            lastY = "228";

            txtStartingXY.Text = previousX + " " + previousY;
            txtEndingXY.Text = lastX + " " + lastY;
            txtTime.Text = "11.0";

            distance = Convert.ToDouble(lastX) - Convert.ToDouble(previousX);
            frames = 29.0; // Convert.ToDouble(txtFrameRate.Text);
            speed = distance / frames;

            output = speed * 3.6;
            speedInKmph = Math.Round(output, 3);

            txtSpeed.Text = Convert.ToString(speedInKmph);

            if (speedInKmph > Convert.ToDouble(txtSpeedLimit.Text))
            {
                txtSpeedStatus.Text = "Speed Limit exceeded";
            }
            else
            {
                txtSpeedStatus.Text = "Within Speed Limit";
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
