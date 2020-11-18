using System.Collections.Generic;
using System.Linq;
using Quests.Enums;
using UnityEngine;

namespace Quests
{
    public class QuestManager : Singleton<QuestManager>
    {
        [SerializeField]
        private List<Quest> quests = new List<Quest>();

        public Quest this[string questId] => quests.First(x => x.id == questId);

        public List<Quest> VisibleQuests =>
            quests
                .Where(q => q.status != QuestStatus.NotStarted)
                .ToList();

        public void Register(Quest quest)
        {
            quests.AddWithId(quest);
        }

        public void Begin(string questId)
        {
            var quest = this[questId];
            if (quest.status == QuestStatus.NotStarted)
            {
                quest.status = QuestStatus.NotCompleted;
                quest.Continue();
            }
        }

        public void Complete(string questId)
        {
            var quest = this[questId];
            if (quest.status == QuestStatus.NotCompleted)
            {
                quest.status = QuestStatus.Completed;
                print($"Quest {quest.title} completed");
            }
        }

        public void Fail(string questId)
        {
            var quest = this[questId];
            if (quest.status == QuestStatus.NotCompleted)
            {
                quest.status = QuestStatus.Failed;
                print($"Quest {quest.title} failed");
            }
        }
    }
}