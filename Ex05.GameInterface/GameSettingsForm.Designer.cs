namespace Ex05.GameInterface
{
    partial class GameSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DoneButton = new System.Windows.Forms.Button();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.BoardSize10Button = new System.Windows.Forms.RadioButton();
            this.BoardSize8Button = new System.Windows.Forms.RadioButton();
            this.BoardSize6Button = new System.Windows.Forms.RadioButton();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.boardSize = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(169, 206);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(107, 33);
            this.DoneButton.TabIndex = 23;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.HideSelection = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(134, 163);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(142, 26);
            this.textBoxPlayer2.TabIndex = 22;
            this.textBoxPlayer2.Text = "[computer]";
            // 
            // BoardSize10Button
            // 
            this.BoardSize10Button.AutoSize = true;
            this.BoardSize10Button.Location = new System.Drawing.Point(187, 50);
            this.BoardSize10Button.Name = "BoardSize10Button";
            this.BoardSize10Button.Size = new System.Drawing.Size(89, 24);
            this.BoardSize10Button.TabIndex = 21;
            this.BoardSize10Button.TabStop = true;
            this.BoardSize10Button.Text = "10 X 10";
            this.BoardSize10Button.UseVisualStyleBackColor = true;
            // 
            // BoardSize8Button
            // 
            this.BoardSize8Button.AutoSize = true;
            this.BoardSize8Button.Location = new System.Drawing.Point(110, 50);
            this.BoardSize8Button.Name = "BoardSize8Button";
            this.BoardSize8Button.Size = new System.Drawing.Size(71, 24);
            this.BoardSize8Button.TabIndex = 20;
            this.BoardSize8Button.TabStop = true;
            this.BoardSize8Button.Text = "8 X 8";
            this.BoardSize8Button.UseVisualStyleBackColor = true;
            // 
            // BoardSize6Button
            // 
            this.BoardSize6Button.AutoSize = true;
            this.BoardSize6Button.Location = new System.Drawing.Point(33, 50);
            this.BoardSize6Button.Name = "BoardSize6Button";
            this.BoardSize6Button.Size = new System.Drawing.Size(71, 24);
            this.BoardSize6Button.TabIndex = 19;
            this.BoardSize6Button.TabStop = true;
            this.BoardSize6Button.Text = "6 X 6";
            this.BoardSize6Button.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(33, 163);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(95, 24);
            this.checkBoxPlayer2.TabIndex = 18;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(134, 120);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(142, 26);
            this.textBoxPlayer1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Player 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Players:";
            // 
            // boardSize
            // 
            this.boardSize.AutoSize = true;
            this.boardSize.Location = new System.Drawing.Point(13, 18);
            this.boardSize.Name = "boardSize";
            this.boardSize.Size = new System.Drawing.Size(91, 20);
            this.boardSize.TabIndex = 14;
            this.boardSize.Text = "Board Size:";
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 251);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.BoardSize10Button);
            this.Controls.Add(this.BoardSize8Button);
            this.Controls.Add(this.BoardSize6Button);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.boardSize);
            this.Name = "GameSettingsForm";
            this.Text = "GameSettings";
            this.Load += new System.EventHandler(this.GameSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.RadioButton BoardSize10Button;
        private System.Windows.Forms.RadioButton BoardSize8Button;
        private System.Windows.Forms.RadioButton BoardSize6Button;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label boardSize;
    }
}