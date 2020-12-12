using Inventory;

namespace Statistics
{
    public class AttributeModifier
    {
        public readonly float       value;
        public readonly BonusScaler scaler;
        public readonly int         order;
        public readonly object      source;

        public AttributeModifier(float value, BonusScaler scaler, int order, object source)
        {
            this.value  = value;
            this.scaler = scaler;
            this.order  = order;
            this.source = source;
        }

        public AttributeModifier(float value, BonusScaler scaler) : this(value, scaler, (int) scaler, null) { }

        public AttributeModifier(float value, BonusScaler scaler, int order) : this(value, scaler, order, null) { }

        public AttributeModifier(float value, BonusScaler scaler, object source) : this(value, scaler, (int) scaler, source) { }
    }
}