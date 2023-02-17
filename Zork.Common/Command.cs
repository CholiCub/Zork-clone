using System;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
    public class Command
    {
        public string Name { get; set; }
        public string[] Verbs { get; set; }
        public Action<CommandContext> Action { get; set; }

        public Command(string name, string verbs, Action<CommandContext> action): this(name, new string[] { verbs }, action)
        {
        }

        public Command(string name, IEnumerable<string> verbs, Action<CommandContext> action)
        {
            Name = name.Trim().ToUpper();
            Verbs = verbs.ToArray();
            Action = action;
            
        }
    }
    
    public class CommandContext
    {
        public Game Game { get; set; }
        public string Input { get; set; }
        public CommandContext(Game game, string input)
        {
            Game = game;
            Input = input;
        }
    }
}
