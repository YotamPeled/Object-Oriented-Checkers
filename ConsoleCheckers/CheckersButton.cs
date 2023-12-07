using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YotamControls;

namespace ConsoleCheckers
{
    internal class CheckersButton : YotamButton
    {
        private uint m_bitPosition;
        public uint bitPosition { get { return m_bitPosition; } }
        public CheckersButton(uint i_bitPosition)
        {
            m_bitPosition = i_bitPosition;
            selectBackColor();
        }

        private void selectBackColor()
        {
            int colorIndex;
            if (UIUtils.isBackColorWhite(m_bitPosition))
            {
                colorIndex = 27;
            }
            else
            {
                colorIndex = 28;
            }

            SelectThemeColor(colorIndex);
        }
    }
}
