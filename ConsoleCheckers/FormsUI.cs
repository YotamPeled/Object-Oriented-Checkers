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
        private int m_CurrentlyViewingPositionNumber = 0;
        private bool isSelectionEnabled = true;
        private uint m_SelectedSquare;
        private List<CheckersButton> m_MarkedButtons = new List<CheckersButton>();
        private Dictionary<uint, CheckersButton> m_PositionToButtonDict = new Dictionary<uint, CheckersButton>();
        public event Action<IMove> PlayerMadeMove;
        public FormsUI()
        {
            InitializeComponent();
            fillDict();
            Reset();
        }

        private void fillDict()
        {
            foreach(CheckersButton buttonSquare in panelCheckers.Controls)
            {
                if (buttonSquare.BitPosition != 0)
                {
                    m_PositionToButtonDict[buttonSquare.BitPosition] = buttonSquare;
                }
            }
        }

        private void loadPieceImagesAndAddActions()
        {
            foreach(CheckersButton button in panelCheckers.Controls)
            {
                button.Draw(GameMasterSingleton.Instance.Board);
                button.Click += Button_Click;
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
                onSelect(clickedSquare.BitPosition);
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
                    onSelect(clickedSquare.BitPosition);
                }
            }
        }

        private void onSelect(uint i_Selection)
        {
            clearMarkedSquares();
            m_SelectedSquare = i_Selection;
            select(i_Selection);
        }

        private void select(uint i_Selection)
        {
            // valid black square
            if (i_Selection != 0)
            {
                markSquare(i_Selection);
                foreach (IMove move in GameMasterSingleton.Instance.Board.LegalMoves)
                {
                    if (move.GetStartSquare() == i_Selection)
                    {
                        markSquare(move.GetTargetSquare());
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private void markSquare(uint i_SquareToMark)
        {
            CheckersButton buttonToMark = m_PositionToButtonDict[i_SquareToMark];
            buttonToMark.Select();
            m_MarkedButtons.Add(buttonToMark);
        }

        private void clearMarkedSquares()
        {
            m_SelectedSquare = 0;
            foreach(CheckersButton button in m_MarkedButtons)
            {
                ImageUtils.ClearCircleFromButton(button);
            }

            m_MarkedButtons.Clear();
        }

        private void OnMadeMove(IMove i_Move)
        {
            clearMarkedSquares();
            m_PositionToButtonDict[i_Move.GetStartSquare()].Draw(GameMasterSingleton.Instance.Board);
            m_PositionToButtonDict[i_Move.GetTargetSquare()].Draw(GameMasterSingleton.Instance.Board);
            if (i_Move.IsCapture())
            {
                m_PositionToButtonDict[i_Move.GetCaptureSquare()].Draw(GameMasterSingleton.Instance.Board);
                updateTakenPiecesPanel();
            }

            m_CurrentlyViewingPositionNumber++;
        }

        private void viewHistoryPosition(int i_PositionNumber)
        {
            if (i_PositionNumber + 1 != GameMasterSingleton.Instance.Board.PositionsHistory.Count)
            {
                GameMasterSingleton.Instance.Board.MadeMove -= OnMadeMove;
                clearMarkedSquares();
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
            this.onSelect(m_SelectedSquare);
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
            m_CurrentlyViewingPositionNumber = 0;
            clearMarkedSquares();
            panelWhiteCapture.ResetPanel();
            panelBlackCapture.ResetPanel();
            loadPieceImagesAndAddActions();
        }
    }
}
