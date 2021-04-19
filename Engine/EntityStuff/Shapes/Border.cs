using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine.EntityStuff
{
    public class Border : Shape, ICollidable, IDrawable
    {

        public enum Side { NULL, TOP, LEFT, RIGHT, BOT}

        /// <summary>
        /// Create a not moving Rectangle
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="tag"></param>
        public Border(Vector2 position, Vector2 scale, string tag) : base(position, scale, tag)
        {
        }

        /// <summary>
        /// Create a Rectangle with velocity and acceleration
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="acceleration"></param>
        /// <param name="scale"></param>
        /// <param name="tag"></param>

        public void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Blue, 2), Position.x, Position.y, Scale.x, Scale.y);
        }

        public override void Move()
        {
        }

        /// <summary>
        /// Returns true if outside Border
        /// </summary>
        /// <param name="a"></param>
        /// <param name="this"></param>
        /// <returns></returns>
        public override ICollidable Colliding(ICollidable a)
        {
            if (a.Position.x < this.Position.x + this.Scale.x &&
                a.Position.x + a.Scale.x > this.Position.x &&
                a.Position.y < this.Position.y + this.Scale.y &&
                a.Position.y + a.Scale.y > this.Position.y)
            {
                return null;
            }

            return a;
        }

        public Side OutOfBorder(ICollidable a)
        {
            if (a.Position.y <= this.Position.y)
            {
                return Side.TOP;
            }
            if (a.Position.x + a.Scale.x >= this.Scale.x + a.Scale.x)
            {
                return Side.RIGHT;
            }
            if (a.Position.y + a.Scale.y >= this.Scale.y + a.Scale.x)
            {
                return Side.BOT;
            }
            if (a.Position.x <= a.Scale.x)
            {
                return Side.LEFT;
            }

            return Side.NULL;
        }
    }
}
