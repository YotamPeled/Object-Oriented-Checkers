using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using YotamControls;

namespace ConsoleCheckers
{
    internal class CheckersButton : YotamButton
    {
        private int m_ImageLength = 100;
        private uint m_BitPosition;
        private int tag;
        public uint BitPosition { get { return m_BitPosition; } }
        public CheckersButton(uint i_bitPosition)
        {
            m_BitPosition = i_bitPosition;         
            tag = PieceMethods.UIntToInt(m_BitPosition);
            selectBackColor();
        }

        private void selectBackColor()
        {
            int colorIndex;
            if (UIUtils.isBackColorWhite(m_BitPosition))
            {
                colorIndex = 27;
            }
            else
            {
                colorIndex = 28;
            }

            SelectThemeColor(colorIndex);
        }

        public void Select()
        {
            ImageUtils.DrawCircleOnButton(this);
        }

        public void UnSelect()
        {
            Draw();
        }

        public void Draw()
        {
            int i, j;
            PieceMethods.IntToCoordinate(tag, out i, out j);
            ePiece piece = GameMasterSingleton.Instance.Board[i, j];
            switch(piece)
            {
                case ePiece.sWhite:
                    drawWhiteSolider();
                    break;
                case ePiece.sBlack:
                    drawBlackSolider();
                    break;
                case ePiece.qWhite:
                    drawWhiteSolider(true);
                    break;
                case ePiece.qBlack:
                    drawBlackSolider(true);
                    break;
                case ePiece.None:
                    this.Image = null;
                    break;
            }
        }

        private void drawWhiteSolider(bool drawQueenFlag = false)
        {
            this.Image = ImageUtils.ResizeImage(Images.white_pawn_icon, m_ImageLength);
        }

        private void drawBlackSolider(bool drawQueenFlag = false)
        {
            this.Image = ImageUtils.ResizeImage(Images.black_pawn_icon, m_ImageLength);            
        }
    }
}
