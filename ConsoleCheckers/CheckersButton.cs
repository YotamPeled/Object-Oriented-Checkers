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
    public class CheckersButton : YotamButton
    {
        private int m_ImageLength = 100;
        private uint m_BitPosition;
        private int tag;
        public uint BitPosition { get { return m_BitPosition; } }
        public CheckersButton(uint i_bitPosition)
        {
            m_BitPosition = i_bitPosition;         
            tag = PieceMethods.UIntToInt(m_BitPosition);
            selectOriginalBackColor();
        }

        public void selectOriginalBackColor()
        {
            eColors color;
            if (UIUtils.isBackColorWhite(m_BitPosition))
            {
                color = eColors.OffWhite;
            }
            else
            {
                color = eColors.Brown;
            }

            SelectThemeColor(color);
        }

        public new void Select()
        {
            ImageUtils.DrawCircleOnButton(this);
        }

        public void UnSelect()
        {
            ImageUtils.ClearCircleFromButton(this);
        }

        public void drawMoveHighlight()
        {
            SelectThemeColor(eColors.LightYellow);
        }

        public void drawCaptureHighlight()
        {
            SelectThemeColor(eColors.LightRed);
        }

        public void Draw(Board i_Board)
        {
            this.Controls.Clear();
            int i, j;
            PieceMethods.IntToCoordinate(tag, out i, out j);
            ePiece piece = i_Board[i, j];
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
            if (drawQueenFlag)
            {
                drawCrown();
            }
        }

        private void drawBlackSolider(bool drawQueenFlag = false)
        {
            this.Image = ImageUtils.ResizeImage(Images.black_pawn_icon, m_ImageLength);
            if (drawQueenFlag)
            {
                drawCrown();
            }
        }

        private void drawCrown()
        {
            int crownSize = m_ImageLength / 2; // Calculate the size of the crown

            Image crown = ImageUtils.ResizeImage(Images.crown_vector_icon, crownSize);

            // Create a new bitmap for the crown with a transparent background
            Bitmap crownBitmap = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(crownBitmap))
            {
                // Fill the new bitmap with a transparent background
                g.Clear(Color.Transparent);

                // Calculate the position to center the crown within the button
                int xPosition = (this.Width - crownSize) / 2;
                int yPosition = (this.Height - crownSize) / 2;

                // Draw the crown onto the transparent bitmap
                g.DrawImage(crown, xPosition, yPosition, crownSize, crownSize);
            }

            // Combine the crown bitmap with the original button image
            Bitmap combinedBitmap = new Bitmap(this.Image);
            using (Graphics g = Graphics.FromImage(combinedBitmap))
            {
                g.DrawImage(crownBitmap, (this.Width - crownSize) / 2, (this.Width - crownSize) / 2);
            }

            // Set the combined image as the button's image
            this.Image = combinedBitmap;
        }


    }
}
