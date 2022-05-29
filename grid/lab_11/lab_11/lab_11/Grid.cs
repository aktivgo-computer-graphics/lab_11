using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace lab_11
{
    class Grid
    {
        List<List<Point>> grid;
        List<List<bool>> gridBool;
        int step;

        public Grid(int countRow, int countColumn, int step)
        {
            this.step = step;
            grid = new List<List<Point>>();
            gridBool = new List<List<bool>>();
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
    }
}
