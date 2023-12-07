using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public partial class FormsUI
    {
        private uint m_SelectedSquare;
        private Board m_GameBoard;
        private Dictionary<uint, CheckersButton> m_PositionToButtonDict = new Dictionary<uint, CheckersButton>();
        public FormsUI()
        {
            m_GameBoard = new Board();
            InitializeComponent();
            fillDict();
            loadPieceImages();
        }

        private void fillDict()
        {
            foreach(CheckersButton buttonSquare in panelCheckers.Controls)
            {
                m_PositionToButtonDict[buttonSquare.bitPosition] = buttonSquare;
            }
        }

        private void loadPieceImages()
        {
            List<uint> whiteSoliders = BitUtils.GetSetBits(m_GameBoard.BitBoards[(int)ePiece.sWhite - 1]);
            List<uint> whiteQueens = BitUtils.GetSetBits(m_GameBoard.BitBoards[(int)ePiece.qWhite - 1]);
            List<uint> blackSoliders = BitUtils.GetSetBits(m_GameBoard.BitBoards[(int)ePiece.sBlack - 1]);
            List<uint> blackQueens = BitUtils.GetSetBits(m_GameBoard.BitBoards[(int)ePiece.qBlack - 1]);

            foreach(uint whitePiece in whiteSoliders)
            {
                m_PositionToButtonDict[whitePiece].Text = "w";
            }

            foreach (uint whiteQueen in whiteQueens)
            {
                m_PositionToButtonDict[whiteQueen].Text = "W";

            }

            foreach (uint blackPiece in blackSoliders)
            {
                m_PositionToButtonDict[blackPiece].Text = "b";

            }

            foreach (uint blackQueen in blackQueens)
            {
                m_PositionToButtonDict[blackQueen].Text = "B";

            }
        }

        private void Select(uint i_Selection)
        {
            m_SelectedSquare = i_Selection;
        }
    }
}
