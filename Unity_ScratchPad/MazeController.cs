using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_ScratchPad.MazeAlgorithms;

namespace Unity_ScratchPad
{
    public class MazeController: IDisposable
    {
        private IOutputMazeTileGrid outputGrid;
        private IMaze maze;
        private Graphics graphics;
        private IMazeGeneratorAlgorithm algo;

        public MazeController(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public void SetAlgorithm(IMazeGeneratorAlgorithm algo)
        {
            this.algo = algo;
        }

        public void Initialize(int height, int width, int tileSize)
        {
            outputGrid = new ColoredGrid(height, width, tileSize, Color.Black, graphics);
            maze = new Maze(height, width, outputGrid);
            maze.SetAlgorithm(this.algo);
        }

        public void Reset()
        {
            maze.Fill(MazeTile.Wall);
        }

        public void Draw()
        {
            maze.Draw();
        }

        public void StepForward(int n = 1)
        {
            maze.StepForward(n);
        }

        public void RunToCompletion()
        {
            maze.RunToCompletion();
        }

        public void Dispose()
        {
            if (outputGrid != null)
            {
                outputGrid.Dispose();
            }
        }
    }
}
