﻿using System;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Objects and vars
        player player;
        Levels level;
        private Camera camera;
        private Texture2D enemygfx;
        private Enemy enemy;
        EntityManager entityManager;
        private Leveleditor.EventHandler eventHandler;
        private int levelid = 0;
        //Menu
        private Menu btnStart, btnQuit;
        private bool paused = false;
        private Texture2D pausedTexture;
        private Rectangle pausedRectangle;
        //Background
        Rectangle bpos;
        Rectangle bposUnder;
        Texture2D background;
        Texture2D backgroundUnderground;
        private Texture2D tree;
        int backgroundWidth = 800;
        int backgroundHeight = 600;
        #endregion

        enum GameState
        {
            MainMenu,  Game
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
            entityManager = new EntityManager();
            eventHandler = new Leveleditor.EventHandler();
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
            background = Content.Load<Texture2D>("harbor");
            bpos = new Rectangle(0, 0, 1920, 700);
            bposUnder = new Rectangle(0, 700,2500,600);
            tree = Content.Load<Texture2D>("tree");
            backgroundUnderground = Content.Load<Texture2D>("ruien");            //Enemies
            enemygfx = Content.Load<Texture2D>("police");
            LoadEnemies();
            //Player
            player = new player(Content.Load<Texture2D>("shiba"));
            //Level
            level = new Levels();
            EntityManager.Content = Content;
            Levels.Content = Content;
            Events.Content = Content;
            level.LevelSelector(levelid);
            //Menu
            IsMouseVisible = true;
            btnStart = new Menu(Content.Load<Texture2D>("paw"), graphics.GraphicsDevice);
            btnStart.pos(new Vector2(350, 350));
            pausedTexture = Content.Load<Texture2D>("Paused");
            pausedRectangle = new Rectangle(0, 0, pausedTexture.Width, pausedTexture.Height);
            btnQuit = new Menu(Content.Load<Texture2D>("Quit"), graphics.GraphicsDevice);
            btnQuit.pos(new Vector2(350,275));
            //Events
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            base.Update(gameTime);
            MouseState mouse = Mouse.GetState();
            switch (gameState)
            {
                //Main Menu
                case GameState.MainMenu:
                    //StartChecker
                    if (btnStart.click == true)
                    {
                        gameState = GameState.Game;
                        
                    }
                    btnStart.Update(mouse);
                    break;
                //Game
                case GameState.Game:
                    //Game
                    if (paused == false)
                    {
                        IsMouseVisible = false;
                        //PauseChecker
                        if (Keyboard.GetState().IsKeyDown(Keys.P))
                        {
                            paused = true;
                            btnStart.click = false;
                        }
                        //Tile Collision
                        foreach (CollisionTiles tiles in level.Map.CollisionTiles)
                        {
                            player.Collision(tiles.Rectangle, level.Map.Width, level.Map.Height);
                            camera.Update(player.Position, level.Map.Width, level.Map.Height);
                        }
                        //Enemy Collision pls help
                        foreach (Enemy enemies in entityManager.Enemies)
                        {
                     //      player.EnemyCollision(enemies.Rectangle);
                            Console.WriteLine("Enemy: " + enemies.Rectangle);
                        }
                        foreach (Events events in eventHandler.EventsList)
                        {
                           player.EventChecker(events.Rectangle, events.EventID);
                        }
                        //Update
                        entityManager.Update(gameTime, levelid);
                        player.Update(gameTime);

                    }
                    //Pause
                    else if (paused == true)
                    {
                        IsMouseVisible = true;
                        if (btnStart.click == true)
                        {
                            paused = false;
                        }
                        if (btnQuit.click == true)
                        {
                            Exit();
                        }
                        btnStart.Update(mouse);
                        btnQuit.Update(mouse);
                    }
                    break;
            }

            
        }

        public void LoadEnemies()
        {
            switch (levelid)
            {
                case 0:
                    entityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(0, 0), 90));
                    entityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(630, 432), 90));
                    entityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(400, 432), 90));
                    entityManager.Enemies.Add(new Enemy(enemygfx, new Vector2(1000, 600), 90)); 
                    eventHandler.EventsList.Add(new Events(0,tree, new Vector2( 690, 430)));
                    eventHandler.EventsList.Add(new Events(0,tree, new Vector2(2140, 1030)));
                    break;
            }
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
            {   //Menu
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MenuBack"), new Rectangle(0, 0, backgroundWidth, backgroundHeight), Color.White);
                    btnStart.Draw(spriteBatch);
                    break;
                //Game
                case GameState.Game:
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
                    spriteBatch.Draw(background, bpos, Color.White);
                    spriteBatch.Draw(backgroundUnderground, bposUnder, Color.White);
                    level.Map.Draw(spriteBatch);
                    entityManager.Draw(spriteBatch);
                    player.Controls();
                    eventHandler.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }
            if (paused == true)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Content.Load<Texture2D>("MenuBack"), new Rectangle(0, 0, backgroundWidth, backgroundHeight), Color.White);
                spriteBatch.Draw(pausedTexture, pausedRectangle, Color.White);
                btnStart.Draw(spriteBatch);
                btnQuit.Draw(spriteBatch);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
