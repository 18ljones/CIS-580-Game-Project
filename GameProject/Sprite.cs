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
        public Texture2D texture { get; private set; }
        public Color Color { get; set; } = Color.White;
        private GameObject owner;
        public float Rotation { get; set; } = 0;

        public bool IsAlive { get; set; } = true;

        public Sprite(Texture2D texture, GameObject obj)
        {
            this.texture = texture;
            owner = obj;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, owner.Position, null, Color, Rotation, new Vector2(texture.Width/2f, texture.Height/2f), 1, SpriteEffects.None, 0);
        }

    }
}
