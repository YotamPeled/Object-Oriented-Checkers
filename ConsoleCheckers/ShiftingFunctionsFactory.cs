using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public class ShiftingFunctionsFactory
    {
        //storing bit position and moves as bit shifts
        private static Dictionary<int, Func<uint, uint>> wFirstMoveDict;
        private static Dictionary<int, Func<uint, uint>> wSecondMoveDict;
        private static Dictionary<int, Func<uint, uint>> wFirstCaptureDict;
        private static Dictionary<int, Func<uint, uint>> wSecondCaptureDict;
        private static Dictionary<int, Func<uint, uint>> bFirstMoveDict;
        private static Dictionary<int, Func<uint, uint>> bSecondMoveDict;
        private static Dictionary<int, Func<uint, uint>> bFirstCaptureDict;
        private static Dictionary<int, Func<uint, uint>> bSecondCaptureDict;

        static ShiftingFunctionsFactory()
        {
            fillDicts();
        }

        public static void GetShiftingFuncs(int i_BitPosition, eColor i_PieceColor, 
            out Func<uint, uint> o_SingleLeft, out Func<uint, uint> o_SingleRight, 
            out Func<uint, uint> o_DoubleLeft, out Func<uint, uint> o_DoubleRight)
        {
            if (i_PieceColor == eColor.White)
            {
                o_SingleLeft = wFirstMoveDict[i_BitPosition];
                o_SingleRight = wSecondMoveDict[i_BitPosition];
                o_DoubleLeft = wFirstCaptureDict[i_BitPosition];
                o_DoubleRight = wSecondCaptureDict[i_BitPosition];
            }
            else
            {
                o_SingleLeft = bFirstMoveDict[i_BitPosition];
                o_SingleRight = bSecondMoveDict[i_BitPosition];
                o_DoubleLeft = bFirstCaptureDict[i_BitPosition];
                o_DoubleRight = bSecondCaptureDict[i_BitPosition];
            }
        }

        private static void fillDicts()
        {
            // fill white move dict
            fillwFirstMove();
            fillwSecondMove();
            fillwFirstCapture();
            fillwSecondCapture();

            // fill black move dict
            fillbFirstMove();
            fillbSecondMove();
            fillbFirstCapture();
            fillbSecondCapture();
        }
        //move left
        private static void fillwFirstMove()
        {
            wFirstMoveDict = new Dictionary<int, Func<uint, uint>>();
            wFirstMoveDict.Add(0, shiftLeftBy(5));
            wFirstMoveDict.Add(1, shiftLeftBy(5));
            wFirstMoveDict.Add(2, shiftLeftBy(5));
            wFirstMoveDict.Add(3, null);
            wFirstMoveDict.Add(4, shiftLeftBy(4));
            wFirstMoveDict.Add(5, shiftLeftBy(4));
            wFirstMoveDict.Add(6, shiftLeftBy(4));
            wFirstMoveDict.Add(7, shiftLeftBy(4));
        }
        //move right
        private static void fillwSecondMove()
        {
            wSecondMoveDict = new Dictionary<int, Func<uint, uint>>();
            wSecondMoveDict.Add(0, shiftLeftBy(4));
            wSecondMoveDict.Add(1, shiftLeftBy(4));
            wSecondMoveDict.Add(2, shiftLeftBy(4));
            wSecondMoveDict.Add(3, shiftLeftBy(4));
            wSecondMoveDict.Add(4, null);
            wSecondMoveDict.Add(5, shiftLeftBy(3));
            wSecondMoveDict.Add(6, shiftLeftBy(3));
            wSecondMoveDict.Add(7, shiftLeftBy(3));
        }
        //capture left
        private static void fillwFirstCapture()
        {
            wFirstCaptureDict = new Dictionary<int, Func<uint, uint>>();
            wFirstCaptureDict.Add(0, shiftLeftBy(9));
            wFirstCaptureDict.Add(1, shiftLeftBy(9));
            wFirstCaptureDict.Add(2, shiftLeftBy(9));
            wFirstCaptureDict.Add(3, null);
            wFirstCaptureDict.Add(4, shiftLeftBy(9));
            wFirstCaptureDict.Add(5, shiftLeftBy(9));
            wFirstCaptureDict.Add(6, shiftLeftBy(9));
            wFirstCaptureDict.Add(7, null);
        }
        // capture right
        private static void fillwSecondCapture()
        {
            wSecondCaptureDict = new Dictionary<int, Func<uint, uint>>();
            wSecondCaptureDict.Add(0, null);
            wSecondCaptureDict.Add(1, shiftLeftBy(7));
            wSecondCaptureDict.Add(2, shiftLeftBy(7));
            wSecondCaptureDict.Add(3, shiftLeftBy(7));
            wSecondCaptureDict.Add(4, null);
            wSecondCaptureDict.Add(5, shiftLeftBy(7));
            wSecondCaptureDict.Add(6, shiftLeftBy(7));
            wSecondCaptureDict.Add(7, shiftLeftBy(7));
        }
        // move left
        private static void fillbFirstMove()
        {
            bFirstMoveDict = new Dictionary<int, Func<uint, uint>>();
            bFirstMoveDict.Add(0, shiftRightBy(3));
            bFirstMoveDict.Add(1, shiftRightBy(3));
            bFirstMoveDict.Add(2, shiftRightBy(3));
            bFirstMoveDict.Add(3, null);
            bFirstMoveDict.Add(4, shiftRightBy(4));
            bFirstMoveDict.Add(5, shiftRightBy(4));
            bFirstMoveDict.Add(6, shiftRightBy(4));
            bFirstMoveDict.Add(7, shiftRightBy(4));
        }
        //move right
        private static void fillbSecondMove()
        {
            bSecondMoveDict = new Dictionary<int, Func<uint, uint>>();
            bSecondMoveDict.Add(0, shiftRightBy(4));
            bSecondMoveDict.Add(1, shiftRightBy(4));
            bSecondMoveDict.Add(2, shiftRightBy(4));
            bSecondMoveDict.Add(3, shiftRightBy(4));
            bSecondMoveDict.Add(4, null);
            bSecondMoveDict.Add(5, shiftRightBy(5));
            bSecondMoveDict.Add(6, shiftRightBy(5));
            bSecondMoveDict.Add(7, shiftRightBy(5));
        }
        //capture left
        private static void fillbFirstCapture()
        {
            bFirstCaptureDict = new Dictionary<int, Func<uint, uint>>();
            bFirstCaptureDict.Add(0, shiftRightBy(7));
            bFirstCaptureDict.Add(1, shiftRightBy(7));
            bFirstCaptureDict.Add(2, shiftRightBy(7));
            bFirstCaptureDict.Add(3, null);
            bFirstCaptureDict.Add(4, shiftRightBy(7));
            bFirstCaptureDict.Add(5, shiftRightBy(7));
            bFirstCaptureDict.Add(6, shiftRightBy(7));
            bFirstCaptureDict.Add(7, null);
        }
        // capture right
        private static void fillbSecondCapture()
        {
            bSecondCaptureDict = new Dictionary<int, Func<uint, uint>>();
            bSecondCaptureDict.Add(0, null);
            bSecondCaptureDict.Add(1, shiftRightBy(9));
            bSecondCaptureDict.Add(2, shiftRightBy(9));
            bSecondCaptureDict.Add(3, shiftRightBy(9));
            bSecondCaptureDict.Add(4, null);
            bSecondCaptureDict.Add(5, shiftRightBy(9));
            bSecondCaptureDict.Add(6, shiftRightBy(9));
            bSecondCaptureDict.Add(7, shiftRightBy(9));
        }

        private static Func<uint, uint> shiftLeftBy(int amount)
        {
            return (number) => { return number << amount; };
        }

        private static Func<uint, uint> shiftRightBy(int amount)
        {
            return (number) => { return number >> amount; };
        }
    }
}
