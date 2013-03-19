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
            game = new Game();
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
                game.BallX - game.BallRadius - 1, game.BallY - game.BallRadius - 1, game.BallRadius * 2 + 1, game.BallRadius * 2 + 1);
            
            // рисуем игроков
            graphics.FillRectangle(System.Drawing.Brushes.Blue, game.Player1X, game.Player1Y,
                game.Player1Width, game.Player1Height);
            graphics.FillRectangle(System.Drawing.Brushes.Red, game.Player2X, game.Player2Y,
                game.Player2Width, game.Player2Height);

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
            if (game.IsPlayer1Win())
            {
                restartGame(1);
            }
            else if (game.IsPlayer2Win())
            {
                restartGame(2);
            }
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

        private void restartGame(int winningPlayerNum)
        {
            gameStepTimer.Stop();
            string message;
            if (winningPlayerNum == 1)
                message = "Правый игрок пропустил мяч";
            else
                message = "Левый игрок пропустил мяч";

            // выводим сообщение посередине окна
            Graphics graphics = Graphics.FromImage(gameFieldPictBox.Image);
            Font font = new Font(new FontFamily("Arial"), 20);
            SizeF size = graphics.MeasureString(message, font);
            Rectangle rectangle = new Rectangle((Game.FieldWidth - (int)size.Width) / 2, (Game.FieldHeight - (int)size.Height) / 2,
                (int)size.Width + 1, (int)size.Height + 1);
            graphics.DrawString(message, font, new SolidBrush(Color.Black), rectangle);

            gameFieldPictBox.Refresh();

            pauseTimer.Start();
        }

        private void pauseTimer_Tick(object sender, EventArgs e)
        {
            pauseTimer.Stop();
            game.Restart();
            gameStepTimer.Start();
        }
    }
}
