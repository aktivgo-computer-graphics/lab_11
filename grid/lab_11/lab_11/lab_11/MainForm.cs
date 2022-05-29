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
        private Bitmap bmp;
        private Graphics Graph;
        
        public MainForm()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            int k = 10;
            int cellCountX = (int)Math.Ceiling((float)ClientSize.Width / k) + 1;
            int cellCountY = (int)Math.Ceiling((float)ClientSize.Height / k);
            Grid grid = new Grid(cellCountX, cellCountY, k);
            DrawLine(grid, 10, 10, 100,  100);                             // 45
            //DrawLine(grid, 10, cellCountY / 2, cellCountX - 10, cellCountY / 2);   // 0
            //DrawLine(grid, cellCountX / 2, 10, cellCountX / 2, cellCountY - 10);   // 90
            grid.DrawGrid(Graph);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            int k = 10;
            int cellCountX = (int)Math.Ceiling((float)ClientSize.Width / k) + 1;
            int cellCountY = (int)Math.Ceiling((float)ClientSize.Height / k);
            Grid grid = new Grid(cellCountX, cellCountY, k);
            DrawBrezenhem(grid, 10, 10, cellCountX - 10, cellCountY - 10, Color.Black);
            grid.DrawGrid(Graph);
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            int k = 10;
            int cellCountX = (int)Math.Ceiling((float)ClientSize.Width / k) + 1;
            int cellCountY = (int)Math.Ceiling((float)ClientSize.Height / k);
            Grid grid = new Grid(cellCountX, cellCountY, k);
            DrawCircle(grid, 33, cellCountX, cellCountY, Color.Black);
            grid.DrawGrid(Graph);
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            int k = 10;
            int cellCountX = (int)Math.Ceiling((float)ClientSize.Width / k) + 1;
            int cellCountY = (int)Math.Ceiling((float)ClientSize.Height / k);
            Grid grid = new Grid(cellCountX, cellCountY, k);
            DrawCircle2(grid, 33, cellCountX, cellCountY, Color.Black);
            grid.DrawGrid(Graph);
        }
        
        private void DrawLine(Grid grid, int x1, int y1, int x2, int y2)
        {
            if (x1 != x2)
            {
                float m = (y2 - y1) / (x2 - x1);
                float y = y1;
                for (int x = x1; x < x2; x++)
                {
                    grid.SetPixel(x,(int)Math.Round(Math.Abs(y)), Color.Black);
                    y += m;
                }
            } else if (y1 == y2)
            {
                grid.SetPixel(x1, y1, Color.Black);
            }
            else
            {
                throw new Exception();
            }
        }
        
        private void DrawBrezenhem(Grid grid, int x1, int y1, int x2, int y2, Color color)
        {
            int x = x1;
            int y = y1;
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            
            bool l;

            int s1 = Math.Sign(x2 - x1);
            int s2 = Math.Sign(y2 - y1);

            if (dy > dx)
            {
                (dy, dx) = (dx, dy);
                l = true;
            }
            else
            {
                l = false;
            }
            
            float e = 2 * dy - dx;

            for (int i = 1; i < dx; i++)
            {
                grid.SetPixel(x, y, Color.Black);
                while (e >= 0)
                {
                    if (l)  x += s1;
                    else y += s2;
                    e -= 2 * dx;
                }
                if (l) y += s2;
                else x += s1;
                e += 2 * dy;
            }
            
            grid.SetPixel(x, y, Color.Coral);
        }
        
        private void DrawCircle(Grid grid, int rad, int cellCountX, int cellCountY, Color color)
        {
            int x = 0;
            int middleX = cellCountX / 2;
            int middleY = cellCountY / 2;
            int y = rad;
            int e = 3 - 2 * rad;
            while (x < y)
            {
                grid.SetPixel(middleX + x, middleY + y, color);
                grid.SetPixel(middleX + y, middleY + x, color);
                grid.SetPixel(middleX + y, middleY - x, color);
                grid.SetPixel(middleX + x, middleY - y, color);
                grid.SetPixel(middleX - x, middleY - y, color);
                grid.SetPixel(middleX - y, middleY - x, color);
                grid.SetPixel(middleX - y, middleY + x, color);
                grid.SetPixel(middleX - x, middleY + y, color);

                if (e < 0)
                {
                    e += 4 * x + 6;
                }
                else
                {
                    e += 4 * (x - y) + 10;
                    y--;
                }
                x++;
            }

            if (x != y) return;
            
            grid.SetPixel(middleX + x, middleY + y, color);
            grid.SetPixel(middleX + y, middleY + x, color);
            grid.SetPixel(middleX + y, middleY - x, color);
            grid.SetPixel(middleX + x, middleY - y, color);
            grid.SetPixel(middleX - x, middleY - y, color);
            grid.SetPixel(middleX - y, middleY - x, color);
            grid.SetPixel(middleX - y, middleY + x, color);
            grid.SetPixel(middleX - x, middleY + y, color);
        }

        private void DrawCircle2(Grid grid, int rad, int cellCountX, int cellCountY, Color color)
        {
            int x = 0;
            int middleX = cellCountX / 2;
            int middleY = cellCountY / 2;
            int y = rad;
            while (x < y)
            {
                grid.SetPixel(middleX + x, middleY + y, color);
                grid.SetPixel(middleX + y, middleY + x, color);
                grid.SetPixel(middleX + y, middleY - x, color);
                grid.SetPixel(middleX + x, middleY - y, color);
                grid.SetPixel(middleX - x, middleY - y, color);
                grid.SetPixel(middleX - y, middleY - x, color);
                grid.SetPixel(middleX - y, middleY + x, color);
                grid.SetPixel(middleX - x, middleY + y, color);

                y = (int)Math.Sqrt(rad * rad - x * x);
                x++;
            }

            if (x != y) return;

            grid.SetPixel(middleX + x, middleY + y, color);
            grid.SetPixel(middleX + y, middleY + x, color);
            grid.SetPixel(middleX + y, middleY - x, color);
            grid.SetPixel(middleX + x, middleY - y, color);
            grid.SetPixel(middleX - x, middleY - y, color);
            grid.SetPixel(middleX - y, middleY - x, color);
            grid.SetPixel(middleX - y, middleY + x, color);
            grid.SetPixel(middleX - x, middleY + y, color);
        }
    }
}