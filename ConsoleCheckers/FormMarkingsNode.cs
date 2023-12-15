using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public class FormMarkingsNode : MarkingsNode
    {
        private Dictionary<uint, CheckersButton> m_PositionToButtonDict;
        public FormMarkingsNode(Dictionary<uint, CheckersButton> i_PositionToButtonDict)
        {
            m_PositionToButtonDict = i_PositionToButtonDict;
            this.LegalMoveSquaresAdded += Show;
        }

        private void iterateOverSelections(bool i_IsShowing)
        {
            CheckersButton checkersSquareToDrawOn;
            if (m_MarkedLegalMoveSquares.Count == 0)
            {
                // draw on relevant squares
                foreach (uint square in m_MarkedMovedPieceSquares)
                {
                    checkersSquareToDrawOn = m_PositionToButtonDict[square];
                    if (i_IsShowing)
                    {
                        checkersSquareToDrawOn.drawMoveHighlight();
                    }
                    else
                    {
                        checkersSquareToDrawOn.selectOriginalBackColor();
                    }
                }

                foreach (uint square in m_MarkedCapturedSquares)
                {
                    checkersSquareToDrawOn = m_PositionToButtonDict[square];
                    if (i_IsShowing)
                    {
                        checkersSquareToDrawOn.drawCaptureHighlight();
                    }
                    else
                    {
                        checkersSquareToDrawOn.selectOriginalBackColor();
                    }
                }
            }
            else
            {
                foreach (uint square in m_MarkedLegalMoveSquares)
                {
                    checkersSquareToDrawOn = m_PositionToButtonDict[square];
                    if (i_IsShowing)
                    {
                        checkersSquareToDrawOn.Select();
                    }
                    else
                    {
                        checkersSquareToDrawOn.UnSelect();
                    }
                }
            }
        }

        public override void Hide()
        {
            bool isShowing = false;
            iterateOverSelections(isShowing);
        }

        public override void Show()
        {
            bool isShowing = true;
            iterateOverSelections(isShowing);
        }
    }
}
