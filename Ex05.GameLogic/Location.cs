namespace GameLogic
{
    public struct Location
    {
        private int m_X;
        private int m_Y;

        public Location(int i_X, int i_Y)
        {
            this.m_X = i_X;
            this.m_Y = i_Y;
        }

        public int X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }

        public int Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }

        public override string ToString()
        {
            return "(" + this.m_X + "," + this.m_Y + ")";
        }
    }
}