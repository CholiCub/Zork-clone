using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;

namespace Zork
{
    class VoiceConsoleOutputService : OutputService
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
                ss.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
                ss.Rate = -1;
                var isDoneSpeaking = false;
                ss.SpeakProgress += (s, e) =>
                {
                    //Console.WriteLine(e.AudioPosition);
                    //Console.WriteLine(e.CharacterCount);
                    //Console.WriteLine(e.CharacterPosition);

                    Console.Write(e.Text+" ");
                };
                ss.SpeakCompleted += (s, e) =>
                {
                    //Console.WriteLine(sValue);
                    isDoneSpeaking = true;
                };

                    ss.SpeakAsync(sValue);
                // Block untill done speacking and text
                while (!isDoneSpeaking) 
                {
                    //Thread.Sleep(10);
                }
            }
        }
    }
}
