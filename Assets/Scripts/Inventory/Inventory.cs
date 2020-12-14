using System;
using System.Collections.Generic;

namespace Inventory
{
    [Serializable]
    public class Inventory
    {
        public List<Item> items = new List<Item>();
        public int        size  = 10;

        public ItemSlot helmet;
        public ItemSlot armor;
        public ItemSlot gloves;
        public ItemSlot boots;
        public ItemSlot weapon;
        public ItemSlot accessory;
        public ItemSlot bag;

        public bool AddItem(Item item)
        {
            if (IsFull())
            {
                return false;
            }

            items.Add(item);
            return true;
        }

        public bool RemoveItem(Item item)
        {
            return items.Remove(item);
        }

        public bool IsFull()
        {
            return items.Count >= size;
        }

        public void IncreaseSize(int amount)
        {
            size += amount;
        }

        public void DecreaseSize(int amount)
        {
            size -= amount;
        }
    }
}