using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public enum eCheckerType
    {
        X, O, K, U, EmptyCell
    }

    public class Checker
    {
        private eCheckerType m_CheckerType;
        public Player m_Player;

        internal Checker(eCheckerType i_CheckerType, Player i_Player)
        {
            m_CheckerType = i_CheckerType;
            m_Player = i_Player;
        }

        internal Checker(Player i_CurrentPlayer, eCheckerType i_CheckerType)
        {
            m_Player = i_CurrentPlayer;
            m_CheckerType = i_CheckerType;
        }

        public Player Player
        {
            get
            {
                return m_Player;
            }
        }

        internal eCheckerType CheckerType
        {
            get
            {
                return this.m_CheckerType;
            }

            set
            {
                m_CheckerType = value;
            }
        }

        public override string ToString()
        {
            return m_CheckerType.ToString();
        }
    }
}