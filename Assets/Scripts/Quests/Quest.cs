using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Quests.Enums;
using Quests.EventArgs;
using UnityEngine;

namespace Quests
{
    [Serializable]
    public class Quest : MonoBehaviour
    {
        [SerializeField]
        private TextAsset inkTextAsset;

        private InkQuest inkQuest;

        public string          id;
        public string          title;
        public List<string>    journalEntries = new List<string>();
        public List<Objective> objectives     = new List<Objective>();
        public QuestStatus     status         = QuestStatus.NotStarted;

        public Objective this[string objectiveId] => objectives.First(x => x.id == objectiveId);

        public List<Objective> ActiveObjectives =>
            objectives
                .Where(o => o.status == ObjectiveStatus.NotCompleted)
                .ToList();

        public void Awake()
        {
            inkQuest               =  new InkQuest(inkTextAsset.text);
            inkQuest.NewObjective  += OnNewObjective;
            inkQuest.QuestFinished += OnQuestFinished;

            var globalTags = inkQuest.GlobalTags;
            if ((globalTags.Count != 3) || (globalTags[0] != "quest"))
            {
                Debug.LogError("Incorrect story tags in text asset. Should be #quest #quest_id #quest name.");
            }

            id    = globalTags[1];
            title = globalTags[2];

            QuestManager.Instance.Register(this);
        }

        private void OnQuestFinished(object sender, System.EventArgs e)
        {
            QuestManager.Instance.Complete(id);
        }

        private void OnNewObjective(object sender, ObjectiveEventArgs e)
        {
            e.Objective.connectedQuestId =  id;
            e.Objective.Completed        += OnObjectiveCompleted;
            objectives.AddWithId(e.Objective);
            print($"New objective! Quest: {title}. Objective: {e.Objective.description}");
        }

        private void OnObjectiveCompleted(object sender, ObjectiveEventArgs e)
        {
            print($"Objective completed! Quest: {title}. Objective: {e.Objective.description}");
            if (objectives.All(x => x.status == ObjectiveStatus.Completed))
            {
                Continue();
            }
        }

        public void Continue()
        {
            try
            {
                journalEntries.Add(inkQuest.Continue());
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }
}