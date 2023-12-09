using System.Windows.Forms;

namespace ConsoleCheckers
{
    internal class MoveAdapter : IMove
    {
        CheckersMove m_Move;
        public MoveAdapter(CheckersMove i_Move) 
        {
            m_Move = i_Move;
        }

        public uint GetStartSquare()
        {
            return BitUtils.BitPositionToUInt(m_Move.MoveValue & 0b0000000000011111);
        }

        public uint GetTargetSquare()
        {
            return BitUtils.BitPositionToUInt((m_Move.MoveValue & 0b0000001111100000) >> 5);
        }

        public uint GetCaptureSquare()
        {
            return BitUtils.BitPositionToUInt((m_Move.MoveValue & 0b0111110000000000) >> 10);
        }

        public bool IsCapture()
        {
            bool isCapture = (m_Move.MoveValue & 0b0111110000000000) != 0;
            return isCapture;
        }

        public bool IsPromotion()
        {
            bool isPromotion = ((m_Move.MoveValue & 0b1000000000000000) >> 15) == 1;
            return isPromotion;
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
    }
}