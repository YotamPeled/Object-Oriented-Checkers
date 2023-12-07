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
        private int m_Position;
        public int Position { get { return m_Position; } }
        public CheckersButton(int i_Position)
        {
            m_Position = i_Position;
            this.Text = m_Position.ToString();
            selectBackColor();
        }

        private void selectBackColor()
        {
            int colorIndex;
            if (UIUtils.isBackColorWhite(m_Position))
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
