using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Biplane
{
    public class Camera
    {
        private SpriteBatch spriteRenderer;
        private Vector2 position; // top left corner of the camera

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Camera(Vector2 cameraPosition, SpriteBatch renderer)
        {
            spriteRenderer = renderer;
            position = cameraPosition;

        }

        public void DrawSprite(GameTime gameTime, Sprite sprite)
        {
            // get the screen position of the sprite
            Vector2 drawPosition = ApplyTransformations(sprite.centerPosition);
            sprite.Draw(gameTime, spriteRenderer, drawPosition);
        }        

        private Vector2 ApplyTransformations(Vector2 spritePosition)
        {
            // apply translation
            Vector2 finalPosition = spritePosition - position;
            // apply scaling and rotation here
            //.....
            //--------------------------------------------
            return finalPosition;
        }

        public void Translate(Vector2 moveVector)
        {
            position += moveVector;
        }

        public void capCameraPosition(Rectangle clientBounds, Rectangle backgroundSize)
        {
            Vector2 cameraPosition = position;
            if (cameraPosition.X > backgroundSize.Width - clientBounds.Width)
            {
                cameraPosition.X = backgroundSize.Width - clientBounds.Width;
            }
            if (cameraPosition.X < 0)
            {
                cameraPosition.X = 0;
            }
            if (cameraPosition.Y > backgroundSize.Height - clientBounds.Height)
            {
                cameraPosition.Y = backgroundSize.Height - clientBounds.Height;
            }
            if (cameraPosition.Y < 0)
            {
                cameraPosition.Y = 0;
            }
            position = cameraPosition;
        }
    }
}
