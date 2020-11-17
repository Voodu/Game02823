using Quests.Enums;
using UnityEngine;

namespace Quests.Items
{
    public class ObjectiveItem : MonoBehaviour
    {
        public ObjectiveType ObjectiveType;
        public string ConnectedQuestId;
        public string ConnectedObjectiveId;
        public string Metadata;

        public virtual void Activate()
        {
            QuestManager.Instance[ConnectedQuestId][ConnectedObjectiveId].RecordProgress(this);
        }
    }
}