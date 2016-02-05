using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace MonoGame_PongGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        public static GameTime GameTime { get; private set; }
        public static GraphicsDeviceManager graphics { get; private set; }

        SpriteBatch spriteBatch;

        SpriteFont vermin_font;
        string some_text = "Pong Game";

        SpriteFont verminBIG_font;
        int scoreP1 = 0;
        int scoreP2 = 0;

        int ballMulti = 1;

        //Player Stuff
        Paddle player;
        Texture2D playerSprite;
        float playerSpeed = 300f;

        //Player2 Stuff
        Paddle player2;
        Texture2D player2Sprite;

        //Ball Stuff
        Ball ball;
        Texture2D ballSprite;
        float ballVelo = 300f;

        //Audio Stuff
        SoundEffect snd_plop;
        SoundEffect snd_beep;

        //Surprising stuff
        Random random;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = false;
    
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Add your initialization logic here
            player = new Paddle();
            player2 = new Paddle();
            ball = new Ball();

            MediaPlayer.Volume = 0.25f;
            MediaPlayer.IsRepeating = true;

            random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Song music = Content.Load<Song>("Music\\ChillMusic.ogg");
            MediaPlayer.Play(music);


            vermin_font = Content.Load<SpriteFont>("vermin");
            verminBIG_font = Content.Load<SpriteFont>("verminBIG");

            snd_plop = Content.Load<SoundEffect>("Sounds\\plop");
            snd_beep = Content.Load<SoundEffect>("Sounds\\beep");
            
            ballSprite = Content.Load<Texture2D>("Sprites\\BallSprite");
            ball.GameObject(ballSprite, ballVelo);

            player.SetInputType = 1;
            playerSprite = Content.Load<Texture2D>("Sprites\\PaddleSprite");
            player.GameObject(playerSprite, playerSpeed, 5f);

            player2.SetInputType = 3;
            player2Sprite = Content.Load<Texture2D>("Sprites\\PaddleSprite");
            player2.GameObject(player2Sprite, playerSpeed, graphics.GraphicsDevice.Viewport.Width - player2Sprite.Width - 5);
        }

        protected override void UnloadContent()
        {
            //Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, ball);
            player2.Update(gameTime, ball);
            ball.Update(gameTime);
            CheckCollision();
            
            base.Update(gameTime);
        }
                

        private void CheckCollision()
        {
            //System.Diagnostics.Debug.WriteLine("Ball Value " + ballMulti);

            //Check BoundingBox collision
            if (ball.BoundingBox.Intersects(player.BoundingBox))
            {
                //ball.Velocity = ballVelo;
                ball.SpeedX = ballVelo;
                ball.SpeedY = random.Next(-50,50);
                ballMulti++;
                ball.Multiplicator = ballMulti;
                snd_plop.Play();
            }

            if (ball.BoundingBox.Intersects(player2.BoundingBox))
            {
                //ball.Velocity = -ballVelo;
                ball.SpeedX = -ballVelo;
                ball.SpeedY = random.Next(-50, 50);
                ballMulti++;
                ball.Multiplicator = ballMulti;
                snd_plop.Play();
            }

            //Check ball position
            var ballX = graphics.GraphicsDevice.Viewport.Width - ball.Width;

            //if ball is at player 1 goal
            if (ball.Position2D.X <= 0)
            {
                ball.BallReset = true;
                scoreP2 += 1; //ADD SCORE P2
                ballMulti = 0;
                snd_beep.Play();
            }

            //if ball is at player 2 goal
            if (ball.Position2D.X >= ballX)
            {
                ball.BallReset = true;
                scoreP1 += 1;//ADD SCORE P1   
                ballMulti = 0;
                snd_beep.Play();
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Start drawing
            spriteBatch.Begin();

            //Draw the Player
            player.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            spriteBatch.DrawString(vermin_font, some_text, new Vector2(graphics.GraphicsDevice.Viewport.Width/2 - vermin_font.MeasureString(some_text).X/2, 0), Color.White);

            spriteBatch.DrawString(verminBIG_font, scoreP1.ToString(), new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - vermin_font.MeasureString(some_text).X * 3, 0), Color.White);
            spriteBatch.DrawString(verminBIG_font, scoreP2.ToString(), new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + vermin_font.MeasureString(some_text).X * 3, 0), Color.White);

            ball.Draw(spriteBatch);

            //End it 
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
