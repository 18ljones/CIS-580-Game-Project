using GameProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject
{
    public class SettingsMenu
    {
        Texture2D background;
        Rectangle backgroundRec;
        private SpriteFont bangers;
        private Game game;
        private int gameHeight;
        private int gameWidth;
        private List<UIElement> UiElements = new List<UIElement>();
        private string volume;
        private Vector2 volumeStringSize;

        public SettingsMenu(Game game, SpriteFont bangers)
        {
            this.game = game;
            this.bangers = bangers;
            gameHeight = game.GraphicsDevice.Viewport.Height;
            gameWidth = game.GraphicsDevice.Viewport.Width;
            background = new Texture2D(game.GraphicsDevice, gameWidth, gameHeight);
            volume = InputManager.GameVolume.ToString();
            volumeStringSize = bangers.MeasureString(volume);
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
            Vector2 settingsStringSize = bangers.MeasureString("Settings");
            Vector2 volumeHeaderStringSize = bangers.MeasureString("Volume");

            if(InputManager.PreviousGameState == GameState.PauseMenu)
                spriteBatch.Draw(background, backgroundRec, Color.Gray * 0.5f);
            else if(InputManager.PreviousGameState == GameState.MainMenu)
                spriteBatch.Draw(background, backgroundRec, Color.YellowGreen);

            spriteBatch.DrawString(bangers, "Settings", new Vector2(gameWidth / 2, gameHeight / 2 - 200) - settingsStringSize / 2, Color.White);
            spriteBatch.DrawString(bangers, "Volume", new Vector2(gameWidth / 2, gameHeight / 2 - volumeHeaderStringSize.Y) - volumeHeaderStringSize / 2, Color.White); ;
            spriteBatch.DrawString(bangers, volume, new Vector2(gameWidth / 2, gameHeight / 2) - volumeStringSize / 2, Color.White);
            foreach (Button b in UiElements)
            {
                b.Draw(gameTime, spriteBatch);
            }
        }

        private void CreateUIElements()
        {
            Vector2 stringSize;
            string buttonText;

            //Back to Back button
            buttonText = "Back";
            stringSize = bangers.MeasureString(buttonText);
            Action backAction = () => 
            {
                InputManager.CurrentGameState = InputManager.PreviousGameState;
                InputManager.PreviousGameState = GameState.SettingsMenu;
            };
            Button backButton = new Button(backAction, bangers, buttonText, new Vector2(gameWidth / 2, gameHeight / 2 + 100) - stringSize / 2);
            UiElements.Add(backButton);

            //Add volume button
            buttonText = "+";
            stringSize = bangers.MeasureString(buttonText);
            Action volumeUpButtonAction = () => 
            {
                if (InputManager.GameVolume < 100)
                {
                    SoundEffect.MasterVolume = MathHelper.Clamp(SoundEffect.MasterVolume + 0.05f, 0, 1);
                    MediaPlayer.Volume = MathHelper.Clamp(MediaPlayer.Volume + 0.05f, 0, 1);
                    InputManager.GameVolume += 5;
                    volume = InputManager.GameVolume.ToString();
                }   
            };
            Button volumeUpButton = new Button(volumeUpButtonAction, bangers, buttonText, new Vector2(gameWidth / 2 + volumeStringSize.X, gameHeight / 2) - stringSize / 2);
            UiElements.Add(volumeUpButton);
            
            //Add volume button
            buttonText = "-";
            stringSize = bangers.MeasureString(buttonText);
            Action volumeDownButtonAction = () => 
            {
                if (InputManager.GameVolume > 0)
                {
                    SoundEffect.MasterVolume = MathHelper.Clamp(SoundEffect.MasterVolume - 0.05f, 0 ,1);
                    MediaPlayer.Volume = MathHelper.Clamp(MediaPlayer.Volume - 0.05f, 0, 1);
                    InputManager.GameVolume -= 5;
                    volume = InputManager.GameVolume.ToString();
                }
            };
            Button volumeDownButton = new Button(volumeDownButtonAction, bangers, buttonText, new Vector2(gameWidth / 2 - volumeStringSize.X, gameHeight / 2) - stringSize / 2);
            UiElements.Add(volumeDownButton);
        }

        private void FillBackground()
        {
            Color[] data = new Color[gameWidth * gameHeight];
            for (int i = 0; i < data.Length; i++) data[i] = Color.White;
            background.SetData(data);
        }
    }
}
