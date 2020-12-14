using System.Collections.Generic;
using System.Linq;
using Common;
using Other;
using Quests.Enums;
using UnityEngine;

namespace Quests
{
    public class QuestManager : Singleton<QuestManager>
    {
        private readonly Dictionary<string, string> questStates = new Dictionary<string, string>();

        [SerializeField]
        private List<Quest> sceneQuests = new List<Quest>();

        public Quest this[string questId] => sceneQuests.First(x => x.id == questId);

        public List<Quest> VisibleQuests =>
            sceneQuests
                .Where(q => q.status != QuestStatus.NotStarted)
                .ToList();

        private void Start()
        {
            SceneManager.Instance.OnSceneUnloaded(_ => sceneQuests.Clear());
        }

        public void Register(Quest quest)
        {
            if (questStates.ContainsKey(quest.id))
            {
                quest.LoadFromJson(questStates[quest.id]);
            }
            else
            {
                sceneQuests.AddWithId(quest);
            }
        }

        public void SaveQuest(Quest quest)
        {
            if (questStates.ContainsKey(quest.id))
            {
                questStates[quest.id] = quest.GetStateJson();
            }
            else
            {
                questStates.Add(quest.id, quest.GetStateJson());
            }
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