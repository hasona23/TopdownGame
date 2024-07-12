using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Zap_ecs;
using Zap_ecs.Tilemaps;

namespace TopdownGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;


        private TilemapRenderer tilemap;
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
            TexturesManager.Init();
            font = Content.Load<SpriteFont>("font");
            Dictionary<int, Texture2D> mydict = new()
            {
                [1] = TexturesManager.groundTile,
                [0] = TexturesManager.wallTile,
            };
            Dictionary<int, Rectangle> rdict = new()
            {
                [1] = new Rectangle(5*16,0,16,16),
                [0] = new Rectangle(7*16,0,16,16),
            };
            tilemap = new TilemapRenderer("D:/Code/C#/TopdownGame/tilemap.csv", mydict,8,new Vector2(0,0),10);
            tilemap.LoadMap();
            base.Initialize();
           
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
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
            tilemap.DrawMapTextures(_spriteBatch);
            _spriteBatch.Draw(TexturesManager.player,new Vector2(720,260),new Rectangle(0,0,TexturesManager.player.Width/4,TexturesManager.player.Height),Color.White,0,new Vector2(TexturesManager.player.Width/2,TexturesManager.player.Height/2),5,SpriteEffects.None,0);
            _spriteBatch.DrawString(font,fps.AverageFramesPerSecond.ToString("00.00"),new Vector2(25,25),Color.White);
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
