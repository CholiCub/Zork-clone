using System;

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
            const string defaultGameFilename = "Zork.json";

            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguements.GameFilename] : defaultGameFilename);
            ConsoleInputService inputService = new ConsoleInputService();
            ConsoleOutputService outputService = new ConsoleOutputService();

            Game game = Game.Load(gameFilename);
            game.OutputService = outputService;
            game.InputService = inputService;

            Console.WriteLine(game.WelcomeMessage);

            Console.WriteLine(game.Player.CurrentRoom.Name);
            Console.WriteLine(game.Player.CurrentRoom.Description);
            while (game.IsRunning)
            {
                inputService.ProcessInput();
            }
            
            Console.WriteLine("Thank you for playing!");
        }
        private enum CommandLineArguements
        {
            GameFilename = 0
        }
    }
}
