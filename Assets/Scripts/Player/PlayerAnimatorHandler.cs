using UnityEngine;

namespace Player
{
  public class PlayerAnimatorHandler : Character.CharacterAnimatorHandler
  {
    PlayerManager playerManager;
    InputHandler inputHandler;
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    int vertical;
    int horizontal;
    public bool canRotate;

    public void Initialize()
    {
      playerStats = GetComponentInParent<PlayerStats>();
      playerManager = GetComponentInParent<PlayerManager>();
      animator = GetComponent<Animator>();
      inputHandler = GetComponentInParent<InputHandler>();
      playerMovement = GetComponentInParent<PlayerMovement>();
      vertical = Animator.StringToHash("Vertical");
      horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {
      float v = 0;

      if (verticalMovement > 0 && verticalMovement < 0.55f)
      {
        v = 0.5f;
      }
      else if (verticalMovement > 0.55f)
      {
        v = 1;
      }
      else if (verticalMovement < 0 && verticalMovement > -0.55f)
      {
        v = -0.5f;
      }
      else if (verticalMovement < -0.55f)
      {
        v = -1;
      }
      else
      {
        v = 0;
      }

      float h = 0;
      if (horizontalMovement > 0 && horizontalMovement < 0.55f)
      {
        h = 0.5f;
      }
      else if (horizontalMovement > 0.55f)
      {
        h = 1;
      }
      else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
      {
        h = -0.5f;
      }
      else if (horizontalMovement < -0.55f)
      {
        h = -1;
      }
      else
      {
        h = 0;
      }

      if (isSprinting)
      {
        v = 2;
        h = horizontalMovement;
      }

      animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
      animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
      if (playerStats.isDead)
        return;
        
      animator.applyRootMotion = isInteracting;
      animator.SetBool("isInteracting", isInteracting);
      animator.CrossFade(targetAnim, 0.2f);
    }

    public void CanRotate()
    {
      canRotate = true;
    }

    public void StopRotation()
    {
      canRotate = false;
    }
    
    private void OnAnimatorMove()
    {
      if (playerManager.isInteracting == false || Time.timeScale <= 0)
        return;

      float delta = Time.deltaTime;
      playerMovement.rigidbody.linearDamping = 0;
      Vector3 deltaPosition = animator.deltaPosition;
      deltaPosition.y = 0;
      Vector3 velocity = deltaPosition / delta;
      // Check for NaN values that can occur on unpause
      Vector3 errorVector = new Vector3(float.NaN, float.NaN, float.NaN);
      if (velocity.Equals(errorVector))
        playerMovement.rigidbody.linearVelocity = Vector3.zero;
      else
        playerMovement.rigidbody.linearVelocity = velocity;
    }
  }
}
