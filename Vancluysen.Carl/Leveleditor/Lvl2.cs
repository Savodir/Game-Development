﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vancluysen.Carl.AI;

namespace Vancluysen.Carl.Leveleditor
{
    class Lvl2: TileMap
    {
        public Texture2D texture;
        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        private static ContentManager content;

        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        public Lvl2(ContentManager content):base(content)
        {
            background = content.Load<Texture2D>("centraalstation");
            backgroundUnderground = content.Load<Texture2D>("metro");
            Generate();
            Finished = false;
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(350, 450), 100));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(900, 250), 100));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(1200, 1000), 150));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(550, 1050), 200));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(1900, 1150), 200));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(1750, 650), 300));
            EventHandler.EventsList.Add(new Events(0, tree, new Vector2(1143, 582)));
            EventHandler.EventsList.Add(new Events(0, tree, new Vector2(1343, 1182)));
            EventHandler.EventsList.Add(new Events(0, tree, new Vector2(2243, 932)));
            EventHandler.EventsList.Add(new Events(0, tree, new Vector2(1443, 382)));
            EventHandler.EventsList.Add(new Events(2, lifepoint, new Vector2(1400, 925)));
            EventHandler.EventsList.Add(new Events(2, lifepoint, new Vector2(1700, 475)));
            EventHandler.EventsList.Add(new Events(1, mapEnd, new Vector2(2220, 630)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Generate()
        {
            spawn(new int[,]
                   {
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,8,8,0,0,8,0,8,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,6,6,6,6,6,6,6},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,8,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,7,7,7,7,7},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,8,8,0,0,8,0,0,0,0,0,0,8,0,8,0,8,0,0,0,7,7,7,7,7,7,7,7},
                       {0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,8,8,0,0,0,8,0,0,0,0,0,0,0,0,0,7,7,7,7,7,7,7,7},
                       {0,0,0,0,0,8,8,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,8,0,8,8,8,8,0,0,0,0,0,0,0,0,0,0,0,0,8,0,7,7,7,7,7,7,7,7},
                       {0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,8,0,0,8,0,0,8,0,0,0,0,0,0,8,0,0,0,0,0,7,7,7,7},
                       {0,0,0,8,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,8,8,8,0,8,8,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,7,7,7,7},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,8,8,8,8,0,8,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,7,7,7,7},
                       {6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,0,6,6,6,6,6,6,6,6,0,0,0,0,6,6,6,6,6,6,6,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0,7,7,7,7,7,7,7,7,6,6,0,0,7,7,7,7,7,7,7,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0,7,7,7,7,7,7,7,7,7,0,0,0,7,7,7,7,7,7,7,7,7,7,7},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,7,7,7,7,7,0,0,6,6,0,0,0,0,0,0,7,7,7,7,7},
                       {0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,7,7,7,7,7,7,0,0,0,0,0,0,0,0,0,0,7,7,7,7,7},
                       {0,7,6,6,0,0,0,0,0,0,0,0,6,6,6,6,6,6,6,0,0,0,0,0,0,7,6,0,0,0,7,7,7,7,7,7,6,6,6,6,6,0,0,0,0,0,7,7,7,7,7},
                       {0,7,7,7,6,6,6,0,0,0,0,0,7,0,0,0,0,0,7,6,6,0,0,0,0,7,7,6,6,6,7,7,7,7,7,7,0,0,0,0,0,0,0,6,6,6,7,7,7,7,7},
                       {0,7,7,7,7,7,7,0,0,0,0,0,7,0,0,6,6,0,0,0,7,6,6,6,6,0,0,0,0,0,0,0,7,7,7,7,0,0,0,0,0,6,6,7,7,7,7,7,7,7,7},
                       {0,7,7,7,7,7,7,6,6,6,6,6,7,0,6,7,7,0,0,0,0,0,0,0,0,0,6,6,6,6,0,0,0,0,0,7,0,0,0,6,6,7,7,7,7,7,7,7,7,7,7},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,0,0,0,0,0,0,0,0,0,0,0,7,7,6,6,6,6,0,0,0,0,0,7,7,7,7,7,7,7,7,7,7,7,7},
                       {6,6,6,6,6,6,6,6,6,6,6,6,6,6,7,7,7,6,6,6,6,6,6,6,6,6,6,0,7,7,7,7,7,7,6,6,6,6,6,7,7,7,7,7,7,7,7,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,6,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                       {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
            }, 50);
        }
    }
}
