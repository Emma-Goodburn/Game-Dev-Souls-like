using UnityEngine;
namespace Enemy
{
  public class AttackState : State
  {
    public ChaseState chaseState;
    public DeadState deadState;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
    {
      Player.PlayerStats playerStats = enemyManager.player.GetComponent<Player.PlayerStats>();
      // move to dead state when dead
      if (enemyStats.isDead)
        return deadState;

      // return to chase if no attacks possible
      if (enemyManager.isInteracting || enemyManager.currentRecoveryTime > 0 || enemyManager.distanceFromTarget > enemyManager.maximumAttackRange || playerStats.isDead)
        return chaseState;

      if (enemyManager.currentAttack == null)
      {
        // Handle rotation if needed
        Vector3 direction = enemyManager.player.transform.position - enemyManager.transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        // Get attack and stay in state to perform
        GetNewAttack(enemyManager);
        return this;
      }
      else
      {
        // pause movement and attack player the back to chase state
        enemyManager.isInteracting = true;
        enemyAnimatorHandler.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        enemyManager.currentRecoveryTime = enemyManager.currentAttack.recoveryTime;
        enemyAnimatorHandler.PlayTargetAnimation(enemyManager.currentAttack.actionAnimation, true);
        return chaseState;
      }
    }
      
    private void GetNewAttack(EnemyManager enemyManager) {
      // check player position
      Vector3 targetDirection = enemyManager.player.transform.position - transform.position;
      float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
      enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.player.transform.position, transform.position);

      int maxScore = 0;

      for (int i = 0; i < enemyManager.enemyAttacks.Length; i++)
      {
        EnemyAttackAction enemyAttackAction = enemyManager.enemyAttacks[i];

        // find valid attacks
        if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumAttackDistance && enemyManager.distanceFromTarget >= enemyAttackAction.minimumAttackDistance)
        {
          if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
          {
            maxScore += enemyAttackAction.attackScore;
          }
        }
      }

      int randomValue = Random.Range(0, maxScore);
      int temporaryScore = 0;

      // pick random valid attack
      for (int i = 0; i < enemyManager.enemyAttacks.Length; i++)
      {
        EnemyAttackAction enemyAttackAction = enemyManager.enemyAttacks[i];

        if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumAttackDistance && enemyManager.distanceFromTarget >= enemyAttackAction.minimumAttackDistance)
        {
          if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
          {
            if (enemyManager.currentAttack != null)
              return;

            temporaryScore += enemyAttackAction.attackScore;

            if (temporaryScore > randomValue)
            {
              enemyManager.currentAttack = enemyAttackAction;
            }
          }
        }
      }
    }
  }
}
