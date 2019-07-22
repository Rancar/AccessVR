using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace VideoOverlayForm
{

    using System;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    public partial class Form3 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        Timer timer = new Timer();

        public Form3()
        {
            InitializeComponent();
            //this.Opacity = 0;
            //this.TopMost = true;

            //
            timer.Enabled = true;
            timer.Interval = 10;  /* 300 millisec */
            timer.Tick += new EventHandler(TimerCallback);
        }

        private void TimerCallback(object sender, EventArgs e)
        {

            this.pictureBox1.Invalidate();
            return;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           // var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            //SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        /*
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Bitmap bmp = CaptureScreen())
            {

                // Set the image attribute's color mappings
                ColorMap[] colorMap = new ColorMap[1];
                colorMap[0] = new ColorMap();
                colorMap[0].OldColor = Color.Black;
                colorMap[0].NewColor = Color.Blue;
                ImageAttributes attr = new ImageAttributes();
                attr.SetRemapTable(colorMap);
                // Draw using the color map
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
            }
        }*/

        //private void pictureBox1_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    using (Bitmap bmp = CaptureScreen())
        //    {
        //        // Set the image attribute's color mappings
        //        ColorMap[] colorMap = new ColorMap[1];
        //        colorMap[0] = new ColorMap();
        //        colorMap[0].OldColor = Color.Black;
        //        colorMap[0].NewColor = Color.Blue;
        //        ImageAttributes attr = new ImageAttributes();
        //        attr.SetRemapTable(colorMap);
        //        // Draw using the color map
        //        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        //        g.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
        //    }
        //}

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Bitmap bmpOld = CaptureScreen())
            {
                Bitmap bmp = ChangeColor(bmpOld);
                // Set the image attribute's color mappings
                ColorMap[] colorMap = new ColorMap[1];
                colorMap[0] = new ColorMap();
                //colorMap[0].OldColor = Color.White;
                //colorMap[0].NewColor = Color.Blue;
                ImageAttributes attr = new ImageAttributes();
                attr.SetRemapTable(colorMap);
                // Draw using the color map
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
                
            }
        }

        private Bitmap CaptureScreen()
        {
            Bitmap b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            //Bitmap b = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(0, 0, 0, 0, b.Size);
            g.Dispose();
            return b;
        }

        private Bitmap ChangeColor(Bitmap scrBitmap)
        {
            //You can change your new color here. Red,Green,LawnGreen any..
           
            Color actualColor;
            //make an empty bitmap the same size as scrBitmap
            Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);
            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    //get the pixel from the scrBitmap image
                    actualColor = scrBitmap.GetPixel(i, j);
                    // > 150 because.. Images edges can be of low pixel colr. if we set all pixel color to new then there will be no smoothness left.
                    if (actualColor.R > 230 && (actualColor.G < 240 && actualColor.B < 240))
                        newBitmap.SetPixel(i, j, Color.FromArgb(actualColor.R / 5, actualColor.G, actualColor.B));
                    else
                        newBitmap.SetPixel(i, j, actualColor);
                }
            }
            return newBitmap;
        }
    }
}
