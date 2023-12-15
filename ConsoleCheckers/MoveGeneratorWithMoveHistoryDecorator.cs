using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    internal class MoveGeneratorWithMoveHistoryDecorator : MoveGeneratorDecorator
    {
        private List<IMove> previousGeneration;
        public void ResetPreviousGeneration()
        {
            previousGeneration.Clear();
        }

        public MoveGeneratorWithMoveHistoryDecorator(IMoveGenerator i_MoveGenerator) : base(i_MoveGenerator)
        {
        }

        public override List<IMove> GiveLegalMoves(uint[] i_BitBoards, eColor i_ColorToMove)
        {
            List<IMove> moveList = new List<IMove>();

            if (previousGeneration != null)
            {
                giveDoubleCapturesAfterCapture(i_BitBoards, moveList);
            }

            if (moveList.Count == 0)
            {
                moveList = base.GiveLegalMoves(i_BitBoards, i_ColorToMove);
            }

            previousGeneration = moveList;
            return moveList;
        }

        private void giveDoubleCapturesAfterCapture(uint[] i_BitBoards, List<IMove> moveList)
        {
            uint allPieces = i_BitBoards[0] | i_BitBoards[1] | i_BitBoards[2] | i_BitBoards[3];
            List<IMove> doubleCaptureMoves = previousGeneration.Where(IMove => IMove.IsDoubleCapture()).ToList();
            foreach (IMove move in doubleCaptureMoves)
            {
                // find the single move where double capture occured
                if ((move.GetTargetSquare() & allPieces) != 0)
                {
                    moveList.AddRange(move.DoubleCapturesList);
                    break;
                }
            }
        }
    }
}
