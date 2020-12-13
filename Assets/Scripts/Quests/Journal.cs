using System.Collections.Generic;
using System.Linq;
using Common;
using Quests.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    public class Journal : Singleton<Journal>
    {
        [SerializeField]
        private GameObject journalPanel;

        [SerializeField]
        private GameObject questMenu;

        [SerializeField]
        private TextMeshProUGUI questTitle;

        [SerializeField]
        private TextMeshProUGUI questNotes;

        [SerializeField]
        private Button questButtonPrefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ToggleJournal(QuestManager.Instance.VisibleQuests);
            }
        }

        public void ToggleJournal(List<Quest> quests)
        {
            if (journalPanel.activeSelf) // if active, then turn off
            {
                journalPanel.SetActive(false);
                ClearQuestMenu();
                GameManager.Instance.player.Freeze(false);
            }
            else // populate and activate
            {
                if (quests.Count == 0)
                {
                    var questButton = Instantiate(questButtonPrefab, questMenu.transform);
                    questButton.GetComponent<TextMeshProUGUI>().SetText("No quests yet.");
                }
                else
                {
                    foreach (var quest in quests)
                    {
                        SetupQuestButton(quest);
                    }

                    SetupQuestText(quests[0]);
                }

                journalPanel.SetActive(true);
                GameManager.Instance.player.Freeze(true);
            }
        }

        private void SetupQuestButton(Quest quest)
        {
            var questButton = Instantiate(questButtonPrefab, questMenu.transform);
            questButton.onClick.AddListener(() => { SetupQuestText(quest); });
            var tick = quest.status == QuestStatus.NotCompleted ? "○ " : string.Empty;
            questButton.GetComponent<TextMeshProUGUI>().SetText(tick + quest.title);
        }

        private void SetupQuestText(Quest quest)
        {
            var objectivesText = quest.ActiveObjectives.Select(x => $"○ {x.description} ({x.currentProgress}/{x.fullProgress})");
            questTitle.SetText(quest.title);
            questNotes.SetText(string.Join("\n", quest.journalEntries)
                               +
                               string.Join("\n", objectivesText));
        }

        private void ClearQuestMenu()
        {
            var questCount = questMenu.transform.childCount;
            for (var i = questCount - 1; i >= 0; --i)
            {
                Destroy(questMenu.transform.GetChild(i).gameObject);
            }
        }
    }
}