using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class Bullet : GameObject
    {
        private float timer;
        private Gun gun;
        private float bulletVelocity = 2f;
        private Vector2 shotGunDirection;
        private float bulletAliveTime = 100.0f;

        public Bullet(Game game, Gun gun) : base(game)
        {
            this.gun = gun;
            shotGunDirection = gun.GetGunDirection(gun.GetGunAngle(gun.MouseDirection));
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += shotGunDirection * bulletVelocity;
            CheckLifeTime();
            UpdateColliderPosition();
        }

        public override void LoadContent()
        {
            sprite = new Sprite(game.Content.Load<Texture2D>("bullet"), this);
            SpriteRenderer.Sprites.Add(sprite);
            SetStartPosition();
            Collider = new Collisions.BoundingCircle(Position, sprite.texture.Width / 2);
        }

        public void CheckLifeTime()
        {
            if(timer >= bulletAliveTime)
            {
                Kill();
            }
        }

        public override void SetStartPosition()
        {
            Position = gun.Position + shotGunDirection * (gun.sprite.texture.Height / 2f);
        }

    }
}
