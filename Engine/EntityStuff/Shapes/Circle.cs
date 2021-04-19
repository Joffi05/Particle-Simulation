using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine.EntityStuff.Shapes
{
    public class Circle : Shape, ICollidable, IDrawable, IMovable
    {
        public float Radius { get { return Scale.x / 2; } set { }}
        public Vector2 CenterPoint { get { return new Vector2(this.Position.x + this.Scale.x / 2, this.Position.y + this.Scale.y / 2); } set { } }
        public Vector2 LastPos;
        public float Mass = 1;
        public Color PaintColor;

        #region Constructors
        public Circle(Vector2 position, float radius) : base(position, new Vector2(2 * radius, 2 * radius), "Particle")
        {
            this.Mass = 100000;
            PaintColor = Color.Black;
            this.LastPos = position;
        }
        public Circle(Vector2 position, Vector2 velocity, float radius, float mass) : base(position, velocity, new Vector2(),new Vector2(2 * radius, 2 * radius), "Particle")
        {
            this.Mass = mass;
            PaintColor = getColor(Mass);
            this.LastPos = position;
        }
        public Circle(Vector2 position, Vector2 velocity, Vector2 acceleration, float radius, float mass) : base(position, velocity, acceleration, new Vector2(2 * radius, 2 * radius), "Particle")
        {
            this.Mass = mass;
            this.LastPos = position;
        }
        #endregion

        public void Draw(Graphics g)
        {
            this.PaintColor = getColor(Mass);
            g.FillEllipse(new SolidBrush(PaintColor), Position.x, Position.y, Scale.x, Scale.y);
        }

        public void CircleCircleCollision(Circle a)
        {
            float dx = this.CenterPoint.x - a.CenterPoint.x;
            float dy = this.CenterPoint.y - a.CenterPoint.y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            if (distance <= a.Radius + this.Radius)
            {
                Vector2 newVelA = getNewVelocity(this, a);
                Vector2 newVelB = getNewVelocity(a, this);
                this.Velocity = newVelA;
                a.Velocity = newVelB;
                this.Position = LastPos;
                a.Position = a.LastPos;
            }
        }

        public void BorderCollision(Border border)
        {
            Border.Side side;

            side = border.OutOfBorder(this);
            if (side != Border.Side.NULL)
            {
                if (side == Border.Side.RIGHT) this.Velocity.x = -this.Velocity.x;
                if (side == Border.Side.LEFT) this.Velocity.x = -this.Velocity.x;
                if (side == Border.Side.BOT) this.Velocity.y = -this.Velocity.y;
                if (side == Border.Side.TOP) this.Velocity.y = -this.Velocity.y;
                this.Position = this.LastPos;
            }
            this.LastPos = this.Position;
        }

        public float GetKin()
        {
            return 0.5f * this.Mass * this.Velocity.GetNorm() * this.Velocity.GetNorm();
        }

        private static Vector2 getNewVelocity(Circle a, Circle b)
        {
            Vector2 VelSum = a.Velocity - b.Velocity;
            Vector2 PosSum = a.Position - b.Position;
            Vector2 CenterPosSum = a.CenterPoint - b.CenterPoint;
            float Norm = CenterPosSum.GetNorm();
            float dotSum = VelSum.x * PosSum.x + VelSum.y * PosSum.y;
            return a.Velocity - (2 * b.Mass / (a.Mass + b.Mass)) * (dotSum) / (Norm * Norm) * (a.Position - b.Position);
        }

        public override void RegisterInList(Entity entity)
        {
            ParticleCollision.AllCircles.Add(entity as Circle);
            base.RegisterInList(entity);
        }
        public override void UnregisterInList(Entity entity)
        {
            ParticleCollision.AllCircles.Remove(entity as Circle);
            base.UnregisterInList(entity);
        }

        private static Color getColor(float mass)
        {
            if (mass <= 1)
            {
                return Color.Yellow;
            }
            else if (mass <= 2)
            {
                return Color.Orange;
            }
            else if (mass <= 3)
            {
                return Color.OrangeRed;
            }
            else if (mass <= 5)
            {
                return Color.Green;
            }
            else if (mass <= 10)
            {
                return Color.Blue;
            }
            else if (mass <= 20)
            {
                return Color.DarkGray;
            }
            else if (mass > 20)
            {
                return Color.Black;
            }

            return Color.Red;
        }
    }
}