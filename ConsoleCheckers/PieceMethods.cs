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

        public static eColor SwapTurn(eColor i_Color)
        {
            return i_Color == eColor.White ? eColor.Black : eColor.White;
        }
    }
}
