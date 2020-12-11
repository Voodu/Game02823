using Quests.Enums;
using UnityEngine;

namespace Quests
{
    public class ObjectiveItem : MonoBehaviour
    {
        public ObjectiveItemData data;

        public virtual void Activate()
        {
            QuestManager.Instance[data.connectedQuestId][data.connectedObjectiveId].RecordProgress(data);
        }
    }
}