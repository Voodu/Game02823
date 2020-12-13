using System;
using Quests.Enums;

namespace Quests
{
    [Serializable]
    public class ObjectiveItemData
    {
        public ObjectiveType objectiveType;
        public string        connectedQuestId;
        public string        connectedObjectiveId;
        public string        data;
    }
}