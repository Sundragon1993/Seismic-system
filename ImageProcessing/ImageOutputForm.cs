using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class ImageOutputForm : Form
    {
        public ImageOutputForm()
        {
            InitializeComponent();
        }

        private void ImageOutputForm_Paint(object sender, PaintEventArgs e)
        {
            int x, y = 10, w = 2, h = 2;
            Pen pen = new Pen(Color.Black, 5);
            for (int i = 1; i < 30; i++)
            {
                x = 10;
                for (int j = 1; j < 40; j++)
                {
                    Graphics g = this.CreateGraphics();
                    g.DrawRectangle(pen, new Rectangle(x, y, w, h));
                    x += 2;
                    g.Dispose();
                }
                y += 2;
            }
            pen.Dispose();
        }
    }
}
