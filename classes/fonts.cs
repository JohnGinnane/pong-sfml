using SFML.Graphics;

namespace pong_sfml {
    public static class Fonts {
        private static Font arial = new Font("fonts/arial.ttf");
        public static Font Arial => arial;

        private static Font hyperspace = new Font("fonts/Hyperspace.ttf");
        public static Font Hyperspace => hyperspace;
    }
}