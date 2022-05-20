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
        private Timer timer;

        private int task;
        
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            graph = Graphics.FromImage(bmp);
            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += timer_tick;
            //timer.Enabled = true;
            task = 0;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            switch (task)
            {
                case 1:
                    break;
                case 2:
                    break;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            task = 1;
            DrawLine(100, 100, ClientSize.Width - 100, ClientSize.Height - 100);
            graph.DrawImage(bmp, 0, 0);
        }

        private void DrawLine(int x1, int y1, int x2, int y2)
        {
            if (x1 != x2)
            {
                double m = (y2 - y1) / (x2 - x1);
                double y = y1;
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
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bmp.Dispose();
            graph.Dispose();
        }
    }
}