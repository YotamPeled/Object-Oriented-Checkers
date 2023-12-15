using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    public abstract class MarkingsNode
    {
        protected List<uint> m_MarkedLegalMoveSquares = new List<uint>();
        protected List<uint> m_MarkedMovedPieceSquares = new List<uint>();
        protected List<uint> m_MarkedCapturedSquares = new List<uint>();
        protected event Action LegalMoveSquaresAdded;

        public void AddMovedPieceSquares(List<uint> i_Squares)
        {
            m_MarkedMovedPieceSquares.AddRange(i_Squares);
        }

        public void AddCapturedPieceSquare(uint i_Square)
        {
            m_MarkedCapturedSquares.Add(i_Square);
        }

        public void AddHighlightedLegalMovesAndReplacePrevious(List<uint> i_Squares)
        {
            ClearHighlightedLegalMoves();
            m_MarkedLegalMoveSquares.AddRange(i_Squares);
            LegalMoveSquaresAdded?.Invoke();
        }

        public void ClearHighlightedLegalMoves()
        {
            Hide();
            m_MarkedLegalMoveSquares.Clear();
        }

        public abstract void Show();

        public abstract void Hide();
    }
}
