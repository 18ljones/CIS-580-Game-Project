using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class PauseMenu
    {
        Texture2D background;
        Rectangle backgroundRec;
        private SpriteFont bangers;
        private Game game;
        private int gameHeight;
        private int gameWidth;
        private string pauseMessage = "Paused";

        public PauseMenu(Game game, SpriteFont bangers)
        {
            this.game = game;
            this.bangers = bangers;
            gameHeight = game.GraphicsDevice.Viewport.Height;
            gameWidth = game.GraphicsDevice.Viewport.Width;
            background = new Texture2D(game.GraphicsDevice, gameWidth, gameHeight);
            FillBackground();
            backgroundRec = new Rectangle(new Point(0, 0), new Point(gameWidth, gameHeight));
        }

        private void FillBackground()
        {
            Color[] data = new Color[gameWidth * gameHeight];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Gray;
            background.SetData(data);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 stringSize = bangers.MeasureString(pauseMessage);
            spriteBatch.Draw(background, backgroundRec, Color.White * 0.5f);
            spriteBatch.DrawString(bangers, pauseMessage, new Vector2(gameWidth / 2, gameHeight / 2 - 100) - stringSize / 2, Color.White);
        }
    }
}
