using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace GameProject
{

    public enum FireRate
    {
        FullAuto = 3,
        SemiAuto = 10,
        Burst = 5
    }

    public class Gun : GameObject
    {
        private Player player;
        public FireRate FireRate { get; set; }
        private float fireTimer;
        private bool isShooting = false;
        public List<Bullet> Bullets = new List<Bullet>();
        public Vector2 MouseDirection => InputManager.GetMouseDirection(new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));
        private SoundEffect shootSoundeffect;
        private MuzzleFlash muzzleFlash;

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
            shootSoundeffect = game.Content.Load<SoundEffect>("shoot_sound");
            sprite = new Sprite(game.Content.Load<Texture2D>("gun"), this);
            muzzleFlash = new MuzzleFlash(game.Content.Load<Texture2D>("muzzle_flash"), this, 16, 5, 0.03);
            SpriteRenderer.Sprites.Add(sprite);
            SpriteRenderer.Sprites.Add(muzzleFlash);
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
            if(InputManager.currentMouseState.LeftButton == ButtonState.Pressed && InputManager.priorMouseState.LeftButton != ButtonState.Pressed && !isShooting)
            {
                Bullets.Add(new Bullet(game, this));
                muzzleFlash.SpawnMuzzleFlash();
                shootSoundeffect.Play();
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
