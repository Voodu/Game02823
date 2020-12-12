using System.Collections.Generic;

public class Inventory
{
    public List<Item> Items { get; private set; }
    public int Size { get; private set; }

    public ItemSlot Helmet { get; private set; }
    public ItemSlot Chest { get; private set; }
    public ItemSlot Gloves { get; private set; }
    public ItemSlot Boots { get; private set; }
    public ItemSlot Weapon { get; private set; }
    public ItemSlot Accessory { get; private set; }
    public ItemSlot Bag { get; private set; }

    public Inventory()
    {
        Items = new List<Item>();
        Helmet = new ItemSlot();
        Size = 10;
    }

    public bool AddItem(Item item)
    {
        if (IsFull()) return false;
        Items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if (Items.Remove(item)) return true;
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