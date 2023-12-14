using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    public static class UIUtils
    {
        public static bool isBackColorWhite(uint i_Position)
        {
            bool isWhite;
            if (i_Position == 0) // unreachable square
            {
                isWhite = true;
            }
            else //reachable
            {
                isWhite = false;
            }

            return isWhite;
        }

        internal static void AddComputerOptions(ComboBox i_ComboBox)
        {
            foreach (EvaluationType evalType in Enum.GetValues(typeof(EvaluationType)))
            {
                i_ComboBox.Items.Add(evalType.ToString());
            }
        }
    }
}
