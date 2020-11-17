using Dialogues;
using UnityEngine;

namespace Quests
{
    public class QuestTest : MonoBehaviour
    {
        public TalkingNPC npc1;
        public TalkingNPC npc2;
        public Quest      quest;

        public ObjectiveItem item;

        private void Awake()
        {
            quest = GetComponent<Quest>();
            item  = GetComponent<ObjectiveItem>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (quest != null)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    QuestManager.Instance.Begin(quest.id);
                }
            }

            if (item != null)
            {
                if ((item.connectedObjectiveId == "kill_bear") && Input.GetKeyDown(KeyCode.K))
                {
                    item.Activate();
                }

                if ((item.connectedObjectiveId == "visit_cave") && Input.GetKeyDown(KeyCode.C))
                {
                    item.Activate();
                }

                if ((item.connectedObjectiveId == "inform_man") && Input.GetKeyDown(KeyCode.I))
                {
                    item.Activate();
                }
            }
        }

        public void Print(string message)
        {
            print(message);
        }
    }
}