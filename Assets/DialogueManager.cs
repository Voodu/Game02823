using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    private const string PlayerPrefix = "ME: ";

    [SerializeField]
    private Dialogue currentDialogue;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private float elementPadding;

    /* UI Prefabs */
    [SerializeField]
    private Text text;

    [SerializeField]
    private Button button;

    private float offset;

    private void Start()
    {
        if (currentDialogue != null)
        {
            ShowDialogue(currentDialogue);
        }
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        canvas.enabled = true;
    }

    public void DisableDialogue()
    {
        currentDialogue.enabled = false;
        canvas.enabled = false;
    }

    public void UpdateDialogueCanvas(IReadOnlyList<string> storyLines, IReadOnlyList<Choice> choices)
    {
        ResetCanvas();
        UpdateStory(storyLines);
        UpdateChoices(choices);
    }

    public void UpdateStory(IReadOnlyList<string> storyLines)
    {
        foreach (var storyLine in storyLines)
        {
            if (storyLine.StartsWith(PlayerPrefix))
            {
                continue;
            }

            var storyText = Instantiate(text);
            storyText.text = storyLine;
            storyText.transform.SetParent(canvas.transform, false);
            storyText.transform.Translate(new Vector2(0, offset));
            offset -= storyText.fontSize + elementPadding;
        }
    }

    public void UpdateChoices(IReadOnlyList<Choice> choices)
    {
        foreach (var choice in choices)
        {
            var choiceButton = Instantiate(button);
            choiceButton.transform.SetParent(canvas.transform, false);
            choiceButton.transform.Translate(new Vector2(0, offset));

            var choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text.Substring(PlayerPrefix.Length);

            var layoutGroup = choiceButton.GetComponent<HorizontalLayoutGroup>();

            choiceButton.onClick.AddListener(() => currentDialogue.ChooseSelected(choice.index));

            offset -= choiceText.fontSize + layoutGroup.padding.top + layoutGroup.padding.bottom + elementPadding;
        }
    }

    private void ResetCanvas()
    {
        var childCount = canvas.transform.childCount;
        for (var i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }

        canvas.enabled = true;
        offset = 0;
    }

    private void HidingButton()
    {
        var endButton = Instantiate(button);
        endButton.transform.SetParent(canvas.transform, false);
        endButton.transform.Translate(new Vector2(0, offset));

        var endText = endButton.GetComponentInChildren<Text>();
        endText.text = "[End conversation]";

        var layoutGroup = endButton.GetComponent<HorizontalLayoutGroup>();
        endButton.onClick.AddListener(DisableDialogue);
        offset -= endText.fontSize + layoutGroup.padding.top + layoutGroup.padding.bottom + elementPadding;
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
        ResetCanvas();
        UpdateStory(currentDialogue.StoryLines);
        // Show [End] button
        HidingButton();
    }
}