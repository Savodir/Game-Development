using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Vancluysen.Carl.Collision
{
   static class RectCollision
    {
      /*public  static bool IsTop(this Rectangle rect1, Rectangle rect2, player player)
        {
         return   rect1.Top + player.Velocity.Y > rect2.Bottom &&
                  rect1.Top < rect2.Top &&
                  rect1.Right > rect2.Left &&
                  rect1.Left < rect2.Right;
        }*/
        public static bool IsTop(this Rectangle r1, Rectangle r2)
        {
            return r1.Bottom >= r2.Top &&
                   r1.Bottom <= r2.Top + (r2.Height / 2) &&
                   r1.Right >= r2.Left + (r2.Width / 5) &&
                   r1.Left <= r2.Right - (r2.Width / 5);
        }

        public static bool OnLeft(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                    r1.Right >= r2.Left&&
                    r1.Top <= r2.Bottom - r2.Width / 4 &&
                    r1.Top >= r2.Top + r2.Width / 4);
        }

        public static bool onRight(this Rectangle r1, Rectangle r2)
        {
            return r1.Left >= r2.Left &&
                   r1.Left <= r2.Right &&
                   r1.Top <= r2.Bottom - (r2.Width / 4) &&
                   r1.Bottom >= r2.Top + (r2.Width / 4);
        }

        public static bool IsBottom(this Rectangle r1, Rectangle r2)
        {
            return r1.Top <= r2.Bottom + (r2.Height / 5) &&
                   r1.Top >= r2.Bottom - 1 &&
                   r1.Right >= r2.Left + r2.Width / 5 &&
                   r1.Left <= r2.Right - r2.Width / 2;
        }

        public static bool EnemyTop(this Rectangle r1, Rectangle r2)
        {
            return r1.Intersects(r2);
        }

    }
}
