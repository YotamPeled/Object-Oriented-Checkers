using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public interface IMove
    {
        int GetIntStartSquare();
        int GetIntTargetSquare();
        int GetIntCaptureSquare();
        uint GetTargetSquare();
        uint GetStartSquare();
        uint GetCaptureSquare();
        bool IsCapture();
        bool IsPromotion();
    }
}
