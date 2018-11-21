using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad
{
    public struct Wall
    {
        public Point SideA, SideB;

        public Wall(Point sideA, Point sideB)
        {
            SideA = sideA;
            SideB = sideB;
        }

        public Wall(int x0, int y0, int x1, int y1) : this(new Point(x0, y0), new Point(x1, y1))
        {

        }
    }
}
