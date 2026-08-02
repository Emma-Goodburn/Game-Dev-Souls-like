using UnityEngine;

namespace Enemy
{
  [CreateAssetMenu(menuName = "A.I/Enemy Actions/ Attack Action")]
  public class EnemyAttackAction : EnemyAction
  {
    public int attackScore = 3;
    public float recoveryTime = 2;

    public float maximumAttackAngle = 35;
    public float minimumAttackAngle = -35;

    public float minimumAttackDistance = 0;
    public float maximumAttackDistance = 5;

    public int damage;
    public bool hasPoisonEffect = false;
  }
}
