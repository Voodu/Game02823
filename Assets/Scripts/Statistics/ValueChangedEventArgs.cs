namespace Statistics
{
    public class ValueChangedEventArgs
    {
        public readonly float newValue;

        public ValueChangedEventArgs(float newValue)
        {
            this.newValue = newValue;
        }
    }
}