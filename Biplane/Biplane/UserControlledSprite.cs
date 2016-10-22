using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Biplane
{
    class UserControlledSprite : Sprite
    {            
        const int g = 10;
        float throttle = 0.1f;              
        float control = 0.5f;
                
        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize, float scale, float rotation,Point collisionOffset)
            : base(textureImage, position, Vector2.Zero, frameSize, scale, rotation, 0, collisionOffset)
        {           
        }

        
        public float rotate
        {
            get { return rotation; }            
        }

        public Vector2 velocity
        {
            get { return speed; }
        }
              

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Movement control
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
                rotation += Convert.ToSingle((Math.PI/(180 * control)));
            if (keyboardState.IsKeyDown(Keys.Down))
                rotation -= Convert.ToSingle((Math.PI/(180 * control)));
            if ((keyboardState.IsKeyDown(Keys.Right)) && (throttle < 10f))
                throttle += 0.1f;
            if ((keyboardState.IsKeyDown(Keys.Left)) && (throttle > 0.1f))
                throttle -= 0.1f;

            speed.X = Convert.ToSingle(throttle * Math.Cos(rotation));
            speed.Y = Convert.ToSingle(throttle * Math.Sin(rotation));

            position += speed;       
           
            TurnaroundCheck();
            
            base.Update(gameTime, clientBounds);
        }

        //Scroll if reached side of background. Fall down if reached sky limit.
        public void SideAndSky(Point sideScrollBoundary, int skyLimit)
        {
            if (position.X > sideScrollBoundary.Y)
                position.X = sideScrollBoundary.X;
            if (position.X < sideScrollBoundary.X)
                position.X = sideScrollBoundary.Y;
            if (position.Y < skyLimit)
                rotation = 1.57f;
        }

        //Turn around
        void TurnaroundCheck()
        {
            float checkRotation = rotation;
            while ((checkRotation < 0f) || (checkRotation > 6.28f))
            {
                if (checkRotation < 0f) 
                {
                    checkRotation += 6.28f;
                }
                else checkRotation -= 6.28f;
            }
            if ((checkRotation >= 1.57f) && (checkRotation <= 4.71f)) 
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            else spriteEffects = SpriteEffects.None;
         }
                        
    }
}
