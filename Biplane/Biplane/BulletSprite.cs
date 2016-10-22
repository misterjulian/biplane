using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Biplane
{
    class BulletSprite: Sprite
    {        
        Vector2 direction;
        public int lifetime;
        


        public BulletSprite(Texture2D textureImage, Vector2 position, Vector2 speed, Point frameSize, float scale, float rotation, Point collisionOffset, GameTime gameTime)
            : base(textureImage, position, speed, frameSize, scale, rotation, 0, collisionOffset)
        {
            this.collisionOffset = collisionOffset;
            this.lifetime = 0;
        }
              
                

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            direction.X = Convert.ToSingle(speed.X * Math.Cos(rotation));
            direction.Y = Convert.ToSingle(speed.Y * Math.Sin(rotation));

            position += direction;

            lifetime += gameTime.ElapsedGameTime.Milliseconds;

        }
       
    }
}
