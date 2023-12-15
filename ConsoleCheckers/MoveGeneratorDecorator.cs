using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public class MoveGeneratorDecorator : IMoveGenerator
    {
        private IMoveGenerator m_MoveGenerator;

        public MoveGeneratorDecorator(IMoveGenerator i_MoveGenerator)
        {
                m_MoveGenerator = i_MoveGenerator;
        }

        public virtual List<IMove> GiveLegalMoves(uint[] i_BitBoards, eColor i_ColorToMove)
        {
            return m_MoveGenerator.GiveLegalMoves(i_BitBoards, i_ColorToMove);
        }
    }
}
