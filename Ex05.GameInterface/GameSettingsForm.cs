using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameInterface;
using GameLogic;

namespace Ex05.GameInterface
{
    public partial class GameSettingsForm : Form
    {
        internal GameSettings m_GameSettings;
        public GameSettingsForm()
        {
            InitializeComponent();
        }

        public int BoardSize()
        {

            int boardSize = 8;

            if (BoardSize6Button.Checked == true)
            {
                boardSize = 6;
            }
            else if (BoardSize8Button.Checked == true)
            {
                boardSize = 8;
            }
            else if (BoardSize10Button.Checked == true)
            {
                boardSize = 10;
            }

            return boardSize;

        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBoxPlayer2.Text;
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = !textBoxPlayer2.Enabled;
            textBoxPlayer2.Text = checkBoxPlayer2.Checked == true ? textBoxPlayer2.Text : "[computer]";
            m_GameSettings.GameMode = checkBoxPlayer2.Checked == true ? eGameMode.UserVsUser : eGameMode.UserVsComputer;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            this.m_GameSettings.Size = this.BoardSize();
            this.m_GameSettings.PlayerName1 = Player1Name;
            this.m_GameSettings.PlayerName2 = Player2Name;


            if (!ValidationProcess.IsValidName(Player1Name) || !ValidationProcess.IsValidName(Player2Name))
            {
                MessageBox.Show(
@"Please enter your name in english letters only, if your name has more than 20 characters
Please enter a nickname", "Incorrect input", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                UIManager newGame = new UIManager(m_GameSettings);
                this.Hide();
                newGame.StartGame();
            }
        }

        private void GameSettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
