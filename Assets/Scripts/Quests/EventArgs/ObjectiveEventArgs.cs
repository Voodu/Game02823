namespace Quests.EventArgs
{
    public class ObjectiveEventArgs : System.EventArgs
    {
        public Objective Objective { get; set; }

        public ObjectiveEventArgs(Objective objective)
        {
            Objective = objective;
        }
    }
}