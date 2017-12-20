using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Vancluysen.Carl.Collision;
using Vancluysen.Carl.Leveleditor;

namespace Vancluysen.Carl
{
    class player
    {
        private Texture2D shibagfx;
        private int maxjump = 450;
        private Vector2 spriteOrigin;
        private Vector2 velocity;
        private Vector2 spawnPosition = new Vector2(0,525);
        private Vector2 position = new Vector2(2156,1075);
        private int life = 5;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private float rotation;
        private SpriteEffects s = new SpriteEffects();
        public Animation _animation;
        private Texture2D _texture;
        private Rectangle rectangle;
        public player(Texture2D texture)
        {
            _texture = texture;
            LoadAnimation();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, _animation.CurrentFrame.SourceRectangle, Color.White, rotation,
                spriteOrigin, 1f, s, 0);
        }

        public void Update(GameTime gametime)
        {
            position += velocity;
            MoveControl(gametime);
            rectangle = new Rectangle((int)position.X, (int)position.Y, _animation.CurrentFrame.SourceRectangle.Width, _animation.CurrentFrame.SourceRectangle.Height);
          if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
            Console.WriteLine("Player: " + spawnPosition);
        }

        private Boolean right, left, runright, runleft, bark, pee, flip = false, jumped = false;

        public void Controls()
        {
            KeyboardState stateKey = Keyboard.GetState();
            if (stateKey.IsKeyDown(Keys.A))
            {
                left = true;
                _animation = AnimWalk;
                s = SpriteEffects.FlipHorizontally;
            }
            if (stateKey.IsKeyUp(Keys.A))
            {
                left = false;
            }
            if (stateKey.IsKeyDown(Keys.D))
            {
                right = true;
                _animation = AnimWalk;
                s = SpriteEffects.None;
            }
            if (stateKey.IsKeyUp(Keys.D))
            {
                right = false;
            }
            if (stateKey.IsKeyDown(Keys.D) && stateKey.IsKeyDown(Keys.LeftShift))
            {
                runright = true;
                _animation = AnimRun;
            }
            if (stateKey.IsKeyUp(Keys.D) || stateKey.IsKeyUp(Keys.LeftShift))
            {
                runright = false;
            }
            if (stateKey.IsKeyDown(Keys.A) && stateKey.IsKeyDown(Keys.LeftShift))
            {
                runleft = true;
                _animation = AnimRun;
            }
            if (stateKey.IsKeyUp(Keys.A) || stateKey.IsKeyUp(Keys.LeftShift))
            {
                runleft = false;
            }
            if (stateKey.IsKeyDown(Keys.E) && isStill() == true)
            {
                bark = true;
                _animation = AnimBark;
            }
            if (stateKey.IsKeyUp(Keys.E))
            {
                bark = false;
            }
            if (stateKey.IsKeyDown(Keys.Q) && isStill() == true)
            {
                pee = true;
                _animation = AnimPee;
            }
            if (stateKey.IsKeyUp(Keys.Q))
            {
                pee = false;
            }
            if (flip == true)
            {
                _animation = AnimFlip;
                position.Y -= 5;
                velocity.Y = -10f;
            }
            if (left == true && right == true)
            {
                left = false;
                right = false;
                runright = false;
                runleft = false;
                velocity.X = 0f;
                _animation = AnimIdle;
            }
            if (stateKey.IsKeyDown(Keys.Space) && jumped == false)
            {
                position.Y -= 4f;
                velocity.Y = -9f;
                jumped = true;
                AnimJump.Counter = 0;
            }
            if (jumped == true) _animation = AnimJump;

            else if (isStill() == true && bark == false &&
                     pee == false)
            {
                velocity.X = 0f;
                _animation = AnimIdle;
            }
        }

        private Animation AnimIdle, AnimWalk, AnimRun, AnimJump, AnimBark, AnimPee, AnimFlip;

        private void LoadAnimation()
        {
            AnimIdle = new Animation();
            AnimIdle.AddFrame(new Rectangle(1, 2, 35, 26));
            AnimIdle.AddFrame(new Rectangle(38, 2, 35, 26));
            AnimIdle.AddFrame(new Rectangle(73, 2, 35, 26));
            AnimIdle.AddFrame(new Rectangle(110, 2, 35, 26));
            AnimIdle.FramesPerS = 15;

            AnimWalk = new Animation();
            AnimWalk.AddFrame(new Rectangle(1, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(37, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(73, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(108, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(142, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(177, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(212, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(247, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(282, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(317, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(352, 240, 35, 26));
            AnimWalk.AddFrame(new Rectangle(357, 240, 35, 26));
            AnimWalk.FramesPerS = 15;

            AnimRun = new Animation();
            AnimRun.AddFrame(new Rectangle(1, 266, 35, 21));
            AnimRun.AddFrame(new Rectangle(40, 268, 35, 21));
            AnimRun.AddFrame(new Rectangle(74, 266, 35, 21));
            AnimRun.AddFrame(new Rectangle(111, 266, 35, 21));
            AnimRun.AddFrame(new Rectangle(147, 266, 35, 21));
            AnimRun.FramesPerS = 15;

            AnimJump = new Animation();
            AnimJump.AddFrame(new Rectangle(1, 292, 33, 23));
            AnimJump.AddFrame(new Rectangle(42, 292, 33, 23));
            AnimJump.AddFrame(new Rectangle(80, 292, 33, 23));
            AnimJump.AddFrame(new Rectangle(117, 289, 33, 23));
            AnimJump.AddFrame(new Rectangle(156, 291, 33, 23));
            AnimJump.AddFrame(new Rectangle(195, 291, 32, 23));
            AnimJump.AddFrame(new Rectangle(232, 292, 32, 23));
            AnimJump.AddFrame(new Rectangle(268, 292, 32, 23));
            AnimJump.AddFrame(new Rectangle(307, 291, 32, 23));
            AnimJump.AddFrame(new Rectangle(345, 291, 32, 23));
            AnimJump.FramesPerS = 15;

            AnimBark = new Animation();
            AnimBark.AddFrame(new Rectangle(2, 62, 34, 23));
            AnimBark.AddFrame(new Rectangle(40, 62, 34, 23));
            AnimBark.AddFrame(new Rectangle(78, 59, 34, 23));
            AnimBark.AddFrame(new Rectangle(115, 62, 34, 23));
            AnimBark.AddFrame(new Rectangle(152, 62, 34, 23));
            AnimBark.AddFrame(new Rectangle(190, 59, 34, 23));
            AnimBark.AddFrame(new Rectangle(226, 60, 34, 23));
            AnimBark.AddFrame(new Rectangle(263, 61, 34, 23));
            AnimBark.AddFrame(new Rectangle(300, 59, 34, 23));
            AnimBark.AddFrame(new Rectangle(337, 59, 34, 23));
            AnimBark.AddFrame(new Rectangle(374, 62, 34, 23));
            AnimBark.AddFrame(new Rectangle(410, 62, 34, 23));
            AnimBark.AddFrame(new Rectangle(446, 62, 34, 23));
            AnimBark.FramesPerS = 15;

            AnimPee = new Animation();
            AnimPee.AddFrame(new Rectangle(2, 140, 36, 23));
            AnimPee.AddFrame(new Rectangle(41, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(81, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(123, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(161, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(200, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(239, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(276, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(313, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(352, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(392, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(430, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(470, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(510, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(547, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(590, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(629, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(668, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(707, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(748, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(785, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(825, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(863, 140, 33, 23));
            AnimPee.AddFrame(new Rectangle(19, 164, 33, 23));
            AnimPee.AddFrame(new Rectangle(53, 165, 33, 23));
            AnimPee.AddFrame(new Rectangle(94, 165, 33, 23));
            AnimPee.AddFrame(new Rectangle(132, 165, 33, 23));
            AnimPee.AddFrame(new Rectangle(171, 140, 33, 23));
            AnimPee.FramesPerS = 15;

            AnimFlip = new Animation();
            AnimFlip.AddFrame(new Rectangle(6, 347, 32, 23));
            AnimFlip.AddFrame(new Rectangle(44, 347, 32, 23));
            AnimFlip.AddFrame(new Rectangle(82, 347, 32, 23));
            AnimFlip.AddFrame(new Rectangle(122, 328, 32, 35));
            AnimFlip.AddFrame(new Rectangle(160, 322, 32, 23));
            AnimFlip.AddFrame(new Rectangle(201, 321, 32, 33));
            AnimFlip.AddFrame(new Rectangle(237, 324, 32, 18));
            AnimFlip.AddFrame(new Rectangle(276, 336, 32, 35));
            AnimFlip.AddFrame(new Rectangle(311, 347, 32, 35));
            AnimFlip.AddFrame(new Rectangle(348, 347, 32, 35));
            AnimFlip.AddFrame(new Rectangle(3866, 347, 32, 35));

            AnimFlip.FramesPerS = 10;
        }

        private void MoveControl(GameTime gametime)
        {
            if (right == true)
            {
                velocity.X = 1.33f;
                AnimWalk.Update(gametime);
            }
            if (left == true)
            {
                velocity.X = -1.3f;
                AnimWalk.Update(gametime);
            }
            if (runright == true)
            {
                velocity.X = 2f;
                AnimRun.Update(gametime);
            }
            if (runleft == true)
            {
                velocity.X = -2f;
                AnimRun.Update(gametime);
            }

            if (bark == true)
            {
                velocity.X = 0;
                velocity.Y = 0;
                AnimBark.Update(gametime);
            }
            if(pee == true)
            {
                velocity.X = 0;
                velocity.Y = 0;
                AnimPee.Update(gametime);
            }
            if(flip == true)
            {
                AnimFlip.Update(gametime);
            }
            if (jumped == true)
            {
                AnimJump.Update(gametime);
            }
            else
            {
                _animation = AnimIdle;
                AnimIdle.Update(gametime);
            }
        }

        public void Collision(Rectangle rect, int x, int y)
        {
            if(rectangle.IsTop(rect))
            {
                rectangle.Y = rect.Y - rectangle.Height;
                velocity.Y = 0f;
                jumped = false;
                flip = false;
            } 
            if (rectangle.OnLeft(rect))
            {
                position.X = rect.X - rectangle.Width - 5;
            }
            if (rectangle.onRight(rect))
            {
                position.X = rect.X + rectangle.Width + 15;
            }
            if (rectangle.IsBottom(rect))
            {
                velocity.Y = 1f;
            } 
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X > x - rectangle.Width)
            {
                position.X = x - rectangle.Width;
            }
            if (position.Y < 0)
            {
                velocity.Y = 1f;
            }
            if (position.Y > y - rectangle.Height)
            {
                position.Y = y - rectangle.Height;
            }
            
        }

        public void EnemyCollision(Rectangle rect)
        {
            if (rectangle.EnemyTop(rect))
            {
                life--;
                position = spawnPosition;
            }
            else flip = false;
        }

        public void EventChecker(Rectangle rect, int eventID)
        {
            if (rectangle.TreeChecker(rect, pee == true, eventID = 0))
            {
                spawnPosition.X = rectangle.X;
                spawnPosition.Y = rectangle.Y;
            }
        }
        private bool isStill()
        {
            if (right == false && left == false && runleft == false && runright == false && jumped == false &&
                flip == false) return true;
            else return false;
        }

    }
}