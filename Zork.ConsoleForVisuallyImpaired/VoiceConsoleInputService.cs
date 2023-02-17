using System;
using System.Collections.Generic;
using System.Text;
using System.Speech.Recognition;
using Google.Cloud.Speech.V1;

namespace Zork
{
    class VoiceConsoleInputService : InputService
    {
        public event EventHandler<string> InputReceived;
        public string speechToText;

      
        SpeechRecognitionEngine listener = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        public void ProcessInput()
        {
         //   listener.LoadGrammar(new DictationGrammar());
         //   listener.SpeechRecognized +=
         //new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
         //  listener.SetInputToDefaultAudioDevice();

         //   listener.RecognizeAsync(RecognizeMode.Multiple);
            string inputString = Console.ReadLine().Trim();
            //if(string.IsNullOrEmpty(speechToText))
            //{ return; }
            //else
            InputReceived?.Invoke(this, inputString);
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
            speechToText = e.Result.Text;
        }
    }
}
