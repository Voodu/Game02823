using Common;
using UnityEngine;

namespace Other
{
    [RequireComponent(typeof(Collider2D))]
    public class SceneChange : MonoBehaviour
    {
        public string targetScene;
        public string targetSpawn;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameManager.Instance.targetSpawn = targetSpawn;
            SceneManager.Instance.ChangeScene(targetScene);
        }
    }
}
