using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    internal class ConsoleUI
    {
        private Board m_Board;
        private List<int> m_SelectedSquares = new List<int>();
        public ConsoleUI(Board i_Board)
        {
            m_Board = i_Board;
            i_Board.MadeMove += OnMadeMove;
            i_Board.GameEnded += OnGameEnd;
            PrintBoard();
        }

        public bool Select(int i_SquareToSelect)
        {
            bool legalSelection = false;

            foreach(IMove legalMove in m_Board.LegalMoves)
            {
                if (PieceMethods.UIntToInt(legalMove.GetStartSquare()) == i_SquareToSelect)
                {
                    m_SelectedSquares.Add(PieceMethods.UIntToInt(legalMove.GetTargetSquare()));
                    legalSelection = true;
                }
            }

            if (legalSelection)
            {
                m_SelectedSquares.Add(i_SquareToSelect);
            }

            PrintBoard();
            return legalSelection;
        }

        public void ClearHighlights()
        {
            m_SelectedSquares.Clear();
            PrintBoard();
        }

        protected virtual void OnMadeMove(IMove i_Move)
        {
            m_SelectedSquares.Clear();
            m_SelectedSquares.Add(PieceMethods.UIntToInt(i_Move.GetStartSquare()));
            m_SelectedSquares.Add(PieceMethods.UIntToInt(i_Move.GetTargetSquare()));

            PrintBoard();
        }

        internal void PrintBoard()
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

                    if (m_SelectedSquares.Contains(PieceMethods.CoordinateToInt(i, j)))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                    }
                    
                    printPiece(m_Board[i, j]);
                    printSpacing(); //changes console color back to black
                }

                Console.WriteLine();         
            }

            Console.WriteLine($"White: {m_Board.whiteScore} | Black: {m_Board.blackScore}");
        }

        private void printPiece(ePiece i_Piece)
        {
            switch(i_Piece)
            {
                case ePiece.None:
                    Console.Write(' ');
                    break;
                case ePiece.sWhite:
                    Console.Write('w');
                    break;
                case ePiece.sBlack:
                    Console.Write('b');
                    break;
                case ePiece.qWhite:
                    Console.Write('W');
                    break;
                case ePiece.qBlack:
                    Console.Write('B');
                    break;
            }
        }

        private void printSpacing()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write('|');
        }

        private void printRow()
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7");
        }

        private void OnGameEnd(eColor i_WinningColor)
        {
            Console.WriteLine($"Game Ended, the winner is {i_WinningColor}");
        }
    }
}
