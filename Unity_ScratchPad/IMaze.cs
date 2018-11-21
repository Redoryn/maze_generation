using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_ScratchPad.MazeAlgorithms;

namespace Unity_ScratchPad
{
    public interface IMaze
    {
        int Height { get; }
        int Width { get; }

        void Fill(MazeTile tile);
        double FilledRatio();
        void MarkAsPartOfMaze(int x, int y);
        IEnumerable<Point> Neighbors(int x, int y, int dist = 1);
        void SetAlgorithm(IMazeGeneratorAlgorithm algo);

        bool IsWallTile(Point p);
        bool IsWallTile(int x, int y);

        bool InBounds(Point p);
        bool InBounds(int x, int y);

        void StepForward(int n = 1);
        void RunToCompletion();

        void Draw();
    }
}
