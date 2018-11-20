using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity_ScratchPad.MazeAlgorithms;

namespace Unity_ScratchPad
{
    public enum MazeTile
    {
        Open,
        Wall,
        Unknown
    }

    public struct Point
    {
        public int x, y;
        public Point (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Wall
    {
        public Point SideA, SideB;

        public Wall(Point sideA, Point sideB)
        {
            SideA = sideA;
            SideB = sideB;           
        }

        public Wall(int x0, int y0, int x1, int y1): this(new Point(x0, y0), new Point(x1, y1))
        {

        }
    }

    public class Maze: IMaze
    {
        public MazeTile[] cells;
        int Height;
        int Width;
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
            Point left = new Point(x - dist, y);
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
            return IsPathTile(p.x, p.y);
        }

        public bool IsWallTile(Point p)
        {
            return IsWallTile(p.x, p.y);
        }

        public bool IsWallTile(int x, int y)
        {
            return Get(x, y) == MazeTile.Wall;
        }

        public void MakePathTile(Point p)
        {
            Set(p.x, p.y, MazeTile.Open);
        }


        public bool InBounds(Point p)
        {
            return InBounds(p.x, p.y);
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
            this.MarkAsPartOfMaze(p.x, p.y);
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
