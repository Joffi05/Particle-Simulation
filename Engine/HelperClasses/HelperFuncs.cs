using ParticleCollision.Engine.EntityStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine
{
    public class HelperFuncs
    {
        private static Random ran = new Random();

        public static float DegToRad(int Deg)
        {
            return (float)(Deg * Math.PI / 180);
        }
        public static int GetRandom(int min, int max)
        {
            return ran.Next(min, max);
        }
    }
}
