using UnityEngine;

namespace Enemy
{
  public class IdleState : State
  {
    public ChaseState chaseState;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
    {
      // If boss dead never leave idle state
      if (enemyStats.isDead)
        return this;

      // Find player 
      enemyManager.player = GameObject.Find("Player");

      // Switch state when player found
      if (enemyManager.player != null)
      {
        return chaseState;
      }
      else
      {
        return this;
      }
    }
  }
}
