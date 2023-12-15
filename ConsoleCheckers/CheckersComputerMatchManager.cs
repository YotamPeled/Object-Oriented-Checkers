using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    internal class CheckersComputerMatchManager : IMatchManager
    {
        private readonly Board m_Board;
        private readonly ICheckersComputer Computer1;
        private readonly ICheckersComputer Computer2;
        private bool m_Turn = true; // true - Computer1 Turn | flase - Computer2 Turn
        private bool m_GameHasEnded = false;
        private int Computer1Score = 0;
        private int Computer2Score = 0;
        public CheckersComputerMatchManager(Board i_Board, bool i_Computer1StartingStarting)
        {
            m_Board = i_Board;
            signUpForEvents();
            Computer1 = new CheckersComputer();
            Computer2 = new CheckersComputer();
            m_Turn = i_Computer1StartingStarting;
        }

        private void signUpForEvents()
        {
            m_Board.TurnChanged += changeTurn;
            m_Board.GameEnded += gameEnded;
            m_Board.ReadyForNextMove += RunPlayTurnInNewThread;
        }

        public void PlayMove(IMove i_MoveToPlay)
        {
            m_Board.MakeMove(i_MoveToPlay);
        }

        public void SetStrategy(Func<uint[], int> computerStrategy, bool isSettingForComputer1)
        {
            if (isSettingForComputer1)
            {
                Computer1.SetEvaluationStrategy(computerStrategy);
            }
            else
            {
                Computer2.SetEvaluationStrategy(computerStrategy);
            }
        }

        public void SwapSides()
        {
            throw new NotImplementedException();
        }

        private async void RunPlayTurnInNewThread()
        {
            if (!m_GameHasEnded)
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
            ICheckersComputer PlayingComputer; ;

            if (m_Turn)
            {
                PlayingComputer = Computer1;
            }
            else
            {
                PlayingComputer = Computer2;
            }

            PlayingComputer.GenerateMove();
            return PlayingComputer.BestMove;
        }

        private void changeTurn()
        {
            m_Turn = !m_Turn;
        }

        private void gameEnded(eColor i_WinningColor)
        {
            m_GameHasEnded = true;
        }

        public void StartMatch()
        {
            m_Board.StartGame();
        }

        public void StopMatch()
        {
            m_GameHasEnded = true;
        }
    }
}
