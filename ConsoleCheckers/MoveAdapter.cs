using System.Collections.Generic;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    internal class MoveAdapter : IMove
    {
        public bool isNextMoveCapture { get; set; } = false;
        public List<IMove> DoubleCapturesList { get; set; }
        CheckersMove m_Move;
        public MoveAdapter(CheckersMove i_Move) 
        {
            m_Move = i_Move;
        }

        public uint GetStartSquare()
        {
            return BitUtils.BitPositionToUInt((int)(m_Move.MoveValue & 0b11111));
        }

        public uint GetTargetSquare()
        {
            return BitUtils.BitPositionToUInt((int)(m_Move.MoveValue >> 5) & 0b11111);
        }

        public uint GetCaptureSquare()
        {
            return BitUtils.BitPositionToUInt((int)(m_Move.MoveValue >> 10) & 0b11111);
        }

        public bool IsCapture()
        {
            bool isCapture = (m_Move.MoveValue & 0b0111110000000000) != 0;
            return isCapture;
        }

        public bool IsPromotion()
        {
            return m_Move.IsPromotion();
        }

        public int GetIntStartSquare()
        {
            // use the appropriate conversion on get startSquare
            return PieceMethods.UIntToInt(GetStartSquare());
        }

        public int GetIntTargetSquare()
        {
            return PieceMethods.UIntToInt(GetTargetSquare());
        }

        public int GetIntCaptureSquare()
        {
            return PieceMethods.UIntToInt(GetCaptureSquare());
        }

        public bool IsDoubleCapture()
        {
            return isNextMoveCapture;
        }

        public int GetMovingPieceValue()
        {
            return (int)m_Move.GetMovingPieceType() - 1;
        }

        public int GetCapturedPieceValue()
        {
            return (int)m_Move.GetCapturedPieceType() - 1;
        }

        public void addDoubleCapture(IMove i_Move)
        {
            if (DoubleCapturesList == null)
            {
                DoubleCapturesList = new List<IMove>();
                isNextMoveCapture = true;
            }

            DoubleCapturesList.Add(i_Move);
        }

        public uint GetMoveValue()
        {
            return m_Move.MoveValue;
        }
    }
}