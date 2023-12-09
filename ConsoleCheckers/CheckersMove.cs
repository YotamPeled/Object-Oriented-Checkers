namespace ConsoleCheckers
{
    public readonly struct CheckersMove
    {
        // Move data packed into a 16 bit value
        // the format is as follow FCCCCCTTTTTSSSSS
        // F = promotion flag, C = Capture, T = Target, S = Start
        private readonly ushort moveValue;
        public ushort MoveValue { get { return moveValue; } }
        //flags
        public const byte NoFlag = 0b0;
        public const byte promotionFlag = 0b00000001;
        public const byte NoCapture = 0b0;
        //Create Move
        public CheckersMove(uint i_StartSquare, uint i_TargetSquare, uint i_CaptureSquare = NoCapture)
        {
            //32 legal playing squares => all these int values are 5 bit values representing a valid playing square
            int startSquare = BitUtils.FindBitPosition(i_StartSquare);
            int targetSquare = BitUtils.FindBitPosition(i_TargetSquare);
            int captureSquare = i_CaptureSquare == NoCapture ? 0 : BitUtils.FindBitPosition(i_CaptureSquare);
            byte flag = getFlag(i_TargetSquare);

            moveValue = (ushort)(startSquare | targetSquare << 5 | captureSquare << 10 | flag << 15);
        }

        private static byte getFlag(uint i_TargetSquare)
        {
            bool whitePromotion = (i_TargetSquare & 0xF0000000) != 0;
            bool blackPromotion = (i_TargetSquare & 0x0000000F) != 0;

            return whitePromotion || blackPromotion ? promotionFlag : NoFlag;
        }
    }
}