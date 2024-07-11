using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zap_ecs;

namespace TopdownGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.ContentManager = Content;
            Globals.windowSize = new(1280,720);
            _graphics.PreferredBackBufferWidth = Globals.windowSize.X;
            _graphics.PreferredBackBufferHeight = Globals.windowSize.Y;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
           
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            TexturesManager.Init();
            font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        FpsCounter fps = new FpsCounter();
        
        protected override void Draw(GameTime gameTime)
        {
            fps.Update(((float)gameTime.ElapsedGameTime.TotalSeconds));

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(TexturesManager.player,new Vector2(720,260),new Rectangle(0,0,TexturesManager.player.Width/4,TexturesManager.player.Height),Color.White,0,new Vector2(TexturesManager.player.Width/2,TexturesManager.player.Height/2),5,SpriteEffects.None,0);
            _spriteBatch.DrawString(font,fps.AverageFramesPerSecond.ToString("00.00"),new Vector2(25,25),Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
