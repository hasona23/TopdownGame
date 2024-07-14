using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopdownGame.Comp_Sys
{
    public class Velocity : Component
    {
       
        public Vector2 dir = Vector2.Zero;
        public float speed;
        public float xRemainder = 0;
        public float yRemainder = 0;
        public Velocity(float speed, Vector2 dir ) 
        {
            this.speed = speed;
            this.dir = dir;
        }
        public Velocity(float speed)
        {
            this.speed = speed;
            
        }
    }
}
