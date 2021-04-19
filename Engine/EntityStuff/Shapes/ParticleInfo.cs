using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine.EntityStuff.Shapes
{
    class ParticleInfo : Shape, IDrawable
    {
        private float Mass;
        public ParticleInfo(Vector2 position, Vector2 toDraw, float mass) : base(position, toDraw, "ParticleInfo")
        {
            this.Mass = mass;
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(new Pen(Color.Black, 3), Position.x, Position.y, Position.x + Scale.x * 25, Position.y + Scale.y * 25);
            g.DrawLine(new Pen(Color.Purple, 3), Position.x, Position.y, Position.x + Scale.x * 25, Position.y);
            g.DrawLine(new Pen(Color.Aqua, 3), Position.x, Position.y, Position.x, Position.y + Scale.y * 25);
            g.DrawString(Mass.ToString(), new Font("Arial", 14), new SolidBrush(Color.Black), Position.x, Position.y);
        }
    }
}
