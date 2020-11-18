using Quests.Enums;
using UnityEngine;

namespace Quests
{
    public class ObjectiveItem : MonoBehaviour
    {
        public ObjectiveType objectiveType;
        public string        connectedQuestId;
        public string        connectedObjectiveId;
        public string        data;

        public virtual void Activate()
        {
            QuestManager.Instance[connectedQuestId][connectedObjectiveId].RecordProgress(this);
        }
    }
}