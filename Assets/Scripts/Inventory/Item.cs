using System;
using UnityEngine;

namespace Inventory
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
        public string itemName;
        public Tier   tier;

        public Item(string itemName, Tier tier)
        {
            this.itemName = itemName;
            this.tier     = tier;
        }

        public Item(string itemName) : this(itemName, Tier.Poor) { }
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