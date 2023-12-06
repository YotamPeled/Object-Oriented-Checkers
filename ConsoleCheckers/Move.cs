using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public struct Move
    {
        public bool IsCapture { get; set; }
        public uint Capture { get; set; }
        public uint Origin { get; set; }
        public uint Destination { get; set; }
    }
}
