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

        public Player(Game game) : base(game)
        {
            this.gun = new Gun(game, this, FireRate.SemiAuto);
        }

        public override void Update(GameTime gameTime)
        {
            Position += InputManager.PlayerDirection;
            gun.Update(gameTime);
            CheckPlayerOutOfBounds();
            UpdateColliderPosition();
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
            if (Position.X < game.GraphicsDevice.Viewport.X)
            {
                Position = new Vector2(game.GraphicsDevice.Viewport.X, Position.Y);
            }
            else if(Position.X > game.GraphicsDevice.Viewport.Width - sprite.texture.Width)
            {
                Position = new Vector2(game.GraphicsDevice.Viewport.Width - sprite.texture.Width, Position.Y);
            }

            if (Position.Y < game.GraphicsDevice.Viewport.Y)
            {
                Position = new Vector2(Position.X, game.GraphicsDevice.Viewport.Y);
            }
            else if(Position.Y > game.GraphicsDevice.Viewport.Height - sprite.texture.Height)
            {
                Position = new Vector2(Position.X, game.GraphicsDevice.Viewport.Height - sprite.texture.Height);
            }
        }

        public override void SetStartPosition()
        {
            Position = new Vector2((game.GraphicsDevice.Viewport.Width / 2) - (sprite.texture.Width / 2), (game.GraphicsDevice.Viewport.Height / 2) - (sprite.texture.Height / 2));
        }
    }
}
