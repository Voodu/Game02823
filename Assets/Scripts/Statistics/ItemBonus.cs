using System;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("Type")]
        public BonusType type;

        [FormerlySerializedAs("Scaler")]
        public BonusScaler scaler;

        [FormerlySerializedAs("Value")]
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
                        c.Health.Add(new AttributeModifier(item.bonus.value, item.bonus.scaler, item));
                        break;
                    case BonusType.Stamina:
                        c.Stamina.Add(new AttributeModifier(item.bonus.value, item.bonus.scaler, item));
                        break;
                    case BonusType.Strength:
                        c.Strength.Add(new AttributeModifier(item.bonus.value, item.bonus.scaler, item));
                        break;
                    case BonusType.Speed:
                        c.Speed.Add(new AttributeModifier(item.bonus.value, item.bonus.scaler, item));
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