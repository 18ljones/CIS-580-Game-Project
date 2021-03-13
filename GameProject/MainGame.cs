using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using GameProject.UI;
using Microsoft.Xna.Framework.Media;

namespace GameProject
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private SpriteFont bangers;
        private Song backgroundMusic;
        private GameScreen gameScreen;
        private PauseMenu pauseMenu;
        private MainMenu mainMenu;
        private SettingsMenu settingsMenu;

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
            // TODO: Add your initialization logic her

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bangers = Content.Load<SpriteFont>("bangers");
            backgroundMusic = Content.Load<Song>("Acción (Merodeador Nocturno)");
            pauseMenu = new PauseMenu(this, bangers);
            mainMenu = new MainMenu(this, bangers);
            settingsMenu = new SettingsMenu(this, bangers);
            gameScreen = new GameScreen(this, bangers);
            gameScreen.LoadContent();

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            Time.SetScaledTime((float)gameTime.ElapsedGameTime.TotalSeconds);

            // TODO: Add your update logic here
            InputManager.KeepMouseInWindow(this);
            InputManager.Update(gameTime);
            
            if (InputManager.Exit) Exit();           

            if(InputManager.CurrentGameState == GameState.MainMenu)
            {
                mainMenu.Update(gameTime);
            }

            if(InputManager.CurrentGameState == GameState.Game)
            {
                gameScreen.Update(gameTime);
            }

            if (InputManager.CurrentGameState == GameState.PauseMenu)
            {
                pauseMenu.Update(gameTime);
            }

            if (InputManager.CurrentGameState == GameState.SettingsMenu)
            {
                settingsMenu.Update(gameTime);
            }

            if (InputManager.Restart)
            {
                gameScreen.Restart();
                InputManager.Restart = false;
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            

            if (InputManager.CurrentGameState == GameState.MainMenu)
            {
                mainMenu.Draw(_spriteBatch, gameTime);
            }

            if (InputManager.CurrentGameState == GameState.Game || InputManager.IsPlaying)
            {
                gameScreen.Draw(gameTime, _spriteBatch);
            }

            if (InputManager.CurrentGameState == GameState.PauseMenu)
            {
                pauseMenu.Draw(_spriteBatch, gameTime);
            }

            if(InputManager.CurrentGameState == GameState.SettingsMenu)
            {
                settingsMenu.Draw(_spriteBatch, gameTime);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
