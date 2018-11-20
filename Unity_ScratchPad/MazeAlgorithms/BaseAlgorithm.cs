using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad.MazeAlgorithms
{
    abstract public class BaseAlgorithm: IMazeGeneratorAlgorithm
    {
        protected IMaze maze;
        protected Plane<bool> visited;
        public BaseAlgorithm(int height, int width, IMaze maze)
        {
            this.maze = maze;
            visited = new Plane<bool>(height, width);
        }

        protected IEnumerable<Point> UnvisitedNeighbors(Point p, int dist=1)
        {
            return maze.Neighbors((int)p.x, (int)p.y, dist).Where(pp => (!IsVisited(pp)));
        }

        public Point WallPosition(Point a, Point b)
        {
            return new Point((a.x + b.x) / 2,
                             (a.y + b.y) / 2);
        }

        protected void MarkAsPartOfMaze(Point p)
        {
            MarkAsPartOfMaze((int)p.x, (int)p.y);
        }

        protected void MarkAsPartOfMaze(int x, int y)
        {
            this.maze.MarkAsPartOfMaze(x, y);
            this.MarkAsVisited(x, y);
        }

        protected void MarkAsVisited(Point p)
        {
            MarkAsVisited((int)p.x, (int)p.y);
        }
        protected void MarkAsVisited(int x, int y)
        {
            visited.Set(x, y, true);
        }

        protected void MarkAsNotVisited(Point p)
        {
            visited.Set((int)p.x, (int)p.y, false);
        }

        protected bool IsVisited(Point p)
        {
            return IsVisited((int)p.x, (int)p.y);
        }

        protected bool IsVisited(int x, int y)
        {
            return visited.Get(x, y);
        }

        public virtual IMazeGeneratorAlgorithm Initialize(int Height, int Width, IMaze maze)
        {
            throw new NotImplementedException();
            return null;
        }

        public virtual void StepForward()
        {
            throw new NotImplementedException();
        }

        public virtual void RunToCompletion()
        {
            throw new NotImplementedException();
        }
    }
}
