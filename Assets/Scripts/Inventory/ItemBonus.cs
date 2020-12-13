using System;
using Statistics;

namespace Inventory
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
        public BonusType type;

        public BonusScaler scaler;

        public float value;

        public ItemBonus(BonusType type, BonusScaler scaler, float value)
        {
            this.type   = type;
            this.scaler = scaler;
            this.value  = value;
        }

        public void Apply(Character c, GearItem item)
        {
            if (item.bonus != null)
            {
                switch (item.bonus.type)
                {
                    case BonusType.Health:
                        c.health.Add(new AttributeModifier(item.bonus.value, item.bonus.scaler, item));
                        break;
                    case BonusType.Stamina:
                        c.stamina.Add(new AttributeModifier(item.bonus.value, item.bonus.scaler, item));
                        break;
                    case BonusType.Strength:
                        c.strength.Add(new AttributeModifier(item.bonus.value, item.bonus.scaler, item));
                        break;
                    case BonusType.Speed:
                        c.speed.Add(new AttributeModifier(item.bonus.value, item.bonus.scaler, item));
                        break;
                }
            }
        }

        public void Remove(Character c, GearItem item)
        {
            c.strength.RemoveAllFromSource(item);
            c.health.RemoveAllFromSource(item);
            c.stamina.RemoveAllFromSource(item);
            c.speed.RemoveAllFromSource(item);
        }
    }
}