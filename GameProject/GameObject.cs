using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using GameProject.Collisions;

namespace GameProject
{
    public class GameObject //: IDisposable
    {
        protected Game game;
        public Vector2 Position { get; set; }

        public Sprite sprite { get; protected set; }

        public bool IsAlive { get; set; } = true;

        private BoundingCircle collider;

        public BoundingCircle Collider { get { return collider; } protected set { collider = value; } }

        public GameObject(Game game)
        {
            this.game = game;
        }

        public virtual void Update(GameTime gameTime)
        {
            return;
        }

        public virtual void LoadContent()
        {
            return;
        }


        public virtual void SetStartPosition()
        {
            return;
        }

        public void Kill()
        {
            sprite.IsAlive = false;
            IsAlive = false;
        }

        public void UpdateColliderPosition()
        {
            collider.Center = Position;
        }

        /*public virtual void Dispose()
        {
            IsAlive = false;
        }*/
    }
}
