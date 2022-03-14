using SFML.System;
using SFML.Graphics;

namespace pong_sfml {
    public class circle : body {
        private float radius;
        public float Radius {
            get { return radius; }
            set { radius = value; }
        }

        private Color fillColour;
        public Color FillColour {
            get { return fillColour; }
            set { fillColour = value; }
        }

        private Color outlineColour;
        public Color OutlineColour {
            get { return outlineColour; }
            set { outlineColour = value; }
        }

        private float outlineThickness;
        public float OutlineThickness {
            get { return outlineThickness; }
            set { outlineThickness = value; }
        }

        public circle(float radius) {
            this.Radius = radius;
            bodytype = enumBodyType.circle;
        }

        public override void draw(RenderWindow window)
        {
            CircleShape cs = new CircleShape(Radius);
            cs.Origin = new Vector2f(Radius, Radius);
            cs.FillColor = FillColour;
            cs.OutlineColor = OutlineColour;
            cs.OutlineThickness = OutlineThickness;
            cs.Position = Position;

            window.Draw(cs);

            base.draw(window);
        }
    }
}