using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad
{
    public struct Point
    {
        public double x, y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point Add(Point p)
        {
            return new Unity_ScratchPad.Point()
            {
                x = this.x + p.x,
                y = this.y + p.y
            };
        }

        public Point Subtract(Point p)
        {
            return new Point()
            {
                x = this.x - p.x,
                y = this.y - p.y
            };
        }
    }
}
