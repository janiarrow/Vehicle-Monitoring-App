using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HVM
{
    public partial class CameraSettings : Form
    {
        private Image _preViewImage = null;
        

        public CameraSettings(Bitmap Image)
        {
            InitializeComponent();
            _preViewImage = Image;
            pictureBox1.Image = _preViewImage;
        }

        private void CameraSettings_Load(object sender, EventArgs e)
        {
            Image image = pictureBox1.Image;
            Graphics g = Graphics.FromImage(image);
            Color _color = Color.Black;
            Brush _brush = new SolidBrush(_color);
            g.FillEllipse(_brush,new Rectangle(trackBar1.Value,trackBar2.Value,4,4));

            pictureBox1.Image = image;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Image image = _preViewImage;
            Graphics g = Graphics.FromImage(image);
            Color _color = Color.Black;
            Brush _brush = new SolidBrush(_color);
            g.FillEllipse(_brush, new Rectangle(trackBar1.Value, trackBar2.Value, 4, 4));

            pictureBox1.Image = image;
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Image image = _preViewImage;
            Graphics g = Graphics.FromImage(image);
            Color _color = Color.Black;
            Brush _brush = new SolidBrush(_color);
            g.FillEllipse(_brush, new Rectangle(trackBar1.Value, trackBar2.Value, 4, 4));

            pictureBox1.Image = image;
            textBox2.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {

            Image image = _preViewImage;
            Graphics g = Graphics.FromImage(image);
            Color _color = Color.Black;
            Brush _brush = new SolidBrush(_color);
            g.FillEllipse(_brush, new Rectangle(trackBar1.Value, trackBar2.Value, 4, 4));

            pictureBox1.Image = image;
            textBox3.Text = trackBar3.Value.ToString();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {

            Image image = _preViewImage;
            Graphics g = Graphics.FromImage(image);
            Color _color = Color.Black;
            Brush _brush = new SolidBrush(_color);
            g.FillEllipse(_brush, new Rectangle(trackBar1.Value, trackBar2.Value, 4, 4));

            pictureBox1.Image = image;
            textBox4.Text = trackBar4.Value.ToString();
        }
    }
}
