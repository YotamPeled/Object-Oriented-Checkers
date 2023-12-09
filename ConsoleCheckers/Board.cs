using System;
using System.Collections.Generic;
using System.Drawing.Design;
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
        private List<IMove> m_LegalMoves = new List<IMove>();
        public List<IMove> LegalMoves
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
        public event Action<IMove> MadeMove;
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
            newBoard();
            GenerateLegalMoves();
        }

        public void newBoard()
        {
            m_BitBoards = new uint[4];
            m_Board = new ePiece[8, 8];
            PositionInitializer.gameEndTest(this);
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

        public bool MakeMove(int i_Start, int i_Target)
        {
            // check the legal move's list for move's existance
            bool isMoveLegal = false;
            if (PieceMethods.CheckValidInt(i_Start) && PieceMethods.CheckValidInt(i_Target))
            {
                foreach (IMove move in LegalMoves)
                {
                    if(move.GetIntStartSquare() == i_Start && move.GetIntTargetSquare() == i_Target)
                    {
                        isMoveLegal = true;
                        updateMove(i_Start, i_Target, move);
                        break;
                    }
                }
            }

            return isMoveLegal;
        }

        private void updateMove(int i_From, int i_To, IMove i_PlayedMove)
        {
            bool swapTurn = true;
            int iFrom, jFrom, iTo, jTo, iCapture, jCapture;
            PieceMethods.IntToCoordinate(i_From, out iFrom, out jFrom);
            PieceMethods.IntToCoordinate(i_To, out iTo, out jTo);
            if (i_PlayedMove.IsCapture())
            {
                PieceMethods.IntToCoordinate(i_PlayedMove.GetIntCaptureSquare(), out iCapture, out jCapture);
                updateBitBoard(i_PlayedMove, this[iFrom, jFrom], this[iCapture, jCapture]);
                this[iCapture, jCapture] = ePiece.None;
            }
            else
            {
                updateBitBoard(i_PlayedMove, this[iFrom, jFrom]);
            }

            this[iTo, jTo] = this[iFrom, jFrom];
            this[iFrom, jFrom] = ePiece.None;
            PromotionCheck(iTo, jTo, i_PlayedMove);
            MadeMove?.Invoke(i_PlayedMove);
            if (gameContinueCheck())
            {
                // check if more captures are available, and if there are don't swap turn
                if (i_PlayedMove.IsCapture())
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

        private void PromotionCheck(int iTarget, int jTarget, IMove i_PlayedMove)
        {
            ePiece movedPiece = this[iTarget, jTarget];
            if (movedPiece != ePiece.qWhite && movedPiece != ePiece.qBlack && i_PlayedMove.IsPromotion())
            {
                if (movedPiece == ePiece.sWhite)
                {
                    promoteWhite(i_PlayedMove);
                    this[iTarget, jTarget] = ePiece.qWhite;
                }
                else if(movedPiece == ePiece.sBlack)
                {
                    promoteBlack(i_PlayedMove);
                    this[iTarget, jTarget] = ePiece.qBlack;
                }
            }
        }

        private void promoteBlack(IMove i_PlayedMove)
        {
            // remove the solider piece 
            BitBoards[(int)ePiece.sBlack - 1] ^= i_PlayedMove.GetTargetSquare();
            // add queen piece
            BitBoards[(int)ePiece.qBlack - 1] |= i_PlayedMove.GetTargetSquare();
        }

        private void promoteWhite(IMove i_PlayedMove)
        {
            // remove the solider piece 
            BitBoards[(int)ePiece.sWhite - 1] ^= i_PlayedMove.GetTargetSquare();
            // add queen piece
            BitBoards[(int)ePiece.qWhite - 1] |= i_PlayedMove.GetTargetSquare();
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
        private void updateBitBoard(IMove i_Move, ePiece i_PieceMakingMove, ePiece i_Captured = ePiece.None)
        {
            // xor with the move made, meaning, remove origin location and add destination location to BitBoard
            m_BitBoards[(int)i_PieceMakingMove - 1] ^= i_Move.GetStartSquare() | i_Move.GetTargetSquare();
            if (i_Move.IsCapture())
            {
                m_BitBoards[(int)i_Captured - 1] ^= i_Move.GetCaptureSquare();
            }
        }

        private bool checkForAvailableCaptures(IMove i_Move)
        {
            bool availableCapture = false;
            // generate legal moves for new position after a move
            GenerateLegalMoves();
            foreach (IMove move in m_LegalMoves)
            {
                // if the piece that just move can capture another piece
                if (move.IsCapture() && move.GetStartSquare() == i_Move.GetTargetSquare())
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
            foreach(IMove move in LegalMoves)
            {
                if (PieceMethods.UIntToInt(move.GetStartSquare()) == i_Square)
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
            noLegalMovesCheck();
        }

        private void noLegalMovesCheck()
        {
            // no legal moves available
            if (m_LegalMoves.Count == 0)
            {
                // black has no legal moves and thus white is the winner
                bool isWhiteWinning = m_ColorTurn == eColor.Black;
                OnGameEnd(isWhiteWinning);
            }
        }
    }
}
