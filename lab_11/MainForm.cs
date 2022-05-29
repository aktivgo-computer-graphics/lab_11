using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_11
{
    public partial class MainForm : Form
    {
        private Graphics graph;
        private Bitmap bmp;

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            graph = CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            graph.Clear(Color.White);
            DrawLine(100, 100, ClientSize.Width - 100, ClientSize.Height - 100);
            graph.DrawImage(bmp, 0, 0);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            graph.Clear(Color.White);
            DrawLineBrezenhem(100, 100, ClientSize.Width - 100, ClientSize.Height - 100);
            graph.DrawImage(bmp, 0, 0);
        }

        private void DrawLine(int x1, int y1, int x2, int y2)
        {
            if (x1 != x2)
            {
                float m = (float)(y2 - y1) / (x2 - x1);
                float y = y1;
                for (int x = x1; x < x2; x++)
                {
                    bmp.SetPixel(x, (int)Math.Round(y), Color.Black);
                    y += m;
                }
            }
            else
            {
                if (y1 == y2)
                {
                    bmp.SetPixel(x1, y1, Color.Red);
                }
            }
        }
        
        private void DrawLineBrezenhem(int x1, int y1, int x2, int y2)
        {
            int x = x1;
            int y = y1;
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int s1 = Math.Sign(x2 - x1);
            int s2 = Math.Sign(y2 - y1);

            bool l;
            float e = 0;

            if (dy > dx)
            {
                (dy, dx) = (dx, dy);
                l = true;
            }
            else
            {
                l = false;
                e = 2 * dx * dy;
            }

            for (int i = 1; i < dx; i++)
            {
                bmp.SetPixel(x, Math.Abs(y) % ClientSize.Height, Color.Black);
                while (e >= 0)
                {
                    if (l)
                    {
                        x += s1;
                    }
                    else
                    {
                        y += s2;
                    }

                    e -= 2 * dx;
                }

                if (l)
                {
                    y += s2;
                }
                else
                {
                    x += s1;
                }
                
                e += 2 * dx;
            }
            
            bmp.SetPixel(x, Math.Abs(y) % ClientSize.Height, Color.Red);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bmp.Dispose();
            graph.Dispose();
        }
    }
}