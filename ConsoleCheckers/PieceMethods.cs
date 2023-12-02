using System;
using System.Collections.Generic;

namespace ConsoleCheckers
{
    public static class PieceMethods
    {
        public static LinkedList<int> GenerateLegalMoves(Board i_Board, ePieces i_Piece, int i_PieceLocation)
        {
            LinkedList<int> legalMoves = new LinkedList<int>();
            switch (i_Piece)
            {
                case ePieces.None:
                    break;
                case ePieces.sWhite:
                    wSoliderMoves(i_Board, i_PieceLocation, legalMoves);
                    break;
                case ePieces.sBlack:
                    bSoliderMoves(i_Board, i_PieceLocation, legalMoves);
                    break;
                case ePieces.qWhite:
                    wQueenMoves(i_Board, i_PieceLocation, legalMoves);
                    break;
                case ePieces.qBlack:
                    bQueenMoves(i_Board, i_PieceLocation, legalMoves);
                    break;
            }

            return legalMoves;
        }

        private static void bQueenMoves(Board i_Board, int i_PieceLocation, LinkedList<int> legalMoves)
        {

        }

        private static void wQueenMoves(Board i_Board, int i_PieceLocation, LinkedList<int> legalMoves)
        {

        }

        private static void bSoliderMoves(Board i_Board, int i_PieceLocation, LinkedList<int> legalMoves)
        {

        }

        private static void wSoliderMoves(Board i_Board, int i_PieceLocation, LinkedList<int> i_LegalMoves)
        {
            if (i_PieceLocation % 10 == 9 || i_PieceLocation % 10 == 8 || i_PieceLocation / 10 == -1 ||
                i_PieceLocation / 10 == 8)
            {
                return;
            }

            bool isCaptured = false;
            int i = i_PieceLocation / 10;
            int j = i_PieceLocation % 10;
            if (CheckValid(i - 1, j - 1))
            {
                if(i_Board[i - 1, j - 1] == ePieces.None)
                {
                    i_LegalMoves.AddLast(i_PieceLocation - 11);
                }
                else if (i_Board[i - 1, j - 1] == ePieces.sBlack || i_Board[i - 1, j - 1] == ePieces.qBlack)
                {
                    if(CheckValid(i - 2, j - 2) && i_Board[i - 2, j - 2] == ePieces.None)
                    {
                        i_LegalMoves.AddLast(i_PieceLocation - 22);
                        isCaptured = true;
                    }
                }
            }

            if (CheckValid(i - 1, j + 1))
            {
                if (!isCaptured && i_Board[i - 1, j + 1] == ePieces.None)
                {
                    i_LegalMoves.AddLast(i_PieceLocation - 9);
                }
                else if (i_Board[i - 1, j + 1] == ePieces.sBlack || i_Board[i - 1, j + 1] == ePieces.qBlack)
                {
                    if (CheckValid(i - 2, j + 2) && i_Board[i - 2, j + 2] == ePieces.None)
                    {
                        if (!isCaptured)
                        {
                            i_LegalMoves.Clear();
                        }

                        i_LegalMoves.AddLast(i_PieceLocation - 18);
                    }
                }
            }
            
        }

        public static bool CheckValid(int i, int j)
        {
            bool validMove = false;
            if (i >= 0 && i <= 7 && j >= 0 && j <= 7)
            {
                validMove = true;
            }

            return validMove;
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
    }
}
