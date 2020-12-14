using Common;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Other
{
    public class SceneManager : Singleton<SceneManager>
    {
        public void ChangeScene(string sceneName)
        {
            USceneManager.LoadScene(sceneName);
        }

        public void OnSceneUnloaded(UnityAction<Scene> callback)
        {
            USceneManager.sceneUnloaded += callback;
        }

        public void OnSceneLoaded(UnityAction<Scene, LoadSceneMode> callback)
        {
            USceneManager.sceneLoaded += callback;
        }

        public void OnActiveSceneChanged(UnityAction<Scene, Scene> callback)
        {
            USceneManager.activeSceneChanged += callback;
        }

        public void ReloadScene()
        {
            USceneManager.LoadScene(USceneManager.GetActiveScene().buildIndex);
        }
    }
}