using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
  public class ViewManager : MonoBehaviour
  {
    public void LoadScene(string sceneName)
    {
      SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
      SceneManager.UnloadSceneAsync(sceneName);
    }
  }
}
