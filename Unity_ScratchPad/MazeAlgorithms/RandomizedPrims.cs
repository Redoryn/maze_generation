using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad.MazeAlgorithms
{

    public class RandomizedPrims: BaseAlgorithm, IMazeGeneratorAlgorithm
    {
        private Random r;
        private List<Wall> walls;

        public RandomizedPrims(): base(0,0, null)
        {
        }

        public RandomizedPrims(int height, int width, IMaze maze) : base(height, width, maze)
        {
            r = new Random(DateTime.Now.Millisecond);
            Start(10, 10);
        }

        public override IMazeGeneratorAlgorithm Initialize(int height, int width, IMaze maze)
        {
            return new RandomizedPrims(height, width, maze);
        }


        private IEnumerable<Wall> SurroundingWalls(int x, int y)
        {
            List<Wall> walls = new List<Wall>();
            foreach (Point p in this.maze.Neighbors(x, y, 2))
            {
                if (this.maze.IsWallTile(p))
                {
                    walls.Add(new Wall(x, y, (int)p.x, (int)p.y));
                }
            }

            return walls.Where(InBounds);
        }

        private bool InBounds(Wall w)
        {
            return this.maze.InBounds(w.SideA) &&
                   this.maze.InBounds(w.SideB);
        }

        private bool IsOnlyOneSideOfDoorVisited(Wall w)
        {
            return (IsVisited(w.SideA) ^ IsVisited(w.SideB));
        }

        private Point WallPosition(Wall w)
        {
            return new Point((w.SideA.x + w.SideB.x) / 2,
                             (w.SideA.y + w.SideB.y) / 2);
        }

        private Wall RandomWallFromWallList(List<Wall> wallList)
        {
            int index = r.Next(wallList.Count);
            Wall w = wallList[index];
            wallList.RemoveAt(index);
            return w;
        }

        private void Start(int x, int y)
        {
            this.walls = new List<Wall>();
            this.maze.Fill(MazeTile.Wall);
            int startingX = x;
            int startingY = y;
            this.MarkAsPartOfMaze(startingX, startingY);
            walls.AddRange(SurroundingWalls(startingX, startingY));
        }

        public override void StepForward()
        {
            if (walls == null || walls.Count == 0) return;

            Wall w = RandomWallFromWallList(walls);
            if (IsOnlyOneSideOfDoorVisited(w))
            {
                Point unvisited = IsVisited(w.SideA) ? w.SideB : w.SideA;
                Point wallPos = WallPosition(w);
                MarkAsPartOfMaze(unvisited);
                MarkAsPartOfMaze(wallPos);
                walls.AddRange(SurroundingWalls((int)unvisited.x, (int)unvisited.y));
            }
        }

        public override void RunToCompletion()
        {
            while (walls.Count > 0)
            {
                StepForward();
            }
        }
    }
}
