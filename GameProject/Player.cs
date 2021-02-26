using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    
    public class Player : GameObject
    {
        private Gun gun;

        public Gun Gun => gun;

        public bool Stunned { get; set; } = false;
        private double stunnedTimer;
        private int stunnedTime = 1;
        private Vector2 stunnedDir;
        private float stunnedPower = 6;

        public Player(Game game) : base(game)
        {
            this.gun = new Gun(game, this, FireRate.SemiAuto);
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            if (!Stunned)
            {
                Position += InputManager.PlayerDirection;
            }
            else
            {
                stunnedTimer += Time.ScaledTime;
                Position += stunnedDir * (stunnedPower * (float)stunnedTimer);
                if(stunnedTimer > stunnedTime)
                {
                    Stunned = false;
                    stunnedTimer = 0;
                    sprite.Color = Color.Black;
                }
            }

            gun.Update(gameTime);
            CheckPlayerOutOfBounds();
            UpdateColliderPosition();
            CheckEnemyCollision(enemies);
        }

        public override void LoadContent()
        {
            sprite = new Sprite(game.Content.Load<Texture2D>("ball"), this);
            sprite.Color = Color.Black;
            SpriteRenderer.Sprites.Add(sprite);
            SetStartPosition();
            Collider = new Collisions.BoundingCircle(Position, sprite.texture.Width / 2);
            gun.LoadContent();
        }

        public void CheckPlayerOutOfBounds()
        {
            if (Position.X < game.GraphicsDevice.Viewport.X + sprite.texture.Width / 2)
            {
                Position = new Vector2(game.GraphicsDevice.Viewport.X + sprite.texture.Width / 2, Position.Y);
            }
            else if(Position.X > game.GraphicsDevice.Viewport.Width - sprite.texture.Width / 2)
            {
                Position = new Vector2(game.GraphicsDevice.Viewport.Width - sprite.texture.Width / 2, Position.Y);
            }

            if (Position.Y < game.GraphicsDevice.Viewport.Y + sprite.texture.Height / 2)
            {
                Position = new Vector2(Position.X, game.GraphicsDevice.Viewport.Y + sprite.texture.Height / 2);
            }
            else if(Position.Y > game.GraphicsDevice.Viewport.Height - sprite.texture.Height / 2)
            {
                Position = new Vector2(Position.X, game.GraphicsDevice.Viewport.Height - sprite.texture.Height / 2);
            }
        }

        public override void SetStartPosition()
        {
            Position = new Vector2((game.GraphicsDevice.Viewport.Width / 2) - (sprite.texture.Width / 2), (game.GraphicsDevice.Viewport.Height / 2) - (sprite.texture.Height / 2));
        }

        public void CheckEnemyCollision(List<Enemy> enemies)
        {
            foreach(Enemy e in enemies)
            {
                if (Collider.CollidesWith(e.Collider))
                {
                    if (!Stunned)
                    {
                        Stunned = true;
                        stunnedDir = Vector2.Normalize(this.Position - e.Position);
                        sprite.Color = Color.Red;
                        InputManager.TimeAlive = 0;
                        InputManager.TimesHit++;
                    }
                    e.Kill();
                }
            }
        }
    }
}
