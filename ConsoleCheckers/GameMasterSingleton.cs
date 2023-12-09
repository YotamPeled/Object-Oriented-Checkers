using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public sealed class GameMasterSingleton
    {
        public Board Board { get; set; }
        public int QueenMoveLimit { get; set; }
        private GameMasterSingleton()
        {
            QueenMoveLimit = 1;
        }

        private static GameMasterSingleton s_Instance = null;
        public static GameMasterSingleton Instance
        {
            get
            {
                if (s_Instance is null)
                {
                    s_Instance = new GameMasterSingleton();
                }

                return s_Instance;
            }
        }
    }
}
