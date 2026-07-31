using UnityEngine;

namespace Enemy
{
  public class ChaseState : State
  {
    public AttackState attackState;
    public IdleState idleState;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
    {
      // return to idle when dead
      if (enemyStats.isDead)
        return idleState;

      // Stay in state while attacks not possible
      if (enemyManager.isInteracting || enemyManager.currentRecoveryTime > 0)
        return this;

      // calculate movement
      Vector3 targetDirection = enemyManager.player.transform.position - enemyManager.transform.position;
      enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.player.transform.position, enemyManager.transform.position);
      float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

      // move if too far from player
      if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange)
      {
        enemyAnimatorHandler.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        enemyManager.navMeshAgent.enabled = true;
      }

      HandleRotateToTarget(enemyManager);

      // move to attack state if attacks possible
      if (enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange && enemyManager.currentRecoveryTime <= 0)
      {
        return attackState;
      }
      else
      {
        // continue chasing if too far
        return this;
      }
    }
    
    // use navmesh to calculate rotation
    public void HandleRotateToTarget(EnemyManager enemyManager)
    {
      Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
      Vector3 targetVelocity = enemyManager.enemyRigidbody.linearVelocity;

      enemyManager.navMeshAgent.enabled = true;
      enemyManager.navMeshAgent.SetDestination(enemyManager.player.transform.position);
      enemyManager.enemyRigidbody.linearVelocity = targetVelocity;
      enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
    }
  }
}
