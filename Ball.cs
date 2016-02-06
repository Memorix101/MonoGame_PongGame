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
        int multiplicator = 1;

        Vector2 spriteSpeed;

        bool ballReset = false;

        /// <summary>
        /// Props
        /// </summary>

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
            set { Position = value;}
            get { return Position; }
        }

        public Vector2 Speedv
        {
            get { return spriteSpeed; }
            set { spriteSpeed = value; }
        }

        /*
        public float SpeedY
        {
            set { spriteSpeed.Y = value; }
        }
        */
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

        public int Multiplicator
        {
            set { multiplicator = value; }
        }

        /// <summary>
        /// Stuff
        /// </summary>
        /// <param name="gameTime"></param>
        // Initializing assets and stuff

        public void GameObject(Texture2D sprite, float speed)
        {

            Texture = sprite;
            //Position = pos;
            //moveSpeed = speed;
            spriteSpeed.X = speed;
            spriteSpeed.Y = 0f;

            Position = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - Width, Game1.graphics.GraphicsDevice.Viewport.Height / 2 - Height);
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

            // System.Diagnostics.Debug.WriteLine(Game1.graphics.GraphicsDevice.Viewport.Width + " - " + Height * spriteScale + " - " + ballPos.X);

            //Position.X += moveSpeed * multiplicator *(float)t;
            //    Position.Y += moveSpeed * multiplicator *(float)t;


            // more stuff
            // Move the sprite by speed, scaled by elapsed time.
            Position += spriteSpeed * multiplicator * (float)t;

            int MaxX = Game1.graphics.GraphicsDevice.Viewport.Width - Texture.Width;
            int MinX = 0;
            int MaxY = Game1.graphics.GraphicsDevice.Viewport.Height - Texture.Height;
            int MinY = 0;

            // Check for bounce.
            if (Position.X > MaxX)
            {
                spriteSpeed.X *= -1;
                Position.X = MaxX;
            }

            else if (Position.X < MinX)
            {
                spriteSpeed.X *= -1;
                Position.X = MinX;
            }

            if (Position.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                Position.Y = MaxY;
            }

            else if (Position.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                Position.Y = MinY;
            }

        }

        void StartPos()
        {
            //Set start postion
            if (ballReset)
            {
                Position = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - Width, Game1.graphics.GraphicsDevice.Viewport.Height / 2 - Height);
                multiplicator = 1;
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
