using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using ParticleCollision.Engine.EntityStuff;
using ParticleCollision.Engine.EntityStuff.Shapes;

namespace ParticleCollision.Engine
{
    public class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }

    public abstract class Engine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "Game";
        private Thread GameLoopThread = null;
        public static Canvas Window = null;

        public static List<Entity> AllEntitys = new List<Entity>();

        public Color BackgroundColor = Color.Gray;

        public Vector2 CameraZoom = new Vector2(1, 1);
        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;

        public Engine(Vector2 ScreenSize, string Title)
        {
            Log.Info("Simulation is starting");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.x, (int)ScreenSize.y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
            Window.MouseMove += Window_MouseMove;
            Window.MouseWheel += Window_MouseWheel;
            Window.MouseDown += Window_MouseDown;
            Window.MouseUp += Window_MouseUp;
            Window.FormClosing += Window_FormClosing;
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            GetMousePos(e);
        }
        private void Window_MouseWheel(object sender, MouseEventArgs e)
        {
            GetMouseWheel(e);
        }
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            GetMouseDown(e);
        }
        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            GetMouseUp(e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        public static void RegisterEntity(Entity entity)
        {
            AllEntitys.Add(entity);
        }
        public static void UnRegisterEntity(Entity entity)
        {
            AllEntitys.Remove(entity);
        }

        void GameLoop()
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });

                    if (!ParticleCollision.pause)
                    {
                        OnUpdate();
                    }
                    else
                    {
                        OnPauseUpdateLoop();
                    }
                    Thread.Sleep(5);
                }
                catch (Exception e)
                {
                    Log.Error($"Window has not been found... {e}");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackgroundColor);
            g.TranslateTransform(CameraPosition.x, CameraPosition.y);
            g.RotateTransform(CameraAngle);
            g.ScaleTransform(CameraZoom.x, CameraZoom.y);

            try
            { 
                //das iwie auslagern
                foreach (IDrawable entity in AllEntitys.ToList())
                {
                    entity.Draw(g);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Renderer got Error: {ex}");
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnPauseUpdateLoop();
        public abstract void OnDraw();
        public abstract void GetMousePos(MouseEventArgs e);
        public abstract void GetMouseWheel(MouseEventArgs e);
        public abstract void GetMouseDown(MouseEventArgs e);
        public abstract void GetMouseUp(MouseEventArgs e);
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}