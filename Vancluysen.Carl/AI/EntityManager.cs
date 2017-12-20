using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Vancluysen.Carl.AI
{
    class EntityManager
    {
        private List<Enemy> enemies = new List<Enemy>();
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        public EntityManager()
        {        }
        private static ContentManager content;

        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        private int levelid;

        public void Update(GameTime gameTime, int id)
        {

            foreach (Enemy enemies in enemies)
            {
                enemies.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy tt in enemies)
            {
                tt.Draw(spriteBatch);
            }
        }
    }
}
