using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    private const string PlayerPrefix = "ME: ";

    private Dialogue currentDialogue;

    [SerializeField]
    private Canvas canvas;

    /* UI Prefabs */
    [SerializeField]
    private TextMeshProUGUI npcText;

    [SerializeField]
    private Button choiceButtonPrefab;

    [SerializeField]
    private GameObject choiceMenu;

    public void ShowDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        canvas.gameObject.SetActive(true);
    }

    public void DisableDialogue()
    {
        currentDialogue.enabled = false;
        canvas.gameObject.SetActive(false);
    }

    public void UpdateDialogueCanvas(IReadOnlyList<string> storyLines, IReadOnlyList<Choice> choices)
    {
        UpdateStory(storyLines);
        ResetChoiceMenu();
        UpdateChoices(choices);
    }

    public void UpdateStory(IReadOnlyList<string> storyLines)
    {
        var npcLines = storyLines.Where(storyLine => !storyLine.StartsWith(PlayerPrefix));
        var npcStory = string.Join(string.Empty, npcLines);
        npcText.SetText(npcStory);
    }

    public void UpdateChoices(IReadOnlyList<Choice> choices)
    {
        var buttonTexts = new List<TextMeshProUGUI>();
        foreach (var choice in choices)
        {
            var choiceButton = Instantiate(choiceButtonPrefab, choiceMenu.transform);

            var choiceText = choiceButton.GetComponent<TextMeshProUGUI>();
            choiceText.SetText(choice.text.Substring(PlayerPrefix.Length));
            buttonTexts.Add(choiceText);

            choiceButton.onClick.AddListener(() => currentDialogue.ChooseSelected(choice.index));
        }

        //AdjustFontSize(buttonTexts);
    }

    private void ResetChoiceMenu()
    {
        var choiceCount = choiceMenu.transform.childCount;
        for (var i = choiceCount - 1; i >= 0; --i)
        {
            Destroy(choiceMenu.transform.GetChild(i).gameObject);
        }
    }

    private void HidingButton()
    {
        var endButton = Instantiate(choiceButtonPrefab, choiceMenu.transform);

        var endText = endButton.GetComponentInChildren<TextMeshProUGUI>();
        endText.SetText("[End conversation]");

        endButton.onClick.AddListener(DisableDialogue);
    }

    public void ParseTags(IReadOnlyList<string> tags)
    {
        foreach (var inkTag in tags)
        {
            if (inkTag == "terminate")
            {
                EndConversation();
            }
        }
    }

    public void EndConversation()
    {
        // Stop parsing further the story
        currentDialogue.StoryNeeded = false;
        // Show current buffer
        ResetChoiceMenu();
        UpdateStory(currentDialogue.StoryLines);
        // Show [End] choiceButton
        HidingButton();
    }

    private void AdjustFontSize(IReadOnlyList<TextMeshProUGUI> textObjects)
    {
        if ((textObjects == null) || (textObjects.Count == 0))
        {
            return;
        }

        // Iterate over each of the text objects in the array to find a good test candidate
        // There are different ways to figure out the best candidate
        // Preferred width works fine for single line text objects
        var   candidateIndex    = 0;
        float maxPreferredWidth = 0;

        for (var i = 0; i < textObjects.Count; i++)
        {
            var preferredWidth = textObjects[i].preferredWidth;
            if (preferredWidth > maxPreferredWidth)
            {
                maxPreferredWidth = preferredWidth;
                candidateIndex    = i;
            }
        }

        // Force an update of the candidate text object so we can retrieve its optimum point size.
        textObjects[candidateIndex].enableAutoSizing = true;
        textObjects[candidateIndex].ForceMeshUpdate();
        var optimumPointSize = textObjects[candidateIndex].fontSize;

        // Disable auto size on our test candidate
        textObjects[candidateIndex].enableAutoSizing = false;

        // Iterate over all other text objects to set the point size
        foreach (var textObject in textObjects)
        {
            textObject.fontSize = optimumPointSize;
        }
    }
}