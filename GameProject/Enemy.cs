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
        public float Speed { get; set; } = (float)(Helper.random.NextDouble() / 2 + 0.25);

        private Player target;

        public Enemy(Game game, Player target) : base(game)
        {
            this.target = target;
            LoadContent();
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            MoveTowardsTarget(gameTime);
            CheckForDamage(target.Gun.Bullets);
            CheckForEnemyCollisions(enemies);
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
            float randomSpotW = Helper.random.Next(-1, 1) * game.GraphicsDevice.Viewport.Width;
            float randomSpotH = Helper.random.Next(-1, 1) * game.GraphicsDevice.Viewport.Height;
            Position = new Vector2((float)(game.GraphicsDevice.Viewport.Width * Helper.random.NextDouble() + randomSpotW), (float)(game.GraphicsDevice.Viewport.Height * Helper.random.NextDouble() + randomSpotH));
        }

        public void CheckForDamage(List<Bullet> bullets)
        {
            foreach(Bullet b in bullets)
            {
                if (Collider.CollidesWith(b.Collider))
                {
                    SetStartPosition();
                    Speed = (float)(Helper.random.NextDouble() / 2 + 0.25);
                }
            }
        }

        public void MoveTowardsTarget(GameTime gameTime)
        {
            Vector2 moveDirection = target.Position - Position;
            Position += moveDirection * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void CheckForEnemyCollisions(List<Enemy> enemies)
        {
            foreach(Enemy e in enemies)
            {
                if (e != this && this.Collider.CollidesWith(e.Collider))
                {
                    Vector2 distance_inside = this.Position - e.Position;

                    if(distance_inside.Length() <= (float)(sprite.texture.Width * sprite.texture.Width))
                    {
                        Position += Vector2.Normalize(distance_inside) * (sprite.texture.Width - distance_inside.Length());
                    }
                }
            }
        }

    }
}
