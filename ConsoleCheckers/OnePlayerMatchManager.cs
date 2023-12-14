using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace ConsoleCheckers
{
    internal class OnePlayerMatchManager : IMatchManager
    {
        private readonly Board m_Board;
        private readonly ICheckersComputer m_Computer;
        private readonly IUI m_Player;
        private bool m_IsPlayerTurn = true; // true - Player Turn | flase - Computer Turn
        private bool m_GameHasEnded = false;
        private int Computer1Score = 0;
        private int Computer2Score = 0;
        public OnePlayerMatchManager(Board i_Board, bool i_IsPlayerTurn, IUI i_UI)
        {
            m_Board = i_Board;
            signUpForEvents();
            m_Computer = new CheckersComputer();
            m_IsPlayerTurn = i_IsPlayerTurn;
            m_Player = i_UI;
            i_UI.PlayerMadeMove += PlayMove;
        }

        private void signUpForEvents()
        {
            m_Board.TurnChanged += changeTurn;
            m_Board.GameEnded += gameEnded;
            m_Board.ReadyForNextMove += ManageTurn;
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

        private void ManageTurn()
        {
            if (!m_GameHasEnded)
            {
                if (m_IsPlayerTurn)
                {
                    m_Player.SetActiveState();
                }
                else
                {
                    m_Player.SetInActiveState();
                    RunPlayTurnInNewThread();
                }
            }
            else
            {
                m_Player.GameHasEnded();
            }
        }

        private async void RunPlayTurnInNewThread()
        {
            if(!m_GameHasEnded)
            {
                IMove generatedMove = await Task.Run(() => PlayTurn());

                // Now that the move is generated, it can be used in the main thread
                if (generatedMove != null && !m_GameHasEnded)
                {
                    m_Board.MakeMove(generatedMove);
                }
            }
        }

        private IMove PlayTurn()
        {
            m_Computer.GenerateMove();
            return m_Computer.BestMove;
        }


        public void SetStrategy(Func<uint[], int> i_ComputerEvaluationStrategy)
        {
            m_Computer.SetEvaluationStrategy(i_ComputerEvaluationStrategy);
        }

        public void StopMatch()
        {
            m_GameHasEnded = true;
            m_Player.PlayerMadeMove -= PlayMove;
        }
    }
}
