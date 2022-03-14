using SFML.System;
using SFML.Graphics;

namespace pong_sfml {
    public class rectangle : body {
        private Vector2f size;
        public Vector2f Size {
            get { return size; }
            set { size = value; }
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

        public rectangle(Vector2f size) {
            this.Size = size;
            bodytype = enumBodyType.rectangle;
        }

        public override void draw(RenderWindow window)
        {
            RectangleShape rs = new RectangleShape(Size);
            rs.Origin = Size / 2f;
            rs.Position = Position;
            rs.FillColor = FillColour;
            rs.OutlineColor = OutlineColour;
            rs.OutlineThickness = OutlineThickness;

            window.Draw(rs);

            base.draw(window);
        }
    }
}