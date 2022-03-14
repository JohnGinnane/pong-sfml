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
            Global.ScreenSize = new Vector2f(512, 512);
            PongSFML pong = new PongSFML();
            pong.run();
        }
    }

    public class PongSFML {
        public RenderWindow window;
        private const float timeStep = 1 / 60.0f;
        private float timeScale = 1.0f;
        private DateTime lastTime;

        // paddles
        rectangle leftPaddle;
        rectangle rightPaddle;
        circle ball;

        float paddleSpeed = 40f;

        int leftScore;
        int rightScore;

        Vector2f playingField;

        public PongSFML() {
            window = Global.createWindow("Pong");
            init();
        }

        public void init() {
            playingField = new Vector2f(Global.ScreenSize.Y / 2f - 200, Global.ScreenSize.Y / 2f + 200);

            leftPaddle = new rectangle(new Vector2f(10, 40));
            leftPaddle.SetPosition(new Vector2f(20, Global.ScreenSize.Y / 2f));
            leftPaddle.OutlineColour = new Color(200, 0, 0);
            leftPaddle.OutlineThickness = 2f;
            leftPaddle.FillColour = new Color(255, 50, 50);
            leftPaddle.Drag = 2f;
            leftScore = 0;

            rightPaddle = new rectangle(new Vector2f(10, 40));
            rightPaddle.SetPosition(new Vector2f(Global.ScreenSize.X - 20, Global.ScreenSize.Y / 2f));
            rightPaddle.OutlineColour = new Color(0, 200, 0);
            rightPaddle.OutlineThickness = 2f;
            rightPaddle.FillColour = new Color(50, 255, 50);
            rightPaddle.Drag = 2f;
            rightScore = 0;
        }

        public void update(float delta) {
            Global.Keyboard.update();

            if (Global.Keyboard["escape"].isPressed) {
                window.Close();
            }

            // left paddle is controlled with W and S keys
            // right paddle is controlled with up and down arrows

            ///////// left paddle ///////////
            if (Global.Keyboard["w"].isPressed) {
                leftPaddle.AddYVelocity(-paddleSpeed);
            }

            if (Global.Keyboard["s"].isPressed) {
                leftPaddle.AddYVelocity(paddleSpeed);
            }

            if (leftPaddle.Position.Y < playingField.X + leftPaddle.Size.Y) {
                leftPaddle.SetYVelocity(0);
                leftPaddle.SetYPosition(playingField.X + leftPaddle.Size.Y);
            }

            if (leftPaddle.Position.Y > playingField.Y - leftPaddle.Size.Y) {
                leftPaddle.SetYVelocity(0);
                leftPaddle.SetYPosition(playingField.Y - leftPaddle.Size.Y);
            }

            leftPaddle.update(delta);

            ///////// right paddle ///////////
            if (Global.Keyboard["up"].isPressed) {
                rightPaddle.AddYVelocity(-paddleSpeed);
            }

            if (Global.Keyboard["down"].isPressed) {
                rightPaddle.AddYVelocity(paddleSpeed);
            }

            if (rightPaddle.Position.Y < playingField.X + rightPaddle.Size.Y) {
                rightPaddle.SetYVelocity(0);
                rightPaddle.SetYPosition(playingField.X + rightPaddle.Size.Y);
            }

            if (rightPaddle.Position.Y > playingField.Y - rightPaddle.Size.Y) {
                rightPaddle.SetYVelocity(0);
                rightPaddle.SetYPosition(playingField.Y - rightPaddle.Size.Y);
            }

            rightPaddle.update(delta);
        }

        public void draw() {
            window.Clear();

            leftPaddle.draw(window);
            rightPaddle.draw(window);

            RectangleShape topEdge = new RectangleShape(new Vector2f(Global.ScreenSize.X, 5));
            topEdge.Position = new Vector2f(0, playingField.X);

            RectangleShape bottomEdge = new RectangleShape(new Vector2f(Global.ScreenSize.X, 5));
            bottomEdge.Position = new Vector2f(0, playingField.Y);

            window.Draw(topEdge);
            window.Draw(bottomEdge);

            window.Display();
        }

        public void run() {
            while (window.IsOpen) {
                if (!window.HasFocus()) { continue; }

                if ((float)(DateTime.Now - lastTime).TotalMilliseconds / 1000f > timeStep) {
                    float delta = timeStep * timeScale;
                    lastTime = DateTime.Now;

                    window.DispatchEvents();
                    update(delta);
                }

                draw();
            }
        }
    }
}
