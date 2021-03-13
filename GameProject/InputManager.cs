using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    public static class InputManager
    {
        public static KeyboardState currentKeyboardState;
        public static KeyboardState priorKeyboardState;
        public static MouseState currentMouseState;
        public static MouseState priorMouseState;

        public static Vector2 PlayerDirection { get; private set; }
        public static int PlayerSpeed { get; set; } = 250;
        public static Vector2 MouseDirection { get; private set; }
        public static int Score { get; set; } = 0;
        public static float TimeAlive { get; set; } = 0f;
        public static int TimesHit { get; set; } = 0;
        public static bool Exit { get; private set; }
        public static bool IsPlaying { get; set; } = false;
        public static bool Restart { get; set; } = false;
        public static int GameVolume { get; set; } = 100;
        public static GameState CurrentGameState { get; set; } = GameState.MainMenu;
        public static GameState PreviousGameState { get; set; } = GameState.MainMenu;

        public static void Update(GameTime gameTime)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            MouseDirection = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            if(CurrentGameState == GameState.Game)
                UpdatePlayerMovement(gameTime);

            if (currentKeyboardState.IsKeyDown(Keys.Escape) && priorKeyboardState.IsKeyUp(Keys.Escape) && (CurrentGameState == GameState.Game || CurrentGameState == GameState.PauseMenu))
            {
                Time.TimeScale = Time.TimeScale == 1 ? 0 : 1;
                if(CurrentGameState == GameState.Game)
                {
                    PreviousGameState = CurrentGameState;
                    CurrentGameState = GameState.PauseMenu;
                }
                else if(CurrentGameState == GameState.PauseMenu)
                {
                    PreviousGameState = CurrentGameState;
                    CurrentGameState = GameState.Game;
                }
                //Exit = true;
            }
            TimeAlive += Time.ScaledTime;
                
        }

        public static void UpdatePlayerMovement(GameTime gameTime)
        {
            PlayerDirection = Vector2.Zero;

            // Get position from Keyboard
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                PlayerDirection += new Vector2(-PlayerSpeed * Time.ScaledTime, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                PlayerDirection += new Vector2(PlayerSpeed * Time.ScaledTime, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                PlayerDirection += new Vector2(0, -PlayerSpeed * Time.ScaledTime);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                PlayerDirection += new Vector2(0, PlayerSpeed * Time.ScaledTime);
            }
        }

        public static Vector2 GetMouseDirection()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        public static void KeepMouseInWindow(Game game)
        {
            if (CurrentGameState == GameState.PauseMenu) return;

            int mouseX = Mouse.GetState().X;
            int MouseY = Mouse.GetState().Y;
            bool oob = false; //if mouse is out of bounds
            if (game.IsActive)
            {
                if (mouseX > game.GraphicsDevice.Viewport.Width)
                {
                    mouseX = game.GraphicsDevice.Viewport.Width;
                    oob = true;
                }
                else if (mouseX < 0)
                {
                    mouseX = 0;
                    oob = true;
                }
                
                if (MouseY > game.GraphicsDevice.Viewport.Height)
                {
                    MouseY = game.GraphicsDevice.Viewport.Height;
                    oob = true;
                }
                else if (MouseY < 0)
                {
                    MouseY = 0;
                    oob = true;
                }

                if(oob) 
                    Mouse.SetPosition(mouseX, MouseY);
            }
        }

        public static void LogError(string error)
        {
            System.Diagnostics.Debug.WriteLine(error);
        }
    }

    public enum GameState
    {
        MainMenu,
        SettingsMenu,
        PauseMenu,
        Game
    }
}
