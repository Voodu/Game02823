using Dialogues;
using UnityEngine;

namespace Unique {
    public class TutorialStarter : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<TalkingNPC>().Talk();
        }
    }
}