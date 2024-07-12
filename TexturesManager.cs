using Microsoft.Xna.Framework.Graphics;

namespace TopdownGame
{
    internal static class  TexturesManager
    {
        public static Texture2D player;
        public static Texture2D bow;
        public static Texture2D arrow;
        public static Texture2D arrow_bow;
        public static Texture2D slime;
        public static Texture2D groundTile;
        public static Texture2D wallTile;
        public static Texture2D fontpng;
        public static Texture2D fontbmp;
        public static Texture2D test;

        public static void Init() 
        {
            player = Globals.ContentManager.Load<Texture2D>("player");
            slime = Globals.ContentManager.Load<Texture2D>("Slime");
            wallTile = Globals.ContentManager.Load<Texture2D>("wall");
            groundTile = Globals.ContentManager.Load<Texture2D>("ground");
            bow = Globals.ContentManager.Load<Texture2D>("bow");
            arrow = Globals.ContentManager.Load<Texture2D>("arrow");
            arrow_bow = Globals.ContentManager.Load<Texture2D>("bow_arrow_anim");
            fontpng = Globals.ContentManager.Load<Texture2D>("fontpng");
            fontbmp = Globals.ContentManager.Load<Texture2D>("fontbmp");
            test = Globals.ContentManager.Load<Texture2D>("tilemap_packed");
        }
    }
}
