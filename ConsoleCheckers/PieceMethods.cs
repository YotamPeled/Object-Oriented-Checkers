using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCheckers
{
    public static class PieceMethods
    {
        public static List<Move> GiveLegalMoves(uint[] i_BitBoards, eColor i_ColorToMove)
        {
            List<Move> moveList;          
            moveList = Moves(i_BitBoards, i_ColorToMove);
            
            return moveList;
        }

        private static List<Move> Moves(uint[] i_BitBoards, eColor i_Color)
        {
            List<Move> movesList = new List<Move>();
            Func<uint, uint> moveLeft;
            Func<uint, uint> moveRight;
            Func<uint, uint> captureLeft;
            Func<uint, uint> captureRight;
            bool isCaptureTurn = false; // Force Captures, if capture found list is cleared and isCaptureTurn becomes true
            uint friendlyPieces;
            uint opposingPieces;
            uint soliders;
            uint queens;

            allocateBitBoards(i_Color, i_BitBoards, out friendlyPieces, out opposingPieces, out soliders, out queens);
            foreach (uint piece in BitUtils.GetSetBits(soliders))
            {
                int piecePosition = BitUtils.FindBitPosition(piece) % 8;
                ShiftingFunctionsFactory.GetShiftingFuncs(piecePosition, i_Color, out moveLeft, out moveRight,
                    out captureLeft, out captureRight);
                // left capture check
                if (captureLeft != null)
                {
                    captureCheck(piece, opposingPieces, friendlyPieces, moveLeft,
                    captureLeft, movesList, ref isCaptureTurn);
                }
                // right capture check
                if (captureRight != null)
                {
                    captureCheck(piece, opposingPieces, friendlyPieces, moveRight,
                    captureRight, movesList, ref isCaptureTurn);
                }
                // normal move left
                if (!isCaptureTurn && moveLeft != null)
                {
                    normalMoveCheck(piece, friendlyPieces | opposingPieces, moveLeft, movesList);
                }
                // normal move right
                if (!isCaptureTurn && moveRight != null)
                {
                    normalMoveCheck(piece, friendlyPieces | opposingPieces, moveRight, movesList);
                }
            }

            foreach (uint queen in BitUtils.GetSetBits(queens))
            {
                foreach (IEnumerable<uint> queenMovesIterator in ShiftingFunctionsFactory.GetQueenIterators(queen))
                {
                    uint captureablePiece = 0;
                    foreach (uint piece in queenMovesIterator)
                    {
                        if (piece == 0)
                        {
                            break;
                        }

                        if (((friendlyPieces | opposingPieces) & piece) == 0) // empty square check
                        {
                            if (captureablePiece != 0)
                            {
                                if (!isCaptureTurn) // clear all none capture moves
                                {
                                    movesList.Clear();
                                    isCaptureTurn = true;
                                }

                                movesList.Add(new Move()
                                {
                                    IsCapture = true,
                                    Origin = queen,
                                    Capture = captureablePiece,
                                    Destination = piece
                                });

                                break;
                            }
                            else if (!isCaptureTurn)
                            {
                                movesList.Add(new Move()
                                {
                                    IsCapture = false,
                                    Origin = queen,
                                    Destination = piece
                                });
                            }
                        }
                        else if ((opposingPieces & piece) != 0) //enemy piece seen
                        {
                            if (captureablePiece != 0) // 2 black pieces in a row seen
                            {
                                break;
                            }

                            captureablePiece = piece;
                        }
                        else //reached a friendly piece
                        {
                            break;
                        }
                    }
                }
            }

            return movesList;
        }

        private static void normalMoveCheck(uint i_Piece, uint i_Board, Func<uint, uint> i_ShiftingFunc, List<Move> i_MoveList)
        {
            bool outOfBounds = i_ShiftingFunc(i_Piece) == 0;
            if (!outOfBounds && !isPieceInTheWay(i_Piece, i_Board, i_ShiftingFunc))
            {
                i_MoveList.Add(new Move() {
                    IsCapture = false,
                    Origin = i_Piece, 
                    Destination = i_ShiftingFunc(i_Piece)
                });
            }
        }

        private static void captureCheck(uint i_Piece, uint i_OpposingPieces, uint i_SamePieces, Func<uint, uint> i_ShiftingFunc, Func<uint, uint> i_2ndShiftingFunc, List<Move> i_MoveList, ref bool io_IsCaptureTurn)
        {
            bool outOfBounds = i_ShiftingFunc(i_Piece) == 0 || i_2ndShiftingFunc(i_Piece) == 0;
            if (!outOfBounds && isPieceInTheWay(i_Piece, i_OpposingPieces, i_ShiftingFunc) &&
                !isPieceInTheWay(i_Piece, i_OpposingPieces | i_SamePieces, i_2ndShiftingFunc))
            {
                if (!io_IsCaptureTurn)
                {
                    io_IsCaptureTurn = true;
                    i_MoveList.Clear();
                }

                i_MoveList.Add(new Move() { 
                               IsCapture = true, 
                               Origin = i_Piece, 
                               Destination = i_2ndShiftingFunc(i_Piece), 
                               Capture = i_ShiftingFunc(i_Piece)
                });
            }
        }

        private static bool isPieceInTheWay(uint piece, uint allColorPieces, Func<uint, uint> i_Shift)
        {
            return (i_Shift(piece) & allColorPieces) != 0; 
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
    }
}
