using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public interface IMove
    {
        // change in the future to properties
        List<IMove> DoubleCapturesList { get; set; }
        uint GetMoveValue();
        void addDoubleCapture(IMove i_Move);
        int GetMovingPieceValue();
        int GetCapturedPieceValue();
        uint GetTargetSquare();
        uint GetStartSquare();
        uint GetCaptureSquare();
        bool IsCapture();
        bool IsPromotion();
        bool IsDoubleCapture();
    }
}
