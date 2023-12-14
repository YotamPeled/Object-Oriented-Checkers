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
        private bool m_IsPlayerTurn = true; // true - Player 1 Turn | flase - Player 2 Turn
        private bool m_GameHasEnded = false;
        public TwoPlayerMatchManager(Board i_Board, IUI i_UI)
        {
            m_Board = i_Board;
            m_UI = i_UI;
            signUpForEvents();
        }

        private void signUpForEvents()
        {
            m_Board.TurnChanged += changeTurn;
            m_Board.GameEnded += gameEnded;
            m_Board.ReadyForNextMove += ManageTurn;
            m_UI.PlayerMadeMove += PlayMove;
        }
        private void ManageTurn()
        {
            if (m_GameHasEnded)
            {
                m_UI.GameHasEnded();
            }
        }

        private void gameEnded(eColor color)
        {
            m_GameHasEnded = true;
        }

        private void changeTurn()
        {
            m_IsPlayerTurn = !m_IsPlayerTurn;
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
            // dont have to stop anything
        }
    }
}
