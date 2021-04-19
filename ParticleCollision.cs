using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParticleCollision.Engine;
using ParticleCollision.Engine.EntityStuff;
using ParticleCollision.Engine.EntityStuff.GUI;
using ParticleCollision.Engine.EntityStuff.Shapes;

namespace ParticleCollision
{
    class ParticleCollision : Engine.Engine
    {
        public static Vector2 ScreenSize = new Vector2(1000, 700);
        public ParticleCollision() : base(ScreenSize, "Particle Collision") { }

        public static List<Circle> AllCircles = new List<Circle>();


        public static bool pause = true;
        private static bool MouseWheelSpins = false;
        private static bool mouseLeftDown = false;
        private static bool mouseRightDown = false;
        public static Vector2 MousePos = new Vector2();

        public Border Border1;
        public Circle Particle1;
        public GUITextBox tb_Comparisons;
        public GUITextBox tb_ComparisonsDes;
        public GUITextBox tb_KinematicsDes;
        public GUITextBox tb_Kinematics;
        public GUITextBox tb_ParticleCount;
        public GUITextBox tb_ParticleCountDes;


        #region Key Handlers
        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { pause = !pause; }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
        }
        #endregion
        #region Mouse Handlers
        public override void GetMousePos(MouseEventArgs e)
        {
            MousePos.x = e.X;
            MousePos.y = e.Y;
        }

        private int BiggerSmaller;
        public override void GetMouseWheel(MouseEventArgs e)
        {
            MouseWheelSpins = true;
            BiggerSmaller = e.Delta / 120;
        }

        public override void GetMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLeftDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouseRightDown = true;
            }
        }

        public override void GetMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLeftDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouseRightDown = false;
            }
        }
        #endregion

        public override void OnDraw()
        {
        }

        public override void OnLoad()
        {
            tb_ComparisonsDes = new GUITextBox("Comparisons:", new Vector2(850, 50), new Vector2(125, 20), "tbComparisonsDes");
            tb_Comparisons = new GUITextBox("0" , new Vector2(850, 70), new Vector2(80, 20), "tbComparisons", true);

            tb_ParticleCountDes = new GUITextBox("Particle Count", new Vector2(850, 90), new Vector2(120, 45), "tbParticleCountDes");
            tb_ParticleCount = new GUITextBox("0", new Vector2(850, 135), new Vector2(120, 20), "tbParticleCount", true);

            tb_KinematicsDes = new GUITextBox("Kinematic energy:", new Vector2(850, 200), new Vector2(120, 45), "tbKinematicsDes");
            tb_Kinematics = new GUITextBox("0", new Vector2(850, 245), new Vector2(80, 20), "tbKinematics", true);

            Border1 = new Border(new Vector2(50, 50), new Vector2(800, 500), "Border");
        }

        
        public override void OnUpdate()
        {
            int comparisons = 0;
            float Kin = 0;
            //iterate all circles
            foreach (Circle circle1 in AllCircles.ToArray())
            {
                circle1.BorderCollision(Border1);
                Kin += circle1.GetKin();
                foreach (Circle circle2 in AllCircles.ToArray())
                {
                    if (circle1 != circle2)
                    {
                        circle1.CircleCircleCollision(circle2);
                        comparisons++;
                    }
                }

                circle1.Move();
            }

            tb_ParticleCount.Text = AllCircles.ToArray().Length.ToString();
            tb_Comparisons.Text = comparisons.ToString();
            tb_Kinematics.Text = Kin.ToString();

            //iterate all entitys
            foreach (Entity entity in AllEntitys.ToArray())
            {
                if(entity.Tag == "ParticleInfo")
                {
                    UnRegisterEntity(entity);
                }
            }
        }

        ParticleInfo particleInfo = null;
        Circle c = null;
        Circle h = null;
        public override void OnPauseUpdateLoop()
        {
            UnRegisterEntity(particleInfo);

            c = getMouseHoveredCircle();
                
            if (c != null)
            {
                particleInfo = new ParticleInfo(c.CenterPoint, c.Velocity, c.Mass);

                h = c;

                if (MouseWheelSpins)
                {
                    if (BiggerSmaller == 1)
                    {
                        if (h.Mass < 30) h.Mass++;
                    }
                    if (BiggerSmaller == -1)
                    {
                        if (h.Mass != 1) h.Mass--;
                    }
                    MouseWheelSpins = false;
                }
            }

            if (mouseLeftDown)
            {
                if (h != null)
                {
                    UnRegisterEntity(particleInfo);
                    particleInfo = new ParticleInfo(h.CenterPoint, new Vector2((MousePos.x - h.CenterPoint.x) / 20, (MousePos.y - h.CenterPoint.y) / 20), h.Mass);
                }
                if (h == null)
                {
                    new Circle(new Vector2(MousePos.x, MousePos.y), new Vector2(), 20, 1);
                    mouseLeftDown = false;
                }
            }
            if (mouseRightDown)
            {
                if (h != null)
                {
                    h.UnregisterInList(h);
                    h = null;
                }
            }

            if (!mouseLeftDown && h != null)
            {
                h.Velocity = particleInfo.Scale;
                h = null;
            }
        }

        private static Circle getMouseHoveredCircle()
        {
            foreach (Circle circle in AllCircles.ToArray())
            {
                if (MousePos.inRect(circle) != null) return (Circle)MousePos.inRect(circle);
            }
            return null;
        }
    }
}