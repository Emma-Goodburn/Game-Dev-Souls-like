using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemy {
  public class DeadState : State
  {
    float timeInState = 0f;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
    {
      Scene currentScene = SceneManager.GetActiveScene();
      // Display message that boss is dead
      Scenes.ContextualTextManager.Instance.DisplayMessage("Boss defeated", false);
      timeInState += Time.deltaTime;
      // wait 3 seconds
      if (timeInState >= 3f)
      {
        Scenes.ViewManager viewManager = GameObject.FindObjectOfType<Scenes.ViewManager>();
        Scenes.VictoryManager victoryManager = GameObject.FindObjectOfType<Scenes.VictoryManager>();
        // Show cursor and unlock it to the center of the screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Load next scene
        if (currentScene.name == "Level1")
          viewManager.ChangeScene("Level2");
        else if (currentScene.name == "Level2")
          victoryManager.ShowVictoryScreen();
      }
      // Boss is dead so never leave state
      return this;
    }
  }
}