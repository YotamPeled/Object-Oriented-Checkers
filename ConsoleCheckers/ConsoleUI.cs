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
            PrintBoard();
        }

        public bool Select(int i_SquareToSelect)
        {
            bool added = false;
            int i;
            int j; 

            PieceMethods.IntToCoordinate(i_SquareToSelect, out i, out j);
            ICollection<int> toSelect =  m_Board.GenerateLegalMoves(i, j);
            toSelect.Add(i_SquareToSelect);

            foreach(int square in toSelect)
            {
                PieceMethods.IntToCoordinate(square, out i, out j);
                if (PieceMethods.CheckValid(i, j) && !m_SelectedSquares.Contains(square))
                {
                    m_SelectedSquares.Add(square);
                    added = true;
                }
            }
            
            if (added)
            {
                PrintBoard();
            }

            return added;
        }

        public void ClearHighlights()
        {
            
            m_SelectedSquares.Clear();
            PrintBoard();
        }

        protected virtual void OnMadeMove(int iFrom, int jFrom, int iTo, int jTo)
        {
            m_SelectedSquares.Clear();
            m_SelectedSquares.Add(PieceMethods.CoordinateToInt(iFrom, jFrom));
            m_SelectedSquares.Add(PieceMethods.CoordinateToInt(iTo, jTo));

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
                    printSpacing();
                }
                Console.WriteLine();         
            }
        }

        private void printPiece(ePieces i_Piece)
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
    }
}
