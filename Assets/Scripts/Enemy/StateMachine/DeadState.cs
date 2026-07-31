using UnityEngine;

namespace Enemy {
  public class DeadState : State
  {
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
    {
      // Boss is dead so never leave state
      return this;
    }
  }
}