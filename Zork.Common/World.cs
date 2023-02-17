using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Zork
{
    public class World
    {
        public List<Room> Rooms { get; set; }
        [JsonIgnore]
        public IReadOnlyDictionary<string, Room> RoomsByName { get; set; }
        public string StartingLocation { get; set; }
        public Player SpawnPlayer() => new Player(this, StartingLocation);

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            RoomsByName = Rooms.ToDictionary(room => room.Name);
            
            var first = Rooms.First();
            first.UpdateNeighbors(this);
            var second = Rooms.Skip(1).First();
            second.UpdateNeighbors(this);

            foreach(var room in Rooms)
            {
                room.UpdateNeighbors(this);
            }
        }
    }
}