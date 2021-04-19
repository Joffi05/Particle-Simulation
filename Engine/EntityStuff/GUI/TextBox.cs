using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCollision.Engine.EntityStuff.GUI
{
    public class GUITextBox : Shape, IDrawable
    {
        public string Text = "";
        private Font Font = new Font("Arial", 14);
        private bool DrawBox;

        public GUITextBox(string text, Vector2 position, Vector2 scale, string tag, bool drawBox = false) : base(position, scale, tag)
        {
            this.Text = text;
            this.DrawBox = drawBox;
        }
        public GUITextBox(string text, Vector2 position, Vector2 scale, Font font, string tag, bool drawBox) : base(position, scale, tag)
        {
            this.Text = text;
            this.DrawBox = drawBox;
            this.Font = font;
        }

        public void Draw(Graphics g)
        {
            RectangleF rect = new RectangleF(Position.x, Position.y, Scale.x, Scale.y);
            g.DrawString(this.Text, this.Font, new SolidBrush(Color.Black), rect);

            if (this.DrawBox)
            {
                g.DrawRectangle(new Pen(Color.Black), Position.x, Position.y, Scale.x, Scale.y);
            }
        }
    }
}
