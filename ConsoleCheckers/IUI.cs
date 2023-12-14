using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public interface IUI
    {
        void Reset();
        void SetActiveState();
        void SetInActiveState();
        void GameHasEnded();
        event Action<IMove> PlayerMadeMove;
    }
}
