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
        private bool m_Ongoing = true;
        public bool GameOngoing { get { return m_Ongoing; } }
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
        public event Action<eColor> GameEnded;
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

        public int whiteScore 
        { 
            get 
            {
                return 12 - BitUtils.GetSetBitsAmount(m_BitBoards[(int)ePiece.sBlack - 1] | m_BitBoards[(int)ePiece.qBlack - 1]);
            } 
        }

        public int blackScore 
        { 
            get
            {
                return 12 - BitUtils.GetSetBitsAmount(m_BitBoards[(int)ePiece.sWhite - 1] | m_BitBoards[(int)ePiece.qWhite - 1]);
            }
        }

        public Board()
        {
            m_BitBoards = new uint[4];
            m_Board = new ePiece[8,8];
            PositionInitializer.gameEndTest(this);
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

            this[iTo, jTo] = this[iFrom, jFrom];
            this[iFrom, jFrom] = ePiece.None;
            MadeMove.Invoke(i_PlayedMove);
            if (gameContinueCheck())
            {
                // check if more captures are available, and if there are don't swap turn
                if (i_PlayedMove.IsCapture)
                {
                    swapTurn &= !checkForAvailableCaptures(i_PlayedMove);
                }

                if (swapTurn)
                {
                    // changeTurn method also generates legal moves for next turn
                    changeTurn();
                }
            }
        }

        private bool gameContinueCheck()
        {
            bool isWhiteAlive = (BitBoards[(int)ePiece.sWhite - 1] | BitBoards[(int)ePiece.qWhite - 1]) != 0;
            bool isBlackAlive = (BitBoards[(int)ePiece.sBlack - 1] | BitBoards[(int)ePiece.qBlack - 1]) != 0;
            bool isOngoingGame = isBlackAlive && isWhiteAlive;
            if (!isOngoingGame)
            {
                OnGameEnd(isWhiteAlive);
            }

            return isOngoingGame;
        }

        private void OnGameEnd(bool i_WhiteStatus)
        {
            eColor winningColor = eColor.Black;
            if (i_WhiteStatus)
            {
                winningColor = eColor.White;
            }

            m_Ongoing = false;
            GameEnded?.Invoke(winningColor);
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

        private bool checkForAvailableCaptures(Move i_Move)
        {
            bool availableCapture = false;
            GenerateLegalMoves();
            foreach (Move move in m_LegalMoves)
            {
                if (move.IsCapture && move.Origin == i_Move.Destination)
                {
                    availableCapture = true;
                    break;
                }
            }

            return availableCapture;
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
