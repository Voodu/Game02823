namespace Statistics {
    public class ItemSlot
    {
        public Item Item     { get; set; }
        public bool Occupied { get; set; }

        public ItemSlot(Item item)
        {
            Item     = item;
            Occupied = false;
        }

        public ItemSlot() : this(null) { }
    }
}

