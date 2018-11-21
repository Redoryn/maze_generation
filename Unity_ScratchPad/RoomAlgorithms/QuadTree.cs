using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad.RoomAlgorithms
{

    public interface IQuadTreeNode<T>
    {
        Point Position { get; }
        double Height { get;}
        double Width { get;}
        IQuadTreeNode<T> Parent { get;}
        /// <summary>
        /// [upper-left, upper-right, lower-left, lower-right]
        /// </summary>
        IEnumerable<IQuadTreeNode<T>> Quarters { get; set; }
        T Data { get; set; }
    }

    public class QuadTreeNode<T> : IQuadTreeNode<T>
    {
        public QuadTreeNode(Point pos, double height, double width):
                this(null, pos, height, width){}

        public QuadTreeNode(IQuadTreeNode<T> parent, Point pos, double height, double width)
        {
            this.Parent = parent;
            this.Position = pos;
            this.Height = height;
            this.Width = width;
            this.Data = default(T);
        }

        public T Data { get; set; }

        public IEnumerable<IQuadTreeNode<T>> Quarters
        {
            get;set;
        }

        public double Height
        {
            get;set;
        }

        public IQuadTreeNode<T> Parent
        {
            get;set;
        }

        public Point Position
        {
            get;set;
        }

        public double Width
        {
            get;set;
        }
    }

    public delegate void QuadtreeNodeDelegate (Point pos, double height, double width, int level);

    public class QuadTree<T>
    {
        private IQuadTreeNode<T> _root;
        public QuadTree(IQuadTreeNode<T> root)
        {
            _root = root;
        }

        public virtual double HorizontalSplice()
        {
            return 0.5;
        }

        public virtual double VerticalSplice()
        {
            return 0.5;
        }

        public void Divide(int maxLevels, QuadtreeNodeDelegate onNode)
        {
            Divide(_root, maxLevels, onNode, 1);
        }

        private void Divide(IQuadTreeNode<T> node, int maxLevels, QuadtreeNodeDelegate onNode, int level)
        {
            if (maxLevels <= 0)
                return;

            onNode(node.Position, node.Height, node.Width, level);
            DivideNode(node);
            foreach (var quarter in node.Quarters)
            {
                Divide(quarter, maxLevels - 1, onNode, level + 1);
            }
        }

        public void DivideNode(IQuadTreeNode<T> node)
        {
            Point topLeft = node.Position;
            double xSplice = VerticalSplice() * node.Width;
            double ySplice = HorizontalSplice() * node.Height;

            Point subTopLeft = topLeft.Add(new Point(0, 0));
            double subTopLeftHeight = ySplice;
            double subTopLeftWidth  = xSplice;
            IQuadTreeNode<T> subTopLeftRoom = new QuadTreeNode<T>(node, subTopLeft, subTopLeftHeight, subTopLeftWidth);

            Point subTopRight = topLeft.Add(new Point(xSplice, 0));
            double subTopRightHeight = ySplice;
            double subTopRightWidth  = node.Width - xSplice;
            IQuadTreeNode<T> subTopRightRoom = new QuadTreeNode<T>(node, subTopRight, subTopRightHeight, subTopRightWidth);

            Point subBottomLeft = topLeft.Add(new Point(0, ySplice));
            double subBottomLeftHeight = node.Height - ySplice ;
            double subBottomLeftWidth  = xSplice;
            IQuadTreeNode<T> subButtomLeftRoom = new QuadTreeNode<T>(node, subBottomLeft, subBottomLeftHeight, subBottomLeftWidth);

            Point subBottomRight = topLeft.Add(new Point(xSplice, ySplice));
            double subBottomRightHeight = node.Height - ySplice;
            double subBottomRightWidth  = node.Width - xSplice;
            IQuadTreeNode<T> subBottomRightRoom = new QuadTreeNode<T>(node, subBottomRight, subBottomRightHeight, subBottomRightWidth);
            node.Quarters = new IQuadTreeNode<T>[] { subTopLeftRoom, subTopRightRoom, subButtomLeftRoom, subBottomRightRoom };
        }
    }
}
