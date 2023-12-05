using System;
using System.Collections.Generic;

namespace ConsoleCheckers
{
    public static class PieceMethods
    {
        public static List<Move> GiveLegalMoves(uint[] i_BitBoards, eColor i_ColorToMove)
        {
            List<Move> moveList;
            if (i_ColorToMove == eColor.White)
            {
                moveList = WhiteMoves(i_BitBoards);
            }
            else
            {
                moveList = BlackMoves(i_BitBoards);
            }
            //Force captures
            bool isCapture = false;
            foreach (Move move in moveList)
            {
                if (move.IsCapture)
                {
                    isCapture = true;
                    break;
                }
            }

            if (isCapture)
            {
                foreach (Move move in moveList)
                {
                    if (!move.IsCapture)
                    {
                        moveList.Remove(move);
                    }
                }
            }

            return moveList;
        }

        private static List<Move> WhiteMoves(uint[] i_BitBoards)
        {
            List<Move> movesList = new List<Move>();
            uint whitePieces = i_BitBoards[0]; // add queens with || i_BitBoards[2]
            uint blackPieces = i_BitBoards[1]; // add queens with || i_BitBoards[3]
            Func<uint, uint> shiftFive = (number) => { return number << 5; };
            Func<uint, uint> shiftFour = (number) => { return number << 4; };
            Func<uint, uint> shiftThree = (number) => { return number << 3; };
            foreach (uint piece in BitUtils.GetSetBits(whitePieces))
            {
                int piecePosition = BitUtils.FindBitPosition(piece) % 8;
                switch (piecePosition)
                {
                    //1 space from right
                    case 0:
                        // check capture condition
                        if (captureCheck(piece, blackPieces, whitePieces, shiftFive, shiftFour, movesList))
                        {
                            break;
                        }
                        else
                        {
                            // normal move checks
                            normalMoveCheck(piece, whitePieces | blackPieces, shiftFive, movesList);
                            normalMoveCheck(piece, whitePieces | blackPieces, shiftFour, movesList);
                        }

                        break;
                    //pinned left
                    case 3:
                        if (captureCheck(piece, blackPieces, whitePieces, shiftFour, shiftThree, movesList))
                        {
                            break;
                        }
                        else
                        {
                            normalMoveCheck(piece, blackPieces | whitePieces, shiftFour, movesList);
                        }
                        break;
                    //pinned right
                    case 4:
                        if (captureCheck(piece, blackPieces, whitePieces, shiftFour, shiftFive, movesList))
                        {
                            break;
                        }
                        else
                        {
                            normalMoveCheck(piece, blackPieces | whitePieces, shiftFour, movesList);
                        }
                        break;
                    //1 space from left
                    case 7:
                        // check capture condition
                        if (captureCheck(piece, blackPieces, whitePieces, shiftThree, shiftFour, movesList))
                        {
                            break;
                        }
                        else
                        {
                            // normal move checks
                            normalMoveCheck(piece, whitePieces | blackPieces, shiftThree, movesList);
                            normalMoveCheck(piece, whitePieces | blackPieces, shiftFour, movesList);
                        }
                        break;
                    // center 
                    case 1:
                    case 2:
                        // check capture condition for both sides
                        if (captureCheck(piece, blackPieces, whitePieces, shiftFour, shiftThree, movesList) ||
                            captureCheck(piece, blackPieces, whitePieces, shiftFive, shiftFour, movesList))
                        {
                            break;
                        }
                        else // check normal move for both sides
                        {
                            normalMoveCheck(piece, whitePieces | blackPieces, shiftFour, movesList);
                            normalMoveCheck(piece, whitePieces | blackPieces, shiftFive, movesList);
                        }
                        break;
                    // center 
                    case 5:
                    case 6:
                        // check capture condition for both sides
                        if (captureCheck(piece, blackPieces, whitePieces, shiftThree, shiftFour, movesList) ||
                            captureCheck(piece, blackPieces, whitePieces, shiftFour, shiftFive, movesList))
                        {
                            break;
                        }
                        else // check normal move for both sides
                        {
                            normalMoveCheck(piece, whitePieces | blackPieces, shiftThree, movesList);
                            normalMoveCheck(piece, whitePieces | blackPieces, shiftFour, movesList);
                        }
                        break;
                }
            }

            return movesList;
        }

        private static List<Move> BlackMoves(uint[] i_BitBoards)
        {
            return null;
        }

        private static void normalMoveCheck(uint i_Piece, uint i_Board, Func<uint, uint> i_ShiftingFunc, List<Move> i_MoveList)
        {
            if (!isPieceInTheWay(i_Piece, i_Board, i_ShiftingFunc))
            {
                i_MoveList.Add(new Move() { IsCapture = false, Origin = i_Piece, Destination = i_ShiftingFunc(i_Piece) });
            }
        }

        private static bool captureCheck(uint i_Piece, uint i_OpposingPieces, uint i_SamePieces, Func<uint, uint> i_ShiftingFunc, Func<uint, uint> i_2ndShiftingFunc, List<Move> i_MoveList)
        {
            bool isCapture = false;
            if (isPieceInTheWay(i_Piece, i_OpposingPieces, i_ShiftingFunc) &&
                               !isPieceInTheWay(i_ShiftingFunc(i_Piece), i_OpposingPieces | i_SamePieces, i_2ndShiftingFunc))
            {
                i_MoveList.Add(new Move() { IsCapture = true, Origin = i_Piece, Destination = i_ShiftingFunc(i_ShiftingFunc(i_Piece)) });
                isCapture = true;
            }

            return isCapture;
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
    }
}
