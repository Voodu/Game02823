namespace Quests.EventArgs
{
    public class NewObjectiveEventArgs : System.EventArgs
    {
        public string    Id        { get; set; }
        public Objective Objective { get; set; }

        public NewObjectiveEventArgs(Objective objective)
        {
            Id        = objective.id;
            Objective = objective;
        }
    }
}