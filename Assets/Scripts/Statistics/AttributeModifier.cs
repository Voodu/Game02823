namespace Statistics
{
    public class AttributeModifier
    {
        public readonly float       Value;
        public readonly BonusScaler Scaler;
        public readonly int         Order;
        public readonly object      Source;

        public AttributeModifier(float value, BonusScaler scaler, int order, object source)
        {
            Value  = value;
            Scaler = scaler;
            Order  = order;
            Source = source;
        }

        public AttributeModifier(float value, BonusScaler scaler) : this(value, scaler, (int) scaler, null) { }

        public AttributeModifier(float value, BonusScaler scaler, int order) : this(value, scaler, order, null) { }

        public AttributeModifier(float value, BonusScaler scaler, object source) : this(value, scaler, (int) scaler, source) { }
    }
}