using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tennis
{
    public partial class GameForm : Form
    {
        private Game game;
        private bool enabledUp = false;
        private bool enabledDown = false;

        public GameForm()
        {
            InitializeComponent();
            game = new Game(3, -3);
            this.ClientSize = new System.Drawing.Size(Game.FieldWidth, Game.FieldHeight);
            gameFieldPictBox.Size = new System.Drawing.Size(Game.FieldWidth, Game.FieldHeight);
            gameFieldPictBox.Image = new Bitmap(Game.FieldWidth, Game.FieldHeight);
        }

        private void UpdateGameField() // перерисовка поля
        {
            Graphics graphics = Graphics.FromImage(gameFieldPictBox.Image);
            // заливаем фон
            graphics.FillRectangle(System.Drawing.Brushes.White, 0, 0, Game.FieldWidth, Game.FieldHeight);
            // рисуем мяч
            graphics.FillEllipse(System.Drawing.Brushes.Black, 
                game.BallX - game.BallRadius, game.BallY - game.BallRadius, game.BallRadius * 2 + 1, game.BallRadius * 2 + 1);
            // рисуем игроков
            graphics.FillRectangle(System.Drawing.Brushes.Blue, game.Player1X, game.Player1Y,
                game.Player1Width + 1, game.Player1Height + 1);
            graphics.FillRectangle(System.Drawing.Brushes.Red, game.Player2X, game.Player2Y,
                game.Player2Width + 1, game.Player2Height + 1);

            gameFieldPictBox.Refresh();
        }

        private void gameStepTimer_Tick(object sender, EventArgs e)
        {
            game.BallMove();
            if (enabledUp)
                game.PlayerMoveUp();
            else if (enabledDown)
                game.PlayerMoveDown();
            game.Player2Move();
            UpdateGameField();
        }


        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                enabledUp = true;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                enabledDown = true;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                enabledUp = false;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                enabledDown = false;
        }
    }
}
