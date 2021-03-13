using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using GameProject.UI;

namespace GameProject
{
    public class MainMenu
    {
        Texture2D background;
        Rectangle backgroundRec;
        private SpriteFont bangers;
        private Game game;
        private int gameHeight;
        private int gameWidth;
        private List<UIElement> UiElements = new List<UIElement>();

        public MainMenu(Game game, SpriteFont bangers)
        {
            this.game = game;
            this.bangers = bangers;
            gameHeight = game.GraphicsDevice.Viewport.Height;
            gameWidth = game.GraphicsDevice.Viewport.Width;
            background = new Texture2D(game.GraphicsDevice, gameWidth, gameHeight);
            FillBackground();
            backgroundRec = new Rectangle(new Point(0, 0), new Point(gameWidth, gameHeight));
            CreateUIElements();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Button b in UiElements)
            {
                b.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 stringSize = bangers.MeasureString("Main Menu");
            spriteBatch.Draw(background, backgroundRec, Color.White);
            spriteBatch.DrawString(bangers, "Main Menu", new Vector2(gameWidth / 2, gameHeight / 2 - 200) - stringSize / 2, Color.White);
            foreach (Button b in UiElements)
            {
                b.Draw(gameTime, spriteBatch);
            }
        }

        private void CreateUIElements()
        {
            Vector2 stringSize;
            string buttonText;

            //Settings button
            buttonText = "Settings";
            stringSize = bangers.MeasureString(buttonText);
            Action settingsAction = () => 
            {
                InputManager.CurrentGameState = GameState.SettingsMenu;
                InputManager.PreviousGameState = GameState.MainMenu;
            };
            Button settingsButton = new Button(settingsAction, bangers, buttonText, new Vector2(gameWidth / 2, gameHeight / 2) - stringSize / 2);
            UiElements.Add(settingsButton);

            //Back to Play button
            buttonText = "Play";
            stringSize = bangers.MeasureString(buttonText);
            Action playAction = () => 
            {
                InputManager.CurrentGameState = GameState.Game;
                InputManager.PreviousGameState = GameState.MainMenu;
                Time.TimeScale = 1;
                InputManager.IsPlaying = true;
            };
            Button playButton = new Button(playAction, bangers, buttonText, new Vector2(gameWidth / 2, gameHeight / 2 - 50) - stringSize / 2);
            UiElements.Add(playButton);

            //Quit game Button
            buttonText = "Quit";
            stringSize = bangers.MeasureString(buttonText);
            Action quitButtonAction = () => { game.Exit(); };
            Button quitButton = new Button(quitButtonAction, bangers, buttonText, new Vector2(gameWidth / 2, gameHeight / 2 + 50) - stringSize / 2);
            UiElements.Add(quitButton);
        }

        private void FillBackground()
        {
            Color[] data = new Color[gameWidth * gameHeight];
            for (int i = 0; i < data.Length; i++) data[i] = Color.YellowGreen;
            background.SetData(data);
        }

    }
}
