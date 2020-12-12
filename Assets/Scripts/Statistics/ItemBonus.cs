namespace Statistics {
    public enum BonusScaler
    {
        Flat        = 100,
        PercentAdd  = 200,
        PercentMult = 300,
    }

    public enum BonusType
    {
        Health, Stamina, Strength, Speed, None
    }

    public class ItemBonus
    {
        public BonusType   Type   { get; private set; }
        public BonusScaler Scaler { get; private set; }
        public float       Value  { get; private set; }

        public ItemBonus(BonusType type, BonusScaler scaler, float value)
        {
            Type   = type;
            Scaler = scaler;
            Value  = value;
        }

        public void Apply(Character c, GearItem item)
        {
            if (item.Bonus != null)
            {
                switch (item.Bonus.Type)
                {
                    case BonusType.Health:
                        c.Health.Add(new AttributeModifier(item.Bonus.Value, item.Bonus.Scaler, item));
                        break;
                    case BonusType.Stamina:
                        c.Stamina.Add(new AttributeModifier(item.Bonus.Value, item.Bonus.Scaler, item));
                        break;
                    case BonusType.Strength:
                        c.Strength.Add(new AttributeModifier(item.Bonus.Value, item.Bonus.Scaler, item));
                        break;
                    case BonusType.Speed:
                        c.Speed.Add(new AttributeModifier(item.Bonus.Value, item.Bonus.Scaler, item));
                        break;
                }
            }
        }

        public void Remove(Character c, GearItem item)
        {
            c.Strength.RemoveAllFromSource(item);
            c.Health.RemoveAllFromSource(item);
            c.Stamina.RemoveAllFromSource(item);
            c.Speed.RemoveAllFromSource(item);
        }
    }
}