using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using TopdownGame.Comp_Sys;
using TopdownGame.Components;
using Zap_ecs;
using Zap_ecs.Tilemaps;

namespace TopdownGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;


        private Tilemap tilemap;
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
        World world1;
        Entity player;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            TexturesManager.Init();
            font = Content.Load<SpriteFont>("font");
            Dictionary<int, Texture2D> mydict = new()
            {
                [2] = TexturesManager.groundTile,
                [1] = TexturesManager.wallTile,
                [0] = TexturesManager.wallTile,
            };
            Dictionary<int, Rectangle> rdict = new()
            {
                [1] = new Rectangle(5*16,0,16,16),
                [0] = new Rectangle(7*16,0,16,16),
            };
            tilemap = new Tilemap("D:\\Code\\C#\\TopdownGame\\Tilemaps\\Level1_collision.csv", mydict,8,new Vector2(150,50),CollisionMode.RigidBody,4f);
            tilemap.LoadMap();
            base.Initialize();
            world1 = new World();
            player = world1.AddAndGetEntity();
            world1.AddComponentToEntity(player, new Sprite(TexturesManager.player,rows:4));
            world1.AddComponentToEntity( player,new Transform(new Vector2(500,360),5));
            Sprite ps = player.GetComponent<Sprite>();
            Transform pt = player.GetComponent<Transform>();
            world1.AddComponentToEntity(player,new Collider(500,360,(int)(ps.src.Width*pt.scale),(int)(ps.src.Height*pt.scale)));
            world1.AddComponentToEntity(player,new Velocity(250));
            

            MovePhysics movePhysics = new MovePhysics(tilemap);
            DrawSystem drawSystem = new DrawSystem();
            world1.AddDrawSystem(drawSystem);
            world1.AddUpdateSystem(movePhysics);

           



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
            
            player.GetComponent<Velocity>().dir = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) 
            {
                player.GetComponent<Velocity>().dir.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.GetComponent<Velocity>().dir.X -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                player.GetComponent<Velocity>().dir.Y -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.GetComponent<Velocity>().dir.Y += 1;
            }
            // TODO: Add your update logic here
            if (player.GetComponent<Velocity>().dir.Length() > 0)
                player.GetComponent<Velocity>().dir.Normalize();
            world1.Update(gameTime);
           
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
            
            _spriteBatch.DrawString(font,fps.AverageFramesPerSecond.ToString("00.00"),new Vector2(25,25),Color.White);
            world1.Draw(gameTime,_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
