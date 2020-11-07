using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
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

    private void ResetCanvas()
    {
        var childCount = canvas.transform.childCount;
        for (var i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }

        offset = 0;
    }

    public void UpdateDialogueCanvas(List<string> storyLines, List<Choice> choices)
    {
        ResetCanvas();
        UpdateStory(storyLines);
        UpdateChoices(choices);
    }

    public void UpdateStory(List<string> storyLines)
    {
        foreach (var storyLine in storyLines)
        {
            var storyText = Instantiate(text);
            storyText.text = storyLine;
            storyText.transform.SetParent(canvas.transform, false);
            storyText.transform.Translate(new Vector2(0, offset));
            offset -= storyText.fontSize + elementPadding;
        }
    }

    public void UpdateChoices(List<Choice> choices)
    {
        foreach (var choice in choices)
        {
            var choiceButton = Instantiate(button);
            choiceButton.transform.SetParent(canvas.transform, false);
            choiceButton.transform.Translate(new Vector2(0, offset));

            var choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            var layoutGroup = choiceButton.GetComponent<HorizontalLayoutGroup>();

            choiceButton.onClick.AddListener(() => currentDialogue.ChoiceSelected(choice.index));

            offset -= choiceText.fontSize + layoutGroup.padding.top + layoutGroup.padding.bottom + elementPadding;
        }
    }
}