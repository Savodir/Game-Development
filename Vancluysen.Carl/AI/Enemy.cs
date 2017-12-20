using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vancluysen.Carl.Collision;

namespace Vancluysen.Carl.AI
{
    class Enemy : EntityManager
    {
        private Rectangle rectangle;

        public Rectangle Rectangle  
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        private Animation animation;
        public Animation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        private Texture2D texture;

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 origin;
        private Vector2 velocity;
        private float rotation;
        private bool right;
        private float distance;
        private float oldDistance;
        private SpriteEffects s = new SpriteEffects();

        public Enemy(Texture2D _texture, Vector2 _position, float _distance)
        {
            texture = _texture;
            position = _position;
            distance = _distance;
            oldDistance = distance;
            LoadAnimation();
            animation = PoliceWalk;
        }

        public void Update(GameTime gameTime)
        {
            moveControl(gameTime);
           position += velocity;
           origin = new Vector2(texture.Width / 2, texture.Height/2);
            if (distance <= 0)
            {
                right = true;
                velocity.X = 1f;
               rectangle = new Rectangle((int)position.X, (int)position.Y, animation.CurrentFrame.SourceRectangle.Width, animation.CurrentFrame.SourceRectangle.Height);

            }
        else if (distance >= oldDistance)
            {
                right = false;
                velocity.X = -1f;
               rectangle = new Rectangle((int)position.X, (int)position.Y, animation.CurrentFrame.SourceRectangle.Width, animation.CurrentFrame.SourceRectangle.Height);

            }
            if (right == true)
            {
                distance += 1;
            }
            else
            {
               distance -= 1f;
            }
        }

        private Animation PoliceWalk;
        private void LoadAnimation()
        {
            PoliceWalk = new Animation();
            PoliceWalk.AddFrame(new Rectangle(12, 140, 30, 50));
            PoliceWalk.AddFrame(new Rectangle(60, 140, 30, 50));
            PoliceWalk.AddFrame(new Rectangle(108, 140, 30, 50));
            PoliceWalk.AddFrame(new Rectangle(156, 140, 30, 50));
            PoliceWalk.FramesPerS = 15;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (velocity.X > 0)
            {
                s = SpriteEffects.None;

                spriteBatch.Draw(texture, position, PoliceWalk.CurrentFrame.SourceRectangle, Color.White, rotation, origin, 1f, s, 0f);
            }
            else
            {
                s = SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(texture, position, PoliceWalk.CurrentFrame.SourceRectangle, Color.White, rotation, origin, 1f, s , 0f);

            }
        }

        private void moveControl(GameTime gameTime)
        {
            PoliceWalk.Update(gameTime);
        }

    }
}
