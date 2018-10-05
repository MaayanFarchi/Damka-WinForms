using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Board
    {
        public readonly BoardCell[,] m_BoardArray;
        private int m_BoardSize;

        public class BoardCell
        {
            private Location m_Location;
            private Checker m_Checker;

            public BoardCell(Location i_Location)
            {
                m_Location = i_Location;
                m_Checker = null;
            }

            public BoardCell(Location i_Location, Checker i_Checker)
            {
                m_Location = i_Location;
                m_Checker = i_Checker;
            }

            public Location Location
            {
                get
                {
                    return m_Location;
                }

                set
                {
                    m_Location = value;
                }
            }

            public Checker Checker
            {
                get
                {
                    return m_Checker;
                }

                set
                {
                    m_Checker = value;
                }
            }
        }

        internal Board(int i_BoardSize, Player i_Player1, Player i_Player2)
        {
            m_BoardArray = new BoardCell[i_BoardSize, i_BoardSize];
            m_BoardSize = m_BoardArray.GetUpperBound(0);

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if (i < ((i_BoardSize / 2) - 1) && (i + j) % 2 != 0)
                    {
                        m_BoardArray[i, j] = new BoardCell(new Location(i, j));
                        m_BoardArray[i, j].Checker = new Checker(eCheckerType.O, i_Player1);
                    }
                    else if (i >= ((i_BoardSize / 2) + 1) && (i + j) % 2 != 0)
                    {
                        m_BoardArray[i, j] = new BoardCell(new Location(i, j));
                        m_BoardArray[i, j].Checker = new Checker(eCheckerType.X, i_Player2);
                    }
                    else
                    {
                        m_BoardArray[i, j] = new BoardCell(new Location(i, j));
                    }
                }
            }
        }

        public BoardCell[,] BoardArray
        {
            get
            {

                return m_BoardArray;
            }
        }

        public int BoardSize
        {
            get
            {

                return m_BoardSize;
            }
        }
    }
}
