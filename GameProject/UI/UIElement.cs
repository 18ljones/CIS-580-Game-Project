using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.UI
{
    public class UIElement
    {
        public Action UiAction { get; set; }
        protected SpriteFont font;

        public UIElement(Action a, SpriteFont font)
        {
            UiAction = a;
            this.font = font;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
