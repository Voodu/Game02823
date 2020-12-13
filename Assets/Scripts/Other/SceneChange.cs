using UnityEngine;

namespace Other {
    [RequireComponent(typeof(Collider2D))]
    public class SceneChange : MonoBehaviour
    {
        public string targetScene;

        private void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.Instance.ChangeScene(targetScene);
        }
    }
}
