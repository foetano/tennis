using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tennis
{
    class Player
    {
        public int X { get; set; }  // координаты левого верхнего угла
        public int Y { get; set; }
        public int Vy { get; set; } // скорость (движение вверх/вниз)
        public int Width { get; set; }  // размеры "ракетки"
        public int Height { get; set; }

        public Player(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public void Move()
        {
            Y += Vy;
        }
    }
}
