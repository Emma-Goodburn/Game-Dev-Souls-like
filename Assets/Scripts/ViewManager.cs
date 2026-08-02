using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
  public class ViewManager : MonoBehaviour
  {
    public void LoadScene(string sceneName)
    {
      SceneManager.LoadScene(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
      SceneManager.UnloadScene(sceneName);
    }

    public void ChangeScene(string sceneName)
    {
      string currentScene = SceneManager.GetActiveScene().name;
      LoadScene(sceneName);
      UnloadScene(currentScene);
    }

    public void ExitGame()
    {
      Application.Quit();
    }
  }
}
