using UnityEngine;

namespace Scenes
{
  public class VictoryManager : MonoBehaviour
  {
    GameObject victoryScreen;

    private void Awake()
    {
      // Find the victory screen GameObject in the scene
      victoryScreen = GameObject.Find("VictoryScreen/Menu Background");
      // Ensure the victory screen is hidden at the start
      victoryScreen.SetActive(false);
    }

    public void ShowVictoryScreen()
    {
      // Show the victory screen
      victoryScreen.SetActive(true);
      // Show cursor and unlock it to the center of the screen
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
  }
}
