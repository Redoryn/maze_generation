using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad.RoomAlgorithms
{
    public struct Room
    {
        public Room(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }       

        public int Height;
        public int Width;
    }

    public class RoomQuadTree: QuadTree<Room>
    {
        public RoomQuadTree(int height, int width):
                base(new QuadTreeNode<Room>(new Point(0, 0), height, width))
        {}

        public override double HorizontalSplice()
        {
            return 0.2;
        }

        public override double VerticalSplice()
        {
            return 0.2;
        }


    }


    public class RoomBuilder
    {
        Random r;
        public RoomBuilder()
        {
            r = new Random(DateTime.Now.Millisecond);
        }

        private IMaze maze;
        private int maxLevel;
        public void GenerateRooms(IMaze maze, int n=1)
        {
            this.maze = maze;
            int height = maze.Height;
            int width = maze.Width;
            maxLevel = 4;
            RoomQuadTree qt = new RoomQuadTree(height, width);
            qt.Divide(maxLevel, GenerateRoom);
        }

        public void GenerateRoom(Point pos, double height, double width, int level)
        {
            if (level != maxLevel) return;

            if (r.Next(100) > 20) return;

            Room room = new Room((int)Math.Ceiling(height), (int)Math.Ceiling(width));
            FillInRoom(maze, room, pos);
        }

        

        private void GenerateRandomRoom(IMaze maze, Point containerPos, Room containerRoom)
        {
            Point containerCenter = CenterPoint(containerRoom);
            Room room = GenerateRoom((int)containerRoom.Height/ 2, (int)containerRoom.Width/ 2);
            Point loc = containerCenter;
            FillInRoom(maze, room, containerPos.Add(CenterPoint(room)));
        }

        private Point CenterPoint(Room container)
        {
            return new Unity_ScratchPad.Point()
            {
                x = container.Width / 2.0f,
                y = container.Height / 2.0f
            };
        }


        private Point RandomPointInRectangle(int rectangleHeight, int rectangleWidth)
        {
            return new Point(r.Next(rectangleWidth), r.Next(rectangleHeight));
        }


        private void FillInRoom(IMaze maze, Room room, Point topLeft)
        {
            int p_x = 0;
            int p_y = 0;
            for (int x = 0; x < room.Width; x++)
            {
                for (int y = 0; y < room.Height; y++)
                {
                    p_x = (int)topLeft.x + x;
                    p_y = (int)topLeft.y + y;
                    if (maze.InBounds(p_x, p_y))
                    {
                        maze.MarkAsPartOfMaze(p_x, p_y);
                    }

                }
            }

        }


        private Room GenerateRoom(int medianHeight, int medianWidth)
        {
            int height = (int)RandomGaussian(medianHeight, 1);
            int width = (int)RandomGaussian(medianWidth, 1);

            return new Room()
            {
                Height = height,
                Width = width
            };
        }

        private double RandomGaussian(double mean=0.5, double stdDev=0.1)
        {
            double u1 = 1.0 - r.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - r.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }

    }
}
