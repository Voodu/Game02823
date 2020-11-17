using System.Collections.Generic;
using System.Linq;
using Quests.Enums;
using Quests.Items;

namespace Quests
{
    public class QuestManager : Singleton<QuestManager>
    {
        public SortedDictionary<string, Quest> Quests { get; set; } = new SortedDictionary<string, Quest>();
        public readonly Dictionary<string, ObjectiveItem> objectiveItems = new Dictionary<string, ObjectiveItem>();
        public List<Quest> VisibleQuests => Quests
                                            .Where(q => q.Value.Status != QuestStatus.NotStarted)
                                            .Select(x => x.Value)
                                            .ToList();

        public Quest this[string questId] => Quests[questId];

        public void Register(Quest quest)
        {
            Quests.Add(quest.Id, quest);
        }

        public void Begin(string questId)
        {
            Quests[questId].Status = QuestStatus.NotCompleted;
            Quests[questId].Continue();
        }
        
        public void Complete(string questId)
        {
            Quests[questId].Status = QuestStatus.Completed;
            print($"Quest {Quests[questId].Title} completed");
        }

        public void Fail(string questId)
        {
            Quests[questId].Status = QuestStatus.Failed;
        }
    }
}