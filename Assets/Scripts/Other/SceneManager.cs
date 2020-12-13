using Common;

namespace Other
{
    public class SceneManager : Singleton<SceneManager>
    {
        public void ChangeScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}