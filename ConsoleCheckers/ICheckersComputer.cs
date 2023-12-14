using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public interface ICheckersComputer
    {
        void SetEvaluationStrategy(Func<uint[], int> evaluationStrategy);
        void GenerateMove();
        IMove BestMove { get; set; }
    }
}
