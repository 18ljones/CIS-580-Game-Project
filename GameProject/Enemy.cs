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
        public float Speed { get; set; } = (float)(Helper.random.NextDouble() * 300 + 150);

        private Player target;

        public Enemy(Game game, Player target) : base(game)
        {
            this.target = target;
            LoadContent();
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            IsAlive = true;
            if (!CheckForDamage(target.Gun.Bullets))
            {
                MoveTowardsTarget(gameTime);
                CheckForEnemyCollisions(enemies);
            }
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

        public bool CheckForDamage(List<Bullet> bullets)
        {
            foreach(Bullet b in bullets)
            {
                if (Collider.CollidesWith(b.Collider) && b.IsAlive)
                {
                    this.Kill();
                    Speed = (float)(Helper.random.NextDouble() * 200 + 100);
                    InputManager.Score++;
                    InputManager.LogError("dead");
                    b.Kill();
                    return true;
                }
            }
            return false;
        }

        public void MoveTowardsTarget(GameTime gameTime)
        {
            Vector2 distanceBetween = target.Position - Position;
            if (distanceBetween != Vector2.Zero)
                distanceBetween.Normalize();
            else
                distanceBetween = Vector2.Zero;
            
            Position += distanceBetween * Speed * Time.ScaledTime;
        }

        public void CheckForEnemyCollisions(List<Enemy> enemies)
        {
            foreach(Enemy e in enemies)
            {
                if (e != this && e.IsAlive && this.IsAlive && this.Collider.CollidesWith(e.Collider))
                {
                    Vector2 distance_inside = this.Position - e.Position;

                    if(distance_inside.Length() <= (float)(sprite.texture.Width * sprite.texture.Width))
                    {
                        Position += Vector2.Normalize(distance_inside) * (sprite.texture.Width - distance_inside.Length());
                    }
                }
            }
        }

        public override void Kill()
        {
            IsAlive = false;
            SetStartPosition();
        }

    }
}
