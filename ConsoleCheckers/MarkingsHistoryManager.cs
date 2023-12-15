using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    public class MarkingsHistoryManager
    {
        private List<MarkingsNode> m_MarkingsHistory = new List<MarkingsNode>();
        private MarkingsNode currentlyViewing;

        public void addToHistory(MarkingsNode i_Node)
        {
            i_Node.ClearHighlightedLegalMoves();
            m_MarkingsHistory.Add(i_Node);
        }

        public void ViewHistory(int i_PositionNumber)
        {           
            currentlyViewing?.Hide();
            MarkingsNode nodeToView = m_MarkingsHistory[i_PositionNumber];
            nodeToView.Show();
            currentlyViewing = nodeToView;
        }

        public void StopViewingHistory()
        {           
            currentlyViewing?.Hide();
            currentlyViewing = null;          
        }
    }
}
