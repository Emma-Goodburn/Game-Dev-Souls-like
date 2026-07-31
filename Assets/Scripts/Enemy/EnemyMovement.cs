using UnityEngine;

namespace Enemy
{
  public class EnemyMovement : MonoBehaviour
  {
    EnemyManager enemyManager;
    EnemyAnimatorHandler enemyAnimatorHandler;

    int vertical;
    int horizontal;

    private void Awake()
    {
      enemyManager = GetComponent<EnemyManager>();
      vertical = Animator.StringToHash("Vertical");
      horizontal = Animator.StringToHash("Horizontal");
      enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
    }

    public void HandleMoveToTarget()
    {
      
    }
  }
}