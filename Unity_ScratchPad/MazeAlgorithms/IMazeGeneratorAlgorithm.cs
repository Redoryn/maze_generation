using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad.MazeAlgorithms
{
    public interface IMazeGeneratorAlgorithm
    {
        IMazeGeneratorAlgorithm Initialize(int Height, int Width, IMaze maze);
        void StepForward();
        void RunToCompletion();
    }
}
