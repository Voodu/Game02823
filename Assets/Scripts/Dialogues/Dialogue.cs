﻿using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

namespace Dialogues
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField]
        private TextAsset inkAsset;

        [SerializeField]
        public string id;

        private Story inkStory;

        public bool         StoryNeeded { get; set; }
        public List<string> StoryLines  { get; private set; } = new List<string>();
        public List<string> Tags        { get; private set; } = new List<string>();
        public List<Choice> Choices     { get; private set; } = new List<Choice>();

        private void Awake()
        {
            inkStory = new Story(inkAsset.text);
            id       = (string) inkStory.variablesState["name"];
            DialogueManager.Instance.Register(this);
        }

        private void OnEnable()
        {
            StoryNeeded = true;
            DialogueManager.Instance.ShowDialogue(this);
        }

        private void OnDisable()
        {
            StoryNeeded = false;
        }

        private void OnDestroy()
        {
            DialogueManager.Instance.SaveDialogue(this);
        }

        private void Update()
        {
            if (StoryNeeded)
            {
                StoryLines = new List<string>();
                Tags       = new List<string>();

                while (StoryNeeded && inkStory.canContinue)
                {
                    inkStory.Continue();
                    ProcessNextLine();
                }

                if (!StoryNeeded)
                {
                    return;
                }

                Choices = inkStory.currentChoices;
                DialogueManager.Instance.UpdateDialogueCanvas(StoryLines, Choices);
                StoryNeeded = false;
            }
        }

        private void ProcessNextLine()
        {
            // Process story
            StoryLines.Add(inkStory.currentText);
            // Process tags
            Tags.AddRange(inkStory.currentTags);
            // Tags have to be parsed instantly, as they may have special effects on the display
            DialogueManager.Instance.ParseTags(inkStory.currentTags);
        }

        public string GetStateJson()
        {
            return inkStory.state.ToJson();
        }

        public void LoadFromJson(string stateJson)
        {
            inkStory.state.LoadJson(stateJson);
        }

        public void ChooseSelected(int id)
        {
            inkStory.ChooseChoiceIndex(id);
            StoryNeeded = true;
        }

        public void Continue()
        {
            inkStory.variablesState["completed"] = true;
        }
    }
}