using UnityEngine;

namespace Enemy
{
  public class IdleState : State
  {
    public ChaseState chaseState;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
    {
      // Find player 
      enemyManager.player = GameObject.Find("Player");

      // Switch state when player found and trigger howl animation
      if (enemyManager.player != null)
      {
        enemyManager.isInteracting = true;
        enemyManager.currentRecoveryTime = 2.6f;
        enemyAnimatorHandler.PlayTargetAnimation("Howl", true);
        return chaseState;
      }
      else
      {
        return this;
      }
    }
  }
}
