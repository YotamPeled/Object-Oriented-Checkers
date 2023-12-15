using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    internal class TwoPlayerMatchManager : IMatchManager
    {
        private readonly Board m_Board;
        private readonly IUI m_UI;
        public TwoPlayerMatchManager(Board i_Board, IUI i_UI)
        {
            m_Board = i_Board;
            m_UI = i_UI;
            signUpForEvents();
        }

        private void signUpForEvents()
        {
            m_UI.PlayerMadeMove += PlayMove;
        }

        public void PlayMove(IMove i_MoveToPlay)
        {
            m_Board.MakeMove(i_MoveToPlay);
        }

        public void StartMatch()
        {
            m_Board.StartGame();
        }

        public void SwapSides()
        {
            throw new NotImplementedException();
        }

        public void StopMatch()
        {
            m_UI.PlayerMadeMove -= PlayMove;
        }
    }
}
