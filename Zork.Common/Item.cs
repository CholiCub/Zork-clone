namespace Zork
{
    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public string Use { get; }

        public Item(string name, string description, string use)
        {
            Name = name;
            Description = description;
            Use = use;
        }
    }
}

