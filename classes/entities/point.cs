using SFML.System;
using SFML.Graphics;

namespace pong_sfml {
    public class point : body {
        private Color colour;
        public Color Colour {
            get { return colour; }
            set { colour = value; }
        }

        public point() {
            colour = Color.White;
        }

        public override void draw(RenderWindow window)
        {
            CircleShape cs = new CircleShape(2f);
            cs.Position = Position;
            cs.Origin = new Vector2f(2f, 2f);
            cs.FillColor = colour;

            window.Draw(cs);

            base.draw(window);
        }
    }
}