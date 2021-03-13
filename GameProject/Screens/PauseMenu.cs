using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.UI;

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
        private List<UIElement> UiElements = new List<UIElement>();

        public PauseMenu(Game game, SpriteFont bangers)
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

        private void FillBackground()
        {
            Color[] data = new Color[gameWidth * gameHeight];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Gray;
            background.SetData(data);
        }

        public void Update(GameTime gameTime)
        {
            foreach(Button b in UiElements)
            {
                b.Update(gameTime);
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 stringSize = bangers.MeasureString(pauseMessage);
            spriteBatch.Draw(background, backgroundRec, Color.White * 0.5f);
            spriteBatch.DrawString(bangers, pauseMessage, new Vector2(gameWidth / 2, gameHeight / 2 - 200) - stringSize / 2, Color.White);
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
                InputManager.PreviousGameState = GameState.PauseMenu;
            };
            Button settingsButton = new Button(settingsAction, bangers, buttonText, new Vector2(gameWidth / 2, gameHeight / 2 - 50) - stringSize / 2);
            UiElements.Add(settingsButton);

            //Back to Main Menu button
            buttonText = "Back to Main Menu";
            stringSize = bangers.MeasureString(buttonText);
            Action backToMMAction = () => 
            {
                InputManager.CurrentGameState = GameState.MainMenu;
                InputManager.PreviousGameState = GameState.PauseMenu;
                InputManager.IsPlaying = false;
                InputManager.Restart = true;
            };
            Button backtoMMButton = new Button(backToMMAction, bangers, buttonText, new Vector2(gameWidth / 2, gameHeight / 2 + 50) - stringSize / 2);
            UiElements.Add(backtoMMButton);

            //Restart button
            buttonText = "Restart";
            stringSize = bangers.MeasureString(buttonText);
            Action restartAction = () =>
            {
                InputManager.CurrentGameState = GameState.Game;
                InputManager.PreviousGameState = GameState.PauseMenu;
                Time.TimeScale = 1;
                InputManager.Restart = true;
            };
            Button restartButton = new Button(restartAction, bangers, buttonText, new Vector2(gameWidth / 2, gameHeight / 2) - stringSize / 2);
            UiElements.Add(restartButton);

            //Quit game Button
            buttonText = "Quit";
            stringSize = bangers.MeasureString(buttonText);
            Action quitButtonAction = () => { game.Exit(); };
            Button quitButton = new Button(quitButtonAction, bangers, buttonText, new Vector2(gameWidth / 2, gameHeight / 2 + 100) - stringSize / 2);
            UiElements.Add(quitButton);
        }
    }
}
