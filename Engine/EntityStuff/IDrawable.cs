using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleCollision.Engine.EntityStuff
{
    public interface IDrawable
    {
        void Draw(Graphics g);
    }
}
