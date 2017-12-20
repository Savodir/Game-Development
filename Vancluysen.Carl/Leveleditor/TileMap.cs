using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vancluysen.Carl.Leveleditor
{
    class TileMap
    {
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

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

        public TileMap()
        {
            
        }

        public void spawn(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number > 0)
                    {
                        collisionTiles.Add(new CollisionTiles(number,new Rectangle(x * size, y * size, size, size)));
                        width = (x + 1) * size;
                        height = (y + 1) * size;
                    }
                    
                }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles levels in collisionTiles)
            {
                levels.Draw(spriteBatch);
            }
        }
    }
}
