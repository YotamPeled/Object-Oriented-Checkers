using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public class Board
    {
        private ePieces[,] m_Board;
        public ePieces this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < m_Board.GetLength(0) && j >= 0 && j < m_Board.GetLength(1))
                {
                    return m_Board[i, j];
                }
                else
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
            }
            set
            {
                if (i >= 0 && i < m_Board.GetLength(0) && j >= 0 && j < m_Board.GetLength(1))
                {
                    m_Board[i, j] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
            }
        }
        public Board()
        {
            m_Board = new ePieces[8,8];
            PositionInitializer.Starting(m_Board);
        }

        private LinkedList<int> generateLegalMoves(int i, int j)
        {
            ePieces movingPiece;
            try
            {
                movingPiece = this[i, j];
            }
            catch
            {
                return new LinkedList<int>();
            }

            if (movingPiece == ePieces.None)
            {
                return new LinkedList<int>();
            }
            else
            {
                LinkedList<int> legalMoves = PieceMethods.GenerateLegalMoves(this, movingPiece, i * 10 + j);
                return legalMoves;
            }
        }

        public bool HighLight(int i, int j)
        {
            LinkedList<int> legalMoves = generateLegalMoves(i, j);

            foreach(int location in legalMoves)
            {
                int row = location / 10;
                int col = location % 10;
                this[row, col] = ePieces.Highlight;
            }
            

            return true;
        }

        public bool MakeMove(int i_From, int i_To)
        {
            bool legalMove = false;
            int iFrom = i_From / 10;
            int jFrom = i_From % 10;
            int iTo = i_To / 10;
            int jTo = i_To % 10;

            LinkedList<int> legalMoves = generateLegalMoves(iFrom, jFrom);
            if (legalMoves.Contains(iTo * 10 + jTo))
            {
                legalMove = true;
            }

            if(legalMove)
            {
                this[iTo, jTo] = this[iFrom, jFrom];
                this[iFrom, jFrom] = ePieces.HighlightFrom;
                clearHighlights();
            }

            return legalMove;
        }

        private void clearHighlights()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j =0; j < 8; j++)
                {
                    if (this[i, j] == ePieces.Highlight)
                    {
                        this[i, j] = ePieces.None;
                    }
                }
            }
        }
    }
}
