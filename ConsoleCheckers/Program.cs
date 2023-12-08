using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GameMasterSingleton.Instance.Board = new Board();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormsUI());
            /*
            Board gameBoard = new Board();
            ConsoleUI UI = new ConsoleUI(gameBoard);
            string input = Console.ReadLine();
            bool isSquareSelected = false;
            int moveFrom = -1;

            while (true && gameBoard.GameOngoing)
            {
                bool breakLoop = false;
                int intInput;
                bool isLegalInput = int.TryParse(input, out intInput);
                isLegalInput = isLegalInput && PieceMethods.CheckValid(intInput / 10, intInput % 10);

                if(isLegalInput)
                {
                    if (isSquareSelected)
                    {
                        if (gameBoard.MakeMove(moveFrom, intInput))
                        {
                            moveFrom = -1;
                            isSquareSelected = false;
                        }
                    }
                    else
                    {
                        isSquareSelected = gameBoard.IsSquareInPlay(intInput);
                        if (isSquareSelected)
                        {
                            moveFrom = intInput;
                            UI.Select(intInput);
                        }
                    }
                }
                else
                {
                    switch(input)
                    {
                        case "m":
                            Console.WriteLine("Show Menu");
                            break;
                        case "r":
                            isSquareSelected = false;
                            UI.ClearHighlights();
                            break;
                        case "q":
                            breakLoop = true;
                            break;
                        default:
                            Console.WriteLine("illegal input");
                            break;
                    }
                }

                if (breakLoop)
                    break;

                input = Console.ReadLine();
            }

            Console.Read();
            */
        }
    }
}
