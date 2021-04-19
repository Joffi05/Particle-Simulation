using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine
{
    public abstract class Entity
    {
        public Vector2 Position = null;
        public Vector2 Velocity = null;
        public Vector2 Acceleration = null;
        public string Tag = "";


        public Entity(Vector2 position, string tag)
        {
            this.Position = position;
            this.Tag = tag;

            RegisterInList(this);
        }
        public Entity(Vector2 position, Vector2 velocity, Vector2 acceleration, string tag)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Acceleration = acceleration;
            this.Tag = tag;

            RegisterInList(this);
        }

        public virtual void RegisterInList(Entity entity)
        {
            Engine.AllEntitys.Add(entity);
        }

        public virtual void UnregisterInList(Entity entity)
        {
            Engine.AllEntitys.Remove(entity);
        }
    }
}
