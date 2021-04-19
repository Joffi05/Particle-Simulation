using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine.EntityStuff
{
    public interface ICollidable
    {
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }

        ICollidable Colliding(ICollidable a);
    }
}
