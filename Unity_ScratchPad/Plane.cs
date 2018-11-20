using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad
{
    public class Plane<T>
    {
        public int Height { get; set; }
        public int Width { get; set; }

        private T[] contents;

        public Plane(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            contents = new T[height * width];
        }

        private int RowMajorIndex(int x, int y)
        {
            return (y * this.Width) + x;
        }

        public T Get(int x, int y)
        {
            return contents[RowMajorIndex(x, y)];
        }

        public void Set(int x, int y, T t)
        {
            contents[RowMajorIndex(x, y)] = t;
        }
    }
}
