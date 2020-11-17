using System;
using Quests.Enums;
using UnityEngine;

namespace Quests
{
    [Serializable]
    public class Objective
    {
        public string                  id;
        public string                  connectedQuestId;
        public string                  description;
        public ObjectiveType           type   = ObjectiveType.None;
        public ObjectiveStatus         status = ObjectiveStatus.NotStarted;
        public (int current, int full) progress;

        public void RecordProgress(ObjectiveItem item)
        {
            switch (item.objectiveType)
            {
                case ObjectiveType.Kill:
                    progress = (progress.current + 1, progress.full);
                    break;
                case ObjectiveType.Gather:
                    progress = (progress.current + 1, progress.full);
                    break;
                case ObjectiveType.Deliver:
                    progress = (progress.current + 1, progress.full);
                    break;
                case ObjectiveType.Escort:
                    progress = (progress.current + 1, progress.full);
                    break;
                case ObjectiveType.Visit:
                    progress = (progress.current + 1, progress.full);
                    break;
                case ObjectiveType.Talk:
                    progress = (progress.current + 1, progress.full);
                    break;
                case ObjectiveType.Other:
                    progress = (progress.current + 1, progress.full);
                    break;
                case ObjectiveType.None:
                    Debug.LogWarning($"No objective type set on {item.gameObject.name}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (progress.current == progress.full)
            {
                status = ObjectiveStatus.Completed;
                QuestManager.Instance[connectedQuestId].Continue();
            }
        }

        public static Objective CreateObjective(string id, string description, string type, string count)
        {
            return new Objective
                   {
                       // Set ConnectedQuestId in Quest!
                       id          = id,
                       description = description,
                       type        = GetObjectiveType(type),
                       status      = ObjectiveStatus.NotCompleted,
                       progress    = (0, int.Parse(count))
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