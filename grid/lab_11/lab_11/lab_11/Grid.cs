using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace lab_11
{
    class Grid
    {
        List<List<Point>> grid;
        List<List<bool>> gridBool;
        List<List<bool>> gridFill;
        int step;

        public Grid(int countRow, int countColumn, int step)
        {
            this.step = step;
            grid = new List<List<Point>>();
            gridBool = new List<List<bool>>();
            gridFill = new List<List<bool>>();
            for (int i = 0; i < countRow; i++)
            {
                List<Point> row = new List<Point>();
                List<bool> rowBool = new List<bool>();
                for (int k = 0; k < countColumn; k++)
                {
                    row.Add(new Point(i * step, k*step));
                    rowBool.Add(false);
                }
                grid.Add(row);
                gridBool.Add(rowBool);
                gridFill.Add(rowBool);
            }
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x < gridBool.Count && y < gridBool[0].Count)
            {
                gridBool[x][y] = true;
            }
        }

        public void DrawGrid(Graphics Graph)
        {
            int sizeRow = grid[0].Count-1;

            foreach (var t in grid)
            {
                Graph.DrawLine(Pens.Black, t[0], new Point(t[sizeRow].X, t[sizeRow].Y));
            }
            
            for (int i = 0; i < grid[0].Count; i++)
            {
                Graph.DrawLine(Pens.Black, grid[0][i], new Point(grid[grid.Count-1][i].X, grid[grid.Count-1][i].Y));
            }
            
            for (int i = 0; i < gridBool.Count; i++)
            {
                for (int k = 0; k < gridBool[0].Count; k++)
                {
                    if (!gridBool[i][k]) continue;
                    Graph.FillRectangle(Brushes.Black, new Rectangle(grid[i][k], new Size(step, step)));
                    Thread.Sleep(10);
                }
            }
        }
        
        public void FillGrid(Graphics Graph, int x0, int y0)
        {
            if (gridBool[x0][y0]) return;
            
            Stack<Point> stack = new Stack<Point>();
            int x = x0, y = y0;
            stack.Push(new Point(x, y));

            while (stack.Count != 0)
            {
                Point p = stack.Pop();
                x = p.X;
                y = p.Y;
                if (!gridFill[x][y])
                {
                    Graph.FillRectangle(Brushes.LightBlue, new Rectangle(grid[x][y], new Size(step, step))); 
                    //Thread.Sleep(1);
                    gridFill[x][y] = true;
                }
                if (x + 1 < gridBool[0].Count && !gridBool[x + 1][y] && !gridFill[x + 1][y])
                {
                    stack.Push(new Point(x + 1, y));
                }
                if (y + 1 < gridBool.Count && !gridBool[x][y + 1] && !gridFill[x][y + 1])
                {
                    stack.Push(new Point(x, y + 1));
                }
                if (x - 1 >= 0 && !gridBool[x - 1][y] && !gridFill[x - 1][y])
                {
                    stack.Push(new Point(x - 1, y));
                }
                if (y - 1 >= 0 && !gridBool[x][y - 1] && !gridFill[x][y - 1])
                {
                    stack.Push(new Point(x, y - 1));
                }
            }
        }
    }
}
