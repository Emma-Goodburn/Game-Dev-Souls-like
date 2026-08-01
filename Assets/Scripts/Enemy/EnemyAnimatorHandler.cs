using UnityEngine;

namespace Enemy
{
  public class EnemyAnimatorHandler : MonoBehaviour
  {
    EnemyStats enemyStats;
    EnemyManager enemyManager;
    public Animator animator;

    public void Awake()
    {
      animator = GetComponent<Animator>();
      enemyStats = GetComponentInParent<EnemyStats>();
      enemyManager = GetComponentInParent<EnemyManager>();
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
      animator.applyRootMotion = isInteracting;
      animator.SetBool("isInteracting", isInteracting);
      animator.CrossFade(targetAnim, 0.2f);
    }
    
    private void OnAnimatorMove()
    {
      if (Time.timeScale <= 0)
        return;

      float delta = Time.deltaTime;
      enemyManager.enemyRigidbody.linearDamping = 0;
      Vector3 deltaPosition = animator.deltaPosition;
      deltaPosition.y = 0;
      Vector3 velocity = deltaPosition / delta;
      // Check for NaN values that can occur on unpause
      Vector3 errorVector = new Vector3(float.NaN, float.NaN, float.NaN);
      if (velocity.Equals(errorVector))
        enemyManager.enemyRigidbody.linearVelocity = Vector3.zero;
      else
        enemyManager.enemyRigidbody.linearVelocity = velocity;
    }
  }
}