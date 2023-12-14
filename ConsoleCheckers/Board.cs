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
        public List<uint[]> PositionsHistory { get; } = new List<uint[]>();
        private bool m_OngoingGame = true;
        public bool GameOngoing { get { return m_OngoingGame; } }
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
        public event Action TurnChanged;
        public event Action ReadyForNextMove;

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

        public Board(uint[] i_BitBoards)
        {
            m_BitBoards = i_BitBoards;
            constructBoardFromBitBoards();
        }

        public void newBoard()
        {
            m_BitBoards = new uint[4];
            PositionInitializer.Starting(this);
            constructBoardFromBitBoards();
        }

        public void StartGame()
        {
            if (m_OngoingGame)
            {
                ReadyForNextMove?.Invoke();
            }
        }

        public void GenerateLegalMoves(bool isDoubleCapture = false)
        {  
            m_LegalMoves = PieceMethods.GiveLegalMoves(BitBoards, m_ColorTurn, isDoubleCapture);
        }

        public bool MakeMove(IMove i_Move)
        {
            // check the legal move's list for move's existance
            bool isMoveLegal = false;
            
            foreach (IMove move in LegalMoves)
            {
                if (i_Move.GetMoveValue() == move.GetMoveValue())
                {
                    isMoveLegal = true;
                    updateMove(move);
                    break;
                }
            }

            if (GameOngoing)
            {
                ReadyForNextMove?.Invoke();
            }
            return isMoveLegal;
        }


        private void updateMove(IMove move)
        {
            PieceMethods.MakeMove(move, m_BitBoards);
            constructBoardFromBitBoards();
            onMadeMove(move);          
        }
    

        protected virtual void onMadeMove(IMove i_Move)
        {
            MadeMove?.Invoke(i_Move);
            if (i_Move.IsDoubleCapture())
            {
                // don't swap turn and generate legal moves
                bool isDoubleCapture = true;
                GenerateLegalMoves(isDoubleCapture);
            }
            else if (gameContinueCheck())
            {
                changeTurn();
            }
        }
        private bool gameContinueCheck()
        {
            bool isWhiteAlive = (BitBoards[(int)ePiece.sWhite - 1] | BitBoards[(int)ePiece.qWhite - 1]) != 0;
            bool isBlackAlive = (BitBoards[(int)ePiece.sBlack - 1] | BitBoards[(int)ePiece.qBlack - 1]) != 0;
            m_OngoingGame = isBlackAlive && isWhiteAlive;
            if (!m_OngoingGame)
            {
                OnGameEnd(isWhiteAlive);
            }

            return m_OngoingGame;
        }

        private void OnGameEnd(bool i_WhiteStatus)
        {
            eColor winningColor = eColor.Black;
            if (i_WhiteStatus)
            {
                winningColor = eColor.White;
            }

            GameEnded?.Invoke(winningColor);
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
            if (!noLegalMovesCheck())
            {
                TurnChanged?.Invoke();
            }
        }

        private bool noLegalMovesCheck()
        {
            // no legal moves available
            if (m_LegalMoves.Count == 0)
            {
                // black has no legal moves and thus white is the winner
                bool isWhiteWinning = m_ColorTurn == eColor.Black;
                OnGameEnd(isWhiteWinning);
                return true;
            }

            return false;
        }

        private void constructBoardFromBitBoards()
        {
            m_Board = new ePiece[8, 8];
            foreach (uint whiteSolider in BitUtils.GetSetBits(m_BitBoards[(int)ePiece.sWhite - 1]))
            {
                setBitPositionOnBoard(ePiece.sWhite, whiteSolider);
            }

            foreach(uint blackSolider in BitUtils.GetSetBits(m_BitBoards[(int)ePiece.sBlack - 1]))
            {
                setBitPositionOnBoard(ePiece.sBlack, blackSolider);
            }

            foreach(uint whiteQueen in BitUtils.GetSetBits(m_BitBoards[(int)ePiece.qWhite - 1]))
            {
                setBitPositionOnBoard(ePiece.qWhite, whiteQueen);
            }

            foreach (uint blackQueen in BitUtils.GetSetBits(m_BitBoards[(int)ePiece.qBlack - 1]))
            {
                setBitPositionOnBoard(ePiece.qBlack, blackQueen);
            }

            PositionsHistory.Add(m_BitBoards.ToArray());
        }

        private void setBitPositionOnBoard(ePiece i_PieceToSet, uint pieceLocation)
        {
            int i, j;
            int location = PieceMethods.UIntToInt(pieceLocation);
            PieceMethods.IntToCoordinate(location, out i, out j);
            m_Board[i, j] = i_PieceToSet;
        }
    }
}
