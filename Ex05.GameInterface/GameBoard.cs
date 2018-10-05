using System;
using System.Drawing;
using GameInterface;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GameLogic;
using Ex05.GameInterface.Properties;

namespace GameInterface
{
    public class GameBoard : Control
    {
        internal Image m_Player1CheckerType = Resources.Pawn__Black_;
        internal Image m_Player2CheckerType = Resources.Pawn__Red_;
        private readonly int r_SizeOfBoard;
        private Form m_BoardForm = new Form();
        private Label m_LabelPlayer1, m_LabelPlayer2;
        private BoardButton[,] m_BoardButtons;
        private Location? m_DestnationCell, m_SourceCell;
        public event Action<Location, Location> BoardMove;
        private bool m_TurnPassed;

        public GameBoard(GameSettings i_Settings)
        {
            r_SizeOfBoard = i_Settings.Size;

            InitializeBoardFormProperties(i_Settings.PlayerName1, i_Settings.PlayerName2);

            InitializeBoardButtons();

            m_TurnPassed = false;
        }

        public bool TurnPassed
        {
            get
            {
                return m_TurnPassed;
            }

            set
            {
                m_TurnPassed = value;
            }
        }

        public Form BoardForm
        {
            get
            {
                return m_BoardForm;
            }
        }

        public Location? SourceCell
        {
            get
            {
                return m_SourceCell;
            }
            set
            {
                m_SourceCell = value;
            }
        }

        public Location? DestnationCell
        {
            get
            {
                return m_DestnationCell;
            }
            set
            {
                m_DestnationCell = value;
            }
        }


        public Label LabelPlayer1
        {
            get
            {
                return m_LabelPlayer1;
            }

            set
            {
                m_LabelPlayer1 = value;
            }
        }

        public Label LabelPlayer2
        {
            get
            {
                return m_LabelPlayer2;
            }

            set
            {
                m_LabelPlayer2 = value;
            }
        }

        public int SizeOfBoard
        {
            get
            {
                return r_SizeOfBoard;
            }
        }

        internal void InitializeBoardFormProperties(String i_Player1Name, String i_Player2Name)
        {
            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = String.Format("{0}:{1}", i_Player1Name, "0");
            m_LabelPlayer1.ForeColor = Color.Blue;
            m_LabelPlayer1.Top = 12;
            m_LabelPlayer1.Height = 50;
            m_LabelPlayer1.Width = 200;
            m_LabelPlayer1.Left = (r_SizeOfBoard / 2) * 10;
            m_LabelPlayer1.Font = new Font("Impact", 25);
            m_BoardForm.Controls.Add(m_LabelPlayer1);

            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = String.Format("{0}:{1}", i_Player2Name, "0");
            m_LabelPlayer2.Top = m_LabelPlayer1.Top;
            m_LabelPlayer2.Height = m_LabelPlayer1.Height;
            m_LabelPlayer2.Width = m_LabelPlayer1.Width + 200;
            m_LabelPlayer2.Left = m_LabelPlayer1.Right + 15;
            m_LabelPlayer2.Font = m_LabelPlayer1.Font;

            m_BoardForm.Controls.Add(m_LabelPlayer2);
            m_BoardForm.FormClosing += Board_Closing;

            int labelSpaceSize = m_LabelPlayer1.Top + m_LabelPlayer1.Height;
            Size sizeToClient = new Size();
            sizeToClient.Width = 500 + r_SizeOfBoard;
            sizeToClient.Height = sizeToClient.Width + labelSpaceSize;

            m_BoardForm.ClientSize = sizeToClient;
            m_BoardForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            m_BoardForm.StartPosition = FormStartPosition.CenterScreen;
            m_BoardForm.MaximizeBox = false;
        }

        internal void InitializeBoardButtons()
        {
            int ButtonSize = m_BoardForm.ClientSize.Width / r_SizeOfBoard;
            int LabelsSpace = m_LabelPlayer1.Top + m_LabelPlayer1.Height;
            m_BoardButtons = new BoardButton[r_SizeOfBoard, r_SizeOfBoard];

            for (int i = 0; i < r_SizeOfBoard; i++)
            {
                for (int j = 0; j < r_SizeOfBoard; j++)
                {
                    if (i < ((r_SizeOfBoard / 2) - 1) && (i + j) % 2 != 0)
                    {
                        BoardButton player1Button = new BoardButton(new Location(i, j));
                        player1Button.Width = ButtonSize;
                        player1Button.Height = ButtonSize;
                        player1Button.Left = j * ButtonSize;
                        player1Button.Top = (i * ButtonSize) + LabelsSpace;
                        player1Button.Click += Button_Cliked;
                        player1Button.Image = Resources.Pawn__Black_;
                        m_BoardForm.Controls.Add(player1Button);
                        m_BoardButtons[i, j] = player1Button;
                    }
                    else if (i >= ((r_SizeOfBoard / 2) + 1) && (i + j) % 2 != 0)
                    {
                        BoardButton player2Button = new BoardButton(new Location(i, j));
                        player2Button.Width = ButtonSize;
                        player2Button.Height = ButtonSize;
                        player2Button.Left = j * ButtonSize;
                        player2Button.Top = (i * ButtonSize) + LabelsSpace;
                        player2Button.Click += Button_Cliked;
                        player2Button.Image = Resources.Pawn__Red_;
                        m_BoardForm.Controls.Add(player2Button);
                        m_BoardButtons[i, j] = player2Button;
                    }
                    else
                    {

                        BoardButton buttonOfEmptyPlace = new BoardButton(new Location(i, j));
                        buttonOfEmptyPlace.Text = string.Empty;
                        buttonOfEmptyPlace.Width = ButtonSize;
                        buttonOfEmptyPlace.Height = ButtonSize;
                        buttonOfEmptyPlace.Left = j * ButtonSize;
                        buttonOfEmptyPlace.Top = (i * ButtonSize) + LabelsSpace;

                        if ((i + j) % 2 == 0)
                        {
                            buttonOfEmptyPlace.BackColor = Color.Gray;
                            buttonOfEmptyPlace.Enabled = false;
                        }
                        else
                        {
                            buttonOfEmptyPlace.Click += Button_Cliked;
                        }

                        m_BoardForm.Controls.Add(buttonOfEmptyPlace);
                        m_BoardButtons[i, j] = buttonOfEmptyPlace;
                    }
                }
            }
        }

        internal void CleanBoard()
        {
            for (int i = 0; i < r_SizeOfBoard; i++)
            {
                for (int j = 0; j < r_SizeOfBoard; j++)
                {
                    if (i < ((r_SizeOfBoard / 2) - 1) && (i + j) % 2 != 0)
                    {
                        m_BoardButtons[i, j].Image = m_Player1CheckerType;
                        m_BoardButtons[i, j].BackColor = Color.Empty;
                    }
                    else if (i >= ((r_SizeOfBoard / 2) + 1) && (i + j) % 2 != 0)
                    {
                        m_BoardButtons[i, j].Image = m_Player2CheckerType;
                        m_BoardButtons[i, j].BackColor = Color.Empty;
                    }
                    else
                    {
                        if (m_BoardButtons[i, j].Enabled)
                        {
                            m_BoardButtons[i, j].Image = null;
                            m_BoardButtons[i, j].Text = string.Empty;
                            m_BoardButtons[i, j].BackColor = Color.Empty;
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "GameBoard";
            this.ResumeLayout(false);

        }

        public void Close()
        {
            m_BoardForm.Close();
        }

        internal void Board_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult Response = MessageBox.Show("Would you like to exit the game?", "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Response == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        internal void Button_Cliked(object sender, EventArgs e)
        {
            BoardButton currentButton = sender as BoardButton;
            if (currentButton != null)
            {
                if (currentButton.BackColor != Color.CornflowerBlue)
                {
                    currentButton.BackColor = Color.CornflowerBlue;
                    if (m_SourceCell == null)
                    {
                        m_SourceCell = currentButton.BoardLocation;
                    }
                }
                else
                {
                    currentButton.BackColor = Color.Empty;
                    m_SourceCell = null;
                }

                if (m_SourceCell != null && !m_SourceCell.Value.Equals(currentButton.BoardLocation))
                {
                    m_DestnationCell = currentButton.BoardLocation;
                    OnBoardMove(m_SourceCell.Value, m_DestnationCell.Value);
                }
            }
        }

        internal virtual void OnBoardMove(Location i_Source, Location i_Dest)
        {
            if (BoardMove != null)
            {
                BoardMove.Invoke(i_Source, i_Dest);
            }
        }

        public void CleanAfterIlegallMove()
        {
            m_BoardButtons[m_SourceCell.Value.X, m_SourceCell.Value.Y].BackColor = Color.Empty;
            m_BoardButtons[m_DestnationCell.Value.X, m_DestnationCell.Value.Y].BackColor = Color.Empty;
        }

        public void CleanAfterMove()
        {
            if (m_SourceCell.Value.X < r_SizeOfBoard && m_SourceCell.Value.Y < r_SizeOfBoard)
            {
                m_BoardButtons[m_SourceCell.Value.X, m_SourceCell.Value.Y].BackColor = Color.Empty;
            }
            m_BoardButtons[m_DestnationCell.Value.X, m_DestnationCell.Value.Y].BackColor = Color.Empty;
            if (m_TurnPassed)
            {
                if (m_SourceCell.Value.X < r_SizeOfBoard && m_SourceCell.Value.Y < r_SizeOfBoard)
                {
                    m_BoardButtons[m_SourceCell.Value.X, m_SourceCell.Value.Y].Image = null;
                }
            }

            m_SourceCell = null;
        }

        public void CleanEatenCheckers(Location i_Location)
        {
            m_BoardButtons[i_Location.X, i_Location.Y].Text = string.Empty;
            m_BoardButtons[i_Location.X, i_Location.Y].Image = null;
        }

        public void ChangeCheckerTypeOnButton(Location i_LocateOfButton, Image i_newImageToButton)
        {
            m_BoardButtons[i_LocateOfButton.X, i_LocateOfButton.Y].Image = i_newImageToButton;
        }
    }
}
