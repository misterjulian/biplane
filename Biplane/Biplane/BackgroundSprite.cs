using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Biplane
{
    class BackgroundSprite : Sprite
    {
        Rectangle backgroundSize;
        
        public BackgroundSprite(Texture2D textureImage, Rectangle backgroundSize)
            : base(textureImage, Vector2.Zero, Point.Zero, 1f, 1)
        {
            this.backgroundSize = backgroundSize;            
        }    
             

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            backgroundSize.X = (int)drawPosition.X;
            backgroundSize.Y = (int)drawPosition.Y;
            spriteBatch.Draw(textureImage, backgroundSize, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depthLayer);
        }
        

    }
}
