using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zork
{
    public class Room
    {
        public string Name { get; }
        public string Description { get; private set; }
        public List<Item> Items { get; set; }
        
        [JsonIgnore]
        public IReadOnlyDictionary<Directions, Room> Neighbors { get; private set; }

        [JsonProperty(PropertyName ="Neighbors")]
        public Dictionary<Directions, string> NeighborName { get; set; }
        public Room(string name, string description, List<Item> items)
        {
            Name = name;
            Description = description;
            Items = items;
        }
        public void UpdateNeighbors(World world)
        {
            Dictionary<Directions, Room> temp = new Dictionary<Directions, Room>();
            foreach (var directionsAndNames in NeighborName)
            {
                Room room = world.RoomsByName[directionsAndNames.Value];
                temp.Add(directionsAndNames.Key, room);
            }
            
            Neighbors = temp;

        }
        public override string ToString() => Name;
    }
}
