using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleCollision.Engine.EntityStuff
{
    public abstract class Shape : Entity, ICollidable, IMovable
    {
        public Vector2 Scale;

        Vector2 ICollidable.Position
        {
            get { return this.Position;  }
            set { this.Position = value; }
        }
        Vector2 ICollidable.Scale
        {
            get { return this.Scale; }
            set { this.Scale = value; }
        }
        Vector2 IMovable.Position
        {
            get { return this.Position; }
            set { this.Position = value; }
        }
        Vector2 IMovable.Velocity
        {
            get { return this.Velocity; }
            set { this.Velocity = value; }
        }
        Vector2 IMovable.Acceleration
        {
            get { return this.Acceleration; }
            set { this.Acceleration = value; }
        }
        public Shape(Vector2 position, Vector2 scale, string tag) : base(position, tag)
        {
            this.Scale = scale;
        }
        public Shape(Vector2 position, Vector2 velocity, Vector2 acceleration, Vector2 scale, string tag) : base(position, velocity, acceleration, tag)
        {
            this.Scale = scale;
        }


        /// <summary>
        /// A method for basic AABB collision detection
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public virtual ICollidable Colliding(ICollidable a)
        {
            if (a.Position.x < this.Position.x + this.Scale.x &&
                a.Position.x + a.Scale.x > this.Position.x &&
                a.Position.y < this.Position.y + this.Scale.y &&
                a.Position.y + a.Scale.y > this.Position.y)
            {
                return a;
            }

            return null;
        }

        public virtual void Move()
        {
            this.Velocity += this.Acceleration;
            this.Position += this.Velocity;
        }
    }
}
