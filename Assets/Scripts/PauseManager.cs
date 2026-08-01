using UnityEngine;

namespace Scene
{
  public class PauseManager : MonoBehaviour
  {
    GameObject pauseMenu;

    private void Awake()
    {
      // Find the pause menu GameObject in the scene
      pauseMenu = GameObject.Find("PauseMenu/Menu Background"); 
      // Ensure the pause menu is hidden at the start
      pauseMenu.SetActive(false); 
    }

    public void PauseGame()
    {
      // Show the pause menu
      pauseMenu.SetActive(true); 
      // Show cursor and unlock it to the center of the screen
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      // Pause the game by setting time scale to 0
      Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
      // Hide the pause menu
      pauseMenu.SetActive(false);
      // Hide cursor and lock it to the center of the screen
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      // Resume the game by setting time scale back to 1
      Time.timeScale = 1f; 
    }
  }
}
