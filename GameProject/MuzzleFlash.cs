using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class MuzzleFlash : AnimatedSprite
    {
        public MuzzleFlash(Texture2D texture, GameObject obj, int spriteSize, int totalSprites, double animationSpeed) : base(texture, obj, spriteSize, totalSprites, animationSpeed)
        {
            IsAlive = false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            UpdateAnimationFrame(gameTime);
            spriteBatch.Draw(texture, owner.Position, SourceRectangle, Color, owner.sprite.Rotation, new Vector2(spriteSize / 2f, owner.sprite.texture.Height / 2f + spriteSize), 1, SpriteEffects.None, 0);
        }

        public override void UpdateAnimationFrame(GameTime gameTime)
        {
            animationTimer += Time.ScaledTime;

            if (animationTimer > animationSpeed)
            {
                animationFrame++;
                if (animationFrame > totalSprites)
                {
                    IsAlive = false;
                    animationFrame = 1;
                }
                animationTimer -= animationSpeed;
            }
            SourceRectangle = new Rectangle(spriteSize * animationFrame, 0, spriteSize, spriteSize);
        }

        public void SpawnMuzzleFlash()
        {
            IsAlive = true;
        }
    }
}
