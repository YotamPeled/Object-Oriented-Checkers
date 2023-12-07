using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public static class UIUtils
    {
        public static bool isBackColorWhite(int i_Position)
        {
            int i, j;
            bool isWhite;
            PieceMethods.IntToCoordinate(i_Position, out i, out j);
            if ((i + j) % 2 == 0)
            {
                isWhite = true;
            }
            else
            {
                isWhite = false;
            }

            return isWhite;
        }
    }
}
