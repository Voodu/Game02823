using System;
using UnityEngine.Serialization;

namespace Statistics
{
    public enum GearType
    {
        Helmet,
        Armor,
        Gloves,
        Boots,
        Weapon,
        Accessory,
        Bag
    }

    [Serializable]
    public class GearItem : Item
    {
        [FormerlySerializedAs("Type")]
        public GearType type;

        [FormerlySerializedAs("Bonus")]
        public ItemBonus bonus;

        public GearItem(GearType type, ItemBonus bonus)
        {
            this.type  = type;
            this.bonus = bonus;
        }

        public GearItem(GearType type) : this(type, null) { }
    }
}