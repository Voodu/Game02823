using System;
using System.Collections.Generic;
using Dialogues;
using Ink.Runtime;
using Quests.EventArgs;
using UnityEngine;

namespace Quests
{
    public class InkQuest
    {
        private readonly Story  inkQuest;
        private          string stateBackup;
        private          bool   error;
        private          string errorMessage;

        public IReadOnlyList<string> GlobalTags => inkQuest.globalTags;

        public InkQuest(string storyJson)
        {
            inkQuest = new Story(storyJson);
            inkQuest.onError += (message, type) =>
                                {
                                    error        = true;
                                    errorMessage = message;
                                };
        }

        public event EventHandler<ObjectiveEventArgs> NewObjective;
        public event EventHandler                     QuestFinished;

        public object Variables(string key)
        {
            return inkQuest.variablesState[key];
        }

        public void Variables(string key, object value)
        {
            inkQuest.variablesState[key] = value;
        }

        public string Continue()
        {
            Variables("continue", true);
            if (inkQuest.canContinue)
            {
                var storyLine = SafeContinue();
                ParseTags(inkQuest.currentTags);
                return storyLine;
            }

            throw new Exception("Cannot continue. Story probably finished.");
        }

        private void OnNewObjective(Objective objective)
        {
            NewObjective?.Invoke(this, new ObjectiveEventArgs(objective));
        }

        private void OnQuestFinished()
        {
            QuestFinished?.Invoke(this, null);
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
                        OnNewObjective(Objective.CreateObjective(tags[++i], tags[++i], tags[++i], tags[++i]));
                        continue;
                    case "finish":
                        OnQuestFinished();
                        continue;
                    case "dialogue":
                        DialogueManager.Instance[tags[++i]].Continue();
                        continue;
                    case "dummy": // Workaround for "out of content" problem
                        continue;
                    default:
                        MonoBehaviour.print($"Unrecognized tag: {tags[i]} in quest.");
                        break;
                }
            }
        }

        private string SafeContinue()
        {
            stateBackup = inkQuest.state.ToJson();
            error       = false;
            var story = inkQuest.Continue();
            if (error)
            {
                inkQuest.state.LoadJson(stateBackup);
                throw new Exception(errorMessage);
            }

            error = false;
            return story;
        }
    }
}