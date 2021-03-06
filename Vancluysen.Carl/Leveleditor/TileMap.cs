﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vancluysen.Carl.AI;
using Vancluysen.Carl.Properties;

namespace Vancluysen.Carl.Leveleditor
{
    class TileMap
    {

        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        protected Rectangle bpos;
        protected Rectangle bposUnder;
        protected Texture2D background, backgroundUnderground, enemygfx, tree, mapEnd, lifepoint, train;
        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        private EventHandler eventHandler;
        public EventHandler EventHandler
        {
            get { return eventHandler; }
            set { eventHandler = value; }
        }
        private EntityManager entityManager;

        public EntityManager EntityManager
        {
            get { return entityManager; }
            set { entityManager = value; }
        }
        private bool finished;

        public bool Finished
        {
            get { return finished; }
            set { finished = value; }
        }

        private ContentManager content;
        public TileMap(ContentManager _content)
        {
            content = _content;
            tree = content.Load<Texture2D>("tree");
            mapEnd = content.Load<Texture2D>("flag");
            enemygfx = content.Load<Texture2D>("police");
            lifepoint = content.Load<Texture2D>("lifepoint");
            train = content.Load<Texture2D>("train");
            bpos = new Rectangle(0, 0, 2550, 700);
            bposUnder = new Rectangle(0, 700, 2550, 600);
            entityManager = new EntityManager();
            eventHandler = new EventHandler();
        }

        private int levelID = 1;

        public int LevelID
        {
            get { return levelID; }
            set { levelID = value; }
        }

        public void spawn(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)

                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number > 0)
                    {
                        width = (x + 1) * size;
                        height = (y + 1) * size;
                            collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size), content, levelID));
                    }
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, bpos, Color.White);
            spriteBatch.Draw(backgroundUnderground, bposUnder, Color.White);
            foreach (CollisionTiles levels in collisionTiles)
            {
                levels.Draw(spriteBatch);
            }
        }
    }
}
