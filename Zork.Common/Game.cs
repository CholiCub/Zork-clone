using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace Zork
{
    public class Game
    {
        [JsonIgnore]
        public Player Player { get;  private set; }
        public World World { get; set; }
        public bool IsRunning { get; private set; } = true;
        [JsonIgnore]
        public int Moves { get; private set; } = 0;
        [JsonIgnore]
        public int Score { get; private set; } = 0;
        [JsonIgnore]
        public Dictionary<string, Command> Commands { get; private set;}

        public OutputService OutputService { get; set; }

        private InputService inputService;

        public InputService InputService
        {
            get => inputService;
            set 
            {
                if (inputService is InputService oldIs)
                {
                    oldIs.InputReceived -= InputService_InputReceived;
                }
                
                inputService = value;

                inputService.InputReceived += InputService_InputReceived;
            }
        }

        private void InputService_InputReceived(object sender, string e)
        {
            var attempt1 = e.TrimStart(' ');
            string rest;
            string commandS;
            if (!(attempt1.Length > 0))
            {
                OutputService.WriteLine("Unknown command.");
                return;
            }
            
            var spaceIndex = attempt1.IndexOf(' ');

            if(spaceIndex == -1)
            {
                commandS = attempt1;
                rest = string.Empty;
            }
            else
            {
                commandS = attempt1.Remove(spaceIndex);
                rest = attempt1.Substring(spaceIndex).Trim();
            }

            if (!(Commands.TryGetValue(commandS.ToUpper(), out Command command)))
            {
                OutputService.WriteLine("Unknown command.");
                return;
            }

            command.Action(new CommandContext(this, rest));
        }

        public Game()
        {
            var commands = new Command[]
            {
                new Command("QUIT", new string[]{"QUIT","Q","EXIT" },(cc) =>CheckForSingleCommandWrapper(cc, () => cc.Game.IsRunning = false)),
                new Command("LOOK", new string[]{"LOOK","L"},(cc) => CheckForSingleCommandWrapper(cc, () => 
                {
                    OutputService.WriteLine(cc.Game.Player.CurrentRoom.Description);
                    foreach (var item in cc.Game.Player.CurrentRoom.Items)
                    {
                        OutputService.WriteLine(item.Description);
                    }
                    cc.Game.Moves++;
                })),
                new Command("REWARD", new string[]{"REWARD","R"},(cc) => CheckForSingleCommandWrapper(cc, () =>
                {
                    cc.Game.Score++;
                    OutputService.WriteLine("Score: "+ cc.Game.Score);
                })),
                new Command("MOVE", new string[]{"MOVE","M"},(cc) => CheckForSingleCommandWrapper(cc, () =>
                {
                    OutputService.WriteLine("Moves: "+ cc.Game.Moves);
                })),
                new Command("NORTH", new string[]{"NORTH","N"}, (cc) =>CheckForSingleCommandWrapper(cc, () => cc.Game.Move(Directions.NORTH))),
                new Command("SOUTH", new string[]{"SOUTH","S"},  (cc) =>CheckForSingleCommandWrapper(cc, () => cc.Game.Move(Directions.SOUTH))),
                new Command("EAST", new string[]{"EAST","E"},  (cc) => CheckForSingleCommandWrapper(cc, () =>cc.Game.Move(Directions.EAST))),
                new Command("WEST", new string[]{"WEST","W"},  (cc) =>CheckForSingleCommandWrapper(cc, () =>cc.Game.Move(Directions.WEST))),
                new Command("UP", new string[]{"UP"},  (cc) => CheckForSingleCommandWrapper(cc, () =>cc.Game.Move(Directions.UP))),
                new Command("DOWN", new string[]{"DOWN"},  (cc) =>CheckForSingleCommandWrapper(cc, () =>cc.Game.Move(Directions.DOWN))),
                new Command("TAKE", new string[]{"TAKE"},  (cc) => CheckForItemCommandWrapper(cc,()=>
                {
                    //check if an item is in room
                    var itemToTake = cc.Input;
                    if (!(cc.Game.Player.CurrentRoom.Items.Count==0))
                    {
                        //check if user input item is an item in the room
                        if(cc.Game.Player.CurrentRoom.Items.Exists( i => i.Name.ToUpper() == itemToTake.ToUpper())==true)
                        {
                            var itemRoom = cc.Game.Player.CurrentRoom.Items.First(i => i.Name.ToUpper() == itemToTake.ToUpper());
                        //if item is an item to be picked up then...
                            if(itemRoom is Item item)
                            {
                                //check if inventory has space 
                                //if it does not...
                                if(cc.Game.Player.Backpack.Count>4)
                                {
                                    OutputService.WriteLine($"{cc.Input} cannot be added. Inventory full." );
                                    return;
                                }
                                //if it does
                                // add to player backpack
                                else
                                {
                                    cc.Game.Player.Backpack.Add(itemRoom);
                                    //remove item in current room
                                    cc.Game.Player.CurrentRoom.Items.Remove(itemRoom);
                                    OutputService.WriteLine($"{cc.Input} was added to your inventory. You currently have {cc.Game.Player.Backpack.Count} items in your inventory.");
                                    return;
                                }
                            }
                            else
                            {
                                OutputService.WriteLine("No items can be taken in this room." );
                                    return;
                            }
                        }
                        //user input is not an item in the room
                        else
                        {
                            OutputService.WriteLine($"{cc.Input} is not an item that can be taken." );
                            return;
                        }
                    }
                    //no items are in the room
                    else
                    {
                        OutputService.WriteLine("No item can be taKen from this room." );
                        return;
                    }

                })),
                new Command("USE", new string[]{"USE"},  (cc) =>CheckForItemCommandWrapper(cc,()=>
                {
                    var itemToUse = cc.Input;

                    //check if inventory is empty
                     if (!(cc.Game.Player.Backpack.Count==0))
                    {
                        //check if user input item is an item in the inventory
                        if(cc.Game.Player.Backpack.Exists(i => i.Name.ToUpper() == itemToUse.ToUpper()))
                        {
                            var itemInventory = cc.Game.Player.Backpack.First(i => i.Name.ToUpper() == itemToUse.ToUpper());
                           //take item away from inventory
                            OutputService.WriteLine(itemInventory.Use);
                            switch (itemInventory.Name)
                            {
                                case "Bird":
                                    {
                                        OutputService.WriteLine("Bird flew away." );
                                        cc.Game.Player.Backpack.Remove(itemInventory);
                                        break;
                                    }
                                case "Blueberries":
                                    {
                                        OutputService.WriteLine("You gained 100 points!" );
                                        cc.Game.Score+=100;
                                        OutputService.WriteLine($"Your current score is {cc.Game.Score}" );
                                        Player.Backpack.Remove(itemInventory);
                                        break;
                                    }
                                default:
                                    return;
                            }
                        }
                         else
                        {
                            OutputService.WriteLine($"{cc.Input} is not an item in your inventory." );
                            return;
                        }
                    }
                      else
                    {
                        OutputService.WriteLine("No item in inventory." );
                        return;
                    }
                })),
                new Command("DROP", new string[]{"DROP"},  (cc) =>CheckForItemCommandWrapper(cc,()=>
                {
                    var itemToDrop = cc.Input;

                    //check if inventory is empty
                     if (!(cc.Game.Player.Backpack.Count==0))
                    {
                        //check if user input item is an item in the inventory
                        if(cc.Game.Player.Backpack.Exists(i => i.Name.ToUpper() == itemToDrop.ToUpper()))
                        {
                            var itemInventory = cc.Game.Player.Backpack.First(i => i.Name.ToUpper() == itemToDrop.ToUpper());
                           //take item away from inventory
                           cc.Game.Player.Backpack.Remove(itemInventory);
                            OutputService.WriteLine($"{cc.Input} was dropped from your inventory." );

                           //add item to current room
                           cc.Game.Player.CurrentRoom.Items.Add(itemInventory);
                            return;
                        }
                        else
                        {
                            OutputService.WriteLine($"{cc.Input} is not an item in your inventory." );
                            return;
                        }
                    }
                     else
                    {
                        OutputService.WriteLine("No item in inventory." );
                        return;
                    }
                })), new Command("INVENTORY", new string[]{"INVENTORY","ITEMS", "I"},  (cc) =>CheckForSingleCommandWrapper(cc, () =>
                {
                            OutputService.WriteLine($"Inventory contains: " );
                    if (!(cc.Game.Player.Backpack.All(i=>i==null)))
                    {
                            foreach (var item in cc.Game.Player.Backpack)
                            {
                                if(!(item==null))
                                {
                                    OutputService.WriteLine($"{item.Name}");
                                }
                                else
                                {
                                    continue;
                                }
                            }
                    }
                    else
                    {
                         OutputService.WriteLine("no items.");
                    }
                })),
            };

            Commands = commands
                .SelectMany(c => c.Verbs.Select(v => new { v, c }))
                .ToDictionary(vc => vc.v, vc => vc.c);
        }
        private void CheckForSingleCommandWrapper(CommandContext cc, Action onSuccess)
        {
            if (cc.Input.Length == 0)
            {
                onSuccess();
            }
            else
            {
                OutputService.WriteLine("Command not understood.");
            }
        }
        private void CheckForItemCommandWrapper(CommandContext cc, Action onSuccess)
        {
            if (cc.Input.Length == 0)
            {
                OutputService.WriteLine("Command not understood.");
            }
            else
            {
                onSuccess();
            }
        }
        [JsonProperty]
        public string WelcomeMessage { get; set; }

        [JsonProperty]
        public string InstructionsGuide { get; set; }
        [JsonProperty]
        public string StartWorldIntroduction { get; set; }
        private void Move(Directions direction)
        {
            if (Player.Move(direction) == false)
            {
                OutputService.WriteLine("The way is shut!");
            }
            else
            {
                DisplayRoomInfo();
            }
            Moves++;
        }
        public void Start()
        {
            OutputService.WriteLine(WelcomeMessage);
            OutputService.WriteLine(InstructionsGuide);
            OutputService.WriteLine(StartWorldIntroduction);

            DisplayRoomInfo();

        }
        private void DisplayRoomInfo()
        {
            OutputService.WriteLine(Player.CurrentRoom.Name);
            OutputService.WriteLine(Player.CurrentRoom.Description);
            if(!(Player.CurrentRoom.Items==null))
            {
                foreach (var item in Player.CurrentRoom.Items)
                {
                    OutputService.WriteLine(item.Description);
                }
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Player = new Player(World, World.StartingLocation);
        }
        /// <summary>
        /// Deserilize into game object from json file
        /// Set player in game, starting location
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>set game object</returns>
        public static Game Load(string filename)
        {
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));

            game.Player = game.World.SpawnPlayer();
            
            return game;
        }

    }
}