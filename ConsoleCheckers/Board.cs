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
        public event Action<Move> MadeMove;
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
            PositionInitializer.randomQueen(this);
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
            // check the legal move's list for move's existance
            bool isMoveLegal = false;
            if (PieceMethods.CheckValidInt(i_From) && PieceMethods.CheckValidInt(i_To))
            {
                foreach (Move move in LegalMoves)
                {
                    if(PieceMethods.UIntToInt(move.Origin) == i_From && PieceMethods.UIntToInt(move.Destination) == i_To)
                    {
                        isMoveLegal = true;
                        updateMove(i_From, i_To, move);
                        MadeMove.Invoke(move);
                        break;
                    }
                }
            }

            return isMoveLegal;
        }

        private void updateMove(int i_From, int i_To, Move i_PlayedMove)
        {
            bool swapTurn = true;
            int iFrom, jFrom, iTo, jTo, iCapture, jCapture;
            PieceMethods.IntToCoordinate(i_From, out iFrom, out jFrom);
            PieceMethods.IntToCoordinate(i_To, out iTo, out jTo);
            if (i_PlayedMove.IsCapture)
            {
                PieceMethods.IntToCoordinate(PieceMethods.UIntToInt(i_PlayedMove.Capture), out iCapture, out jCapture);
                updateBitBoard(i_PlayedMove, this[iFrom, jFrom], this[iCapture, jCapture]);
                this[iCapture, jCapture] = ePiece.None;
            }
            else
            {
                updateBitBoard(i_PlayedMove, this[iFrom, jFrom]);
            }
            // check if more captures are available, and if there are don't swap turn
            if (i_PlayedMove.IsCapture)
            {
                GenerateLegalMoves();
                foreach(Move move in m_LegalMoves)
                {
                    if (move.IsCapture && move.Origin == i_PlayedMove.Destination)
                    {
                        swapTurn = false;
                        break;
                    }
                }
            }

            if (swapTurn)
            {
                // changeTurn method also generates legal moves for next turn
                changeTurn();
            }

            this[iTo, jTo] = this[iFrom, jFrom];
            this[iFrom, jFrom] = ePiece.None;
        }

        private void updateBitBoard(Move i_Move, ePiece i_PieceMakingMove, ePiece i_Captured = ePiece.None)
        {
            // xor with the move made, meaning, remove origin location and add destination location to BitBoard
            m_BitBoards[(int)i_PieceMakingMove - 1] ^= i_Move.Origin | i_Move.Destination;
            if (i_Move.IsCapture)
            {
                m_BitBoards[(int)i_Captured - 1] ^= i_Move.Capture;
            }
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

        private void changeTurn()
        {
            if (m_ColorTurn == eColor.White)
            {
                m_ColorTurn = eColor.Black;
            }
            else
            {
                m_ColorTurn = eColor.White;
            }

            GenerateLegalMoves();
        }
    }
}
