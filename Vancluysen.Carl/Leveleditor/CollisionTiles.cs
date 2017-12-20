using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vancluysen.Carl.Leveleditor
{
    class CollisionTiles : Levels
    {
        public CollisionTiles(int i, Rectangle rect)
        {
            texture = Content.Load<Texture2D>("harbortile" + i);
            this.Rectangle = rect;
        }
    }
}
