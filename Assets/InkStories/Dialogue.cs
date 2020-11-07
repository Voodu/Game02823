using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkAsset;

    private Story inkStory;
    private bool  storyNeeded;

    private void Awake()
    {
        inkStory = new Story(inkAsset.text);
    }

    private void OnEnable()
    {
        storyNeeded = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (storyNeeded)
        {
            var storyLines = GetNewStoryLines();
            DialogueManager.Instance.UpdateDialogueCanvas(storyLines, inkStory.currentChoices);
            storyNeeded = false;
        }
    }

    private List<string> GetNewStoryLines()
    {
        var storyLines = new List<string>();
        while (inkStory.canContinue)
        {
            storyLines.Add(inkStory.Continue());
        }

        return storyLines;
    }

    public void ChoiceSelected(int id)
    {
        inkStory.ChooseChoiceIndex(id);
        storyNeeded = true;
    }
}