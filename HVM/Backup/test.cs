﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;

using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;
namespace HVM
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }      // Process image
        private void ProcessImage( Bitmap bitmap )
        {
            // lock image
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle( 0, 0, bitmap.Width, bitmap.Height ),
                ImageLockMode.ReadWrite, bitmap.PixelFormat );

            // step 1 - turn background to black
            ColorFiltering colorFilter = new ColorFiltering( );

            colorFilter.Red   = new IntRange( 0, 64 );
            colorFilter.Green = new IntRange( 0, 64 );
            colorFilter.Blue  = new IntRange( 0, 64 );
            colorFilter.FillOutsideRange = false;

            colorFilter.ApplyInPlace( bitmapData );

            // step 2 - locating objects
            BlobCounter blobCounter = new BlobCounter( );

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = 5;
            blobCounter.MinWidth = 5;

            blobCounter.ProcessImage( bitmapData );
            Blob[] blobs = blobCounter.GetObjectsInformation( );
            bitmap.UnlockBits( bitmapData );

            // step 3 - check objects' type and highlight
            SimpleShapeChecker shapeChecker = new SimpleShapeChecker( );

            Graphics g = Graphics.FromImage( bitmap );
            Pen yellowPen = new Pen( Color.Yellow, 2 ); // circles
            Pen redPen = new Pen( Color.Red, 2 );       // quadrilateral
            Pen brownPen = new Pen( Color.Brown, 2 );   // quadrilateral with known sub-type
            Pen greenPen = new Pen( Color.Green, 2 );   // known triangle
            Pen bluePen = new Pen( Color.Blue, 2 );     // triangle

            for ( int i = 0, n = blobs.Length; i < n; i++ )
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints( blobs[i] );

                DoublePoint center;
                double radius;

                // is circle ?
                if ( shapeChecker.IsCircle( edgePoints, out center, out radius ) )
                {
                    g.DrawEllipse( yellowPen,
                        (float) ( center.X - radius ), (float) ( center.Y - radius ),
                        (float) ( radius * 2 ), (float) ( radius * 2 ) );
                }
                else
                {
                    List<IntPoint> corners;

                    // is triangle or quadrilateral
                    if ( shapeChecker.IsConvexPolygon( edgePoints, out corners ) )
                    {
                        // get sub-type
                        PolygonSubType subType = shapeChecker.CheckPolygonSubType( corners );

                        Pen pen;

                        if ( subType == PolygonSubType.Unknown )
                        {
                            pen = ( corners.Count == 4 ) ? redPen : bluePen;
                        }
                        else
                        {
                            pen = ( corners.Count == 4 ) ? brownPen : greenPen;
                        }

                      //  g.DrawPolygon( pen, ToPointsArray( corners ) );
                    }
                }
            }

            yellowPen.Dispose( );
            redPen.Dispose( );
            greenPen.Dispose( );
            bluePen.Dispose( );
            brownPen.Dispose( );
            g.Dispose( );

            // put new image to clipboard
            Clipboard.SetDataObject( bitmap );
            // and to picture box
            pictureBox1.Image = bitmap;

            UpdatePictureBoxPosition( );
        }

        // Conver list of AForge.NET's points to array of .NET points
        private AForge.Point[] ToPointsArray(List<IntPoint> points)
        {
            AForge.Point[] array = new AForge.Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new AForge.Point(points[i].X, points[i].Y);
            }

            return array;
        }

        // Update size and position of picture box control
        private void UpdatePictureBoxPosition()
        {
            int imageWidth;
            int imageHeight;

            if (pictureBox1.Image == null)
            {
                imageWidth = 320;
                imageHeight = 240;
            }
            else
            {
                imageWidth = pictureBox1.Image.Width;
                imageHeight = pictureBox1.Image.Height;
            }

           // Rectangle rc = splitContainer.Panel2.ClientRectangle;

            pictureBox1.SuspendLayout();
            pictureBox1.Location = new System.Drawing.Point((imageWidth + 2) / 2, (imageHeight + 2) / 2);
            pictureBox1.Size = new Size(imageWidth + 2, imageHeight + 2);
            pictureBox1.ResumeLayout();
        }
    }
}
