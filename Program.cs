using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace pong_sfml
{
    class Program
    {
        static void Main(string[] args)
        {
            PongSFML pong = new PongSFML();
            pong.run();
        }
    }

    public class PongSFML {
        public RenderWindow window;
        private const float timeStep = 1 / 60.0f;
        private float timeScale = 1.0f;
        private DateTime lastTime;

        public PongSFML() {
            window = Global.createWindow("Pong");
            init();
        }

        public void init() {

        }

        public void update() {

        }

        public void draw() {

        }

        public void run() {

        }
    }
}
