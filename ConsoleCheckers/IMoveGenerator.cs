using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public interface IMoveGenerator
    {
        List<IMove> GiveLegalMoves(uint[] i_BitBoards, eColor i_ColorToMove);
    }
}
