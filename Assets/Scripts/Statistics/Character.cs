using System;

namespace Statistics {
    [Serializable]
    public class Character
    {
        public string    Name      { get; private set; }
        public Attribute Health    { get; private set; }
        public Attribute Stamina   { get; private set; }
        public Attribute Strength  { get; private set; }
        public Attribute Speed     { get; private set; }
        public Inventory Inventory { get; private set; }

        public Character(string name)
        {
            Name      = name;
            Health    = new Attribute(100);
            Stamina   = new Attribute(100);
            Strength  = new Attribute(1);
            Speed     = new Attribute(1);
            Inventory = new Inventory();
        }

        public GearItem GetItemFromInventory(GearType type)
        {
            foreach (GearItem item in Inventory.Items)
            {
                if (item.Type == type)
                    return item;
            }
            return null;
        }

        public void EquipItem(GearItem item)
        {
            switch (item.Type)
            {
                case GearType.Helmet:
                    if (!Inventory.Helmet.Occupied)
                    {
                        item.Bonus.Apply(this, item);
                        Inventory.Helmet.Occupied = true;
                    }
                    break;
                case GearType.Armor:
                    if (!Inventory.Chest.Occupied)
                    {
                        item.Bonus.Apply(this, item);
                        Inventory.Chest.Occupied = true;
                    };
                    break;
                case GearType.Gloves:
                    if (!Inventory.Gloves.Occupied)
                    {
                        item.Bonus.Apply(this, item);
                        Inventory.Gloves.Occupied = true;
                    };
                    break;
                case GearType.Boots:
                    if (!Inventory.Boots.Occupied)
                    {
                        item.Bonus.Apply(this, item);
                        Inventory.Boots.Occupied = true;
                    };
                    break;

                case GearType.Weapon:
                    if (!Inventory.Weapon.Occupied)
                    {
                        item.Bonus.Apply(this, item);
                        Inventory.Weapon.Occupied = true;
                    };
                    break;
                case GearType.Accessory:
                    if (!Inventory.Accessory.Occupied)
                    {
                        item.Bonus.Apply(this, item);
                        Inventory.Accessory.Occupied = true;
                    };
                    break;
                case GearType.Bag:
                    if (!Inventory.Bag.Occupied)
                    {
                        item.Bonus.Apply(this, item);
                        Inventory.Bag.Occupied = true;
                    };
                    break;
            }
        }

        public void Unequip(GearItem item)
        {
            switch (item.Type)
            {
                case GearType.Helmet:
                    item.Bonus.Remove(this, item);
                    Inventory.Helmet.Occupied = false;
                    break;
                case GearType.Armor:
                    item.Bonus.Remove(this, item);
                    Inventory.Chest.Occupied = false;
                    break;
                case GearType.Gloves:
                    item.Bonus.Remove(this, item);
                    Inventory.Gloves.Occupied = false;
                    break;
                case GearType.Boots:
                    item.Bonus.Remove(this, item);
                    Inventory.Boots.Occupied = false;
                    break;
                case GearType.Weapon:
                    item.Bonus.Remove(this, item);
                    Inventory.Weapon.Occupied = false;
                    break;
                case GearType.Accessory:
                    item.Bonus.Remove(this, item);
                    Inventory.Accessory.Occupied = false;
                    break;
                case GearType.Bag:
                    item.Bonus.Remove(this, item);
                    Inventory.Bag.Occupied = false;
                    break;
            }
        }
    }
}
