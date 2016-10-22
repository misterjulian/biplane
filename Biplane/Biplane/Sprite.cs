using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Biplane
{
    public abstract class Sprite
    {
        protected Texture2D textureImage;
        
        protected const int defaultMillisecondsPerFrame = 16;
        protected Vector2 speed;
        protected Vector2 position;
        protected Point frameSize;
        protected float scale;
        protected float depthLayer;
        protected float rotation;
        public Vector2 origin;
        protected SpriteEffects spriteEffects;
        protected Point collisionOffset;

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize, float scale)
            : this(textureImage, position, Vector2.Zero, frameSize, scale, 0)
        {
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize, float scale, float depthLayer)
            : this(textureImage, position, Vector2.Zero, frameSize, scale, depthLayer)
        {
        }

        public Sprite(Texture2D textureImage, Vector2 position, Vector2 speed, Point frameSize, float scale, float depthLayer)
            : this(textureImage, position, Vector2.Zero, frameSize, scale, 0f, depthLayer, Point.Zero)
        {
        }

        public Sprite(Texture2D textureImage, Vector2 position, Vector2 speed, Point frameSize, float scale, float rotation, float depthLayer, Point collisionOffset)
        {
            this.textureImage = textureImage;            
            this.speed = speed;
            this.frameSize = new Point((int)(frameSize.X * scale), (int)(frameSize.Y * scale));
            this.scale = scale;
            this.depthLayer = depthLayer;
            this.rotation = rotation;
            this.origin = new Vector2(frameSize.X / 2, frameSize.Y / 2);
            this.spriteEffects = SpriteEffects.None;
            this.collisionOffset = new Point((int)(collisionOffset.X * scale), (int)(collisionOffset.Y * scale));
            this.position = new Vector2(position.X + origin.X, position.Y + origin.Y);
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)           
        {
            spriteBatch.Draw(textureImage, position, null, Color.White, rotation, origin, scale, spriteEffects, depthLayer);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            spriteBatch.Draw(textureImage, drawPosition, null, Color.White, rotation, origin, scale, spriteEffects, depthLayer);            
        }


        public Vector2 centerPosition
        {
            get { return new Vector2(position.X + frameSize.X / 2, position.Y + frameSize.Y / 2); }            
        }

        public RotatedRectangle collisionRect
        {
            get
            {
                return new RotatedRectangle(new Rectangle(
                    (int)position.X + collisionOffset.X,
                    (int)position.Y + collisionOffset.Y,
                    (frameSize.X - (collisionOffset.X * 2)),
                    (frameSize.Y - (collisionOffset.Y * 2))), rotation);                
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

    }
}
