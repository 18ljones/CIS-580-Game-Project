using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Collisions;

namespace GameProject
{
    public class AnimatedSprite : Sprite
    {
        public Nullable<Rectangle> SourceRectangle = null;
        protected int animationFrame = 1;
        /// <summary>
        /// the size of each sprite in the sprite sheet
        /// </summary>
        protected int spriteSize;
        protected int totalSprites;
        protected double animationSpeed;
        protected double animationTimer;

        public AnimatedSprite(Texture2D texture, GameObject obj, int spriteSize, int totalSprites, double animationSpeed) : base(texture, obj)
        {
            this.spriteSize = spriteSize;
            this.totalSprites = totalSprites;
            this.animationSpeed = animationSpeed;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            UpdateAnimationFrame(gameTime);
            spriteBatch.Draw(texture, owner.Position, SourceRectangle, Color, Rotation, new Vector2(spriteSize / 2f, spriteSize / 2f), 1, SpriteEffects.None, 0);
        }

        public virtual void UpdateAnimationFrame(GameTime gameTime)
        {
            if (IsAlive)
            {
                animationTimer += Time.ScaledTime;

                if(animationTimer > animationSpeed)
                {
                    animationFrame++;
                    if (animationFrame > totalSprites)
                    {
                        animationFrame = 1;
                    }
                    animationTimer -= animationSpeed;
                }

                SourceRectangle = new Rectangle(spriteSize, spriteSize * animationFrame, spriteSize, spriteSize);
            }
        }
    }
}
