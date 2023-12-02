using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    internal static class ConsoleUI
    {
        internal static void PrintBoard(Board i_Board)
        {
            Console.Clear();
            printRow();
            for (int i = 0; i < 8; i++)
            {
                Console.Write(i);
                printSpacing();
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    printPiece(i_Board[i, j]);
                    printSpacing();
                }
                Console.WriteLine();         
            }
        }

        private static void printPiece(ePieces i_Piece)
        {
            switch(i_Piece)
            {
                case ePieces.None:
                    Console.Write(' ');
                    break;
                case ePieces.sWhite:
                    Console.Write('w');
                    break;
                case ePieces.sBlack:
                    Console.Write('b');
                    break;
                case ePieces.qWhite:
                    Console.Write('W');
                    break;
                case ePieces.qBlack:
                    Console.Write('B');
                    break;
                case ePieces.Highlight:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write(' ');
                    break;
                case ePieces.HighlightFrom:
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write(' ');
                    break;
            }
        }

        private static void printSpacing()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write('|');
        }

        private static void printRow()
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7");
        }
    }
}
