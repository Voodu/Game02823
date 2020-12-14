using System;
using System.Linq;
using Inventory;

namespace Statistics
{
    [Serializable]
    public class Character
    {
        public string              name;
        public Attribute           health;
        public Attribute           stamina;
        public Attribute           strength;
        public Attribute           speed;
        public Inventory.Inventory inventory;

        public Character(string name)
        {
            this.name = name;
            health    = new Attribute(100);
            stamina   = new Attribute(100);
            strength  = new Attribute(1);
            speed     = new Attribute(1);
            inventory = new Inventory.Inventory();
        }

        public GearItem GetItemFromInventory(GearType type)
        {
            return inventory.items.Where(x => x is GearItem).Cast<GearItem>().FirstOrDefault(item => item.type == type);
        }

        public bool EquipItem(GearItem item)
        {
            switch (item.type)
            {
                case GearType.Helmet:
                    if (!inventory.helmet.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        inventory.helmet.Occupied = true;
                        inventory.helmet.Item     = item;
                        return true;
                    }

                    break;
                case GearType.Armor:
                    if (!inventory.armor.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        inventory.armor.Occupied = true;
                        inventory.armor.Item     = item;
                        return true;
                    }

                    ;
                    break;
                case GearType.Gloves:
                    if (!inventory.gloves.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        inventory.gloves.Occupied = true;
                        inventory.gloves.Item     = item;
                        return true;
                    }

                    ;
                    break;
                case GearType.Boots:
                    if (!inventory.boots.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        inventory.boots.Occupied = true;
                        inventory.boots.Item     = item;
                        return true;
                    }

                    ;
                    break;

                case GearType.Weapon:
                    if (!inventory.weapon.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        inventory.weapon.Occupied = true;
                        inventory.weapon.Item     = item;
                        return true;
                    }

                    ;
                    break;
                case GearType.Accessory:
                    if (!inventory.accessory.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        inventory.accessory.Occupied = true;
                        inventory.accessory.Item     = item;
                        return true;
                    }

                    ;
                    break;
                case GearType.Bag:
                    if (!inventory.bag.Occupied)
                    {
                        item.bonus.Apply(this, item);
                        inventory.bag.Occupied = true;
                        inventory.bag.Item     = item;
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
                    inventory.helmet.Occupied = false;
                    break;
                case GearType.Armor:
                    item.bonus.Remove(this, item);
                    inventory.armor.Occupied = false;
                    break;
                case GearType.Gloves:
                    item.bonus.Remove(this, item);
                    inventory.gloves.Occupied = false;
                    break;
                case GearType.Boots:
                    item.bonus.Remove(this, item);
                    inventory.boots.Occupied = false;
                    break;
                case GearType.Weapon:
                    item.bonus.Remove(this, item);
                    inventory.weapon.Occupied = false;
                    break;
                case GearType.Accessory:
                    item.bonus.Remove(this, item);
                    inventory.accessory.Occupied = false;
                    break;
                case GearType.Bag:
                    item.bonus.Remove(this, item);
                    inventory.bag.Occupied = false;
                    break;
            }
        }
    }
}