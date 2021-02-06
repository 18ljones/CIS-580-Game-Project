using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class Enemy : GameObject
    {
        public int Health { get; set; } = 1;
        public int Speed { get; set; } = 1;


        public Enemy(Game game) : base(game)
        {
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateColliderPosition();
        }

        public override void LoadContent()
        {
            sprite = new Sprite(game.Content.Load<Texture2D>("ball"), this);
            sprite.Color = Color.White;
            SpriteRenderer.Sprites.Add(sprite);
            SetStartPosition();
            Collider = new Collisions.BoundingCircle(Position, sprite.texture.Width / 2);
        }

        public override void SetStartPosition()
        {
            Position = new Vector2((float)(game.GraphicsDevice.Viewport.Width * Helper.random.NextDouble()), (float)(game.GraphicsDevice.Viewport.Height * Helper.random.NextDouble()));
        }

        public void CheckForDamage(List<Bullet> bullets)
        {
            foreach(Bullet b in bullets)
            {
                if (Collider.CollidesWith(b.Collider))
                {
                    SetStartPosition();
                    UpdateColliderPosition();
                }
            }
        }

    }
}
