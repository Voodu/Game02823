using UnityEngine;

namespace Quests {
    [RequireComponent(typeof(ObjectiveItem))]
    public class KillObjectiveActivator : MonoBehaviour
    {
        private void OnDestroy()
        {
            GetComponent<ObjectiveItem>().Activate();
        }
    }
}
