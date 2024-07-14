

namespace TopdownGame.Comp_Sys
{
    public class Transform:IComponent
    {
         
        public Vector2 pos;
        public float scale;
        public float rotation;

        public Transform(Vector2 pos, float scale = 1, float rotation = 0) 
        {
            this.pos = pos;
            this.scale = scale;
            this.rotation = rotation;
        }
    }
}
