using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vancluysen.Carl.AI;

namespace Vancluysen.Carl.Leveleditor
{
class Lvl1: TileMap
{   public Texture2D texture;
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

        public Lvl1(ContentManager content):base(Content)
        {       Generate();
            Finished = false;
            background = content.Load<Texture2D>("harbor");
            backgroundUnderground = content.Load<Texture2D>("ruien");
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(525, 300), 125));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(900, 250), 200));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(1325, 350), 125));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(2100, 1000), 400));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(2150, 450), 150));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(1800, 400), 100));
            EntityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(2050, 250), 100));
            EventHandler.EventsList.Add(new Events(0, tree, new Vector2(690, 430)));
            EventHandler.EventsList.Add(new Events(0, tree, new Vector2(2140, 1030)));
            EventHandler.EventsList.Add(new Events(0, tree, new Vector2(1690, 530)));
            EventHandler.EventsList.Add(new Events(2, lifepoint, new Vector2(1250, 475)));
            EventHandler.EventsList.Add(new Events(2, lifepoint, new Vector2(1000, 975)));
            EventHandler.EventsList.Add(new Events(2, lifepoint, new Vector2(1750, 225)));
            EventHandler.EventsList.Add(new Events(1, mapEnd, new Vector2(2325, 282)));
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
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2},
                       {0,0,0,0,1,1,0,0,0,0,0,0,1,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,5,5,5,5,5,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,5,0,0,0,5,5,5,0,0,0,0,0,0,2,2,2},
                       {0,1,1,0,0,0,0,0,1,1,1,0,0,5,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,5,0,0,0,0,0,0,5,5,1,1,2,2,2},
                       {0,0,0,0,1,1,0,0,0,0,0,0,0,5,0,5,5,0,5,0,5,5,0,0,1,1,1,0,0,1,1,1,0,5,0,0,0,0,0,0,0,0,0,0,5,5,2,2,2,2,2},
                       {0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,5,0,0,5,0,0,5,0,1,0,0,0,0,0,2,2,2,0,5,5,5,5,0,0,0,0,0,0,0,0,0,2,2,2,2,2},
                       {0,0,0,0,0,0,0,3,1,0,0,0,0,5,1,5,5,0,0,0,5,5,0,0,0,0,0,0,0,2,2,2,0,5,0,0,0,0,0,0,1,1,1,1,0,0,2,2,2,2,2},
                       {1,1,1,1,1,1,1,2,2,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,5,0,1,1,1,1,1,2,0,0,2,0,1,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,1,2,2,2,2,2,2,0,0,2,0,0,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,0,0,0,0,0,0,0,0,1,0,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,0,0,0,0,1,1,1,0,0,0,0,0,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,0,0,1,0,0,0,0,0,0,1,1,1,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,2,0,2,0,1,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,1,0,0,1,1,1,2,0,1,1,2,1,1,1,0,0,1,0,0,0,0,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,2,1,1,0,0,0,2,1,0,0,0,0,2,0,0,0,0,0,0,1,1,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,2,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,0,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                       {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                }, 50);
        }
    }

}