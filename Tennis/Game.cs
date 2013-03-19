using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tennis
{
    class Game
    {
        private Ball ball;
        private Player player1, player2;

        public const int FieldWidth = 500;   // размеры 
        public const int FieldHeight = 300;  // игрового поля
        public const int PlayerVelocity = 2;   // скорость игроков

        /* свойства - параметры мяча */
        public int BallX
        {
            get { return (int)Math.Round(ball.X); }
            set { ball.X = value; }
        }
        public int BallY
        {
            get { return (int)Math.Round(ball.Y); }
            set { ball.Y = value; }
        }
        public int BallRadius
        {
            get { return ball.Radius; }
            set { ball.Radius = value; }
        }

        /* свойства - параметры "ракеток" игроков*/
        #region игрок раз
        public int Player1X
        {
            get { return player1.X; }
            set { player1.X = value; }
        }
        public int Player1Y
        {
            get { return player1.Y; }
            set { player1.Y = value; }
        }
        public int Player1Vy
        {
            get { return player1.Vy; }
            set { player1.Vy = value; }
        }
        public int Player1Width
        {
            get { return player1.Width; }
            set { player1.Width = value; }
        }
        public int Player1Height
        {
            get { return player1.Height; }
            set { player1.Height = value; }
        }
        #endregion
        #region игрок два
        public int Player2X
        {
            get { return player2.X; }
            set { player2.X = value; }
        }
        public int Player2Y
        {
            get { return player2.Y; }
            set { player2.Y = value; }
        }
        public int Player2Vy
        {
            get { return player2.Vy; }
            set { player2.Vy = value; }
        }
        public int Player2Width
        {
            get { return player2.Width; }
            set { player2.Width = value; }
        }
        public int Player2Height
        {
            get { return player2.Height; }
            set { player2.Height = value; }
        }
        #endregion

        /* конструктор */
        public Game(int ballVx, int ballVy)
        {
            // создаём мяч
            ball = new Ball(FieldWidth / 2, FieldHeight / 2, ballVx, ballVy);
            // задаём начальные параметры "ракеток" игроков
            int playerWidth = 10, playerHeight = 50;
            int startPos = 200;
            // создаём игроков
            player1 = new Player(0, startPos, playerWidth, playerHeight);
            player2 = new Player(FieldWidth - playerWidth, FieldHeight - startPos - playerHeight, playerWidth, playerHeight);
        }

        public void BallMove()
        {
            bool borderColloission = checkBallBorderCollision();
            bool playerColloission = checkPlayerBallCollision();
            if (!playerColloission)
                playerColloission = checkAngleCollision();
            if (!borderColloission || !playerColloission) 
                ball.Move();
        }

        public void PlayerMoveUp()
        {
            player1.Vy = -3;
            PlayerMove(player1);
        }
        public void PlayerMoveDown()
        {
            player1.Vy = 3;
            PlayerMove(player1);
        }

        public void Player2Move()
        {
            player2.Vy = (ball.Y > player2.Y + (player2.Height / 2)) ? PlayerVelocity : -PlayerVelocity;
            PlayerMove(player2);
        }

        private void PlayerMove(Player player)
        {
            checkPlayerBorderCollision(player);
            if (!checkAngleCollision())
                player.Move();
            player.Vy = 0;
        }

        private bool checkBallBorderCollision() // проверка на столкновение мяча со стенкой
        {
            if (((ball.Y + ball.Radius) + ball.Vy > FieldHeight) ||
                ((ball.Y - ball.Radius) + ball.Vy < 0))
            {
                ball.Vy = -ball.Vy;
                return true;
            }
            if (((ball.X + ball.Radius) + ball.Vx > FieldWidth) ||
                ((ball.X - ball.Radius) + ball.Vx < 0))
            {
                ball.Vx = -ball.Vx;
                return true;
            }
            return false;
        }

        private void checkPlayerBorderCollision(Player player) // проверка столкновения "ракетки" с границами
        {
            if ((player.Y + player.Vy) < 0) // верхняя граница
                player.Vy = 0 - player.Y;
            else if (((player.Y + player.Height) + player.Vy) > Game.FieldHeight) // нижняя граница
                player.Vy = Game.FieldHeight - (player.Y + player.Height);
        }

        private bool checkPlayerBallCollision() // проверка столкновения мяча с "ракеткой"
        {
            int newX = (int)Math.Round(ball.X + ball.Vx);
            int newY = (int)Math.Round(ball.Y + ball.Vy);
            // левая
            if ((newX - ball.Radius) < (player1.X + player1.Width) &&
                newY > player1.Y && newY < (player1.Y + player1.Height))
            {
                ball.Vx = -ball.Vx;
                return true;
            }
            // правая
            else if ((newX + ball.Radius) > player2.X &&
                newY > player2.Y && newY < (player2.Y + player2.Height))
            {
                ball.Vx = -ball.Vx;
                return true;
            }
            return false;
        }

        private bool checkAngleCollision() // проверка столкновения с краем "ракетки"
        {
            int ballX = (int)Math.Round(ball.X + ball.Vx);
            int ballY = (int)Math.Round(ball.Y + ball.Vy);
            // левый игрок
            if ((ballX - ball.Radius) < (player1.X + player1.Width))
            {
                int pl1X = player1.X;
                int pl1Y = player1.Y + player1.Vy;
                // верхний край
                if (distanceBetweenTwoPoints(ballX, ballY, pl1X + player1.Width, pl1Y) < ball.Radius)
                {
                    ball.Vx = Math.Abs(ball.Vx);
                    ball.Vy = -Math.Abs(ball.Vy);
                    return true;
                }
                // нижний край
                else if (distanceBetweenTwoPoints(ballX, ballY, pl1X + player1.Width, pl1Y + player1.Height) < ball.Radius)
                {
                    ball.Vx = Math.Abs(ball.Vx);
                    ball.Vy = Math.Abs(ball.Vy);
                    return true;
                }
            }
            else if ((ballX - ball.Radius) < player2.X)  // правый игрок
            {
                int pl2X = player2.X;
                int pl2Y = player2.Y + player2.Vy;
                // верхний край
                if (distanceBetweenTwoPoints(ballX, ballY, pl2X, pl2Y) < ball.Radius)
                {
                    ball.Vx = -Math.Abs(ball.Vx);
                    ball.Vy = -Math.Abs(ball.Vy);
                    return true;
                }
                // нижний край
                else if (distanceBetweenTwoPoints(ballX, ballY, pl2X, pl2Y + player2.Height) < ball.Radius)
                {
                    ball.Vx = -Math.Abs(ball.Vx);
                    ball.Vy = Math.Abs(ball.Vy);
                    return true;
                }
            }
            return false;
        }

        private double distanceBetweenTwoPoints(double x1, double y1, double x2, double y2)
        {
            double dx = x1 - x2;
            double dy = y1 - y2;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
