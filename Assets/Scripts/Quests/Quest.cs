using System;
using System.Collections.Generic;
using Ink.Runtime;
using Quests.Enums;
using UnityEngine;

namespace Quests
{
    public class Quest : MonoBehaviour
    {
        private string stateBackup;

        private bool error = false;

        // Story parser?
        public Objective this[string objectiveId] => Objectives[objectiveId];

        public string Id { get; private set; }
        public string Title { get; private set; }
        public List<string> JournalEntries { get; set; } = new List<string>();
        public SortedDictionary<string, Objective> Objectives { get; set; } = new SortedDictionary<string, Objective>();
        public QuestStatus Status { get; set; } = QuestStatus.NotStarted;

        [SerializeField]
        private TextAsset inkTextAsset;
        private Story InkQuest;

        public void Awake()
        {
            InkQuest = new Story(inkTextAsset.text);
            InkQuest.onError += (message, type) =>
                                {
                                    print(message);
                                    print(type);
                                    error = true;
                                };


            var globalTags = InkQuest.globalTags;
            if (globalTags[0] != "quest")
            {
                Debug.LogError("No quest story found in text asset. Are you missing the #quest tag?");
            }

            Id = globalTags[1];
            Title = globalTags[2];

            QuestManager.Instance.Register(this);
        }

        public void Continue()
        {
            InkQuest.variablesState["continue"] = true;
            //JournalEntries.Add(new JournalEntry {Description = SafeContinue(InkQuest)});
            if (InkQuest.canContinue)
            {
                JournalEntries.Add(SafeContinue(InkQuest));
                ParseTags(InkQuest.currentTags);
            }
            else
            {
                Debug.LogWarning("Trying to continue finished quest story");
            }
        }

        public void ParseTags(List<string> tags)
        {
            for (var i = 0; i < tags.Count; i++)
            {
                switch (tags[i])
                {
                    case "quest":
                        i += 2;
                        continue;
                    case "objective":
                        AddObjective(tags[++i], tags[++i], tags[++i], tags[++i]);
                        continue;
                    case "finish":
                        QuestManager.Instance.Complete(Id);
                        continue;
                    case "dummy": // Workaround for "out of content" problem
                        continue;
                    default:
                        print($"Unrecognized tag: {tags[i]} in quest {Title}");
                        break;
                }
            }
        }

        private void AddObjective(string id, string description, string type, string count)
        {
            Objectives.Add(id, new Objective
            {
                ConnectedQuestId = Id,
                Id = id,
                Description = description,
                Type = GetObjectiveType(type),
                Status = ObjectiveStatus.NotCompleted,
                Progress = (0, int.Parse(count))
            });

            print("New objective!");
        }

        private ObjectiveType GetObjectiveType(string typeName)
        {
            switch (typeName)
            {
                case "none":
                    return ObjectiveType.None;
                case "kill":
                    return ObjectiveType.Kill;
                case "gather":
                    return ObjectiveType.Gather;
                case "deliver":
                    return ObjectiveType.Deliver;
                case "escort":
                    return ObjectiveType.Escort;
                case "visit":
                    return ObjectiveType.Visit;
                case "talk":
                    return ObjectiveType.Talk;
                case "other":
                    return ObjectiveType.Other;
                default:
                    return ObjectiveType.None;
            }
        }

        private string SafeContinue(Story inkStory)
        {
            stateBackup = inkStory.state.ToJson();

            var r = inkStory.Continue();
            if (error)
            {
                error = false;
                inkStory.state.LoadJson(stateBackup);
                return string.Empty;
            }

            error = false;
            return r;
        }
    }
}