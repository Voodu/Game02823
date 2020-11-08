using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TalkingNPC npc1;
    public TalkingNPC npc2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            npc1.Talk();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            npc2.Talk();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            npc1.CompleteTask();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            npc2.CompleteTask();
        }
    }
}
