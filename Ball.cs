using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;

namespace MonoGame_PongGame
{
    class Ball
    {

        Texture2D Texture;
        Vector2 Position;
        float moveSpeed;

        bool ballReset = false;

        // Initializing assets and stuff
        public void GameObject(Texture2D sprite, float speed)
        {

            Texture = sprite;
       //     Position = pos;
            moveSpeed = speed;

            Position = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - Width, Game1.graphics.GraphicsDevice.Viewport.Height / 2 - Height);
        }

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    Texture.Width,
                    Texture.Height);
            }
        }
        
        public Vector2 Position2D
        {
            get { return Position; }
        }

        public float Velocity
        {
            set { moveSpeed = value; }
            get { return moveSpeed; }
        }

        public bool BallReset
        {
            set { ballReset = value; }
            get { return ballReset; }
        }

        public void Update(GameTime gameTime)
        {
            GamePlay(gameTime);
        }


        void GamePlay(GameTime gameTime)
        {
            var t = gameTime.ElapsedGameTime.TotalSeconds;

            StartPos();

           // ballPos.Y = MathHelper.Clamp(ballPos.Y, 0, Game1.graphics.GraphicsDevice.Viewport.Width * spriteScale);

            //Console.WriteLine(Game1.graphics.GraphicsDevice.Viewport.Width + " - " + Height * spriteScale + " - " + ballPos.X);

            Position.X += moveSpeed * (float)t; 

        }

        void StartPos()
        {
            //Set start postion
            if (ballReset)
            {
                Position = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - Width, Game1.graphics.GraphicsDevice.Viewport.Height / 2 - Height);
                ballReset = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)

        {
            spriteBatch.Draw(Texture, Position, Color.White);
            //spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, spriteScale, SpriteEffects.None, 0f);
        }

    }
}
