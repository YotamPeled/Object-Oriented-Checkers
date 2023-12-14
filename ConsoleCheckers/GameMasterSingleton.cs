using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckers
{
    public sealed class GameMasterSingleton
    {
        public bool isPlayingAgainstComputer { get; set; } = true;
        public bool PlayerTurn { get; set; } = true; // true is Human, false is Computer
        private IMatchManager m_MatchManager;
        public IMatchManager MatchManager { get { return m_MatchManager; } }
        
        private Board m_Board;
        public Board Board
        {
            get { return m_Board; }
            set
            {
                m_Board = value;
            }
        }
        public int QueenMoveLimit { get; set; }
        private GameMasterSingleton()
        {
            QueenMoveLimit = 1;
        }
        
        public int ComputerDepth { get; set; } = 8;

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

        public void StartMatch()
        {
            m_MatchManager?.StartMatch();
        }

        public void SetUpComputerMatch(Func<uint[], int> Computer1EvalStrategy, Func<uint[], int> Computer2EvalStrategy, bool i_IsComputer1Starting)
        {
            bool isSettingForComputer1 = true;
            CheckersComputerMatchManager matchManager = new CheckersComputerMatchManager(m_Board, i_IsComputer1Starting);
            matchManager.SetStrategy(Computer1EvalStrategy, isSettingForComputer1);
            matchManager.SetStrategy(Computer2EvalStrategy, !isSettingForComputer1);
            m_MatchManager = matchManager;
        }

        public void SetUp1PlayerMatch(Func<uint[], int> ComputerEvalStrategy, IUI i_UI, bool i_IsPlayerStarting)
        {
            OnePlayerMatchManager matchManager = new OnePlayerMatchManager(m_Board, i_IsPlayerStarting, i_UI);
            matchManager.SetStrategy(ComputerEvalStrategy);
            m_MatchManager = matchManager;
        }

        internal void SetUp2PlayerMatch(IUI i_UI)
        {
            TwoPlayerMatchManager matchManager = new TwoPlayerMatchManager(m_Board, i_UI);
            m_MatchManager = matchManager;
        }
    }
}
