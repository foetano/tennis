using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tennis
{
    class Ball
    {
        public double X { get; set; }  // координаты центра
        public double Y { get; set; }
        public int Radius { get; set; }
        public double Vx { get; set; } // скорость
        public double Vy { get; set; }

        public Ball(int x, int y, double vx, double vy)
        {
            this.X = x;
            this.Y = y;
            this.Vx = vx;
            this.Vy = vy;
            this.Radius = 10;
        }

        public void Move()
        {
            X += Vx;
            Y += Vy;
        }
    }
}
