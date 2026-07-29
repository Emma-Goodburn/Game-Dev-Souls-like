using UnityEngine;

namespace Enemy
{
  public class EnemyManager : MonoBehaviour
  {
    EnemyMovement enemyMovement;
    public bool isPerformingAction;
    private void Awake()
    {
      enemyMovement = GetComponent<EnemyMovement>();
    }

    private void FixedUpdate() {
      HandleStateMachine();
    }
    
    private void HandleStateMachine()
    {
      if (enemyMovement.player == null)
      {
        enemyMovement.player = GameObject.Find("Player");
      }
      else
      {
        enemyMovement.HandleMoveToTarget();
      }
    }
  }
}
