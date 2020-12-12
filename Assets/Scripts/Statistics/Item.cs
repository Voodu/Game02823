namespace Statistics {
    public enum Tier
    {
        Poor, Common, Uncommon, Rare, Epic, Legendary
    }

    public class Item
    {
        public string Name { get; private set; }
        public Tier   Tier { get; private set; }

        public Item(string name, Tier tier)
        {
            Name = name;
            Tier = tier;
        }

        public Item(string name) : this(name, Tier.Poor) { }
        public Item() : this("", Tier.Poor) { }

        public string TierToColor(Tier tier)
        {
            switch (tier)
            {
                case Tier.Poor:
                    return "Gray";
                case Tier.Common:
                    return "White";
                case Tier.Uncommon:
                    return "Green";
                case Tier.Rare:
                    return "Blue";
                case Tier.Epic:
                    return "Purple";
                case Tier.Legendary:
                    return "Orange";
                default:
                    return "";
            }
        }
    }
}