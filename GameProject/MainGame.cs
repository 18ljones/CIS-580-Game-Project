using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;

        private List<Enemy> enemies = new List<Enemy>();

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
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
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            InputManager.Update(gameTime);
            if (InputManager.Exit) Exit();
            player.Update(gameTime, enemies);
            foreach(Enemy e in enemies)
            {
                e.Update(gameTime, enemies);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            SpriteRenderer.DrawSprites(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
