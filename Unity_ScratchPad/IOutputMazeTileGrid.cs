using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_ScratchPad
{
    public interface IOutputMazeTileGrid: IDisposable
    {
        void MarkOpen(int x, int y);
        void MarkWall(int x, int y);
        void Draw();
    }
}
