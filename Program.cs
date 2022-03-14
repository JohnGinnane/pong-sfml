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

        rectangle leftPaddle;
        rectangle rightPaddle;
        
        rectangle ball;
        DateTime ballSpawn;

        rectangle topEdge;
        rectangle bottomEdge;

        float maxPaddleSpeed = 300f;
        float paddleAcceleration = 120f;

        int leftScore;
        int rightScore;

        rectangle lastPoint;

        Vector2f playingField;

        public PongSFML() {
            window = Global.createWindow("Pong");
            init();
        }

        public void init() {
            Global.sfx.Add("wall", new sound("sounds/wall.wav"));
            Global.sfx.Add("paddle", new sound("sounds/paddle.wav"));
            Global.sfx.Add("score", new sound("sounds/score.wav"));

            playingField = new Vector2f(Global.ScreenSize.Y / 2f - 200, Global.ScreenSize.Y / 2f + 200);

            leftPaddle = new rectangle(new Vector2f(10, 40));
            leftPaddle.SetPosition(new Vector2f(20, Global.ScreenSize.Y / 2f));
            leftPaddle.OutlineColour = new Color(200, 0, 0);
            leftPaddle.OutlineThickness = 2f;
            leftPaddle.FillColour = new Color(255, 50, 50);
            leftPaddle.Drag = 10f;
            leftScore = 0;

            rightPaddle = new rectangle(new Vector2f(10, 40));
            rightPaddle.SetPosition(new Vector2f(Global.ScreenSize.X - 20, Global.ScreenSize.Y / 2f));
            rightPaddle.OutlineColour = new Color(0, 200, 0);
            rightPaddle.OutlineThickness = 2f;
            rightPaddle.FillColour = new Color(50, 255, 50);
            rightPaddle.Drag = 10f;
            rightScore = 0;

            topEdge = new rectangle(new Vector2f(Global.ScreenSize.X, 5));
            topEdge.SetPosition(new Vector2f(Global.ScreenSize.X / 2f, playingField.X + 20));
            topEdge.FillColour = Color.White;

            bottomEdge = new rectangle(new Vector2f(Global.ScreenSize.X, 5));
            bottomEdge.SetPosition(new Vector2f(Global.ScreenSize.X / 2f, playingField.Y - 20));
            bottomEdge.FillColour = Color.White;

            ballSpawn = DateTime.Now.AddSeconds(1);

            if (util.randint(0, 1) > 0) {
                lastPoint = rightPaddle;
            } else {
                lastPoint = leftPaddle;
            }
        }

        public void update(float delta) {
            Global.Keyboard.update();

            if (Global.Keyboard["escape"].isPressed) {
                window.Close();
            }

            if (ball == null) {
                if (DateTime.Now > ballSpawn) {
                    ball = new rectangle(new Vector2f(10, 10));
                    ball.SetPosition(Global.ScreenSize / 2f);
                    ball.SetVelocity(new Vector2f(100, util.randfloat(-200, 200)));
                    ball.FillColour = Color.White;

                    if (lastPoint == rightPaddle) {
                        ball.SetXVelocity(ball.Velocity.X * -1);
                    }
                }
            } else {
                ball.update(delta);

                // check for collisions against the walls
                if (intersection.rectangleInsideRectangle(ball.ToFloatRect(), topEdge.ToFloatRect()) ||
                    intersection.rectangleInsideRectangle(ball.ToFloatRect(), bottomEdge.ToFloatRect())) {
                    Global.sfx["wall"].play();
                    ball.SetYVelocity(ball.Velocity.Y * -1);
                }

                // check for collisions against the paddles
                if (intersection.rectangleInsideRectangle(ball.ToFloatRect(), leftPaddle.ToFloatRect())) {
                    Global.sfx["paddle"].play();
                    Vector2f dir = util.normalise(ball.Position - leftPaddle.Position);
                    dir *= util.magnitude(ball.Velocity * -1.1f);
                    ball.SetVelocity(dir);
                }

                // check for collisions against the paddles
                if (intersection.rectangleInsideRectangle(ball.ToFloatRect(), rightPaddle.ToFloatRect())) {
                    Global.sfx["paddle"].play();
                    Vector2f dir = util.normalise(ball.Position - rightPaddle.Position);
                    dir *= util.magnitude(ball.Velocity * -1.1f);
                    ball.SetVelocity(dir);
                }

                if (ball.Position.X < 0) {
                    rightScore += 1;
                    ballSpawn = DateTime.Now.AddSeconds(1);
                    ball = null;
                    Global.sfx["score"].play();                    
                } else if (ball.Position.X > Global.ScreenSize.X) {
                    leftScore += 1;
                    ballSpawn = DateTime.Now.AddSeconds(1);
                    ball = null;
                    Global.sfx["score"].play();
                }
            }

            // left paddle is controlled with W and S keys
            // right paddle is controlled with up and down arrows

            ///////// left paddle ///////////
            if (Global.Keyboard["w"].isPressed && leftPaddle.Velocity.Y > -maxPaddleSpeed) {
                leftPaddle.AddYVelocity(-paddleAcceleration);
            }

            if (Global.Keyboard["s"].isPressed && leftPaddle.Velocity.Y < maxPaddleSpeed) {
                leftPaddle.AddYVelocity(paddleAcceleration);
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
            if (Global.Keyboard["up"].isPressed && rightPaddle.Velocity.Y > -maxPaddleSpeed) {
                rightPaddle.AddYVelocity(-paddleAcceleration);
            }

            if (Global.Keyboard["down"].isPressed && rightPaddle.Velocity.Y < maxPaddleSpeed) {
                rightPaddle.AddYVelocity(paddleAcceleration);
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

            topEdge.draw(window);
            bottomEdge.draw(window);

            // Scores
            Text leftScoreText = new Text(leftScore.ToString(), Fonts.Hyperspace);
            leftScoreText.CharacterSize = 48;
            leftScoreText.Position = new Vector2f(10, 0);
            leftScoreText.FillColor = Color.White;
            window.Draw(leftScoreText);

            Text rightScoreText = new Text(rightScore.ToString(), Fonts.Hyperspace);
            rightScoreText.CharacterSize = 48;
            rightScoreText.Position = new Vector2f(Global.ScreenSize.X - 10 - 24, 0);
            rightScoreText.FillColor = Color.White;
            window.Draw(rightScoreText);

            leftPaddle.draw(window);
            rightPaddle.draw(window);

            // fence in the middle
            int divisions = 20;
            float spacing = (playingField.Y - playingField.X) / divisions;

            for (int i = 1; i < divisions - 1; i++) {
                RectangleShape rs = new RectangleShape(new Vector2f(2, spacing / 2f));
                rs.Position = new Vector2f(Global.ScreenSize.X / 2f, playingField.X + spacing * i);
                rs.Origin = new Vector2f(-1, spacing / -2f);
                rs.FillColor = Color.White;
                window.Draw(rs);
            }

            if (ball != null) { ball.draw(window); }

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
