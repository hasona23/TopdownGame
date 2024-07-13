using Microsoft.Xna.Framework.Graphics;
using System;
using TopdownGame.Comp_Sys;

namespace TopdownGame.Components
{
    public class Sprite:IComponent
    {
        
        public Texture2D texture;
        public Rectangle src = Rectangle.Empty;
        public SpriteEffects spriteEffects = SpriteEffects.None;
        public int rows;
        public int cols;
        public Sprite(Texture2D texture, int cols = 1, int rows = 1, int x = 0, int y = 0, int offsetX = 0, int offsetY = 0)
        {
            this.texture = texture;
            this.cols = cols;
            this.rows = rows;
            int cellWidth = texture.Width / rows;
            int cellHeight = texture.Height / cols;

            src = new Rectangle(x * cellWidth,y * cellHeight ,cellWidth,cellHeight);
        }
        
    }

    public class DrawSystem : GameSystem 
    {
        public DrawSystem() : base() 
        {
            SetRequirments(typeof(Transform),typeof(Sprite));
        }

        public override void Update(GameTime gt, SpriteBatch sb = null)
        {
            if (sb == null)
                return;
            foreach(Entity entity in entities) 
            {
                Sprite s = entity.GetComponent<Sprite>();
                Transform t = entity.GetComponent<Transform>();

                sb.Draw(s.texture,new Rectangle(t.pos.ToPoint(),new Point(s.src.Width*(int)t.scale,s.src.Height*(int)t.scale)),s.src,Color.White);

            }
        }
    }
}
