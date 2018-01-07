using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vancluysen.Carl.AI;
using Vancluysen.Carl.Leveleditor;
using Vancluysen.Carl.Leveleditor;

namespace Vancluysen.Carl
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Objects and vars
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private TileMap current;
        private Lvl1 lvl1;
        private Lvl2 lvl2;
        Player player;
        private Camera camera;
        private Enemy enemy;

        //Menu
        private Menu btnStart, btnQuit, btnQwerty, btnAzerty;

        private bool qwerty = false;
        private bool paused = false;
        private Texture2D pausedTexture;

        private Rectangle pausedRectangle;

        //Background
        int backgroundWidth = 800;

        int backgroundHeight = 600;

        #endregion

        enum GameState
        {
            MainMenu,
            Game
        }

        private GameState gameState = GameState.MainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(GraphicsDevice.Viewport);
            lvl1 = new Lvl1(Content);
            lvl2 = new Lvl2(Content);
            current = lvl1;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Background
            graphics.PreferredBackBufferWidth = backgroundWidth;
            graphics.PreferredBackBufferHeight = backgroundHeight;
            //Player
            player = new Player(Content.Load<Texture2D>("shiba"), camera, Content, graphics.GraphicsDevice);
            //Level
            EntityManager.Content = Content;
            Lvl1.Content = Content;
            Events.Content = Content;
            //Menu
            IsMouseVisible = true;
            btnAzerty = new Menu(Content.Load<Texture2D>("azerty"), graphics.GraphicsDevice);
            btnAzerty.pos(new Vector2(200, 325));
            pausedTexture = Content.Load<Texture2D>("Paused");
            pausedRectangle = new Rectangle(340, 160, 150, 150);
            btnQuit = new Menu(Content.Load<Texture2D>("Quit"), graphics.GraphicsDevice);
            btnQuit.pos(new Vector2(backgroundWidth - 125, backgroundHeight - 75));
            btnQwerty = new Menu(Content.Load<Texture2D>("qwerty"), graphics.GraphicsDevice);
            btnQwerty.pos(new Vector2(450,325));
            //Apply
            graphics.ApplyChanges();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            base.Update(gameTime);
            MouseState mouse = Mouse.GetState();
            switch (gameState)
            {
                //Main Menu
                case GameState.MainMenu:
                    if (btnAzerty.click == true)
                    {
                        qwerty = false;
                        gameState = GameState.Game;
                    }
                    if (btnQwerty.click == true)
                    {
                        qwerty = true;
                        gameState = GameState.Game;
                    }
                    btnAzerty.Update(mouse);
                    btnQwerty.Update(mouse);
                    break;
                //Game
                case GameState.Game:
                    if (paused == false)
                    {
                        IsMouseVisible = false;
                        //PauseChecker
                        if (Keyboard.GetState().IsKeyDown(Keys.P))
                        {
                            paused = true;
                            btnQwerty.click = false;
                            btnAzerty.click = false;

                        }
                        //Tile Collision
                        foreach (CollisionTiles tiles in current.CollisionTiles)
                        {
                            player.Collision(tiles.Rectangle, current.Width, current.Height);
                            camera.Update(player.Position, current.Width, current.Height);
                        }
                        //Enemy Collision
                        foreach (Enemy enemies in current.EntityManager.Enemies)
                        {
                            player.EnemyCollision(enemies.Rectangle);
                           // Console.WriteLine("Enemy: " + enemies.Rectangle);
                        }
                        //Event Checker
                        foreach (Events events in current.EventHandler.EventsList)
                        {
                            player.EventChecker(events, current, lvl1, lvl2, spriteBatch);
                        }
                        if (lvl1.Finished == true) current = lvl2;
                        //Update
                        current.EntityManager.Update(gameTime);
                        player.Update(gameTime);
                    }
                    //Pause
                    else if (paused == true)
                    {
                        IsMouseVisible = true;
                        if (btnAzerty.click == true)
                        {
                            paused = false;
                            qwerty = false;
                        }
                        if (btnQwerty.click == true)
                        {
                            paused = false;
                            qwerty = true;
                        }
                        if (btnQuit.click == true)
                        {
                            Exit();
                        }
                        btnAzerty.Update(mouse);
                        btnQwerty.Update(mouse);

                        btnQuit.Update(mouse);
                    }
                    break;
            }
            Console.WriteLine(lvl2.Finished);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            switch (gameState)
            {
                //Menu
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MenuBack"),
                        new Rectangle(0, 0, backgroundWidth, backgroundHeight), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("GameName"), new Rectangle(backgroundWidth / 2 - 300,0,600,150), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("keyboardlayout"), new Rectangle(backgroundWidth / 2 - 200,250,400,75), Color.White);
                    btnAzerty.Draw(spriteBatch);
                    btnQwerty.Draw(spriteBatch);
                    break;
                //Game
                case GameState.Game:
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        camera.Transform);
                    current.Draw(spriteBatch);
                    current.EntityManager.Draw(spriteBatch);
                    player.Controls(qwerty);
                    current.EventHandler.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    if (lvl2.Finished == true)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(Content.Load<Texture2D>("endscreen"),
                            new Rectangle(0, 0, backgroundWidth, backgroundHeight), Color.White);
                        spriteBatch.End();
                    }
                    if (player.Lives == 0)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(Content.Load<Texture2D>("gameover"),
                            new Rectangle(0, 0, backgroundWidth, backgroundHeight), Color.White);
                        spriteBatch.End();
                    }


                    break;
            }
            if (paused == true)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Content.Load<Texture2D>("Paused"),
                    new Rectangle(backgroundWidth / 2 - 225, 100, 450, 75), Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>("keyboardlayout"), new Rectangle(backgroundWidth / 2 - 200, 250, 400, 75), Color.White);
                btnAzerty.Draw(spriteBatch);
                btnQwerty.Draw(spriteBatch);
                spriteBatch.Draw(Content.Load<Texture2D>("pauseScreen"),
                    new Rectangle(0, 0, backgroundWidth, backgroundHeight), new Color(Color.White, 0.5f));
                btnQuit.Draw(spriteBatch);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}