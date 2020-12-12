using System;
using System.Collections.Generic;

namespace Statistics
{
    [Serializable]
    public class Inventory
    {
        public List<Item> Items = new List<Item>();
        public int        Size  = 10;

        public ItemSlot Helmet;
        public ItemSlot Chest;
        public ItemSlot Gloves;
        public ItemSlot Boots;
        public ItemSlot Weapon;
        public ItemSlot Accessory;
        public ItemSlot Bag;

        public bool AddItem(Item item)
        {
            if (IsFull())
            {
                return false;
            }

            Items.Add(item);
            return true;
        }

        public bool RemoveItem(Item item)
        {
            if (Items.Remove(item))
            {
                return true;
            }

            return false;
        }

        public bool IsFull()
        {
            return Items.Count >= Size;
        }

        public void IncreaseSize(int amount)
        {
            Size = Size + amount;
        }

        public void DecreaseSize(int amount)
        {
            Size = Size - amount;
        }
    }
}