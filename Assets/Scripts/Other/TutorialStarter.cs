using Common;
using Dialogues;
using UnityEngine;

namespace Other
{
    public class TutorialStarter : MonoBehaviour
    {
        private void Start()
        {
            if (GameManager.Instance.targetSpawn == "Start")
            {
                GetComponent<Dialogue>().enabled = true;
            }
        }
    }
}