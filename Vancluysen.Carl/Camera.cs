using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vancluysen.Carl
{
    class Camera
    {
        private Matrix transform;

        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        private Vector2 centre;
        private Viewport viewport;

        public Camera(Viewport Viewport)
        {
            viewport = Viewport;
        }

        public void Update(Vector2 position, int x, int y)
        {
            if (position.X < viewport.Width / 2)
            {
                centre.X = viewport.Width / 2;}
            else if (position.X > x - (viewport.Width / 2))
            {
                centre.X = x - (viewport.Width / 2);
            }
            else centre.X = position.X;


            if (position.Y < viewport.Height / 2)
            {
                centre.Y = viewport.Height / 2;
            }
            else if (position.Y > y - (viewport.Height / 2))
            {
                centre.Y = y - (viewport.Height / 2);
            }
            else centre.Y = position.Y;
            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2),
                -centre.Y + (viewport.Height / 2), 0));

        }
    }
}
