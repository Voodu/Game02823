using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dialogue))]
public class TalkingNPC : MonoBehaviour
{
    private Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<Dialogue>();
        dialogue.enabled = false;
    }

    public void Talk()
    {
        dialogue.enabled = true;
    }

    public void CompleteTask()
    {
        dialogue.inkStory.variablesState["completed"] = true;
    }
}
