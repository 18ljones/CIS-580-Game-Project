using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{

    public enum FireRate
    {
        FullAuto,
        SemiAuto,
        Burst
    }

    public class Gun : GameObject
    {
        private Player player;
        public FireRate FireRate { get; set; }
        public List<Bullet> Bullets = new List<Bullet>();
        public Vector2 MouseDirection => InputManager.GetMouseDirection(new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));

        public Gun(Game game, Player player, FireRate fireRate) : base(game)
        {
            this.player = player;
            FireRate = fireRate;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateGunRotation(MouseDirection);
            Shoot();
            UpdateBulletPosition(gameTime);
            CheckDeadBullets();
        }
        public override void LoadContent()
        {
            sprite = new Sprite(game.Content.Load<Texture2D>("gun"), this);
            SpriteRenderer.Sprites.Add(sprite);
            SetStartPosition();
        }

        public void UpdateGunRotation(Vector2 mouseDirection)
        {
            //gun.Rotation = rotation;
            float angle = GetGunAngle(mouseDirection);
            Vector2 point = GetGunDirection(angle);
            sprite.Rotation = angle - MathF.PI / 2;
            Position = point * (player.sprite.texture.Width / 2) + new Vector2(player.Position.X, player.Position.Y);
        }

        public Vector2 GetGunDirection(float angle)
        {
            return Vector2.Normalize(-new Vector2(MathF.Cos(angle), MathF.Sin(angle)));
        }

        public float GetGunAngle(Vector2 mouseDirection)
        {
            return MathF.Atan2(mouseDirection.Y, mouseDirection.X);
        }

        public void Shoot()
        {
            if(InputManager.currentMouseState.LeftButton == ButtonState.Pressed && InputManager.priorMouseState.LeftButton != ButtonState.Pressed)
            {
                Bullets.Add(new Bullet(game, this));
            }
        }

        public void CheckDeadBullets()
        {
            for(int i = 0; i < Bullets.Count; i++)
            {
                if (!Bullets[i].IsAlive)
                {
                    Bullets[i] = null;
                    Bullets.RemoveAt(i);
                }
            }
        }

        public void UpdateBulletPosition(GameTime gameTime)
        {
            foreach(Bullet b in Bullets)
            {
                b.Update(gameTime);
            }
        }

        public override void SetStartPosition()
        {
            Position = new Vector2((float)(player.Position.X), (float)(player.Position.Y - (sprite.texture.Height / 2)));
        }
    }
}
