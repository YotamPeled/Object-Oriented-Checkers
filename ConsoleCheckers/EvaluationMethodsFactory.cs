using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public enum EvaluationType
    {
        Basic,
        QueenWorthUp
    }

    internal static class EvaluationMethodsFactory
    {
        public static Func<uint[], int> GetEvaluationStrategy(EvaluationType i_Type)
        {
            Func<uint[], int> strategy = null;
            switch (i_Type)
            {
                case EvaluationType.Basic:
                    strategy = basic;
                    break;
                case EvaluationType.QueenWorthUp:
                    strategy = queenWorthUp; 
                    break;
            }

            return strategy;
        }
        private static int basic(uint[] i_BitBoards)
        {
            int blackSolidersAmount = BitUtils.GetSetBitsAmount(i_BitBoards[(int)ePiece.sBlack - 1]);
            int blackQueenAmount = BitUtils.GetSetBitsAmount(i_BitBoards[(int)ePiece.qBlack - 1]);
            int whiteSolidersAmount = BitUtils.GetSetBitsAmount(i_BitBoards[(int)ePiece.sWhite - 1]);
            int whiteQueensAmount = BitUtils.GetSetBitsAmount(i_BitBoards[(int)ePiece.qWhite - 1]);
            // amount of white pieces - amount of black pieces. queens count as 2.
            return whiteSolidersAmount + (whiteQueensAmount * 2) - (blackSolidersAmount + (blackQueenAmount * 2));
        }

        private static int queenWorthUp(uint[] i_BitBoards)
        {
            int blackSolidersAmount = BitUtils.GetSetBitsAmount(i_BitBoards[(int)ePiece.sBlack - 1]);
            int blackQueenAmount = BitUtils.GetSetBitsAmount(i_BitBoards[(int)ePiece.qBlack - 1]);
            int whiteSolidersAmount = BitUtils.GetSetBitsAmount(i_BitBoards[(int)ePiece.sWhite - 1]);
            int whiteQueensAmount = BitUtils.GetSetBitsAmount(i_BitBoards[(int)ePiece.qWhite - 1]);
            // amount of white pieces - amount of black pieces. queens count as 2.
            return whiteSolidersAmount + whiteQueensAmount * 10 - (blackSolidersAmount + (blackQueenAmount * 10));
        }
    }
}
