public enum GearType
{
    Helmet, Armor, Gloves, Boots, Weapon, Accessory, Bag
}

public class GearItem : Item
{
    public GearType Type { get; private set; }
    public ItemBonus Bonus { get; private set; }

    public GearItem(GearType type, ItemBonus bonus)
    {
        Type = type;
        Bonus = bonus;
    }

    public GearItem(GearType type) : this(type, null) { }
}