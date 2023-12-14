using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    public static class PieceMethods
    {
        private static List<IMove> previousGeneration;

        public static void ResetPreviousGeneration()
        {
            previousGeneration.Clear();
        }

        public static List<IMove> GiveLegalMoves(uint[] i_BitBoards, eColor i_ColorToMove, bool i_IsDoubleCapture = false)
        {
            List<IMove> moveList = new List<IMove>();
            if (previousGeneration != null)
            {
                giveDoubleCapturesAfterCapture(i_BitBoards, moveList);
            }
            
            if (moveList.Count == 0)
            {
                moveList = generateMoves(i_BitBoards, i_ColorToMove, i_IsDoubleCapture);
            }

            previousGeneration = moveList;
            return moveList;
        }

        private static void giveDoubleCapturesAfterCapture(uint[] i_BitBoards, List<IMove> moveList)
        {
            uint allPieces = i_BitBoards[0] | i_BitBoards[1] | i_BitBoards[2] | i_BitBoards[3];
            List<IMove> doubleCaptureMoves = previousGeneration.Where(IMove => IMove.IsDoubleCapture()).ToList();
            foreach (IMove move in doubleCaptureMoves)
            {
                // find the single move where double capture occured
                if ((move.GetTargetSquare() & allPieces) != 0)
                {
                    moveList.AddRange(move.DoubleCapturesList);
                    break;
                }
            }         
        }

        private static List<IMove> generateMoves(uint[] i_BitBoards, eColor i_Color, bool i_IsDoubleCapture)
        {
            List<IMove> movesList = new List<IMove>();
            bool isCaptureTurn = false; // Force Captures, if capture found list is cleared and isCaptureTurn becomes true
            Func<uint, uint> moveLeft, moveRight, captureLeft, captureRight;
            uint friendlyPieces, opposingPieces, soliders;
            allocateBitBoards(i_Color, i_BitBoards, out friendlyPieces, out opposingPieces, out soliders, out _);
            ePiece movingSolider = getSoliderPiece(i_Color);
            foreach (uint soliderStartSquare in BitUtils.GetSetBits(soliders))
            {
                int piecePosition = BitUtils.FindBitPosition(soliderStartSquare) % 8;
                ShiftingFunctionsFactory.GetShiftingFuncs(piecePosition, i_Color, out moveLeft, out moveRight,
                    out captureLeft, out captureRight);
                // left capture check
                if (captureLeft != null && captureCheck(soliderStartSquare, opposingPieces, friendlyPieces, moveLeft, captureLeft))
                {
                    if (!isCaptureTurn)
                    {
                        isCaptureTurn = doCaptureSeenDuringTurn(movesList);
                    }

                    IMove moveToAdd = makeMoveAfterCapture(i_Color, i_BitBoards, soliderStartSquare, moveLeft(soliderStartSquare),
                                                           captureLeft(soliderStartSquare), movingSolider, i_Color);
                    movesList.Add(moveToAdd);
                }
                // right capture check
                if (captureRight != null && captureCheck(soliderStartSquare, opposingPieces, friendlyPieces, moveRight, captureRight))
                {
                    if (!isCaptureTurn)
                    {
                        isCaptureTurn = doCaptureSeenDuringTurn(movesList);
                    }

                    IMove moveToAdd = makeMoveAfterCapture(i_Color, i_BitBoards, soliderStartSquare, moveRight(soliderStartSquare),
                                                           captureRight(soliderStartSquare), movingSolider, i_Color);
                    movesList.Add(moveToAdd);
                }

                // normal move left
                if (!i_IsDoubleCapture && !isCaptureTurn && moveLeft != null && normalMoveCheck(soliderStartSquare, friendlyPieces | opposingPieces, moveLeft))
                {
                    movesList.Add(PackMove(soliderStartSquare, moveLeft(soliderStartSquare), movingSolider));
                }
                // normal move right
                if (!i_IsDoubleCapture && !isCaptureTurn && moveRight != null && normalMoveCheck(soliderStartSquare, friendlyPieces | opposingPieces, moveRight))
                {
                    movesList.Add(PackMove(soliderStartSquare, moveRight(soliderStartSquare), movingSolider));
                }
            }

            queenMoves(i_BitBoards, isCaptureTurn, movesList, i_Color);
            return movesList;
        }

        private static void queenMoves(uint[] i_BitBoards, bool i_IsCaptureTurn, List<IMove> i_Moves, eColor i_ColorTurn)
        {
            uint queens, friendly, opposing;
            allocateBitBoards(i_ColorTurn, i_BitBoards, out friendly, out opposing, out _, out queens);
            ePiece movingQueen = getQueenPiece(i_ColorTurn);
            foreach (uint queenStartSquare in BitUtils.GetSetBits(queens))
            {
                foreach (IEnumerable<uint> queenMovesIterator in ShiftingFunctionsFactory.GetQueenIterators(queenStartSquare, opposing))
                {
                    uint captureablePiece = 0;
                    foreach (uint queenTargetSquare in queenMovesIterator)
                    {
                        if (queenTargetSquare == 0)
                        {
                            break;
                        }

                        if (((friendly | opposing) & queenTargetSquare) == 0) // empty square check
                        {
                            if (captureablePiece != 0)
                            {
                                if (!i_IsCaptureTurn) // clear all none capture moves
                                {
                                    i_IsCaptureTurn = doCaptureSeenDuringTurn(i_Moves);
                                }

                                IMove moveToAdd = makeMoveAfterCapture(i_ColorTurn, i_BitBoards, queenStartSquare, captureablePiece, queenTargetSquare, movingQueen, i_ColorTurn);
                                i_Moves.Add(moveToAdd);
                                break;
                            }
                            else if (!i_IsCaptureTurn)
                            {
                                i_Moves.Add(PackMove(queenStartSquare, queenTargetSquare, movingQueen));
                            }
                        }
                        else if ((opposing & queenTargetSquare) != 0) //enemy piece seen
                        {
                            if (captureablePiece != 0) // 2 black pieces in a row seen
                            {
                                break;
                            }

                            captureablePiece = queenTargetSquare;
                        }
                        else //reached a friendly piece
                        {
                            break;
                        }
                    }
                }
            }
        }

        private static bool normalMoveCheck(uint i_Piece, uint i_Board, Func<uint, uint> i_ShiftingFunc)
        {
            bool isLegalMove = false;
            bool outOfBounds = i_ShiftingFunc(i_Piece) == 0;
            if (!outOfBounds && !isPieceInTheWay(i_Piece, i_Board, i_ShiftingFunc))
            {
                isLegalMove = true;
            }

            return isLegalMove;
        }

        private static bool captureCheck(uint i_Piece, uint i_OpposingPieces, uint i_SamePieces, Func<uint, uint> i_ShiftingFunc, 
            Func<uint, uint> i_2ndShiftingFunc)
        {
            bool isLegalMove = false;
            bool outOfBounds = i_ShiftingFunc(i_Piece) == 0 || i_2ndShiftingFunc(i_Piece) == 0;
            if (!outOfBounds && isPieceInTheWay(i_Piece, i_OpposingPieces, i_ShiftingFunc) &&
                !isPieceInTheWay(i_Piece, i_OpposingPieces | i_SamePieces, i_2ndShiftingFunc))
            {
                isLegalMove = true;
            }

            return isLegalMove;
        }

        private static IMove makeMoveAfterCapture(eColor i_Color, uint[] i_BitBoards, uint startSquare, uint capturedSquare, uint targetSquare, ePiece movingPiece, eColor movingPieceColor)
        {
            ePiece capturedPiece = getCapturedPieceType(i_Color, i_BitBoards, capturedSquare); 
            IMove moveToAdd = PackMove(startSquare, targetSquare, movingPiece, capturedSquare, capturedPiece);
            MakeMove(moveToAdd, i_BitBoards);
            doubleCaptureCheck(moveToAdd, targetSquare, i_BitBoards, movingPieceColor);
            unMakeMove(moveToAdd, i_BitBoards);

            return moveToAdd;
        }

        private static void doubleCaptureCheck(IMove move, uint pieceLocation, uint[] i_BitBoards, eColor i_MovingPieceColor)
        {
            // checks front and back captures
            ePiece movingPiece = getPieceType(pieceLocation, i_BitBoards);
            uint friendly, opposing;
            allocateBitBoards(i_MovingPieceColor, i_BitBoards, out friendly, out opposing, out _, out _);
            Func<uint, uint> moveLeft, moveRight, captureLeft, captureRight;
            int bitPosition = BitUtils.FindBitPosition(pieceLocation) % 8;
            ShiftingFunctionsFactory.GetShiftingFuncs(bitPosition, eColor.White, out moveLeft, out moveRight, out captureLeft, out captureRight);
            if (captureLeft != null && captureCheck(pieceLocation, opposing, friendly, moveLeft, captureLeft))
            {
                IMove moveToAdd = makeMoveAfterCapture(i_MovingPieceColor, i_BitBoards, pieceLocation, moveLeft(pieceLocation), captureLeft(pieceLocation), movingPiece, i_MovingPieceColor);
                move.addDoubleCapture(moveToAdd);
            }

            if (captureRight != null && captureCheck(pieceLocation, opposing, friendly, moveRight, captureRight))
            {
                IMove moveToAdd = makeMoveAfterCapture(i_MovingPieceColor, i_BitBoards, pieceLocation, moveRight(pieceLocation), captureRight(pieceLocation), movingPiece, i_MovingPieceColor);
                move.addDoubleCapture(moveToAdd);
            }

            ShiftingFunctionsFactory.GetShiftingFuncs(bitPosition, eColor.Black, out moveLeft, out moveRight, out captureLeft, out captureRight);
            if (captureLeft != null && captureCheck(pieceLocation, opposing, friendly, moveLeft, captureLeft))
            {
                IMove moveToAdd = makeMoveAfterCapture(i_MovingPieceColor, i_BitBoards, pieceLocation, moveLeft(pieceLocation), captureLeft(pieceLocation), movingPiece, i_MovingPieceColor);
                move.addDoubleCapture(moveToAdd);
            }

            if (captureRight != null && captureCheck(pieceLocation, opposing, friendly, moveRight, captureRight))
            {
                IMove moveToAdd = makeMoveAfterCapture(i_MovingPieceColor, i_BitBoards, pieceLocation, moveRight(pieceLocation), captureRight(pieceLocation), movingPiece, i_MovingPieceColor);
                move.addDoubleCapture(moveToAdd);
            }
        }

        private static ePiece getPieceType(uint pieceLocation, uint[] i_BitBoards)
        {
            ePiece movingPiece;
            if ((pieceLocation & i_BitBoards[0]) != 0)
            {
                movingPiece = ePiece.sWhite;
            }
            else if ((pieceLocation & i_BitBoards[1]) != 0)
            {
                movingPiece = ePiece.sBlack;
            }
            else if ((pieceLocation & i_BitBoards[2]) != 0)
            {
                movingPiece = ePiece.qWhite;
            }
            else
            {
                movingPiece = ePiece.qBlack;
            }

            return movingPiece;
        }

        private static bool isPieceInTheWay(uint piece, uint allColorPieces, Func<uint, uint> i_Shift)
        {
            return (i_Shift(piece) & allColorPieces) != 0; 
        }

        private static ePiece getSoliderPiece(eColor i_Color)
        {
            if (i_Color == eColor.White)
            {
                return ePiece.sWhite;
            }
            else
            {
                return ePiece.sBlack;
            }
        }

        private static ePiece getQueenPiece(eColor i_Color)
        {
            if (i_Color == eColor.White)
            {
                return ePiece.qWhite;
            }
            else
            {
                return ePiece.qBlack;
            }
        }

        private static bool doCaptureSeenDuringTurn(List<IMove> movesList)
        {
            movesList.Clear();
            return true;
        }

        public static int CoordinateToInt(int i, int j)
        {
            return i * 10 + j;
        }

        public static void IntToCoordinate(int coordinate, out int i, out int j)
        {
            i = coordinate / 10;
            j = coordinate % 10;
        }

        public static int UIntToInt(uint i_Piece)
        {
            int locationNumbering = BitUtils.FindBitPosition(i_Piece);
            if (locationNumbering == -1) // no bit found
            {
                return 0;
            }

            int offset;
            switch (locationNumbering / 4)
            {
                case 0: offset =  0; break;
                case 1: offset = 1; break;
                case 2: offset = 4; break;
                case 3: offset = 5; break;
                case 4: offset = 8; break;
                case 5: offset = 9; break;
                case 6: offset = 12; break;
                case 7: offset = 13; break;
                default: offset = 0; break; // Out of range, can't happen as we're using 32 bit numbers
            }

            return 76 - locationNumbering * 2 - offset;
        }

        internal static bool CheckValid(int i, int j)
        {
            return i >= 0 && i <= 7 && j >= 0 && j <= 7;
        }

        internal static bool CheckValidInt(int i_Square)
        {
            int i, j;
            IntToCoordinate(i_Square, out i, out j);
            return CheckValid(i, j);
        }

        private static void allocateBitBoards(eColor i_Color, uint[] i_BitBoards, out uint o_Friendly,
            out uint o_Opposing, out uint o_Soliders, out uint o_Queens)
        {
            if (i_Color == eColor.White)
            {
                o_Friendly = i_BitBoards[0] | i_BitBoards[2];
                o_Opposing = i_BitBoards[1] | i_BitBoards[3];
                o_Soliders = i_BitBoards[0];
                o_Queens = i_BitBoards[2];
            }
            else
            {
                o_Friendly = i_BitBoards[1] | i_BitBoards[3];
                o_Opposing = i_BitBoards[0] | i_BitBoards[2];
                o_Soliders = i_BitBoards[1];
                o_Queens = i_BitBoards[3];
            }
        }

        private static IMove PackMove(uint i_StartSquare, uint i_TargetSquare, ePiece i_MovingPiece, uint i_CaptureSquare = 0b00000, ePiece i_CapturedPiece = 0b00)
        {
            int PieceValue = (int)i_MovingPiece - 1;
            int CapturedPieceValue = i_CapturedPiece == ePiece.None ? 0 : (int)i_CapturedPiece - 1;
            return new MoveAdapter(new CheckersMove(i_StartSquare, i_TargetSquare, PieceValue, i_CaptureSquare, CapturedPieceValue));
        }

        public static void MakeMove(IMove i_MoveToMake, uint[] i_BitBoards)
        {
            int movingPieceValue = i_MoveToMake.GetMovingPieceValue();
            i_BitBoards[movingPieceValue] ^= i_MoveToMake.GetStartSquare() | i_MoveToMake.GetTargetSquare();
            if (i_MoveToMake.IsPromotion())
            {
                // 2 = queen offset
                i_BitBoards[movingPieceValue + 2] ^= i_MoveToMake.GetTargetSquare();
                i_BitBoards[movingPieceValue] ^= i_MoveToMake.GetTargetSquare();
            }

            if (i_MoveToMake.IsCapture())
            {
                int capturedPieceValue = i_MoveToMake.GetCapturedPieceValue();
                i_BitBoards[capturedPieceValue] ^= i_MoveToMake.GetCaptureSquare();
            }
        }

        public static void unMakeMove(IMove i_MoveToMake, uint[] i_BitBoards)
        {
            MakeMove(i_MoveToMake, i_BitBoards);
        }

        private static ePiece getCapturedPieceType(eColor i_ColorTurn, uint[] i_BitBoards, uint captureablePiece)
        {
            ePiece capturedPieceType;
            if (i_ColorTurn == eColor.White)
            {
                capturedPieceType = (i_BitBoards[(int)ePiece.sBlack - 1] & captureablePiece) != 0 ? ePiece.sBlack : ePiece.qBlack;
            }
            else
            {
                capturedPieceType = (i_BitBoards[(int)ePiece.sWhite - 1] & captureablePiece) != 0 ? ePiece.sWhite : ePiece.qWhite;
            }

            return capturedPieceType;
        }

        public static eColor SwapTurn(eColor i_Color)
        {
            return i_Color == eColor.White ? eColor.Black : eColor.White;
        }
    }
}
