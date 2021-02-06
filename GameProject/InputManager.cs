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


        public static bool Exit { get; private set; }

        public static void Update(GameTime gameTime)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            UpdatePlayerMovement(gameTime);
            MouseDirection = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit = true;
        }

        public static void UpdatePlayerMovement(GameTime gameTime)
        {
            PlayerDirection = Vector2.Zero;

            // Get position from Keyboard
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                PlayerDirection += new Vector2(-PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                PlayerDirection += new Vector2(PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                PlayerDirection += new Vector2(0, -PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                PlayerDirection += new Vector2(0, PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public static Vector2 GetMouseDirection(Vector2 screenSize)
        {
            return Vector2.Normalize((screenSize / 2 - MouseDirection) - PlayerDirection);
        }
    }
}
