using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public static class BitUtils
    {
        public static uint ExtractBits(uint number, int startBit, int endBit)
        {
            // Calculate the mask for the desired bits
            uint mask = ((1U << (endBit - startBit + 1)) - 1) << startBit;

            // Extract the bits using the mask
            uint extractedBits = (number & mask) >> startBit;

            return extractedBits;
        }

        public static List<uint> GetSetBits(uint number)
        {
            List<uint> bitList = new List<uint>();

            for (int i = 0; i < 32; i++)
            {
                if ((number & (1 << i)) != 0)
                {
                    bitList.Add((uint)(0b01 << i));
                }
            }

            return bitList;
        }

        public static string ToBitString(uint value)
        {
            return Convert.ToString(value, 2).PadLeft(32, '0');
        }

        public static int FindBitPosition(uint number)
        {
            int position = -1;

            while (number > 0)
            {
                position++;
                number >>= 1;
            }

            return position;
        }
    }
}
