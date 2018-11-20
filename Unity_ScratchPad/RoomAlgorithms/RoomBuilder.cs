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


    public class RoomBuilder
    {
        Random r;
        public RoomBuilder()
        {
            r = new Random(DateTime.Now.Millisecond);
        }

        public void GenerateRooms(IMaze maze, int n=1)
        {
            int height = maze.Height;
            int width = maze.Width;
            RecursiveSubDivideRoomGeneration(maze, new Point(0, 0), new Room(height, width), 3);
        }

        private void RecursiveSubDivideRoomGeneration(IMaze maze, Point topLeft, Room room, int minLevel = 1)
        {
            if (room.Width <= 0 || room.Height <= 0) return;

            if (minLevel == 0)
            {
                GenerateRandomRoom(maze, topLeft, room);
                return;
            }

            int xSplice = (int)RandomGaussian(room.Width * 0.75, 1);
            int ySplice = (int)RandomGaussian(room.Height * 0.75, 1);
            //int xSplice = r.Next(room.Width);
            //int ySplice = r.Next(room.Height);
            //int xSplice = (int)room.Width / 2;
            //int ySplice = (int)room.Height / 2;

            Point subTopLeft     = topLeft.Add(new Point(0, 0));
            int subTopLeftHeight = ySplice;
            int subTopLeftWidth  = xSplice;
            Room subTopLeftRoom = new Room(subTopLeftHeight, subTopLeftWidth);

            Point subTopRight    = topLeft.Add(new Point(xSplice, 0));
            int subTopRightHeight = ySplice;
            int subTopRightWidth = room.Width - xSplice;
            Room subTopRightRoom = new Room(subTopRightHeight, subTopRightWidth);

            Point subBottomLeft  = topLeft.Add(new Point(0, ySplice));
            int subBottomLeftHeight = room.Height - ySplice;
            int subBottomLeftWidth = xSplice;
            Room subButtomLeftRoom = new Room(subBottomLeftHeight, subBottomLeftWidth);

            Point subBottomRight = topLeft.Add(new Point(xSplice, ySplice));
            int subBottomRightHeight = room.Height - ySplice;
            int subBottomRightWidth = room.Width - xSplice;
            Room subBottomRightRoom = new Room(subBottomRightHeight, subBottomRightWidth);

            RecursiveSubDivideRoomGeneration(maze, subTopLeft, subTopLeftRoom, r.Next(minLevel));
            RecursiveSubDivideRoomGeneration(maze, subTopRight, subTopRightRoom, r.Next(minLevel));
            RecursiveSubDivideRoomGeneration(maze, subBottomLeft, subButtomLeftRoom, r.Next(minLevel));
            RecursiveSubDivideRoomGeneration(maze, subBottomRight, subBottomRightRoom, r.Next(minLevel));
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
