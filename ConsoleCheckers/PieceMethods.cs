using System;
using System.Collections.Generic;

namespace ConsoleCheckers
{
    public static class PieceMethods
    {
        public static ICollection<int> GiveLegalMoves(Board i_Board, ePieces i_Piece, int i, int j)
        {
            ICollection<int> legalMoves = null;

            switch (i_Piece)
            {
                case ePieces.None:
                    break;
                case ePieces.sWhite:
                    legalMoves = SoliderMoves(i_Board, i, j, -1, ePieces.sBlack);
                    break;
                case ePieces.sBlack:
                    legalMoves = SoliderMoves(i_Board, i, j, 1, ePieces.sWhite);
                    break;
                case ePieces.qWhite:
                    legalMoves = wQueenMoves(i_Board, i, j);
                    break;
                case ePieces.qBlack:
                    legalMoves = bQueenMoves(i_Board, i, j);
                    break;
            }

            return legalMoves;
        }

        private static ICollection<int> bQueenMoves(Board i_Board, int i, int j)
        {
            throw new NotImplementedException();
        }

        private static ICollection<int> wQueenMoves(Board i_Board, int i, int j)
        {
            throw new NotImplementedException();
        }

        private static ICollection<int> SoliderMoves(Board i_Board, int i, int j, int i_Direction, ePieces i_Captureable)
        {
            List<int> legalMoves = new List<int>();
            bool isRightCaptureable = CheckValid(i + (1 * i_Direction), j + 1) && CheckValid(i + (2 * i_Direction), j + 2) &&
                                      i_Board[i + (1* i_Direction), j + 1] == i_Captureable && i_Board[i + (2 * i_Direction), j + 2] == ePieces.None;
            bool isLeftCaptureable = CheckValid(i + (1 * i_Direction), j - 1) && CheckValid(i + (2 * i_Direction), j - 2) && 
                                     i_Board[i + (1 * i_Direction), j - 1] == i_Captureable && i_Board[i + (2 * i_Direction), j - 2] == ePieces.None;

            if (isRightCaptureable || isLeftCaptureable)
            {
                if (isRightCaptureable)
                {
                    legalMoves.Add(CoordinateToInt(i + (2 * i_Direction), j + 2));
                }

                if(isLeftCaptureable)
                {
                    legalMoves.Add(CoordinateToInt(i + (2 * i_Direction), j - 2));
                }
            }
            else
            {
                if (CheckValid(i + (1 * i_Direction), j + 1) && i_Board[i + (1 * i_Direction), j + 1] == ePieces.None)
                {
                    legalMoves.Add(CoordinateToInt(i + (1 * i_Direction), j + 1));
                }
                
                if(CheckValid(i + (1 * i_Direction), j - 1) && i_Board[i + (1 * i_Direction), j - 1] == ePieces.None)
                {
                    legalMoves.Add(CoordinateToInt(i + (1 * i_Direction), j - 1));
                }
            }

            return legalMoves;
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
