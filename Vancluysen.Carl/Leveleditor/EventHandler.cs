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
    class EventHandler
    {
        private List<Events> eventsList = new List<Events>();
        public List<Events> EventsList
        {
            get { return eventsList; }
        }
        private static ContentManager content;

        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        private Texture2D texture;
        private Vector2 position;
        public EventHandler()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Events tt in eventsList)
            {
                tt.Draw(spriteBatch);
            }
        }
    }
}
