using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public class Board
    {
        private ePieces[,] m_Board;
        private eTurn m_Turn = eTurn.White;
        public eTurn Turn 
        {
            get
            {
                return m_Turn;
            }
        }
        public event Action<int, int, int, int> MadeMove;
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

        private IEnumerable<int> FindCells(Func<ePieces, bool> condition)
        {
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    if (condition(this[i, j]))
                    {
                        yield return i * 10 + j;
                    }
                }
            }
        }

        public ICollection<int> GenerateLegalMoves(int i, int j)
        {
            bool legalSquare;
            try
            {  
                legalSquare = this[i, j] != ePieces.None;
            }
            catch
            {
                legalSquare = false;
            }

            if (legalSquare)
            {
                return PieceMethods.GiveLegalMoves(this, this[i, j], i, j);
            }

            return null;
        }

        public bool MakeMove(int i_From, int i_To)
        {
            bool legalMove = false;
            int iFrom = i_From / 10;
            int jFrom = i_From % 10;
            int iTo = i_To / 10;
            int jTo = i_To % 10;

            ICollection<int> legalMoves = GenerateLegalMoves(iFrom, jFrom);
            if (legalMoves.Contains(iTo * 10 + jTo))
            {
                legalMove = true;
            }

            if(legalMove)
            {
                this[iTo, jTo] = this[iFrom, jFrom];
                this[iFrom, jFrom] = ePieces.None;
                MadeMove.Invoke(iFrom, jFrom, iTo, jTo);
                SwapTurn();
            }

            return legalMove;
        }

        internal bool IsSquareInPlay(int intInput)
        {
            bool isSquareInPlay = false;
            int i;
            int j;
            PieceMethods.IntToCoordinate(intInput, out i, out j);

            if (m_Turn == eTurn.White)
            {
                try
                {
                    isSquareInPlay = this[i, j] == ePieces.sWhite || this[i, j] == ePieces.qWhite;
                }
                catch
                {
                    isSquareInPlay = false;
                }
                    
            }
            else if (m_Turn == eTurn.Black)
            {
                try
                {
                    isSquareInPlay = this[i, j] == ePieces.sBlack || this[i, j] == ePieces.qBlack;
                }
                catch
                {
                    isSquareInPlay = false;
                }
            }

            return isSquareInPlay;
        }

        private void SwapTurn()
        {
            if (m_Turn == eTurn.White)
            {
                m_Turn = eTurn.Black;
            }
            else
            {
                m_Turn = eTurn.White;
            }
        }
    }
}
