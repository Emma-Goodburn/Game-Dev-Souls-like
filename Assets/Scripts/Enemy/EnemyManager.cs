using UnityEngine;

namespace Enemy
{
  public class EnemyManager : MonoBehaviour
  {
    EnemyMovement enemyMovement;
    EnemyAnimatorHandler enemyAnimatorHandler;
    public bool isInteracting;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    public float currentRecoveryTime = 0;

    private void Awake()
    {
      enemyMovement = GetComponent<EnemyMovement>();
      enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
    }

    private void Update() {
      HandleRecoveryTime();
    }

    private void FixedUpdate() {
      HandleStateMachine();
    }

    private void HandleStateMachine()
    {
      if (enemyMovement.player != null)
      {
        enemyMovement.distanceFromTarget = Vector3.Distance(enemyMovement.player.transform.position, transform.position);
      }

      if (enemyMovement.player == null)
      {
        enemyMovement.player = GameObject.Find("Player");
      }
      else if (enemyMovement.distanceFromTarget > enemyMovement.stoppingDistance)
      {
        enemyMovement.HandleMoveToTarget();
      }
      else if (enemyMovement.distanceFromTarget <= enemyMovement.stoppingDistance)
      {
        AttackTarget();
      }
    }

    private void AttackTarget()
    {
      if (isInteracting)
        return;

      if (currentAttack == null)
      {
        enemyMovement.HandleRotateToTarget();
        GetNewAttack();
      }
      else
      {
        isInteracting = true;
        enemyAnimatorHandler.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        currentRecoveryTime = currentAttack.recoveryTime;
        enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
        currentAttack = null;
      }
    }

    private void GetNewAttack()
    {
      Vector3 targetDirection = enemyMovement.player.transform.position - transform.position;
      float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
      enemyMovement.distanceFromTarget = Vector3.Distance(enemyMovement.player.transform.position, transform.position);

      int maxScore = 0;

      for (int i = 0; i < enemyAttacks.Length; i++)
      {
        EnemyAttackAction enemyAttackAction = enemyAttacks[i];

        if (enemyMovement.distanceFromTarget <= enemyAttackAction.maximumAttackDistance && enemyMovement.distanceFromTarget <= enemyAttackAction.minimumAttackDistance)
        {
          if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle <= enemyAttackAction.minimumAttackAngle)
          {
            maxScore += enemyAttackAction.attackScore;
          }
        }
      }

      int randomValue = Random.Range(0, maxScore);
      int temporaryScore = 0;

      for (int i = 0; i < enemyAttacks.Length; i++)
      {
        EnemyAttackAction enemyAttackAction = enemyAttacks[i];

        if (enemyMovement.distanceFromTarget <= enemyAttackAction.maximumAttackDistance && enemyMovement.distanceFromTarget >= enemyAttackAction.minimumAttackDistance)
        {
          if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
          {
            if (currentAttack != null)
              return;

            temporaryScore += enemyAttackAction.attackScore;

            if (temporaryScore > randomValue)
            {
              currentAttack = enemyAttackAction;
            }
          }
        }
      }
    }

    private void HandleRecoveryTime()
    {
      if (currentRecoveryTime > 0)
      {
        currentRecoveryTime -= Time.deltaTime;
      }
      if (isInteracting)
      {
        if (currentRecoveryTime <= 0)
        {
          isInteracting = false;
        }
      }
    }
  }
}
