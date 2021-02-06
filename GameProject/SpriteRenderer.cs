using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public static class SpriteRenderer
    {
        public static List<Sprite> Sprites = new List<Sprite>();

        public static void DrawSprites(SpriteBatch spriteBatch)
        {
            foreach(Sprite s in Sprites)
            {
                if(s.IsAlive)
                    s.Draw(spriteBatch);
            }
        }
    }
}
