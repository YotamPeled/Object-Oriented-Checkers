using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    static class PositionInitializer
    {
        public static void Starting(ePieces[,] i_Board)
        {
            if (i_Board.GetLength(0) != 8 || i_Board.GetLength(1) != 8)
            {
                throw new Exception("board dimensions aren't corrent");
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        if (i < 3)
                        {
                            i_Board[i, j] = ePieces.sBlack;
                        }
                        else if (i > 4)
                        {
                            i_Board[i, j] = ePieces.sWhite;
                        }
                    }
                }
            }
        }
    }
}
