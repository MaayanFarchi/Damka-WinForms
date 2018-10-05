using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Player
    {
        private string m_Name;
        private int m_NumOfCheckersAlive;
        private int m_CuurentRoundScore;
        private int m_TotalScore;
        private bool m_MyTurn;
        private bool m_HasMoreMoves;

        internal Player(string i_Name, int i_NumOfCheckersAlive, bool i_IsYourTurn)
        {
            this.m_Name = i_Name;
            this.m_NumOfCheckersAlive = i_NumOfCheckersAlive;
            this.m_MyTurn = i_IsYourTurn;
            this.m_CuurentRoundScore = 0;
            this.m_TotalScore = 0;
            this.m_HasMoreMoves = true;
        }

        internal Player(string i_Name, int i_NumOfCheckersAlive, bool i_IsYourTurn, int i_CuurentRoundScore)
        {
            this.m_Name = i_Name;
            this.m_NumOfCheckersAlive = i_NumOfCheckersAlive;
            this.m_MyTurn = i_IsYourTurn;
            this.m_CuurentRoundScore = 0;
            this.m_TotalScore += i_CuurentRoundScore;
            this.m_HasMoreMoves = true;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        public int CuurentRoundScore
        {
            get
            {
                return m_CuurentRoundScore;
            }

            set
            {
                m_CuurentRoundScore = value;
            }
        }

        public int TotalScore
        {
            get
            {
                return m_TotalScore;
            }

            set
            {
                m_TotalScore = value;
            }
        }

        public int NumOfCheckersAlive
        {
            get
            {
                return m_NumOfCheckersAlive;
            }

            set
            {
                m_NumOfCheckersAlive = value;
            }
        }

        public bool MyTurn
        {
            get
            {
                return m_MyTurn;
            }

            set
            {
                m_MyTurn = value;
            }
        }

        public bool HasMoreMoves
        {
            get
            {
                return m_HasMoreMoves;
            }

            set
            {
                m_HasMoreMoves = value;
            }
        }
    }
}