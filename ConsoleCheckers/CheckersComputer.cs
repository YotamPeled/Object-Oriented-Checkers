using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    public class CheckersComputer : ICheckersComputer
    {
        private Func<uint[], int> EvaluationStrategyMethod;
        public IMove BestMove { get; set; }
        private uint[] m_BitBoards;
        private bool isDoubleCapture = false;

        public void GenerateMove()
        {
            eColor colorToMove = GameMasterSingleton.Instance.Board.Turn;
            m_BitBoards = GameMasterSingleton.Instance.Board.BitBoards;
            int searchDepth = GameMasterSingleton.Instance.ComputerDepth;
            int pieceCount = getPieceCount();
            searchDepth += addDepth(pieceCount);
            EvaluationNode bestEvaluation = new EvaluationNode() { evaluation = lostPositionEvaluation(colorToMove), depth = -1 };
            Func<int, int, int> evaluatingFunc = getMaxingFunc(colorToMove);
            foreach(IMove move in PieceMethods.GiveLegalMoves(m_BitBoards, colorToMove, isDoubleCapture))
            {
                PieceMethods.MakeMove(move, m_BitBoards);
                eColor movingColor = move.IsDoubleCapture() ? colorToMove : PieceMethods.SwapTurn(colorToMove);
                int goLower = movingColor == colorToMove ? 0 : 1;
                EvaluationNode currentMoveEvalNode = MiniMax(searchDepth - goLower, movingColor);
                PieceMethods.unMakeMove(move, m_BitBoards);
                bestEvaluation = giveBetterEvaluation(bestEvaluation, currentMoveEvalNode, colorToMove);
                if (bestEvaluation.Equals(currentMoveEvalNode))
                {
                    BestMove = move;
                }
            }

            isDoubleCapture = BestMove.IsDoubleCapture();
        }

        private EvaluationNode MiniMax(int i_Depth, eColor colorTurn, int numExtensions = 0, int alpha = int.MinValue, int beta = int.MaxValue)
        {
            if (i_Depth == 0)
            {
                int evaluation = EvaluationStrategyMethod(m_BitBoards);
                EvaluationNode node = new EvaluationNode() { evaluation = evaluation, depth = 0 };
                return node;
            }

            List<IMove> moves = PieceMethods.GiveLegalMoves(m_BitBoards, colorTurn);
            if(moves.Count == 0)
            {
                return new EvaluationNode() { evaluation = lostPositionEvaluation(colorTurn), depth = i_Depth};
            }

            EvaluationNode bestEvaluation = new EvaluationNode() { evaluation = lostPositionEvaluation(colorTurn), depth = -1 };
            Func<int, int, int> evaluatingFunc = getMaxingFunc(colorTurn);
            for(int i = 0; i < moves.Count; i++)
            {
                int extension = moves[i].IsCapture() && numExtensions <= 6 ? 1 : 0; 
                PieceMethods.MakeMove(moves[i], m_BitBoards);
                foreach (int _ in generatePositionAfterDoubleCaptures(moves[i]))
                {
                    EvaluationNode currentEvaluation = MiniMax(i_Depth - 1 + extension, PieceMethods.SwapTurn(colorTurn), numExtensions + extension, alpha, beta);
                    bestEvaluation = giveBetterEvaluation(bestEvaluation, currentEvaluation, colorTurn);
                    if (colorTurn == eColor.White)
                    {
                        alpha = Math.Max(alpha, bestEvaluation.evaluation);
                    }
                    else
                    {
                        beta = Math.Min(beta, bestEvaluation.evaluation);
                    }
                }

                PieceMethods.unMakeMove(moves[i], m_BitBoards);
                if (beta <= alpha)
                {
                    break; // Beta cut-off
                }
            }

            return bestEvaluation;
        }

        private IEnumerable<int> generatePositionAfterDoubleCaptures(IMove i_MoveToMake)
        {
            if (i_MoveToMake.IsDoubleCapture())
            {
                foreach (IMove moveDoubleCapture in i_MoveToMake.DoubleCapturesList)
                {
                    PieceMethods.MakeMove(moveDoubleCapture, m_BitBoards);
                    foreach (int bitBoard in generatePositionAfterDoubleCaptures(moveDoubleCapture))
                    {
                        yield return 1;
                    }

                    PieceMethods.unMakeMove(moveDoubleCapture, m_BitBoards);
                }
            }
            else
            {
                yield return 1;
            }
        }

        private EvaluationNode giveBetterEvaluation(EvaluationNode bestEvaluation, EvaluationNode currentEvaluation, eColor colorTurn)
        {
            EvaluationNode betterEvalNode;
            if (bestEvaluation.evaluation == currentEvaluation.evaluation)
            {
                // found at an earlier stage of the search
                betterEvalNode = bestEvaluation.depth > currentEvaluation.depth ? bestEvaluation : currentEvaluation;
            }
            else
            {
                Func<int, int, int> maxingFunc = getMaxingFunc(colorTurn);
                int bestEval = maxingFunc(bestEvaluation.evaluation, currentEvaluation.evaluation);
                betterEvalNode = bestEval == bestEvaluation.evaluation ? bestEvaluation : currentEvaluation;
            }

            return betterEvalNode;
        }

        private ulong SimpleHash(uint[] bitBoards)
        {
            ulong hash = 0;
            ulong prime = 1099511628211UL; // A large prime number

            foreach (uint board in bitBoards)
            {
                hash ^= board * prime;
                prime *= 31; // Update the prime number for diversity
            }

            return hash;
        }   

        private Func<int, int, int> getMaxingFunc(eColor i_Color)
        {
            if (i_Color == eColor.White)
            {
                return Math.Max;
            }
            else
            {
                return Math.Min;
            }
        }

        private int lostPositionEvaluation(eColor i_Color)
        {
            return i_Color == eColor.White ? int.MinValue : int.MaxValue;
        }

        public void SetEvaluationStrategy(Func<uint[], int> evaluationStrategy)
        {
            this.EvaluationStrategyMethod = evaluationStrategy;
        }

        private int getPieceCount()
        {
            int count = 0;
            foreach(uint bitBoard in m_BitBoards)
            {
                count += BitUtils.GetSetBitsAmount(bitBoard);
            }

            return count;
        }

        private int addDepth(int pieceCount)
        {
            int depthToAdd = 0;
            if (pieceCount <= 4)
            {
                depthToAdd = 4;
            }
            else if (pieceCount <= 6)
            {
                depthToAdd = 3;
            }
            else if (pieceCount <= 8)
            {
                depthToAdd = 2;
            }

            return depthToAdd;
        }

        public struct TranspositionEntry
        {
            public int evaluation;
            public int depth;

            // You can add more fields if needed
        }

        private struct EvaluationNode
        {
            public int evaluation { get; set; }
            public int depth { get; set; }
        }
    }
}
