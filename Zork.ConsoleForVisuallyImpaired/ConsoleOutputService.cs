using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;

namespace Zork
{
    class ConsoleOutputService : OutputService
    {
        public void Write(object value)
        {
            Console.Write(value);
        }

        public void WriteLine(object value)
        {
            if (value is string sValue)
            {
                var ss = new SpeechSynthesizer();
                ss.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult);
                var isDoneSpeaking = false;
                ss.SpeakCompleted += (s, e) =>
                {
                    Console.WriteLine(sValue);
                    isDoneSpeaking = true;
                };
                //ss.SpeakProgress += (s, e) =>
                //{
                //    Console.WriteLine(e.AudioPosition);
                //    Console.WriteLine(e.CharacterCount);
                //    Console.WriteLine(e.CharacterPosition);
                //    Console.WriteLine(e.Text);
                //};
                ss.SpeakAsync(sValue);
                // Block untill done speacking and text
                while(!isDoneSpeaking) 
                {
                    Thread.Sleep(10);
                }
            }
        }
    }
}
