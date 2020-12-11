using System.Collections.Generic;
using System.Linq;
using Common;
using Ink.Runtime;
using Quests;
using Quests.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        [SerializeField]
        private string playerPrefix = "ME: ";
        
        [SerializeField]
        private string extensionLinePrefix = "EXT";

        private Dialogue dialogue;
        [SerializeField]
        private List<Dialogue> dialogues = new List<Dialogue>();
        public Dialogue this[string dialogueId] => dialogues.First(x => x.id == dialogueId);

        /* UI Prefabs */
        [SerializeField]
        private GameObject dialoguePanel;

        [SerializeField]
        private TextMeshProUGUI npcText;

        [SerializeField]
        private Button choiceButtonPrefab;

        [SerializeField]
        private GameObject choiceMenu;

        public void Register(Dialogue dialogue)
        {
            dialogues.AddWithId(dialogue);
        }

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
            // TODO: Resign from tags and move everything to use extension lines
            foreach (var inkTag in tags)
            {
                if (inkTag == "terminate")
                {
                    EndConversation();
                }
            }
        }

        public void ParseExtensionLines(IEnumerable<string> lines)
        {
            // Extension lines are in format: EXT <keyword> <data1> <data2> <...>
            // Ex. EXT FINISH 50 100
            // Ex. EXT QUEST start bear_killing
            foreach (var line in lines)
            {
                var words = line.Split(' ');
                switch (words[1].Trim())
                {
                    case "FINISH":
                        print($"Exp: {words[2].Trim()}, gold: {words[3].Trim()}");
                        break;
                    case "QUEST":
                        var questId = words[3].Trim();
                        if (words[2].Trim() == "start")
                        {
                            QuestManager.instance.Begin(questId);
                        }
                        else if (words[2].Trim() == "progress")
                        {
                            var objectiveId = words[4].Trim();
                            QuestManager.instance[questId][objectiveId].RecordProgress(new ObjectiveItemData()
                                                                                       {
                                                                                           connectedQuestId = questId,
                                                                                           connectedObjectiveId = objectiveId,
                                                                                           objectiveType = ObjectiveType.Talk
                                                                                       });
                        }

                        break;
                    default:
                        print($"Unknown EXT: {words[1].Trim()}");
                        break;
                }
            }
        }

        private void ShowStory(IEnumerable<string> storyLines)
        {
            var lines = storyLines as string[] ?? storyLines.ToArray();
            ParseExtensionLines(lines.Where(sl => sl.StartsWith(extensionLinePrefix)));
            var npcLines = lines.Where(line => !line.StartsWith(playerPrefix) && !line.StartsWith(extensionLinePrefix));
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