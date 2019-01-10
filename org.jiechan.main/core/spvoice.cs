using SpeechLib;

namespace org.jiechan.main.core {
    public enum VoiceStatus {
        Play,
        Ready,
        Pause,
    }

    public class spvoice {
        
        public static spvoice GetInstance() {
            // 定义一个标识确保线程同步

            if (sp == null) {
                lock (locker) {
                    if (sp == null) {
                        sp = new spvoice();
                    }

                }
            }
            return sp;
        }

        private static readonly object locker = new object();

        private static spvoice sp;

        private readonly SpVoice _voice;


        private spvoice() {
            _voice = new SpVoice();
        }

        public void Speak(string text, SpeechVoiceSpeakFlags speakFlag = SpeechVoiceSpeakFlags.SVSFDefault) {
            _voice.Speak(text, speakFlag);
        }

        public void Pause() {
            if (_voice != null)
                _voice.Pause();
        }

        public void speakText(string str) {
            SpVoice _voice = new SpVoice();
            _voice.Speak(str, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }


    }
}

