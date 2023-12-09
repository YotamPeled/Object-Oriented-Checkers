using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    public partial class FormsUI
    {
        private uint m_SelectedSquare;
        private List<CheckersButton> m_MarkedButtons = new List<CheckersButton>();
        private Board m_GameBoard;
        private Dictionary<uint, CheckersButton> m_PositionToButtonDict = new Dictionary<uint, CheckersButton>();
        public FormsUI()
        {
            m_GameBoard = GameMasterSingleton.Instance.Board;
            m_GameBoard.MadeMove += OnMadeMove;
            m_GameBoard.GameEnded += OnGameEnd;
            InitializeComponent();
            fillDict();
            loadPieceImagesAndAddActions();
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
                button.Draw();
                button.Click += Button_Click;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            CheckersButton clickedSquare = sender as CheckersButton;
            // white square clicked
            if (clickedSquare.BitPosition == 0 || m_SelectedSquare == 0)
            {
                select(clickedSquare.BitPosition);
            }
            else
            {
                bool isLegalMove = false;
                foreach(IMove move in m_GameBoard.LegalMoves)
                {
                    if (move.GetStartSquare() == m_SelectedSquare && move.GetTargetSquare() == clickedSquare.BitPosition)
                    {
                        isLegalMove = true;
                        m_GameBoard.MakeMove(
                            PieceMethods.UIntToInt(m_SelectedSquare), PieceMethods.UIntToInt(clickedSquare.BitPosition)
                            );
                        break;
                    }
                }

                if (!isLegalMove)
                {
                    select(clickedSquare.BitPosition);
                }
            }
        }

        private void select(uint i_Selection)
        {
            clearMarkedSquares();
            m_SelectedSquare = i_Selection;
            // valid black square
            if (i_Selection != 0)
            {
                markSquare(i_Selection);
                foreach (IMove move in m_GameBoard.LegalMoves)
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
            m_PositionToButtonDict[i_Move.GetStartSquare()].Draw();
            m_PositionToButtonDict[i_Move.GetTargetSquare()].Draw();
            if (i_Move.IsCapture())
            {
                m_PositionToButtonDict[i_Move.GetCaptureSquare()].Draw();
                updateTakenPiecesPanel();
            }
        }

        private void updateTakenPiecesPanel()
        {
            if (m_GameBoard.Turn == eColor.White)
            {
                panelWhiteCapture.AddImage();
            }
            else if(m_GameBoard.Turn == eColor.Black)
            {
                panelBlackCapture.AddImage();
            }
        }

        private void OnGameEnd(eColor i_Color)
        {
            MessageBox.Show($"game has ended, the winner is {i_Color}");
        }

        private void TrackBarQueenMoveLimit_ValueChanged(object sender, EventArgs e)
        {
            int limitValue = ((TrackBar)sender).Value;
            LabelQueenMoveLimit.Text = limitValue.ToString();
            GameMasterSingleton.Instance.QueenMoveLimit = limitValue;
            m_GameBoard.GenerateLegalMoves();
            this.select(m_SelectedSquare);
        }
    }
}
