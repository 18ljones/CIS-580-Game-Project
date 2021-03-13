using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameProject.Collisions;

namespace GameProject.UI
{
    public class Button : UIElement
    {
        private string buttonText;
        public Vector2 position { get; private set; }
        public int TextScale { get; set; } = 1;
        public Color TextColor { get; set; } = Color.White;
        private BoundingRectangle boundingRec;
        private bool selected = false;


        public Button(Action a, SpriteFont font, string text, Vector2 position) : base(a, font)
        {
            buttonText = text;
            this.position = position;
            generateBoundingRec();
        }

        public override void Update(GameTime gameTime)
        {
            CheckIfSelected();
            CheckClicked();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, buttonText, position, TextColor);
        }

        public void OnMouseOver()
        {
            TextColor = Color.LightGray;
        }

        public void OnMouseLeave()
        {
            TextColor = Color.White;
        }

        private void generateBoundingRec()
        {
            Vector2 stringSize = font.MeasureString(buttonText);
            boundingRec = new BoundingRectangle(position.X, position.Y, stringSize.X, stringSize.Y);
        }

        private void CheckIfSelected()
        {
            if(!selected && CollisionHelper.Contains(boundingRec, InputManager.currentMouseState.X, InputManager.currentMouseState.Y))
            {
                selected = true;
                OnMouseOver();
            }
            else if (selected && !CollisionHelper.Contains(boundingRec, InputManager.currentMouseState.X, InputManager.currentMouseState.Y))
            {
                OnMouseLeave();
                selected = false;
            }
        }

        private void CheckClicked()
        {  
            //CHECK BUTTON CLICK AND CALL ACTION
            if (selected && InputManager.currentMouseState.LeftButton == ButtonState.Pressed && InputManager.priorMouseState.LeftButton != ButtonState.Pressed)
            {
                UiAction();
            }
        }
    }
}
