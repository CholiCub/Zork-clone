using System;
using System.Speech.Synthesis;
namespace Zork
{
    class Program
    {
        /// <summary>
        /// Create default file.
        /// If file not supplied use default file to deserilize into game object.
        /// contains: Welcome message, world object: starting location(Room),  
        /// Set player in game, staring location
        /// Run game
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //added spech
            SpeechSynthesizer TextToSpeech = new SpeechSynthesizer();
            TextToSpeech.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            const string defaultGameFilename = "Zork.json";

            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguements.GameFilename] : defaultGameFilename);

            VoiceConsoleInputService inputService = new VoiceConsoleInputService();
            VoiceConsoleOutputService outputService = new VoiceConsoleOutputService();

            Game game = Game.Load(gameFilename);
            game.OutputService = outputService;
            game.InputService = inputService;
            
            game.Start();
            
            while (game.IsRunning)
            {
                inputService.ProcessInput();
            }
           
            outputService.Write("Thank you for playing!");
        }
        private enum CommandLineArguements
        {
            GameFilename = 0
        }
    }
}
