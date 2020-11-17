using UnityEngine;

namespace Dialogues {
    [RequireComponent(typeof(Dialogue))]
    public class TalkingNPC : MonoBehaviour
    {
        private Dialogue dialogue;

        // Start is called before the first frame update
        private void Start()
        {
            dialogue = GetComponent<Dialogue>();
        }

        public void Talk()
        {
            dialogue.enabled = true;
        }
    }
}