using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
//using AForge.Math;
using System.Drawing.Imaging;
using System.IO;

namespace HVM
{
    public partial class VehicleIdentification : Form
    {
        private Bitmap _sourceImage;
        private int _frameCount = 0;

        public VehicleIdentification()
        {
            InitializeComponent();
        }

        private void btnCreateTemplate_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFile = new OpenFileDialog();

            if (_openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CreateTemplate(_openFile.FileName);
            }
        }

        public void CreateTemplate(string FileName)
        {
            UseWaitCursor = true;
            FileInfo _SourceImage = new FileInfo(FileName);

            // Loading some file  
            using (Bitmap SampleImage = (Bitmap)AForge.Imaging.Image.FromFile(FileName))
            {
                // We must convert it to grayscale because  
                // the filter accepts 8 bpp grayscale images
                Grayscale GF = new Grayscale(0.2125, 0.7154, 0.0721);
                using (Bitmap GSampleImage = GF.Apply(SampleImage))
                {
                    // Detecting image edges and saving the result  
                    CannyEdgeDetector CED = new CannyEdgeDetector(0, 70);
                    CED.ApplyInPlace(GSampleImage);
                    GSampleImage.Save("testEDGED.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    //Create Template according to specific Size
                    string _templateSource = Application.StartupPath + "\\Template\\";
                    System.Drawing.Bitmap _createdImage = new Bitmap("testEDGED.jpg");
                    System.Drawing.Size _newSize = new System.Drawing.Size(320, 240);
                    System.Drawing.Bitmap _testImage = new Bitmap(_createdImage, _newSize);

                    FileInfo _templateFileInfo = new FileInfo(FileName);
                    _testImage.Save(_templateSource + _templateFileInfo.Name, ImageFormat.Jpeg);

                    lblCreatingStatus.Text = "New Template Created";
                    pictureBoxTemplate.Image = _testImage;
                    pictureBoxSource.Image = new Bitmap(FileName);

                    //Remove Temp Grayscale Image
                    File.Delete("testBW.jpg");
                }
            }
            UseWaitCursor = false;
        }

        public double Matching(string sourceImage, string template)
        {
            try
            {
                UseWaitCursor = true;

                Bitmap _sourceImage = (Bitmap)AForge.Imaging.Image.FromFile(sourceImage);
                Bitmap _template = (Bitmap)AForge.Imaging.Image.FromFile(template);

                pictureBoxSource.Image = _sourceImage;
                pictureBoxTemplate.Image = _template;

                // create template matching algorithm's instance
                ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.9f);

                // find all matchings with specified above similarity
                TemplateMatch[] matchings = tm.ProcessImage(_sourceImage, _template);

                // highlight found matchings
                BitmapData data = _sourceImage.LockBits(new Rectangle(0, 0, _sourceImage.Width, _sourceImage.Height), ImageLockMode.ReadWrite, _sourceImage.PixelFormat);

                //foreach (TemplateMatch m in matchings)
                //{
                //    Drawing.Rectangle(data, m.Rectangle, Color.White);
                //    // do something else with matching
                //}

                _sourceImage.UnlockBits(data);

                UseWaitCursor = false;

                return double.Parse(matchings[0].Similarity.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public double Matching1(Bitmap sourceImage, string template)
        {
            try
            {
                UseWaitCursor = true;

                Bitmap _sourceImage = (Bitmap)AForge.Imaging.Image.Clone(sourceImage, PixelFormat.Format24bppRgb);
                Bitmap _template = (Bitmap)AForge.Imaging.Image.FromFile(template);

                pictureBoxSource.Image = _sourceImage;
                pictureBoxTemplate.Image = _template;

                // create template matching algorithm's instance
                ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.9f);

                // find all matchings with specified above similarity
                TemplateMatch[] matchings = tm.ProcessImage(_sourceImage, _template);

                // highlight found matchings
                BitmapData data = _sourceImage.LockBits(new Rectangle(0, 0, _sourceImage.Width, _sourceImage.Height), ImageLockMode.ReadWrite, _sourceImage.PixelFormat);


                _sourceImage.UnlockBits(data);

                UseWaitCursor = false;

                return double.Parse(matchings[0].Similarity.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public void Process(string SourceImageFileName)
        {
            string[] _templateList = Directory.GetFiles(Application.StartupPath + "\\Template\\");

            List<double> _probabilityOfMatchings = new List<double>(_templateList.Length);
            double _maxResult = 0;

            for (int j = 0; j < _probabilityOfMatchings.Capacity; j++)
            {
                _probabilityOfMatchings.Add(Matching(SourceImageFileName, _templateList[j]));
            }

            if (_probabilityOfMatchings.Count > 0)
            {
                _maxResult = _probabilityOfMatchings.Max();
            }

            FileInfo _fileName = new FileInfo(_templateList[_probabilityOfMatchings.IndexOf(_maxResult)]);

            lblResult.Text = _fileName.Name.Remove(_fileName.Name.IndexOf("."));
            pictureBoxTemplate.Image = new Bitmap(_fileName.FullName);
            richTextBox1.AppendText("Time :" + DateTime.Now.ToShortTimeString() + ": Frame :" + _frameCount + " : Identified Object : " + _fileName.Name.Remove(_fileName.Name.IndexOf(".")) + "\n");

            File.Delete(SourceImageFileName);
        }

        public void ProcessByImage(Bitmap SourceImageFileName)
        {
            string[] _templateList = Directory.GetFiles(Application.StartupPath + "\\Template\\");

            List<double> _probabilityOfMatchings = new List<double>(_templateList.Length);
            double _maxResult = 0;

            for (int j = 0; j < _probabilityOfMatchings.Capacity; j++)
            {
                _probabilityOfMatchings.Add(Matching1(SourceImageFileName, _templateList[j]));
            }

            if (_probabilityOfMatchings.Count > 0)
            {
                _maxResult = _probabilityOfMatchings.Max();
            }

            FileInfo _fileName = new FileInfo(_templateList[_probabilityOfMatchings.IndexOf(_maxResult)]);

            lblResult.Text = _fileName.Name.Remove(_fileName.Name.IndexOf("."));
            pictureBoxTemplate.Image = new Bitmap(_fileName.FullName);
            richTextBox1.AppendText(DateTime.Now.ToShortTimeString() + ": Frame :" + _frameCount + " : Identified Object : " + _fileName.Name.Remove(_fileName.Name.IndexOf(".")) + "\n");
            string array = _fileName.Name.Remove(_fileName.Name.IndexOf(".")).ToString();

            if (!array.Contains("Back Ground"))
            {
                richTextBox2.AppendText(array + "\n");
            }
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFile = new OpenFileDialog();

            if (_openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string FileName = _openFile.FileName;
                FileInfo _sourceFile = new FileInfo(FileName);

                // Loading some file  
                using (Bitmap SampleImage = (Bitmap)AForge.Imaging.Image.FromFile(FileName))
                {
                    // We must convert it to grayscale because  
                    // the filter accepts 8 bpp grayscale images              

                    Grayscale GF = new Grayscale(0.2125, 0.7154, 0.0721);
                    using (Bitmap GSampleImage = GF.Apply(SampleImage))
                    {
                        // Detecting image edges and saving the result  
                        CannyEdgeDetector CED = new CannyEdgeDetector(0, 70);
                        CED.ApplyInPlace(GSampleImage);
                        GSampleImage.Save("tempEDGED" + _sourceFile.Name, System.Drawing.Imaging.ImageFormat.Jpeg);

                        //Create Template according to specific Size
                        string _sourceImagePath = Application.StartupPath + "\\TemporaryImage\\";
                        System.Drawing.Bitmap _createdImage = new Bitmap("tempEDGED" + _sourceFile.Name);
                        System.Drawing.Size _newSize = new System.Drawing.Size(320, 240);
                        System.Drawing.Bitmap _testImage = new Bitmap(_createdImage, _newSize);

                        FileInfo _templateFileInfo = new FileInfo(FileName);
                        _testImage.Save(_sourceImagePath + _templateFileInfo.Name, ImageFormat.Jpeg);

                        //Remove Temp Grayscale Image
                        File.Delete("testBW.jpg");

                        Process(_sourceImagePath + "\\" + _templateFileInfo.Name);
                    }
                }
            }
        }

        private void OpenVideoSource(string source)
        {
            this.Cursor = Cursors.WaitCursor;

            CloseVideoSource();

            videoSourcePlayer1.VideoSource = new FileVideoSource(source);
            videoSourcePlayer1.Start();
            
            //_videoSource = source;
            this.Cursor = Cursors.Default;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenVideoSource(openFileDialog.FileName);
                timerSampleMaker.Start();
            }
        }

        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            _sourceImage = image;
            _frameCount++;
        }

        private void timerSampleMaker_Tick(object sender, EventArgs e)
        {
            if (_sourceImage != null)
            {
                try
                {
                    // Loading some file  
                    Bitmap _sample = (Bitmap)AForge.Imaging.Image.Clone(_sourceImage);

                    // We must convert it to grayscale because  
                    // the filter accepts 8 bpp grayscale images              

                    Grayscale GF = new Grayscale(0.2125, 0.7154, 0.0721);
                    Bitmap GSampleImage = GF.Apply(_sample);

                    // Detecting image edges and saving the result  
                    CannyEdgeDetector CED = new CannyEdgeDetector(0, 70);
                    CED.ApplyInPlace(GSampleImage);

                    Bitmap _temp = new Bitmap(320, 240);
                    _temp = GSampleImage;
                    pictureBoxSource.Image = _temp;

                    ProcessByImage(_temp);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void videoSourcePlayer1_PlayingFinished(object sender, ReasonToFinishPlaying reason)
        {
            timerSampleMaker.Stop();
            CloseVideoSource();
        }

        private void CloseVideoSource()
        {
            //this.Cursor = Cursors.WaitCursor;

            //videoSourcePlayer1.SignalToStop();

            //for (int i = 0; (i < 50) && (videoSourcePlayer1.IsRunning); i++)
            //{
            //    Thread.Sleep(100);
            //}
            //if (videoSourcePlayer1.IsRunning)
            //    videoSourcePlayer1.Stop();

            //videoSourcePlayer1.BorderColor = Color.Black;
            //this.Cursor = Cursors.Default;
        }

        private void VehicleIdentification_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.WaitForStop();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            lblResult.Text = "";
        }


    }
}
