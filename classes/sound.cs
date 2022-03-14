using SFML.Audio;

namespace pong_sfml {
    public class sound {
        public SoundStatus status {
            get {
                return sfmlSound.Status;
            }
        }

        private SoundBuffer buffer;
        private Sound sfmlSound;
        
        public sound(string filename) {
            buffer = new SoundBuffer(filename);
            sfmlSound = new Sound(buffer);
        }

        public void play(bool loop = false) {
            sfmlSound.Loop = loop;
            sfmlSound.Play();
        }

        public void stop() {
            sfmlSound.Stop();
        }

        public void pause() {
            sfmlSound.Pause();
        }
    }
}