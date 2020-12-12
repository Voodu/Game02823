using System;
using UnityEngine;

namespace Statistics
{
    public enum Tier
    {
        Poor,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [Serializable]
    public class Item : MonoBehaviour
    {
        public string Name;
        public Tier   Tier;

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