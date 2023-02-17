using System.Collections.Generic;

namespace Zork
{
    public class Player
    {
        private readonly World World;
        public Room CurrentRoom { get; set; }

        //public Inventory Inventory { get; set; }
        public List<Item> Backpack { get; set; }

        public Player(World world, string startingLocation)
        {
            World = world;

            CurrentRoom = World.RoomsByName[startingLocation];

            Backpack = new List<Item>(); 
        }

        public bool Move(Directions directions)
        {
            bool isValidMove = CurrentRoom.Neighbors.TryGetValue(directions, out Room room);
            if(isValidMove)
            {
                CurrentRoom = room;
            }
            return isValidMove;
        }

    }
}