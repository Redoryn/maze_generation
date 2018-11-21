using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_ScratchPad.MazeAlgorithms;

namespace Unity_ScratchPad.MazeAlgorithms
{
    public class RecursiveBacktracker : BaseAlgorithm, IMazeGeneratorAlgorithm
    {
        Stack<Point> frontier;
        Random r;
        public RecursiveBacktracker():base(0,0,null){}

        public RecursiveBacktracker(int height, int width, IMaze maze):base(height, width, maze) {
            r = new Random(DateTime.Now.Millisecond);
            this.maze.Fill(MazeTile.Wall);
            frontier = null;
            int startingX = 0;
            int startingY = 0;
            current = new Point(startingX, startingY);
            MarkAsPartOfMaze(current);
        }

        public IMazeGeneratorAlgorithm Initialize(int height, int width, IMaze maze)
        {
            return new RecursiveBacktracker(height, width, maze);
        }

        public override void RunToCompletion()
        {
            while (frontier == null || frontier.Count > 0)
            {
                StepForward();
            }
        }

        private Point current;
        public override void StepForward()
        {
            if (frontier == null)
            {
                frontier = new Stack<Point>();
            }

            var unvisited = UnvisitedNeighbors(current, 2);
            if (unvisited.Count() > 0)
            {
                Point choosen = RandomElem(unvisited);
                frontier.Push(current);
                Point wall = WallPosition(current, choosen);
                MarkAsPartOfMaze(wall);
                current = choosen;
                MarkAsPartOfMaze(current);

            } else if (frontier.Count() > 0)
            {
                current = frontier.Pop();
            }
        }

        private T RandomElem<T>(IEnumerable<T> options)
        {
            return options.ElementAt(r.Next(options.Count()));
        }

    }
}
