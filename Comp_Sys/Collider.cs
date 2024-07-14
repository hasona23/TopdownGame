using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopdownGame.Components;
using Zap_ecs.Tilemaps;

namespace TopdownGame.Comp_Sys
{
    public class Collider : IComponent
    {

        public Rectangle rect;
       
        Entity collidedWith;
        public Collider(int x, int y, int width, int height) 
        {
           rect = new Rectangle(x, y, width,height);
        }

        public bool IsCollidingAt(List<Entity> collisionList,Vector2 pos) 
        {
            MoveRect(pos);
            foreach(var entity in collisionList) 
            {
                if (this.rect.Intersects(entity.GetComponent<Collider>().rect) && entity.GetComponent<Collider>() != this )
                {
                    collidedWith = entity;
                    return true;
                }
            }
            collidedWith = null;
            return false;
        }
        public bool IsCollidingTileAt(List<Tile> collisionList, Vector2 pos)
        {
            MoveRect(pos);
            foreach (var tile in collisionList)
            {
                if (this.rect.Intersects(tile.rect))
                {
                    return true;
                }
            }
            return false;
        }
        private void MoveRect(Vector2 pos) 
        {
            rect.Location = pos.ToPoint();
        }
    }

    public class MovePhysics : GameSystem 
    {
        
        List<Tile> collisionTile;
        public MovePhysics(params Tilemap[] collisionTilemaps) : base()
        {
            AddRequiredComponent<Collider>();
            AddRequiredComponent<Transform>();

            collisionTile = new List<Tile>();

            foreach (var tilemap in collisionTilemaps) 
            {
                if(tilemap.collisionMode != CollisionMode.RigidBody)
                    continue;
                collisionTile.AddRange(tilemap.GetCollisions(1));
            }
        }
        
        public override void Update(GameTime gt, SpriteBatch sb = null)
        {
            foreach(int id in registeredEntityIds)
            {
                Entity entity = world.GetEntityById(id);
                if (entity.HasComponent<Velocity>()) 
                {
                    var velocity = entity.GetComponent<Velocity>();
                    float changeX = velocity.dir.X * velocity.speed * (float)(gt.ElapsedGameTime.TotalSeconds);
                    float changeY = velocity.dir.Y * velocity.speed * (float)(gt.ElapsedGameTime.TotalSeconds);
                    MoveX(collisionTile,this.Entities,changeX,entity);
                    MoveY(collisionTile, this.Entities,changeY, entity);
                }
            }
        }
        private int Sign(int i) 
        {
            if (i > 0)
                return 1;
            else if(i < 0) return -1;
            else return 0;
        }
        private void MoveX(List<Tile> tiles, List<Entity> collisionList,float changeX, Entity entity)
        {
            var collision = entity.GetComponent<Collider>();
            var velocity = entity.GetComponent<Velocity>();
            var transform = entity.GetComponent<Transform>();
            velocity.xRemainder += changeX;
            int move = (int)velocity.xRemainder;
            
            if (move != 0)
            {
                velocity.xRemainder -= move;
                int sign = Sign(move);
                while (move != 0)
                {
                    if (!collision.IsCollidingAt(collisionList, transform.pos + new Vector2(sign, 0)) && !collision.IsCollidingTileAt(tiles, transform.pos + new Vector2(sign, 0)))
                    {
                        // There is no Solid immediately beside us
                        transform.pos.X += sign;
                        move -= sign;

                    }
                    else
                    {
                        
                        break;
                    }
                }
            }
        }
            private void MoveY(List<Tile> tiles, List<Entity> collisionList, float changeY,Entity entity)
            {
                var collision = entity.GetComponent<Collider>();
                var velocity = entity.GetComponent<Velocity>();
                var transform = entity.GetComponent<Transform>();

                velocity.yRemainder += changeY;
                int move = (int)velocity.yRemainder;
                if (move != 0)
                {
                    velocity.yRemainder -= move;
                    int sign = Sign(move);
                    while (move != 0)
                    {
                        if (!collision.IsCollidingAt(collisionList, transform.pos + new Vector2(0, sign)) && !collision.IsCollidingTileAt(tiles, transform.pos + new Vector2(0,sign)))
                        {
                            // There is no Solid immediately beside us
                            transform.pos.Y += sign;
                            move -= sign;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
    }
}
