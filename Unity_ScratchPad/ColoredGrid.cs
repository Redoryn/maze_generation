using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad
{
    public class ColoredGrid: IDisposable, IOutputMazeTileGrid
    {
        public int TileSize { get; set; }
        public int TilePadding { get; set; }
        public int Rows { get; }
        public int Columns { get; }

        private Color[] contents;
        private Dictionary<Color, Brush> brushes;
        private Graphics graphics;
        public ColoredGrid(int rows, int columns, int tileSize, Color defaultColor, Graphics graphics)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.TileSize = tileSize;
            this.TilePadding = 0;
            contents = new Color[this.Rows * this.Columns];
            brushes = new Dictionary<Color, Brush>();
            this.graphics = graphics;
            Fill(defaultColor);
        }

        public void Fill(Color c)
        {
            for (int i = 0; i < this.Rows * this.Columns; i++)
            {
                contents[i] = c;
            }
        }


        private int RowMajorIndex(int column, int row)
        {
            return (row * this.Columns) + column;
        }

        public Color Get(int column, int row)
        {
            return contents[RowMajorIndex(column, row)];
        }

        public void Set(int column, int row, Color color)
        {
            contents[RowMajorIndex(column, row)] = color;
        }

        public void Draw()
        {
            this.Draw(graphics);
        }

        public void Draw(Graphics graphics) {
            int x = 0;
            int y = 0;
            Color color = contents[0];
            Brush brush = GetBrush(color);
            for (int r = 0; r < this.Rows; r++)
            {
                x = TilePadding;
                for (int c = 0; c < this.Columns; c++)
                {
                    Color possibleColor = Get(c, r);
                    if (!possibleColor.Equals(color))
                    {
                        brush = GetBrush(possibleColor);
                        color = possibleColor;
                    }

                    graphics.FillRectangle(brush, new Rectangle(x, y, TileSize, TileSize));
                    x += (TileSize + TilePadding);
                }
                y += (TileSize + TilePadding);
            }
        }

        public Brush GetBrush(Color c)
        {
            if (!brushes.ContainsKey(c))
            {
                brushes[c] = new SolidBrush(c);
            }
            return brushes[c];
        }

        public void Dispose()
        {
            foreach(KeyValuePair<Color, Brush> kvp in brushes)
            {
                kvp.Value.Dispose();
            }
            if (this.graphics != null)
                graphics.Dispose();
        }

        public void MarkOpen(int x, int y)
        {
            this.Set(x, y, Color.Blue);
        }

        public void MarkWall(int x, int y)
        {
            this.Set(x, y, Color.Black);
        }
    }
}
