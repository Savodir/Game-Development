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


        #region Objects and vars
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private TileMap Current;
        private Lvl1 Lvl1;
        private Lvl2 Lvl2;
        player player;
        private Camera camera;
        private Texture2D enemygfx;
        private Texture2D tree;
        private Texture2D mapEnd;
        private Enemy enemy;
        //Menu
        private Menu btnStart, btnQuit;
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
            Lvl1 = new Lvl1(Content);
            Lvl2 = new Lvl2(Content);
            Current = Lvl1;
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
            player = new player(Content.Load<Texture2D>("shiba"), camera, Content, graphics.GraphicsDevice);
            //Level
            EntityManager.Content = Content;
            Lvl1.Content = Content;
            Events.Content = Content;
            //Menu
            IsMouseVisible = true;
            btnStart = new Menu(Content.Load<Texture2D>("paw"), graphics.GraphicsDevice);
            btnStart.pos(new Vector2(350, 350));
            pausedTexture = Content.Load<Texture2D>("Paused");
            pausedRectangle = new Rectangle(340, 160, 150, 150);
            btnQuit = new Menu(Content.Load<Texture2D>("Quit"), graphics.GraphicsDevice);
            btnQuit.pos(new Vector2(350, 275));
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
                        foreach (CollisionTiles tiles in Current.CollisionTiles)
                        {
                            player.Collision(tiles.Rectangle, Current.Width, Current.Height);
                            camera.Update(player.Position, Current.Width, Current.Height);
                        }
                        //Enemy Collision pls help
                        foreach (Enemy enemies in Current.EntityManager.Enemies)
                        {
                            player.EnemyCollision(enemies.Rectangle);
                            Console.WriteLine("Enemy: " + enemies.Rectangle);
                        }
                        foreach (Events events in Current.EventHandler.EventsList)
                        {
                            player.EventChecker(events, Current, Lvl1, Lvl2, spriteBatch);
                        }
                        if (Lvl1.Finished == true) Current = Lvl2;
                        spriteBatch.End();
                        //Update
                        Current.EntityManager.Update(gameTime);
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
            Console.WriteLine(Lvl2.Finished);
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
                    btnStart.Draw(spriteBatch);
                    break;
                //Game
                case GameState.Game:
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        camera.Transform);
                    Current.Draw(spriteBatch);
                    Current.EntityManager.Draw(spriteBatch);
                    player.Controls();
                    Current.EventHandler.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    if (Lvl2.Finished == true)
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
                spriteBatch.Draw(Content.Load<Texture2D>("MenuBack"),
                    new Rectangle(0, 0, backgroundWidth, backgroundHeight), Color.White);
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