using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public enum eGameMode
    {
        UserVsComputer, UserVsUser
    }

    public struct GameSettings
    {
        private int m_Size;
        private eGameMode m_Mode;
        private string m_PlayerName1;
        private string m_PlayerName2;

        public int Size
        {
            get
            {

                return m_Size;
            }

            set
            {
                m_Size = value;
            }
        }

        public string PlayerName1
        {
            get
            {

                return m_PlayerName1;
            }

            set
            {
                m_PlayerName1 = value;
            }
        }

        public string PlayerName2
        {
            get
            {

                return m_PlayerName2;
            }

            set
            {
                m_PlayerName2 = value;
            }
        }

        public eGameMode GameMode
        {
            get
            {

                return m_Mode;
            }

            set
            {
                m_Mode = value;
            }
        }
    }
}