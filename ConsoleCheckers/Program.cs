using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    class Program
    {
        static void Main(string[] args)
        {
            Board gameBoard = new Board();

            ConsoleUI.PrintBoard(gameBoard);

            string input = Console.ReadLine();
            bool isSquareSelected = false;
            int moveFrom = -1;

            while(!input.Equals('q'))
            {
                int intInput;
                bool isLegalInput = int.TryParse(input, out intInput);
                isLegalInput = isLegalInput || PieceMethods.CheckValid(intInput / 10, intInput % 10);

                if(isLegalInput)
                {
                    if (isSquareSelected)
                    {
                        gameBoard.MakeMove(moveFrom, intInput);
                        ConsoleUI.PrintBoard(gameBoard);
                        moveFrom = -1;
                        isSquareSelected = false;
                    }
                    else
                    {
                        isSquareSelected = true;
                        gameBoard.HighLight(intInput / 10, intInput % 10);
                        ConsoleUI.PrintBoard(gameBoard);
                        moveFrom = intInput;
                    }
                }
                else
                {
                    Console.WriteLine("illegal input");
                }

                input = Console.ReadLine();
            }
        }
    }
}
