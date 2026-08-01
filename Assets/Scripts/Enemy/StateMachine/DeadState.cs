using UnityEngine;

namespace Enemy {
  public class DeadState : State
  {
    float timeInState = 0f;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
    {
      // Display message that boss is dead
      Scene.ContextualTextManager.Instance.DisplayMessage("Boss defeated", false);
      timeInState += Time.deltaTime;
      // wait 3 seconds
      if (timeInState >= 3f)
      {
        Scene.ViewManager viewManager = GameObject.FindObjectOfType<Scene.ViewManager>();
        // Show cursor and unlock it to the center of the screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Load menu scene
        viewManager.LoadScene("Menu");
      }
      // Boss is dead so never leave state
      return this;
    }
  }
}