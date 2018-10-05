using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex05.GameInterface.Properties;
using GameLogic;

namespace GameInterface
{
    public class UIManager
    {
        public GameManager m_GameManager;
        public GameBoard m_GameBoard;

        internal Image BlackKingImage = Resources.KingBlack;
        internal Image RedKingImage = Resources.King__Red_;
        internal Image BlackPawnImage = Resources.Pawn__Black_;
        internal Image RedPawnImage = Resources.Pawn__Red_;

        public UIManager(GameSettings i_settings)
        {
            m_GameBoard = new GameBoard(i_settings);
            m_GameManager = new GameManager(i_settings);
            m_GameManager.GameMove += GameMove_CheckerWasMoved;
            m_GameManager.EatChecker += EatChecker_CheckerWasEaten;
            m_GameManager.TurnSwap += TurnSwap_TurnsWhereSwapped;
            m_GameManager.RoundEnded += RoundEnded_RoundHasEnded;
            m_GameManager.DoubleEat += DoubleEat_ADoubleEatWasMade;
            m_GameBoard.BoardMove += BoardMove_CheckerWasMoved;
        }

        public void StartGame()
        {
            m_GameBoard.BoardForm.ShowDialog();
        }

        private void EatChecker_CheckerWasEaten(Location i_LocateThatChange)
        {
            m_GameBoard.CleanEatenCheckers(i_LocateThatChange);
        }

        private void GameMove_CheckerWasMoved(Location i_LocateThatChange, eCheckerType i_ChangeToThisType)
        {
            Image newImageToButton;

            switch (i_ChangeToThisType)
            {
                default:
                case eCheckerType.EmptyCell:
                    newImageToButton = null;
                    break;
                case eCheckerType.X:
                    newImageToButton = RedPawnImage;
                    break;
                case eCheckerType.O:
                    newImageToButton = BlackPawnImage;
                    break;
                case eCheckerType.U:
                    newImageToButton = BlackKingImage;
                    break;
                case eCheckerType.K:
                    newImageToButton = RedKingImage;
                    break;
            }
            m_GameBoard.ChangeCheckerTypeOnButton(i_LocateThatChange, newImageToButton);
        }

        internal Location DoubleEat_ADoubleEatWasMade(string i_StrLocateThatChange)
        {
            string sourceStr = i_StrLocateThatChange[0] + "" + i_StrLocateThatChange[1];

            Location LocationToChange = ConvertStrMoveToLocation(sourceStr);

            return LocationToChange;
        }

        private void BoardMove_CheckerWasMoved(Location i_Source, Location i_Dest)
        {
            m_GameBoard.TurnPassed = false;

            if (!m_GameManager.GameOver)
            {
                string move = ConverLocationToStrMove(i_Source, i_Dest);
                bool legalMove = m_GameManager.DoMove(move, m_GameManager.GameBoard, true);

                if (!legalMove)
                {
                    OperateIlegalMove();
                }

                else
                {

                    m_GameBoard.TurnPassed = true;
                    m_GameBoard.CleanAfterMove();
                    if (m_GameManager.GameMode == eGameMode.UserVsComputer)
                    {
                        OperateComputerMove();
                    }

                    m_GameManager.UpdateKings(m_GameManager.GameBoard);
                    OpreateDoubleEatenCheckers();
                    m_GameManager.SwipeTurns();
                    m_GameManager.UpdateGameStatus();

                }
            }
        }

        private void RoundEnded_RoundHasEnded(string i_GameOverMessage)
        {
            DialogResult result = MessageBox.Show(i_GameOverMessage, "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                m_GameBoard.Close();
            }
            else
            {
                OpreateAnotherRound();
            }
        }

        private void OpreateAnotherRound()
        {
            m_GameBoard.LabelPlayer1.Text = string.Format("{0}:{1}", m_GameManager.Player1.Name, m_GameManager.Player1.TotalScore);
            m_GameBoard.LabelPlayer2.Text = string.Format("{0}:{1}", m_GameManager.Player2.Name, m_GameManager.Player2.TotalScore);
            m_GameBoard.CleanBoard();
            m_GameManager.ResetGame(m_GameBoard.SizeOfBoard);
        }

        private void TurnSwap_TurnsWhereSwapped(Player i_CurrentPlayerPlaying)
        {
            Label labelOfCurrentPlayer = i_CurrentPlayerPlaying == m_GameManager.Player1 ? m_GameBoard.LabelPlayer1 : m_GameBoard.LabelPlayer2;
            Label labelOfOtherPlayer = i_CurrentPlayerPlaying != m_GameManager.Player1 ? m_GameBoard.LabelPlayer1 : m_GameBoard.LabelPlayer2;
            labelOfCurrentPlayer.ForeColor = Color.Blue;
            labelOfOtherPlayer.ForeColor = Color.Black;
        }

        private void OperateIlegalMove()
        {
            DialogResult error = MessageBox.Show("Ilegall Move, please try again", "Damka", MessageBoxButtons.OK, MessageBoxIcon.Question);
            m_GameBoard.CleanAfterIlegallMove();
            m_GameBoard.SourceCell = null;
            m_GameBoard.DestnationCell = null;
        }

        private void OperateComputerMove()
        {
            m_GameManager.SwipeTurns();
            string computerMove = m_GameManager.DoComputerMove(m_GameManager.GameBoard);
            string sourceStr = computerMove[0] + "" + computerMove[1];
            m_GameBoard.SourceCell = ConvertStrMoveToLocation(sourceStr);
            m_GameBoard.CleanAfterMove();
        }

        private void OpreateDoubleEatenCheckers()
        {
            if (m_GameManager.LocationToChange.X != -1)
            {
                m_GameBoard.CleanEatenCheckers(m_GameManager.LocationToChange);
                m_GameManager.LocationToChange = new Location(-1, -1);
            }
        }

        public static Location ConvertStrMoveToLocation(string sourceStr)
        {
            int c1 = sourceStr[0] - 65;
            int c2 = sourceStr[1] - 97;
            Location location = new Location(c2, c1);

            return location;
        }

        public static string ConverLocationToStrMove(Location i_source, Location i_dest)
        {
            return char.ConvertFromUtf32(i_source.Y + 65) + char.ConvertFromUtf32(i_source.X + 97)
                + '>' + char.ConvertFromUtf32(i_dest.Y + 65) + char.ConvertFromUtf32(i_dest.X + 97);
        }
    }
}
