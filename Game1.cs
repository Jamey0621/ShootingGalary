using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //need to decalre the assets
        Texture2D target_Sprite;
        Texture2D crosshairs_Sprite;
        Texture2D sky_Sprite;

        SpriteFont gameFont;

        Vector2 targetPos = new Vector2(300, 300);
        const int TARGET_RADIUS = 45;

        
        MouseState mState;
        bool mReleased = true;
        float mouseTargetDist;


        int score = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            target_Sprite = Content.Load<Texture2D>("target");
            crosshairs_Sprite = Content.Load<Texture2D>("crosshairs");
            sky_Sprite = Content.Load<Texture2D>("sky");

            gameFont = Content.Load<SpriteFont>("galleryFont");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            // Score when the mouse is clicked 
            mState = Mouse.GetState();

            mouseTargetDist = Vector2.Distance(targetPos, new Vector2(mState.X, mState.Y));

            if(mState.LeftButton == ButtonState.Pressed && mReleased == true )
            {
                if (mouseTargetDist < TARGET_RADIUS)
                {
                    score++;
                    
                    // need to move the target after clicked
                    Random rand = new Random();
                    targetPos.X = rand.Next(TARGET_RADIUS , _graphics.PreferredBackBufferWidth - TARGET_RADIUS +1);
                    targetPos.Y = rand.Next(TARGET_RADIUS, _graphics.PreferredBackBufferHeight - TARGET_RADIUS + 1);

                }
                mReleased = false;
            }

            if(mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }
            // end of the scoreing and mouse click 
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(sky_Sprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(target_Sprite, new Vector2(targetPos.X - TARGET_RADIUS, targetPos.Y - TARGET_RADIUS), Color.White);

            _spriteBatch.DrawString(gameFont, "SCORE =", new Vector2(250, 0), Color.Black);
            _spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(400,0), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
