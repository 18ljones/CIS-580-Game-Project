using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Collisions;

namespace GameProject
{
    public class Sprite
    {
        public Texture2D texture { get; protected set; }
        public Color Color { get; set; } = Color.White;
        protected GameObject owner;
        public float Rotation { get; set; } = 0;

        public bool IsAlive { get; set; } = true;

        public Sprite(Texture2D texture, GameObject obj)
        {
            this.texture = texture;
            owner = obj;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, owner.Position, null, Color, Rotation, new Vector2(texture.Width/2f, texture.Height/2f), 1, SpriteEffects.None, 0);
        }

    }
}
