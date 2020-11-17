using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class Journal : Singleton<Journal>
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ShowJournal(QuestManager.Instance.VisibleQuests);
            }
        }

        public void ShowJournal(List<Quest> quests)
        {
            foreach (var quest in quests)
            {
                print(quest.title);
                print(string.Join(string.Empty, quest.journalEntries));
            }
        }
    }
}