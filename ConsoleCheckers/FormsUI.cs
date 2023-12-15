using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    public partial class FormsUI : IUI
    {
        private MarkingsHistoryManager m_MarkingsHistory;
        private FormMarkingsNode m_Markings;
        private int m_CurrentlyViewingPositionNumber = 0;
        private bool isSelectionEnabled = true;
        private uint m_SelectedSquare;
        private Dictionary<uint, CheckersButton> m_PositionToButtonDict = new Dictionary<uint, CheckersButton>();
        public event Action<IMove> PlayerMadeMove;
        public FormsUI()
        {
            InitializeComponent();
            fillDict();
        }

        private void fillDict()
        {
            foreach(CheckersButton buttonSquare in panelCheckers.Controls)
            {
                if (buttonSquare.BitPosition != 0)
                {
                    m_PositionToButtonDict[buttonSquare.BitPosition] = buttonSquare;
                    buttonSquare.Click += Button_Click;
                }
            }
        }

        private void loadPieceImages(bool addActions = false)
        {
            foreach(CheckersButton button in panelCheckers.Controls)
            {
                button.Draw(GameMasterSingleton.Instance.Board);                
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (!isSelectionEnabled || !GameMasterSingleton.Instance.PlayerTurn)
            {
                return;
            }

            CheckersButton clickedSquare = sender as CheckersButton;
            // white square clicked
            if (clickedSquare.BitPosition == 0 || m_SelectedSquare == 0)
            {
                m_SelectedSquare = clickedSquare.BitPosition;
                selectSquareAndItsLegalMoves(clickedSquare.BitPosition);
            }
            else
            {
                bool isLegalMove = false;
                foreach(IMove move in GameMasterSingleton.Instance.Board.LegalMoves)
                {
                    if (move.GetStartSquare() == m_SelectedSquare && move.GetTargetSquare() == clickedSquare.BitPosition)
                    {
                        isLegalMove = true;
                        PlayerMadeMove?.Invoke(move);
                        break;
                    }
                }

                if (!isLegalMove)
                {
                    m_SelectedSquare = clickedSquare.BitPosition;
                    selectSquareAndItsLegalMoves(clickedSquare.BitPosition);
                }
            }
        }

        private void selectSquareAndItsLegalMoves(uint i_Selection)
        {
            List<uint> squaresToMark = new List<uint>();
            // valid black square
            if (i_Selection != 0)
            {
                squaresToMark.Add(i_Selection);
                foreach (IMove move in GameMasterSingleton.Instance.Board.LegalMoves)
                {
                    if (move.GetStartSquare() == i_Selection)
                    {
                        squaresToMark.Add(move.GetTargetSquare());
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            m_Markings.AddHighlightedLegalMovesAndReplacePrevious(squaresToMark);
        }

        private void OnMadeMove(IMove i_Move)
        {
            m_PositionToButtonDict[i_Move.GetStartSquare()].Draw(GameMasterSingleton.Instance.Board);
            m_PositionToButtonDict[i_Move.GetTargetSquare()].Draw(GameMasterSingleton.Instance.Board);
            if (i_Move.IsCapture())
            {
                m_PositionToButtonDict[i_Move.GetCaptureSquare()].Draw(GameMasterSingleton.Instance.Board);
                updateTakenPiecesPanel();
            }

            ReplaceMarkingsNode(i_Move);
            m_CurrentlyViewingPositionNumber++;
        }

        private void ReplaceMarkingsNode(IMove i_Move)
        {
            m_Markings.Hide();
            m_MarkingsHistory.addToHistory(m_Markings);

            m_Markings = new FormMarkingsNode(m_PositionToButtonDict);
            List<uint> movedPieceSquares = new List<uint>() { i_Move.GetStartSquare(), i_Move.GetTargetSquare() };
            m_Markings.AddMovedPieceSquares(movedPieceSquares);

            if (i_Move.IsCapture())
            {
                m_Markings.AddCapturedPieceSquare(i_Move.GetCaptureSquare());
            }

            m_Markings.Show();
        }

        private void viewHistoryPosition(int i_PositionNumber)
        {
            if (i_PositionNumber + 1 != GameMasterSingleton.Instance.Board.PositionsHistory.Count)
            {
                GameMasterSingleton.Instance.Board.MadeMove -= OnMadeMove;
                m_Markings.Hide();
                m_MarkingsHistory.ViewHistory(i_PositionNumber);
                SetInActiveState();
                Board boardToView = new Board(GameMasterSingleton.Instance.Board.PositionsHistory[i_PositionNumber]);
                foreach (CheckersButton button in panelCheckers.Controls)
                {
                    button.Draw(boardToView);
                }
            }
            else
            {
                GameMasterSingleton.Instance.Board.MadeMove += OnMadeMove;
                SetActiveState();
                foreach (CheckersButton button in panelCheckers.Controls)
                {
                    button.Draw(GameMasterSingleton.Instance.Board);
                }

                m_MarkingsHistory.StopViewingHistory();
                m_Markings.Show();
            }
            
        }

        private void updateTakenPiecesPanel()
        {
            if (GameMasterSingleton.Instance.Board.Turn == eColor.White)
            {
                panelWhiteCapture.AddImage();
            }
            else if(GameMasterSingleton.Instance.Board.Turn == eColor.Black)
            {
                panelBlackCapture.AddImage();
            }
        }

        private void OnGameEnd(eColor i_Color)
        {
            SetInActiveState();
            MessageBox.Show($"game has ended, the winner is {i_Color}");
        }

        private void TrackBarQueenMoveLimit_ValueChanged(object sender, EventArgs e)
        {
            int limitValue = ((TrackBar)sender).Value;
            LabelQueenMoveLimit.Text = limitValue.ToString();
            GameMasterSingleton.Instance.QueenMoveLimit = limitValue;
            GameMasterSingleton.Instance.Board.GenerateLegalMoves();
            this.selectSquareAndItsLegalMoves(m_SelectedSquare);
        }

        public void SetActiveState()
        {
            isSelectionEnabled = true;
        }

        public void SetInActiveState()
        {
            isSelectionEnabled = false;
        }

        public void GameHasEnded()
        {
            OnGameEnd(GameMasterSingleton.Instance.Board.Turn);
        }

        public void Reset()
        {
            SetActiveState();
            
            m_Markings?.Hide();
            m_MarkingsHistory?.StopViewingHistory();
            m_MarkingsHistory = new MarkingsHistoryManager();
            m_Markings = new FormMarkingsNode(m_PositionToButtonDict);
            m_CurrentlyViewingPositionNumber = 0;
            panelWhiteCapture.ResetPanel();
            panelBlackCapture.ResetPanel();
            loadPieceImages();
        }
    }
}
