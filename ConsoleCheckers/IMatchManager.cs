using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public interface IMatchManager
    {
        void StartMatch();
        void PlayMove(IMove i_MoveToPlay);
        void SwapSides();
        void StopMatch();
    }
}
