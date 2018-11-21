using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_ScratchPad.MazeAlgorithms;
using Unity_ScratchPad.RoomAlgorithms;

namespace Unity_ScratchPad
{
    public class MazeController: IDisposable
    {
        private IOutputMazeTileGrid outputGrid;
        private IMaze maze;
        private Graphics graphics;
        private IMazeGeneratorAlgorithm algo;

        public bool GenerateRooms { get; internal set; }

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

        public void AddRooms()
        {
            RoomBuilder builder = new RoomBuilder();
            builder.GenerateRooms(maze, 6);
            maze.Draw();
            Console.WriteLine(string.Format("Filled ratio: {0}", maze.FilledRatio()));
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
