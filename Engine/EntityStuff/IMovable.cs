using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine
{
    public interface IMovable
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 Acceleration { get; set; }
        void Move();
    }
}
