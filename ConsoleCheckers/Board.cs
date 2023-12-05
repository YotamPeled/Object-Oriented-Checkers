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
        private List<Move> m_LegalMoves = new List<Move>();
        public List<Move> LegalMoves
        {
            get
            {
                return m_LegalMoves;
            }
        }
        private uint[] m_BitBoards;
        public uint[] BitBoards 
        {
            get
            {
                return m_BitBoards;
            }
        }
        private ePiece[,] m_Board;
        private eColor m_ColorTurn = eColor.White;
        public eColor Turn 
        {
            get
            {
                return m_ColorTurn;
            }
        }
        public event Action<int, int, int, int> MadeMove;
        public ePiece this[int i, int j]
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
            m_BitBoards = new uint[4];
            m_Board = new ePiece[8,8];
            PositionInitializer.Starting(this);
            GenerateLegalMoves();
        }

        private IEnumerable<int> FindCells(Func<ePiece, bool> condition)
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

        public void GenerateLegalMoves()
        {  
            m_LegalMoves = PieceMethods.GiveLegalMoves(BitBoards, m_ColorTurn);
        }

        public bool MakeMove(int i_From, int i_To)
        {
            return false;
        }

        internal bool IsSquareInPlay(int i_Square)
        {
            bool isSquareInPlay = false;
            foreach(Move move in LegalMoves)
            {
                if (PieceMethods.UIntToInt(move.Origin) == i_Square)
                {
                    isSquareInPlay = true;
                    break;
                }
            }

            return isSquareInPlay;
        }

        private void SwapTurn()
        {
            if (m_ColorTurn == eColor.White)
            {
                m_ColorTurn = eColor.Black;
            }
            else
            {
                m_ColorTurn = eColor.White;
            }
        }
    }
}
