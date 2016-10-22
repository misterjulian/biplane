using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Biplane
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {        
        SpriteBatch spriteBatch;
        UserControlledSprite player;
        BackgroundSprite background;

        List<InteractiveSprite> spriteList = new List<InteractiveSprite>();
        int enemyLimit = 10;
        Rectangle spawnLimit = new Rectangle(100, 100, 1800, 1500); 
        List<BulletSprite> bulletList = new List<BulletSprite>();
        Camera camera;

        Rectangle backgroundSize = new Rectangle(0, 0, 2000, 2000);
        RotatedRectangle ground = new RotatedRectangle(new Rectangle(0, 1865, 2000, 135), 0f);
        Point sideScrollBoundary = new Point(-50, 2050);
        int skyLimit = -50;

        Texture2D squareTexture;
        Texture2D bulletTexture;
        Texture2D targetTexture;
        const int fireDelay = 100;
        const int bulletLife = 650;
        int allowFire;
        
        Random rnd = new Random();        
        KeyboardState previousKeyboardState;


        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        //Content loading - sprites and textures
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            camera = new Camera(Vector2.Zero, spriteBatch);
            player = new UserControlledSprite(Game.Content.Load<Texture2D>(@"Images/Plane1"), new Vector2(100, 100), new Point (179, 86), 0.5f, 0f, new Point (12, 22));
            background = new BackgroundSprite(Game.Content.Load<Texture2D>(@"Images/Background3"), backgroundSize);
            
            bulletTexture = Game.Content.Load<Texture2D>(@"Images/bullet");
            targetTexture = Game.Content.Load<Texture2D>(@"Images/target");
            squareTexture = Game.Content.Load<Texture2D>(@"Images/Square");

            base.LoadContent();            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //Player control
            KeyboardState keyboardState = Keyboard.GetState();

            FireControl(keyboardState, gameTime);

            previousKeyboardState = keyboardState;
            
            //Player updates
            player.Update(gameTime, Game.Window.ClientBounds);
            player.SideAndSky(sideScrollBoundary, skyLimit);  
            
            //Sprite list updates
            for (int i = 0; i < spriteList.Count; ++i)
            {
                InteractiveSprite s = spriteList[i];
                s.Update(gameTime, Game.Window.ClientBounds);         
                                
            }

            //Collision Detection, bullet removal
            for (int i = 0; i < bulletList.Count; ++i)
            {
                BulletSprite s = bulletList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.lifetime >= bulletLife)
                {
                    bulletList.RemoveAt(i);
                    --i;
                    continue;
                }    
                for (int j = 0; j < spriteList.Count; ++j)
                {
                    InteractiveSprite k = spriteList[j];
                    if (s.collisionRect.Intersects(k.collisionRect))
                    {
                        spriteList.RemoveAt(j);
                        --j;
                        bulletList.RemoveAt(i);
                        --i;                        
                    }
                }
                        
            }

            if (player.collisionRect.Intersects(ground))
                ((Game1)Game).Exit();
            

            //Camera updates
            camera.Position = new Vector2(player.centerPosition.X - Game.Window.ClientBounds.Width/2, player.centerPosition.Y - Game.Window.ClientBounds.Height/2);
            camera.capCameraPosition(Game.Window.ClientBounds, backgroundSize);

            //Create enemies
            PopulateEnemies();
                
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            //Draw the background
            camera.DrawSprite(gameTime, background);
            // Draw the player
            camera.DrawSprite(gameTime, player);
            // Draw all sprites
            foreach (Sprite s in spriteList)                        
                camera.DrawSprite(gameTime, s);
            foreach (Sprite s in bulletList)
                camera.DrawSprite(gameTime, s);            
            //spriteBatch.Draw(squareTexture, new Rectangle(100, 100, 100, 100), null, Color.Blue, 0f, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        void PopulateEnemies()
        {
            while (spriteList.Count < enemyLimit)
            {
                int X = rnd.Next(spawnLimit.X, spawnLimit.X + spawnLimit.Width);
                int Y = rnd.Next(spawnLimit.X, spawnLimit.X + spawnLimit.Width);
                spriteList.Add(new InteractiveSprite(targetTexture, new Vector2(X, Y), Vector2.Zero, new Point(100, 88), 1f, Point.Zero));
            }
        }

        void FireControl(KeyboardState keyboardState, GameTime gameTime)
        {
            allowFire -= gameTime.ElapsedGameTime.Milliseconds;
            if (keyboardState.IsKeyDown(Keys.Space) && (allowFire < 0))
            {
                bulletList.Add(new BulletSprite(bulletTexture, player.centerPosition,
                    new Vector2(10f + Math.Abs(player.velocity.X), 10f + Math.Abs(player.velocity.Y)),
                    new Point(10, 10), 0.5f, player.rotate, Point.Zero, gameTime));
                allowFire = fireDelay;
            }           
        }
                
    }
}
