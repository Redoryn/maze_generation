using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity_ScratchPad.MazeAlgorithms;

namespace Unity_ScratchPad
{
    public class Maze: IMaze
    {
        public MazeTile[] cells;
        public int Height { get; }
        public int Width { get; }
        private Random r;
        private IOutputMazeTileGrid mazeTileGrid;
        private IMazeGeneratorAlgorithm algo;
        public Maze(int height, int width, IOutputMazeTileGrid grid)
        {
            this.Height = height;
            this.Width = width;
            this.cells = new MazeTile[height * width];
            r = new Random(DateTime.Now.Millisecond);
            mazeTileGrid = grid;
        }

        public void SetAlgorithm(IMazeGeneratorAlgorithm algo)
        {
            this.algo = algo;
            this.algo = algo.Initialize(this.Height, this.Width, this);
        }

        private int RowMajorIndex(int x, int y)
        {
            return (y * this.Width) + x;
        }

        public void Set(int x, int y, MazeTile tile)
        {
            this.cells[RowMajorIndex(x, y)] = tile;
        }

        public MazeTile Get(int x, int y)
        {
            return this.cells[RowMajorIndex(x, y)];
        }

        public IEnumerable<Point> Neighbors (int x, int y, int dist = 1)
        {
            Point above = new Point(x, y - dist);
            Point below = new Point(x, y + dist);
            Point left  = new Point(x - dist, y);
            Point right = new Point(x + dist, y);

            List<Point> points = new List<Point>()
            {
                above, below, left, right
            };
            return points.Where(InBounds);
        }

        public bool IsPathTile(int x, int y)
        {
            return Get(x, y) == MazeTile.Open;
        }

        public bool IsPathTile(Point p)
        {
            return IsPathTile((int)p.x, (int)p.y);
        }

        public bool IsWallTile(Point p)
        {
            return IsWallTile((int)p.x, (int)p.y);
        }

        public bool IsWallTile(int x, int y)
        {
            return Get(x, y) == MazeTile.Wall;
        }

        public void MakePathTile(Point p)
        {
            Set((int)p.x, (int)p.y, MazeTile.Open);
        }


        public bool InBounds(Point p)
        {
            return InBounds((int)p.x, (int)p.y);
        }

        public bool InBounds(int x, int y)
        {
            return !(x < 0 || x >= this.Width ||
                     y < 0 || y >= this.Height);
        }       

        private void Fill(MazeTile tile)
        {
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                this.cells[i] = tile;
            }
        }

        private void MarkAsPartOfMaze(int x, int y)
        {
            Set(x, y, MazeTile.Open);
        }

        private void MarkAsPartOfMaze(Point p)
        {
            this.MarkAsPartOfMaze((int)p.x, (int)p.y);
        }
        
        public void Draw()
        {
            this.Draw(this.mazeTileGrid);
        }

        public void Draw(IOutputMazeTileGrid tileGrid)
        {
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    MazeTile tile = this.Get(x, y);
                    if (tile == MazeTile.Open)
                    {
                        tileGrid.MarkOpen(x, y);
                    }
                    else if (tile == MazeTile.Wall)
                    {
                        tileGrid.MarkWall(x, y);
                    }
                }
            }
            tileGrid.Draw();
        }

        public double FilledRatio()
        {
            int countFilled = 0;
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    MazeTile tile = this.Get(x, y);
                    if (tile == MazeTile.Wall)
                    {
                        countFilled++;
                    }
                }
            }
            return (double)countFilled / (Width * Height);
        }

        void IMaze.Fill(MazeTile tile)
        {
            this.Fill(tile);
        }

        void IMaze.MarkAsPartOfMaze(int x, int y)
        {
            this.MarkAsPartOfMaze(x, y);
        }

        public void StepForward(int n)
        {
            for (int i = 0; i < n; i++)
            {
                this.algo.StepForward();
            }
            
        }

        public void RunToCompletion()
        {
            this.algo.RunToCompletion();
        }
    }
}
