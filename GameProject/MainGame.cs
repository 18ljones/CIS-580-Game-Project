using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameProject
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;

        private List<Enemy> enemies = new List<Enemy>();
        private SpriteFont bangers;
        private PauseMenu pauseMenu;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            player = new Player(this);
            for(int i = 0; i < 5; i++)
            {
                enemies.Add(new Enemy(this, player));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player.LoadContent();
            bangers = Content.Load<SpriteFont>("bangers");
            pauseMenu = new PauseMenu(this, bangers);
        }

        protected override void Update(GameTime gameTime)
        {
            Time.SetScaledTime((float)gameTime.ElapsedGameTime.TotalSeconds);

            // TODO: Add your update logic here
            InputManager.KeepMouseInWindow(this);
            InputManager.Update(gameTime);
            if (InputManager.Exit) Exit();
            if (Time.ScaledTime != 0)
            {
                player.Update(gameTime, enemies);
                foreach (Enemy e in enemies)
                {
                    e.Update(gameTime, enemies);
                }
                
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);
            TimeSpan timeAlive = TimeSpan.FromSeconds((double)(new decimal(InputManager.TimeAlive)));
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            SpriteRenderer.DrawSprites(_spriteBatch, gameTime);
            _spriteBatch.DrawString(bangers, $"Score: {InputManager.Score}", new Vector2(2, 2), Color.White);
            _spriteBatch.DrawString(bangers, $"Times Hit: {InputManager.TimesHit}", new Vector2(2, 40), Color.White);
            _spriteBatch.DrawString(bangers, $"Times Alive: {timeAlive.TotalMinutes:00}:{timeAlive.Seconds:00}.{timeAlive.Milliseconds:00}", new Vector2(2, 80), Color.White);

            if (InputManager.IsPaused)
            {
                pauseMenu.Draw(_spriteBatch, gameTime);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
