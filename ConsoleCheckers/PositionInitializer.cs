using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    static class PositionInitializer
    {
        public static void Starting(Board i_Board)
        {
            int n = 24; 
            List<Tuple<int, int>> pairs = generatePairs(n);
         
            foreach (var pair in pairs)
            {
                if (pair.Item1 < 3)
                {
                    i_Board.BitBoards[(int)ePiece.sBlack - 1] >>= 1;
                    i_Board.BitBoards[(int)ePiece.sBlack - 1] |= 1U << 31;
                }
                else 
                {
                    i_Board.BitBoards[(int)ePiece.sWhite - 1] <<= 1;
                    i_Board.BitBoards[(int)ePiece.sWhite - 1] |= 1;
                }
            }
        }

        public static void randomQueen(Board i_Board)
        {
            int n = 24;
            List<Tuple<int, int>> pairs = generatePairs(n);

            foreach (var pair in pairs)
            {
                if (pair.Item1 < 3)
                {
                    i_Board.BitBoards[(int)ePiece.qBlack - 1] >>= 1;
                    i_Board.BitBoards[(int)ePiece.qBlack - 1] |= 1U << 31;
                }
                else
                {
                    i_Board.BitBoards[(int)ePiece.qWhite - 1] <<= 1;
                    i_Board.BitBoards[(int)ePiece.qWhite - 1] |= 1;
                }
            }
        }

        public static void gameEndTest(Board i_Board)
        {
            int n = 2;
            List<Tuple<int, int>> pairs = generatePairs(n);
            int counter = 0;
            foreach (var pair in pairs)
            {
                counter++;
                if (counter == 1)
                {
                    i_Board.BitBoards[(int)ePiece.qBlack - 1] |= 1U << 31;
                }
                else
                {
                    i_Board.BitBoards[(int)ePiece.qWhite - 1] |= 1U << 30;
                }
            }
        }

        public static void SetupDoubleCapture(Board i_Board)
        {
            // Set bitboard positions for the pieces
            i_Board.BitBoards[(int)ePiece.sBlack - 1] = 0b00000100000000100000000000000000; // Bit representation for two black pieces
            i_Board.BitBoards[(int)ePiece.sWhite - 1] = 0b00000000000000000010000000000000; // Bit representation for two white pieces
        }

        public static void DoubleCaptureBugTest(Board i_Board)
        {
            i_Board.BitBoards[(int)ePiece.sBlack - 1] = 0b00000000000000000000000001000000; // Bit representation for two black pieces
            i_Board.BitBoards[(int)ePiece.sWhite - 1] = 0b00000000000000000000000000000110; // Bit representation for two white pieces
            i_Board.BitBoards[(int)ePiece.qBlack - 1] = 0b00000000000000000000000000100000;
        }


        static List<Tuple<int, int>> generatePairs(int n)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();

            for (int i = 0; i < n; i++)
            {
                int first, second;

                first = i / 4 < 3 ? i / 4 : i / 4 + 2;
                second = first % 2 == 0 ? i % 4 * 2 + 1 : i % 4 * 2;

                result.Add(new Tuple<int, int>(first, second));
            }

            return result;
        }
    }
}
