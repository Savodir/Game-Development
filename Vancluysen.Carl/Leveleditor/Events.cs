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
    class Events: EventHandler
    {

        private static ContentManager content;

        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }
        private Texture2D texture;
        private int eventID;

        public int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        private Vector2 position;
        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Events(int _eventID, Texture2D _texture, Vector2 _position)
        {
            eventID = _eventID;
            texture = _texture;
            position = _position;
            rectangle = new Rectangle((int)position.X, (int)position.Y , texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
