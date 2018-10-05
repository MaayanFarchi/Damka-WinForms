using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GameLogic;

public delegate Location WasADoubleEat(string i_Message);

namespace GameLogic
{
    public class GameManager
    {
        private Player m_Player1;
        private Player m_Player2;
        public Board m_GameBoard;
        private eGameMode m_GameMode;
        private bool m_GameOver;
        public Location m_LocationToChange;

        public event Action<Location, eCheckerType> GameMove;

        public event Action<Location> EatChecker;

        public event Action<Player> TurnSwap;

        public Action<string> RoundEnded;

        public event WasADoubleEat DoubleEat;

        public GameManager(GameSettings i_Settings)
        {
            double numOfCheckers = CalcNumOfCheckers(i_Settings.Size);

            m_Player1 = new Player(i_Settings.PlayerName1, (int)numOfCheckers, true);
            m_Player2 = new Player(i_Settings.PlayerName2, (int)numOfCheckers, false);
            m_GameBoard = new Board(i_Settings.Size, m_Player1, m_Player2);
            m_GameMode = i_Settings.GameMode;
            m_GameOver = false;
            m_LocationToChange = new Location(-1, -1);
        }

        public Location LocationToChange
        {
            get
            {
                return m_LocationToChange;
            }

            set
            {
                m_LocationToChange = value;
            }
        }

        internal double CalcNumOfCheckers(int i_BoardSize)
        {
            double numOfCheckers = 0;

            if (i_BoardSize == 6)
            {
                numOfCheckers = 6;
            }
            else if (i_BoardSize == 8)
            {
                numOfCheckers = 12;
            }
            else if (i_BoardSize == 10)
            {
                numOfCheckers = 20;
            }

            return numOfCheckers;
        }

        public Board GameBoard
        {
            get
            {

                return m_GameBoard;
            }
        }

        public eGameMode GameMode
        {
            get
            {
                return m_GameMode;
            }
        }

        public bool GameOver
        {
            get
            {

                return m_GameOver;
            }

            set
            {
                m_GameOver = value;
            }
        }

        public Player Player1
        {
            get
            {
                return m_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }
        }

        protected virtual void OnGameMove(Location i_DestnationCell, eCheckerType i_Checker)
        {
            if (GameMove != null)
            {
                GameMove.Invoke(i_DestnationCell, i_Checker);
            }
        }

        protected virtual void OnEatChecker(Location i_DestnationCell)
        {
            if (EatChecker != null)
            {
                EatChecker.Invoke(i_DestnationCell);
            }
        }

        public virtual void OnRoundEnded()
        {
            bool player1won = this.Player1.CuurentRoundScore > this.Player2.CuurentRoundScore;

            bool draw = this.Player1.CuurentRoundScore == this.Player2.CuurentRoundScore;

            string winnerName = player1won ? this.Player1.Name : this.Player2.Name;

            int winnerScore = player1won ? this.Player1.TotalScore : this.Player2.TotalScore;

            int loserScore = !player1won ? this.Player1.TotalScore : this.Player2.TotalScore;

            string GameOverMessage;

            if (!draw)
            {
                GameOverMessage = string.Format(@"{0} won! total point balance: {1}:{2} 
Another round?", winnerName, winnerScore, loserScore);
            }
            else
            {
                GameOverMessage = string.Format(@"Tie: {0}:{1}
Another round?", winnerScore, loserScore);
            }

            if (RoundEnded != null)
            {
                RoundEnded.Invoke(GameOverMessage);
            }
        }

        public void ResetGame(int i_BoardSize)
        {
            double numOfCheckers = CalcNumOfCheckers(i_BoardSize);

            m_Player1 = new Player(m_Player1.Name, (int)numOfCheckers, true, m_Player1.CuurentRoundScore);
            m_Player2 = new Player(m_Player2.Name, (int)numOfCheckers, false, m_Player2.CuurentRoundScore);
            m_GameBoard = new Board(i_BoardSize, m_Player1, m_Player2);
            m_LocationToChange = new Location(-1, -1);
            m_GameOver = false;
            OnTurnSwap();
        }

        public string DoComputerMove(Board i_GameBoard)
        {
            bool aMoveWasMade = false;

            for (int i = m_GameBoard.BoardSize; i >= 0; i--)
            {
                for (int j = m_GameBoard.BoardSize; j >= 0; j--)
                {
                    if (m_GameBoard.BoardArray[i, j].Checker != null)
                    {
                        if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.O)
                        {
                            string eatDownAndRight = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                            ">" + char.ConvertFromUtf32(j + 67) + char.ConvertFromUtf32(i + 99);

                            aMoveWasMade |= DoMove(eatDownAndRight, m_GameBoard, false);
                            if (aMoveWasMade)
                            {

                                return eatDownAndRight;
                            }

                            string eatDownAndLeft = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                                ">" + char.ConvertFromUtf32(j + 63) + char.ConvertFromUtf32(i + 99);

                            aMoveWasMade |= DoMove(eatDownAndLeft, m_GameBoard, false);
                            if (aMoveWasMade)
                            {

                                return eatDownAndLeft;
                            }
                        }
                    }
                }
            }
            for (int i = m_GameBoard.BoardSize; i >= 0; i--)
            {
                for (int j = m_GameBoard.BoardSize; j >= 0; j--)
                {
                    if (m_GameBoard.BoardArray[i, j].Checker != null)
                    {
                        if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.O)
                        {
                            string moveDownAndRight = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                            ">" + char.ConvertFromUtf32(j + 66) + char.ConvertFromUtf32(i + 98);

                            aMoveWasMade |= DoMove(moveDownAndRight, m_GameBoard, false);
                            if (aMoveWasMade)
                            {

                                return moveDownAndRight;
                            }

                            string moveDownAndLeft = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                                ">" + char.ConvertFromUtf32(j + 64) + char.ConvertFromUtf32(i + 98);

                            aMoveWasMade |= DoMove(moveDownAndLeft, m_GameBoard, false);
                            if (aMoveWasMade)
                            {

                                return moveDownAndLeft;
                            }
                        }
                    }
                }
            }

            m_Player2.HasMoreMoves = false;

            return "no more moves";
        }

        public bool DoMove(string i_Move, Board i_Board, bool i_IsFirstMoveForChecker)
        {
            bool aMoveWasMade = false;
            bool aDoubleMoveWasMade = false;

            Board.BoardCell sourceCell, destinationCell;

            int sourceCellX, sourceCellY, destinationCellX, destinationCellY;

            sourceCellX = Convert.ToInt32(i_Move[0]) - 65;
            sourceCellY = Convert.ToInt32(i_Move[1]) - 97;
            destinationCellX = Convert.ToInt32(i_Move[3]) - 65;
            destinationCellY = Convert.ToInt32(i_Move[4]) - 97;

            eCheckerType checker = eCheckerType.EmptyCell;

            if (ValidationProcess.IsLegalMoveFormat(i_Move, i_Board.BoardSize))
            {
                sourceCell = i_Board.BoardArray[sourceCellY, sourceCellX];

                destinationCell = i_Board.BoardArray[destinationCellY, destinationCellX];

                if (ValidationProcess.IsLegalMove(sourceCell, destinationCell, i_Board, Player1.MyTurn ? Player2.Name : Player1.Name))
                {
                    aMoveWasMade = true;
                    destinationCell.Checker = sourceCell.Checker;
                    destinationCell.Checker.Player.Name = sourceCell.Checker.Player.Name;
                    checker = sourceCell.Checker.CheckerType;
                    sourceCell.Checker = null;

                    if (Math.Abs(destinationCellY - sourceCellY) == 2)
                    {
                        //eating down
                        if (destinationCellY - sourceCellY == 2)
                        {
                            //eating down & right
                            if (destinationCellX - sourceCellX == 2)
                            {
                                i_Board.BoardArray[sourceCellY + 1, sourceCellX + 1].Checker.Player.NumOfCheckersAlive--;
                                i_Board.BoardArray[sourceCellY + 1, sourceCellX + 1].Checker = null;
                                OnEatChecker(new Location(sourceCellY + 1, sourceCellX + 1));
                            }
                            //eating down & left
                            else
                            {
                                i_Board.BoardArray[sourceCellY + 1, sourceCellX - 1].Checker.Player.NumOfCheckersAlive--;
                                i_Board.BoardArray[sourceCellY + 1, sourceCellX - 1].Checker = null;
                                OnEatChecker(new Location(sourceCellY + 1, sourceCellX - 1));

                            }
                            string nextMoveDownAndRight = "" + i_Move[3] + i_Move[4] + ">" +
                            char.ConvertFromUtf32(Convert.ToInt32(i_Move[3]) + 2) +
                            char.ConvertFromUtf32(Convert.ToInt32(i_Move[4]) + 2);
                            aDoubleMoveWasMade = DoMove(nextMoveDownAndRight, i_Board, false);
                            if (aDoubleMoveWasMade)
                            {
                                OnDoubleEat(nextMoveDownAndRight);
                            }

                            string nextMoveDownAndLeft = "" + i_Move[3] + i_Move[4] + ">" +
                            char.ConvertFromUtf32(Convert.ToInt32(i_Move[3]) - 2) +
                            char.ConvertFromUtf32(Convert.ToInt32(i_Move[4]) + 2);
                            aDoubleMoveWasMade = DoMove(nextMoveDownAndLeft, i_Board, false);
                            if (aDoubleMoveWasMade)
                            {
                                OnDoubleEat(nextMoveDownAndLeft);
                            }
                        }
                        //eating up
                        else
                        {
                            //eating up & right
                            if (destinationCellX - sourceCellX == 2)
                            {
                                i_Board.BoardArray[sourceCellY - 1, sourceCellX + 1].Checker.Player.NumOfCheckersAlive--;
                                i_Board.BoardArray[sourceCellY - 1, sourceCellX + 1].Checker = null;
                                OnEatChecker(new Location(sourceCellY - 1, sourceCellX + 1));
                            }
                            //eating up & left
                            else
                            {
                                i_Board.BoardArray[sourceCellY - 1, sourceCellX - 1].Checker.Player.NumOfCheckersAlive--;
                                i_Board.BoardArray[sourceCellY - 1, sourceCellX - 1].Checker = null;
                                OnEatChecker(new Location(sourceCellY - 1, sourceCellX - 1));
                            }

                            string nextMoveUpAndRight = "" + i_Move[3] + i_Move[4] + ">" +
                            char.ConvertFromUtf32(Convert.ToInt32(i_Move[3]) + 2) +
                            char.ConvertFromUtf32(Convert.ToInt32(i_Move[4]) - 2);

                            aDoubleMoveWasMade = DoMove(nextMoveUpAndRight, i_Board, false);
                            if (aDoubleMoveWasMade)
                            {
                                OnDoubleEat(nextMoveUpAndRight);
                            }

                            string nextMoveUpAndLeft = "" + i_Move[3] + i_Move[4] + ">" +
                            char.ConvertFromUtf32(Convert.ToInt32(i_Move[3]) - 2) +
                            char.ConvertFromUtf32(Convert.ToInt32(i_Move[4]) - 2);

                            aDoubleMoveWasMade = DoMove(nextMoveUpAndLeft, i_Board, false);
                            if (aDoubleMoveWasMade)
                            {
                                OnDoubleEat(nextMoveUpAndLeft);
                            }
                        }
                    }
                }
            }

            Location destnationLocation = new Location(destinationCellY, destinationCellX);
            if (aMoveWasMade)
            {
                OnGameMove(destnationLocation, checker);
            }

            return aMoveWasMade;
        }

        public virtual void OnDoubleEat(string i_LocatioOfEatenChecker)
        {
            if (DoubleEat != null)
            {
                m_LocationToChange = DoubleEat.Invoke(i_LocatioOfEatenChecker);
            }
        }

        private bool player1HasMoreMoves(Board i_Board)
        {
            bool aMoveWasMade = false;

            for (int i = m_GameBoard.BoardSize; i >= 0; i--)
            {
                for (int j = m_GameBoard.BoardSize; j >= 0; j--)
                {
                    if (m_GameBoard.BoardArray[i, j].Checker != null)
                    {
                        if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.X)
                        {
                            string eatUpAndLeft = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                            ">" + char.ConvertFromUtf32(j + 67) + char.ConvertFromUtf32(i + 99);

                            aMoveWasMade |= virtualDoMove(eatUpAndLeft, m_GameBoard, false);

                            if (aMoveWasMade)
                            {
                                return true;
                            }

                            string eatUpAndRight = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                                ">" + char.ConvertFromUtf32(j + 63) + char.ConvertFromUtf32(i + 99);

                            aMoveWasMade |= virtualDoMove(eatUpAndRight, m_GameBoard, false);

                            if (aMoveWasMade)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            for (int i = m_GameBoard.BoardSize; i >= 0; i--)
            {
                for (int j = m_GameBoard.BoardSize; j >= 0; j--)
                {
                    if (m_GameBoard.BoardArray[i, j].Checker != null)
                    {
                        if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.O)
                        {
                            string moveUpAndLeft = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                            ">" + char.ConvertFromUtf32(j + 66) + char.ConvertFromUtf32(i + 98);

                            aMoveWasMade |= virtualDoMove(moveUpAndLeft, m_GameBoard, false);
                            if (aMoveWasMade)
                            {
                                return true;
                            }
                            string moveUpAndRight = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                                ">" + char.ConvertFromUtf32(j + 64) + char.ConvertFromUtf32(i + 98);

                            aMoveWasMade |= virtualDoMove(moveUpAndRight, m_GameBoard, false);
                            if (aMoveWasMade)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool player2HasMoreMoves(Board i_Board)
        {
            return virtualDoComputerMove(i_Board);
        }

        public bool virtualDoMove(string i_Move, Board i_Board, bool i_IsFirstMoveForChecker)
        {
            bool aMoveWasMade = false;
            Board.BoardCell sourceCell, destinationCell;

            int sourceCellX, sourceCellY, destinationCellX, destinationCellY;

            sourceCellX = Convert.ToInt32(i_Move[0]) - 65;
            sourceCellY = Convert.ToInt32(i_Move[1]) - 97;
            destinationCellX = Convert.ToInt32(i_Move[3]) - 65;
            destinationCellY = Convert.ToInt32(i_Move[4]) - 97;

            if (ValidationProcess.IsLegalMoveFormat(i_Move, i_Board.BoardSize))
            {
                sourceCell = i_Board.BoardArray[sourceCellY, sourceCellX];
                destinationCell = i_Board.BoardArray[destinationCellY, destinationCellX];

                if (ValidationProcess.VirtualIsLegalMove(sourceCell, destinationCell, i_Board, Player1.MyTurn ? Player2.Name : Player1.Name))
                {
                    aMoveWasMade = true;
                }
            }

            return aMoveWasMade;
        }

        private bool virtualDoComputerMove(Board i_GameBoard)
        {
            bool aMoveWasMade = false;

            for (int i = m_GameBoard.BoardSize; i >= 0; i--)
            {
                for (int j = m_GameBoard.BoardSize; j >= 0; j--)
                {
                    if (m_GameBoard.BoardArray[i, j].Checker != null)
                    {
                        if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.O)
                        {
                            string eatDownAndRight = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                            ">" + char.ConvertFromUtf32(j + 67) + char.ConvertFromUtf32(i + 99);

                            aMoveWasMade |= virtualDoMove(eatDownAndRight, m_GameBoard, false);
                            if (aMoveWasMade)
                            {
                                return true;
                            }

                            string eatDownAndLeft = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                                ">" + char.ConvertFromUtf32(j + 63) + char.ConvertFromUtf32(i + 99);

                            aMoveWasMade |= virtualDoMove(eatDownAndLeft, m_GameBoard, false);
                            if (aMoveWasMade)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            for (int i = m_GameBoard.BoardSize; i >= 0; i--)
            {
                for (int j = m_GameBoard.BoardSize; j >= 0; j--)
                {
                    if (m_GameBoard.BoardArray[i, j].Checker != null)
                    {
                        if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.O)
                        {
                            string moveDownAndRight = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                            ">" + char.ConvertFromUtf32(j + 66) + char.ConvertFromUtf32(i + 98);

                            aMoveWasMade |= virtualDoMove(moveDownAndRight, m_GameBoard, false);
                            if (aMoveWasMade)
                            {
                                return true;
                            }
                            string moveDownAndLeft = "" + char.ConvertFromUtf32(j + 65) + char.ConvertFromUtf32(i + 97) +
                                ">" + char.ConvertFromUtf32(j + 64) + char.ConvertFromUtf32(i + 98);

                            aMoveWasMade |= virtualDoMove(moveDownAndLeft, m_GameBoard, false);
                            if (aMoveWasMade)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void UpdateKings(Board i_Board)
        {
            int i = 0;

            for (int j = 0; j <= i_Board.BoardSize; j++)
            {
                if (i_Board.BoardArray[i, j].Checker != null)
                {
                    if (i_Board.BoardArray[i, j].Checker.CheckerType == eCheckerType.X)
                    {
                        i_Board.BoardArray[i, j].Checker.CheckerType = eCheckerType.K;

                        Location i_destnation = new Location(i, j);
                        OnGameMove(i_destnation, eCheckerType.K);
                    }
                }
            }
            i = i_Board.BoardSize;
            for (int j = 0; j <= i_Board.BoardSize; j++)
            {
                if (i_Board.BoardArray[i, j].Checker != null)
                {
                    if (i_Board.BoardArray[i, j].Checker.CheckerType == eCheckerType.O)
                    {
                        i_Board.BoardArray[i, j].Checker.CheckerType = eCheckerType.U;
                        Location i_destnation = new Location(i, j);
                        OnGameMove(i_destnation, eCheckerType.U);
                    }
                }
            }
        }

        public void SwipeTurns()
        {
            m_Player1.MyTurn = !m_Player1.MyTurn;
            m_Player2.MyTurn = !m_Player2.MyTurn;
            OnTurnSwap();
        }

        private void OnTurnSwap()
        {
            if (TurnSwap != null)
            {
                TurnSwap.Invoke(Player1.MyTurn == true ? Player1 : Player2);
            }
        }

        public void UpdateGameStatus()
        {
            if (m_Player1.MyTurn == true)
            {
                m_Player1.HasMoreMoves = player1HasMoreMoves(m_GameBoard);
            }
            else
            {
                m_Player2.HasMoreMoves = player2HasMoreMoves(m_GameBoard);
            }

            bool winType1 = m_Player1.NumOfCheckersAlive == 0 && (m_Player2.NumOfCheckersAlive > 0);
            bool winType2 = (m_Player2.NumOfCheckersAlive == 0) && (m_Player1.NumOfCheckersAlive > 0);
            //bool winType3 = (!m_Player1.HasMoreMoves) && (m_Player2.HasMoreMoves);
            //bool winType4 = (m_Player1.HasMoreMoves) && (!m_Player2.HasMoreMoves);
            //bool draw = (!m_Player1.HasMoreMoves) && (!m_Player2.HasMoreMoves);

            if (winType1 || winType2) //winType3 || winType4 || draw)
            {
                m_GameOver = true;
                calcScores();
                m_Player1.TotalScore += m_Player1.CuurentRoundScore;
                m_Player2.TotalScore += m_Player2.CuurentRoundScore;
                OnRoundEnded();
            }
        }

        private void calcScores()
        {
            for (int i = m_GameBoard.BoardSize; i >= 0; i--)
            {
                for (int j = m_GameBoard.BoardSize; j >= 0; j--)
                {
                    if (m_GameBoard.BoardArray[i, j].Checker != null)
                    {
                        if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.X)
                        {
                            m_Player1.CuurentRoundScore += 1;
                        }
                        else if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.K)
                        {
                            m_Player1.CuurentRoundScore += 4;
                        }
                        else if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.O)
                        {
                            m_Player2.CuurentRoundScore += 1;
                        }
                        else if (m_GameBoard.BoardArray[i, j].Checker.CheckerType == eCheckerType.U)
                        {
                            m_Player2.CuurentRoundScore += 4;
                        }
                    }
                }
            }

            if (m_Player1.CuurentRoundScore > m_Player2.CuurentRoundScore)
            {
                m_Player1.CuurentRoundScore = m_Player1.CuurentRoundScore - m_Player2.CuurentRoundScore;
                m_Player2.CuurentRoundScore = 0;

            }
            else if (m_Player1.CuurentRoundScore < m_Player2.CuurentRoundScore)
            {
                m_Player2.CuurentRoundScore = m_Player2.CuurentRoundScore - m_Player1.CuurentRoundScore;
                m_Player1.CuurentRoundScore = 0;
            }
            else
            {
                m_Player1.CuurentRoundScore = 0;
                m_Player2.CuurentRoundScore = 0;
            }
        }
    }
}
