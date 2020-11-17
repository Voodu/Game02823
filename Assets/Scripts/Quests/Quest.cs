using System;
using System.Collections.Generic;
using System.Linq;
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

        private void OnNewObjective(object sender, NewObjectiveEventArgs args)
        {
            args.Objective.connectedQuestId = id;
            objectives.AddWithId(args.Objective);
            print($"New objective! Quest: {title}. Objective: {args.Objective.description}");
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