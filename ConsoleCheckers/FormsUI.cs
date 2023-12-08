using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                foreach(Move move in m_GameBoard.LegalMoves)
                {
                    if (move.Origin == m_SelectedSquare && move.Destination == clickedSquare.BitPosition)
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
                foreach (Move move in m_GameBoard.LegalMoves)
                {
                    if (move.Origin == i_Selection)
                    {
                        markSquare(move.Destination);
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

        private void OnMadeMove(Move i_Move)
        {
            clearMarkedSquares();
            m_PositionToButtonDict[i_Move.Origin].Draw();
            m_PositionToButtonDict[i_Move.Destination].Draw();
            if (i_Move.IsCapture)
            {
                m_PositionToButtonDict[i_Move.Capture].Draw();
            }
        }
    }
}
