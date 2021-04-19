using ParticleCollision.Engine.EntityStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        #region constructors
        public Vector2()
        {
            x = Zero().x;
            y = Zero().y;
        }

        public Vector2(float X, float Y)
        {
            this.x = X;
            this.y = Y;
        }

        public static Vector2 Zero()
        {
            return new Vector2(0f, 0f);
        }
        #endregion

        #region operators
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
        }
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }
        public static Vector2 operator /(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x / rhs.x, lhs.y / rhs.y);
        }
        public static Vector2 operator /(int lhs, Vector2 rhs)
        {
            return new Vector2(lhs / rhs.x, lhs / rhs.y);
        }
        public static Vector2 operator /(Vector2 lhs, int rhs)
        {
            return new Vector2(lhs.x / rhs, lhs.x / rhs);
        }
        public static Vector2 operator /(float lhs, Vector2 rhs)
        {
            return new Vector2(lhs / rhs.x, lhs / rhs.y);
        }
        public static Vector2 operator *(float lhs, Vector2 rhs)
        {
            return new Vector2(lhs * rhs.x, lhs * rhs.y);
        }
        public static Vector2 operator *(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x * rhs.x, lhs.y * rhs.y);
        }
        public static Vector2 operator *(Vector2 lhs, int rhs)
        {
            return new Vector2(lhs.x * rhs, lhs.y * rhs);
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y) return true;
            return false;
        }
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
            {
                return false;
            }
            return this == (Vector2)obj;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
        #endregion 

        public Vector2 Abs()
        {
            return new Vector2(Math.Abs(this.x), Math.Abs(this.y));
        }
        public Vector2 Sqrt()
        {
            return new Vector2((float)Math.Sqrt(this.x), (float)Math.Sqrt(this.y));
        }
        public float GetNorm()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public ICollidable inRect(ICollidable a)
        {
            if (a.Position.x < this.x + 1 &&
                a.Position.x + a.Scale.x > this.x &&
                a.Position.y < this.y + 1 &&
                a.Position.y + a.Scale.y > this.y)
            {
                return a;
            }

            return null;
        }
    }
}

