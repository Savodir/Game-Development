using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Vancluysen.Carl.Leveleditor
{
    class CollisionTiles
    {
        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        private Texture2D texture;
        public CollisionTiles(int i, Rectangle rect, ContentManager Content, int ID)
        {
                texture = Content.Load<Texture2D>("tile" + i);
                this.Rectangle = rect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
