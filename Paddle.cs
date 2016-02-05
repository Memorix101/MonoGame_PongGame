using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGame_PongGame
{
     class Paddle
    {

        Texture2D Texture;
        Vector2 Position;
        float moveSpeed;
        bool reset = false;
        int inputType = (int)InputType.None;

        enum InputType
        {
            None,
            WASD,
            Arrows,
            AI
        }

     //   float spriteScale = 0.5f;

        // Initializing assets and stuff
        public void GameObject( Texture2D sprite, float speed, float offset)
        {
            Texture = sprite;
            //Position = pos;
            moveSpeed = speed;

            //set paddle to middle
            Position = new Vector2(offset, Game1.graphics.GraphicsDevice.Viewport.Height/2 - Height);
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

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public int SetInputType
        {
            set { inputType = value; }
        }

        public void Update(GameTime gameTime, Ball ball)
        {
            Input(gameTime, ball);
        }

        void Input(GameTime gameTime, Ball ball)
        {

            StartPos();
            var t = gameTime.ElapsedGameTime.TotalSeconds;
            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.graphics.GraphicsDevice.Viewport.Height - Height);

            // System.Diagnostics.Debug.WriteLine(Game1.graphics.GraphicsDevice.Viewport.Height + " - " + Height * spriteScale + " - " + paddlePos.Y);

            switch (inputType)
            {
                case (int)InputType.Arrows:
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        Position.Y -= moveSpeed * (float)t;

                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        Position.Y += moveSpeed * (float)t;
                    break;

                case (int)InputType.WASD:
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                        Position.Y -= moveSpeed * (float)t;

                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                        Position.Y += moveSpeed * (float)t;
                    break;

                case (int)InputType.AI:
                    Position.Y = ball.Position2D.Y;
                    break;

                default:
                    //None
                    break;

            }
        }

        void StartPos()
        {
            //Set start postion
            if (reset)
            {
                Position = new Vector2(5, Game1.graphics.GraphicsDevice.Viewport.Height / 2 - Height);
                reset = false;
            }
        }


        public void Draw(SpriteBatch spriteBatch)

        {
          spriteBatch.Draw(Texture, Position, Color.White);
          //  spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, spriteScale, SpriteEffects.None, 0f);
        }

    }
}
    