using System;

namespace Statistics
{
    public enum BonusScaler
    {
        Flat        = 100,
        PercentAdd  = 200,
        PercentMult = 300
    }

    public enum BonusType
    {
        Health,
        Stamina,
        Strength,
        Speed,
        None
    }

    [Serializable]
    public class ItemBonus
    {
        public BonusType Type;

        public BonusScaler Scaler;

        public float Value;

        public ItemBonus(BonusType type, BonusScaler scaler, float value)
        {
            Type   = type;
            Scaler = scaler;
            Value  = value;
        }

        public void Apply(Character c, GearItem item)
        {
            if (item.bonus != null)
            {
                switch (item.bonus.Type)
                {
                    case BonusType.Health:
                        c.Health.Add(new AttributeModifier(item.bonus.Value, item.bonus.Scaler, item));
                        break;
                    case BonusType.Stamina:
                        c.Stamina.Add(new AttributeModifier(item.bonus.Value, item.bonus.Scaler, item));
                        break;
                    case BonusType.Strength:
                        c.Strength.Add(new AttributeModifier(item.bonus.Value, item.bonus.Scaler, item));
                        break;
                    case BonusType.Speed:
                        c.Speed.Add(new AttributeModifier(item.bonus.Value, item.bonus.Scaler, item));
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