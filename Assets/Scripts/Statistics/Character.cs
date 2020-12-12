using System;

namespace Statistics
{
    [Serializable]
    public class Character
    {
        public string    Name;
        public Attribute Health;
        public Attribute Stamina;
        public Attribute Strength;
        public Attribute Speed;
        public Inventory Inventory;

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
                if (item.type == type)
                {
                    return item;
                }
            }

            return null;
        }

        public bool EquipItem(GearItem item)
        {
            switch (item.type)
            {
                case GearType.Helmet:
                    if (!Inventory.Helmet.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        Inventory.Helmet.Occupied = true;
                        return true;
                    }

                    break;
                case GearType.Armor:
                    if (!Inventory.Chest.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        Inventory.Chest.Occupied = true;
                        return true;
                    }

                    ;
                    break;
                case GearType.Gloves:
                    if (!Inventory.Gloves.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        Inventory.Gloves.Occupied = true;
                        return true;
                    }

                    ;
                    break;
                case GearType.Boots:
                    if (!Inventory.Boots.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        Inventory.Boots.Occupied = true;
                        return true;
                    }

                    ;
                    break;

                case GearType.Weapon:
                    if (!Inventory.Weapon.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        Inventory.Weapon.Occupied = true;
                        return true;
                    }

                    ;
                    break;
                case GearType.Accessory:
                    if (!Inventory.Accessory.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        Inventory.Accessory.Occupied = true;
                        return true;
                    }

                    ;
                    break;
                case GearType.Bag:
                    if (!Inventory.Bag.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        Inventory.Bag.Occupied = true;
                        return true;
                    }

                    ;
                    break;
            }

            return false;
        }

        public void Unequip(GearItem item)
        {
            switch (item.type)
            {
                case GearType.Helmet:
                    item.bonus.Remove(this, item);
                    Inventory.Helmet.Occupied = false;
                    break;
                case GearType.Armor:
                    item.bonus.Remove(this, item);
                    Inventory.Chest.Occupied = false;
                    break;
                case GearType.Gloves:
                    item.bonus.Remove(this, item);
                    Inventory.Gloves.Occupied = false;
                    break;
                case GearType.Boots:
                    item.bonus.Remove(this, item);
                    Inventory.Boots.Occupied = false;
                    break;
                case GearType.Weapon:
                    item.bonus.Remove(this, item);
                    Inventory.Weapon.Occupied = false;
                    break;
                case GearType.Accessory:
                    item.bonus.Remove(this, item);
                    Inventory.Accessory.Occupied = false;
                    break;
                case GearType.Bag:
                    item.bonus.Remove(this, item);
                    Inventory.Bag.Occupied = false;
                    break;
            }
        }
    }
}