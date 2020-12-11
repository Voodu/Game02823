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
                if ((quest.id == "bear_killing") && Input.GetKeyDown(KeyCode.S))
                {
                    QuestManager.Instance.Begin(quest.id);
                }

                if ((quest.id == "flower_collecting") && Input.GetKeyDown(KeyCode.F))
                {
                    QuestManager.Instance.Begin(quest.id);
                }
            }

            if (item != null)
            {
                // Bear
                if ((item.data.connectedObjectiveId == "kill_bear") && Input.GetKeyDown(KeyCode.K))
                {
                    item.Activate();
                }

                if ((item.data.connectedObjectiveId == "visit_cave") && Input.GetKeyDown(KeyCode.C))
                {
                    item.Activate();
                }

                if ((item.data.connectedObjectiveId == "inform_man") && Input.GetKeyDown(KeyCode.I))
                {
                    item.Activate();
                }

                // Flowers
                if ((item.data.connectedObjectiveId == "find_roses") && Input.GetKeyDown(KeyCode.R))
                {
                    item.Activate();
                }

                if ((item.data.connectedObjectiveId == "find_dandelions") && Input.GetKeyDown(KeyCode.D))
                {
                    item.Activate();
                }

                if ((item.data.connectedObjectiveId == "find_tulips") && Input.GetKeyDown(KeyCode.T))
                {
                    item.Activate();
                }

                if ((item.data.connectedObjectiveId == "inform_girl") && Input.GetKeyDown(KeyCode.G))
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