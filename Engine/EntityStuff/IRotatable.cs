using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine.EntityStuff
{
    interface IRotatable : IDrawable
    {
        void Rotate(float degree);
    }
}
