using System.Windows.Forms;
using GameLogic;

namespace GameInterface
{
    internal class BoardButton : Button
    {
        internal Location m_BoardLocation;

        public BoardButton(Location i_BoardLocation)
        {
            m_BoardLocation = i_BoardLocation;
        }

        public Location BoardLocation
        {
            get
            {
                return m_BoardLocation;
            }

            set
            {
                m_BoardLocation = value;
            }
        }
    }
}