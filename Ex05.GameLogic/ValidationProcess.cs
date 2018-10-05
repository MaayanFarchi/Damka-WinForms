using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public static class ValidationProcess
    {
        internal static bool IsLegalMoveFormat(string i_UserInput, int i_BoardLength)
        {
            bool isLegalMoveFormat = true;
            if (i_UserInput.Length != 5 || i_UserInput[2] != '>' ||
                i_UserInput[0] < 'A' || i_UserInput[0] > Convert.ToChar('A' + i_BoardLength) ||
                i_UserInput[3] < 'A' || i_UserInput[3] > Convert.ToChar('A' + i_BoardLength) ||
                i_UserInput[1] < 'a' || i_UserInput[1] > Convert.ToChar('a' + i_BoardLength) ||
                i_UserInput[4] < 'a' || i_UserInput[4] > Convert.ToChar('a' + i_BoardLength))
            {
                isLegalMoveFormat = false;
            }
            else
            {
                isLegalMoveFormat = true;
            }

            return isLegalMoveFormat;
        }

        private static bool isValidEnglishString(string i_UserInput)
        {
            bool isEnglishString = true;

            foreach (char letter in i_UserInput)
            {
                if (!char.IsLetter(letter))
                {
                    isEnglishString = false;
                    break;
                }
            }

            return isEnglishString;
        }

        public static bool IsValidName(string i_UserInput)
        {
            bool validString = isValidEnglishString(i_UserInput) || i_UserInput == "[computer]";

            bool validLength = i_UserInput.Length <= 20 || i_UserInput == "[computer]";

            return validLength && validString;
        }

        internal static bool IsLegalMove(Board.BoardCell i_SourceCell, Board.BoardCell i_DestinationCell, Board i_Board, string i_PlayerName)
        {
            bool isLegal = false;

            if (i_SourceCell.Checker == null)
            {
                isLegal = false;
            }
            else
            {
                if (i_PlayerName != i_SourceCell.Checker.Player.Name)
                {
                    return false;
                }

                if (i_SourceCell.Checker.CheckerType == eCheckerType.O || i_SourceCell.Checker.CheckerType == eCheckerType.X)
                {
                    if (i_SourceCell.Checker.CheckerType == eCheckerType.O)
                    {
                        if ((Math.Abs(i_DestinationCell.Location.Y - i_SourceCell.Location.Y) == 1 &&
                            (i_DestinationCell.Location.X - i_SourceCell.Location.X) == 1 &&
                            i_DestinationCell.Checker == null) ||
                            (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == 2 &&
                            i_DestinationCell.Location.X - i_SourceCell.Location.X == 2 &&
                            i_DestinationCell.Checker == null &&
                            i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y + 1].Checker != null &&
                            !i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y + 1].Checker.Player.Name.Equals
                            (i_SourceCell.Checker.Player.Name)) ||
                            (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == -2 &&
                            i_DestinationCell.Location.X - i_SourceCell.Location.X == 2 &&
                            i_DestinationCell.Checker == null &&
                            i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y - 1].Checker != null &&
                            !i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y - 1].Checker.Player.Name.Equals
                            (i_SourceCell.Checker.Player.Name)))
                        {
                            isLegal = true;
                        }
                    }
                    else
                    {
                        if ((Math.Abs(i_DestinationCell.Location.Y - i_SourceCell.Location.Y) == 1 &&
                           (i_DestinationCell.Location.X - i_SourceCell.Location.X) == -1 &&
                           i_DestinationCell.Checker == null) ||
                           (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == -2 &&
                            i_DestinationCell.Location.X - i_SourceCell.Location.X == -2 &&
                            i_DestinationCell.Checker == null &&
                            i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y - 1].Checker != null &&
                            !i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y - 1].Checker.Player.Name.Equals
                            (i_SourceCell.Checker.Player.Name)) ||
                            (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == 2 &&
                            i_DestinationCell.Location.X - i_SourceCell.Location.X == -2 &&
                            i_DestinationCell.Checker == null &&
                            i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y + 1].Checker != null &&
                            !i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y + 1].Checker.Player.Name.Equals
                            (i_SourceCell.Checker.Player.Name)))
                        {
                            isLegal = true;
                        }
                    }
                }
                else
                {
                    if ((Math.Abs(i_DestinationCell.Location.Y - i_SourceCell.Location.Y) == 1 &&
                        Math.Abs(i_DestinationCell.Location.X - i_SourceCell.Location.X) == 1 &&
                        i_DestinationCell.Checker == null) ||
                        (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == 2 &&
                        i_DestinationCell.Location.X - i_SourceCell.Location.X == 2 &&
                        i_DestinationCell.Checker == null &&
                        i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y + 1].Checker != null &&
                        !i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y + 1].Checker.Player.Name.Equals
                        (i_SourceCell.Checker.Player.Name)) ||
                        (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == -2 &&
                        i_DestinationCell.Location.X - i_SourceCell.Location.X == 2 &&
                        i_DestinationCell.Checker == null &&
                        i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y - 1].Checker != null &&
                        !i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y - 1].Checker.Player.Name.Equals
                        (i_SourceCell.Checker.Player.Name)) ||
                        (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == -2 &&
                        i_DestinationCell.Location.X - i_SourceCell.Location.X == -2 &&
                        i_DestinationCell.Checker == null &&
                        i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y - 1].Checker != null &&
                        !i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y - 1].Checker.Player.Name.Equals
                        (i_SourceCell.Checker.Player.Name)) ||
                        (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == 2 &&
                        i_DestinationCell.Location.X - i_SourceCell.Location.X == -2 &&
                        i_DestinationCell.Checker == null &&
                        i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y + 1].Checker != null &&
                        !i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y + 1].Checker.Player.Name.Equals
                        (i_SourceCell.Checker.Player.Name)))
                    {
                        isLegal = true;
                    }
                }
            }

            return isLegal;
        }

        internal static bool VirtualIsLegalMove(Board.BoardCell i_SourceCell, Board.BoardCell i_DestinationCell, Board i_Board, string i_PlayerName)
        {
            bool isLegal = false;

            if (i_SourceCell.Checker == null)
            {
                isLegal = false;
            }
            else
            {
                if (i_SourceCell.Checker.CheckerType == eCheckerType.O || i_SourceCell.Checker.CheckerType == eCheckerType.X)
                {
                    if (i_SourceCell.Checker.CheckerType == eCheckerType.O)
                    {
                        if ((Math.Abs(i_DestinationCell.Location.Y - i_SourceCell.Location.Y) == 1 &&
                            (i_DestinationCell.Location.X - i_SourceCell.Location.X) == 1 &&
                            i_DestinationCell.Checker == null) ||
                            (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == 2 &&
                            i_DestinationCell.Location.X - i_SourceCell.Location.X == 2 &&
                            i_DestinationCell.Checker == null &&
                            i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y + 1].Checker != null &&
                            !i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y + 1].Checker.Player.Name.Equals
                            (i_SourceCell.Checker.Player.Name)) ||
                            (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == -2 &&
                            i_DestinationCell.Location.X - i_SourceCell.Location.X == 2 &&
                            i_DestinationCell.Checker == null &&
                            i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y - 1].Checker != null &&
                            !i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y - 1].Checker.Player.Name.Equals
                            (i_SourceCell.Checker.Player.Name)))
                        {
                            isLegal = true;
                        }
                    }
                    else
                    {
                        if ((Math.Abs(i_DestinationCell.Location.Y - i_SourceCell.Location.Y) == 1 &&
                           (i_DestinationCell.Location.X - i_SourceCell.Location.X) == -1 &&
                           i_DestinationCell.Checker == null) ||
                           (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == -2 &&
                            i_DestinationCell.Location.X - i_SourceCell.Location.X == -2 &&
                            i_DestinationCell.Checker == null &&
                            i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y - 1].Checker != null &&
                            !i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y - 1].Checker.Player.Name.Equals
                            (i_SourceCell.Checker.Player.Name)) ||
                            (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == 2 &&
                            i_DestinationCell.Location.X - i_SourceCell.Location.X == -2 &&
                            i_DestinationCell.Checker == null &&
                            i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y + 1].Checker != null &&
                            !i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y + 1].Checker.Player.Name.Equals
                            (i_SourceCell.Checker.Player.Name)))
                        {
                            isLegal = true;
                        }
                    }
                }
                else
                {
                    if ((Math.Abs(i_DestinationCell.Location.Y - i_SourceCell.Location.Y) == 1 &&
                        Math.Abs(i_DestinationCell.Location.X - i_SourceCell.Location.X) == 1 &&
                        i_DestinationCell.Checker == null) ||
                        (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == 2 &&
                        i_DestinationCell.Location.X - i_SourceCell.Location.X == 2 &&
                        i_DestinationCell.Checker == null &&
                        i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y + 1].Checker != null &&
                        !i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y + 1].Checker.Player.Name.Equals
                        (i_SourceCell.Checker.Player.Name)) ||
                        (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == -2 &&
                        i_DestinationCell.Location.X - i_SourceCell.Location.X == 2 &&
                        i_DestinationCell.Checker == null &&
                        i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y - 1].Checker != null &&
                        !i_Board.BoardArray[i_SourceCell.Location.X + 1, i_SourceCell.Location.Y - 1].Checker.Player.Name.Equals
                        (i_SourceCell.Checker.Player.Name)) ||
                        (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == -2 &&
                        i_DestinationCell.Location.X - i_SourceCell.Location.X == -2 &&
                        i_DestinationCell.Checker == null &&
                        i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y - 1].Checker != null &&
                        !i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y - 1].Checker.Player.Name.Equals
                        (i_SourceCell.Checker.Player.Name)) ||
                        (i_DestinationCell.Location.Y - i_SourceCell.Location.Y == 2 &&
                        i_DestinationCell.Location.X - i_SourceCell.Location.X == -2 &&
                        i_DestinationCell.Checker == null &&
                        i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y + 1].Checker != null &&
                        !i_Board.BoardArray[i_SourceCell.Location.X - 1, i_SourceCell.Location.Y + 1].Checker.Player.Name.Equals
                        (i_SourceCell.Checker.Player.Name)))
                    {
                        isLegal = true;
                    }
                }
            }

            return isLegal;
        }
    }
}