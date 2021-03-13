using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject
{
    public class GameScreen
    {
        private Player player;

        private List<Enemy> enemies = new List<Enemy>();
        private Game game;
        private SpriteFont bangers;

        public GameScreen(Game game, SpriteFont bangers)
        {
            this.game = game;
            this.bangers = bangers;
            player = new Player(game);
            for (int i = 0; i < 5; i++)
            {
                enemies.Add(new Enemy(game, player));
            }
        }

        public void LoadContent()
        {
            player.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (Time.ScaledTime != 0)
            {
                player.Update(gameTime, enemies);
                foreach (Enemy e in enemies)
                {
                    e.Update(gameTime, enemies);
                }

            }
        }

        public void Restart()
        {
            player.SetStartPosition();
            InputManager.Score = 0;
            InputManager.TimesHit = 0;
            InputManager.TimeAlive = 0;
            foreach(Enemy e in enemies)
            {
                e.Kill();
            }
            player.Gun.KillBullets();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {        
            TimeSpan timeAlive = TimeSpan.FromSeconds((double)(new decimal(InputManager.TimeAlive)));
            SpriteRenderer.DrawSprites(spriteBatch, gameTime);
            spriteBatch.DrawString(bangers, $"Score: {InputManager.Score}", new Vector2(2, 2), Color.White);
            spriteBatch.DrawString(bangers, $"Times Hit: {InputManager.TimesHit}", new Vector2(2, 40), Color.White);
            spriteBatch.DrawString(bangers, $"Times Alive: {timeAlive.TotalMinutes:00}:{timeAlive.Seconds:00}.{timeAlive.Milliseconds:00}", new Vector2(2, 80), Color.White);
        }
    }
}
