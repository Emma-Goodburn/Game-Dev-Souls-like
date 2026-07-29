using UnityEngine;

namespace Enemy
{
  public class EnemyAnimatorHandler : MonoBehaviour
  {
    EnemyStats enemyStats;
    EnemyMovement enemyMovement;
    public Animator animator;

    public void Awake()
    {
      animator = GetComponent<Animator>();
      enemyStats = GetComponentInParent<EnemyStats>();
      enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
      if (enemyStats.isDead)
        return;

      animator.applyRootMotion = isInteracting;
      animator.SetBool("isInteracting", isInteracting);
      animator.CrossFade(targetAnim, 0.2f);
    }
    
    private void OnAnimatorMove()
    {
      float delta = Time.deltaTime;
      enemyMovement.enemyRigidbody.linearDamping = 0;
      Vector3 delatPosition = animator.deltaPosition;
      delatPosition.y = 0;
      Vector3 velocity = delatPosition / delta;
      enemyMovement.enemyRigidbody.linearVelocity = velocity;
    }
  }
}