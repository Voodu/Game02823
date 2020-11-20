using System.Collections.Generic;
using System.Linq;
using Common;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        [SerializeField]
        private string playerPrefix = "ME: ";

        private Dialogue dialogue;

        /* UI Prefabs */
        [SerializeField]
        private GameObject dialoguePanel;

        [SerializeField]
        private TextMeshProUGUI npcText;

        [SerializeField]
        private Button choiceButtonPrefab;

        [SerializeField]
        private GameObject choiceMenu;

        public void ShowDialogue(Dialogue dialogue)
        {
            this.dialogue = dialogue;
            dialoguePanel.gameObject.SetActive(true);
        }

        public void DisableDialogue()
        {
            dialogue.enabled = false;
            dialoguePanel.gameObject.SetActive(false);
        }

        public void UpdateDialogueCanvas(IEnumerable<string> storyLines, IEnumerable<Choice> choices)
        {
            ShowStory(storyLines);
            ClearChoiceMenu();
            ShowChoices(choices);
        }

        public void ParseTags(IEnumerable<string> tags)
        {
            foreach (var inkTag in tags)
            {
                if (inkTag == "terminate")
                {
                    EndConversation();
                }
            }
        }

        private void ShowStory(IEnumerable<string> storyLines)
        {
            var npcLines = storyLines.Where(storyLine => !storyLine.StartsWith(playerPrefix));
            var npcStory = string.Join(string.Empty, npcLines);
            npcText.SetText(npcStory);
        }

        private void ShowChoices(IEnumerable<Choice> choices)
        {
            foreach (var choice in choices)
            {
                var choiceButton = Instantiate(choiceButtonPrefab, choiceMenu.transform);
                choiceButton.onClick.AddListener(() => dialogue.ChooseSelected(choice.index));

                var choiceText = choiceButton.GetComponent<TextMeshProUGUI>();
                choiceText.SetText(choice.text.Substring(playerPrefix.Length));
            }
        }

        private void EndConversation()
        {
            dialogue.StoryNeeded = false;
            ShowStory(dialogue.StoryLines);
            ClearChoiceMenu();
            ShowEndingButton();
        }

        private void ClearChoiceMenu()
        {
            var choiceCount = choiceMenu.transform.childCount;
            for (var i = choiceCount - 1; i >= 0; --i)
            {
                Destroy(choiceMenu.transform.GetChild(i).gameObject);
            }
        }

        private void ShowEndingButton()
        {
            var endButton = Instantiate(choiceButtonPrefab, choiceMenu.transform);
            endButton.onClick.AddListener(DisableDialogue);

            var endText = endButton.GetComponentInChildren<TextMeshProUGUI>();
            endText.SetText("[End conversation]");
        }
    }
}