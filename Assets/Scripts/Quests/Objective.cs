using System;
using Quests.Enums;
using Quests.EventArgs;
using UnityEngine;

namespace Quests
{
    [Serializable]
    public class Objective
    {
        public string                                 id;
        public string                                 connectedQuestId;
        public string                                 description;
        public ObjectiveType                          type   = ObjectiveType.None;
        public ObjectiveStatus                        status = ObjectiveStatus.NotStarted;
        public int                                    currentProgress, fullProgress;
        public event EventHandler<ObjectiveEventArgs> Completed;

        private void OnCompleted(Objective objective)
        {
            Completed?.Invoke(this, new ObjectiveEventArgs(objective));
        }

        public void RecordProgress(ObjectiveItem item)
        {
            switch (item.objectiveType)
            {
                case ObjectiveType.Kill:
                    currentProgress++;
                    break;
                case ObjectiveType.Gather:
                    currentProgress++;
                    break;
                case ObjectiveType.Deliver:
                    currentProgress++;
                    break;
                case ObjectiveType.Escort:
                    currentProgress++;
                    break;
                case ObjectiveType.Visit:
                    currentProgress++;
                    break;
                case ObjectiveType.Talk:
                    currentProgress++;
                    break;
                case ObjectiveType.Other:
                    currentProgress++;
                    break;
                case ObjectiveType.None:
                    Debug.LogWarning($"No objective type set on {item.gameObject.name}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (currentProgress == fullProgress)
            {
                status = ObjectiveStatus.Completed;
                OnCompleted(this);
                // BE - QuestManager.Instance[connectedQuestId].Continue();
            }
        }

        public static Objective CreateObjective(string id, string description, string type, string count)
        {
            return new Objective
                   {
                       // Set ConnectedQuestId in Quest!
                       id           = id,
                       description  = description,
                       type         = GetObjectiveType(type),
                       status       = ObjectiveStatus.NotCompleted,
                       fullProgress = int.Parse(count)
                   };
        }

        private static ObjectiveType GetObjectiveType(string typeName)
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
    }
}