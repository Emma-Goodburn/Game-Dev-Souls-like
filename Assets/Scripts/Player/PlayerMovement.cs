using UnityEngine;

namespace Player
{
  public class PlayerMovement : MonoBehaviour
  {
    PlayerStats playerStats;
    PlayerManager playerManager;
    Transform cameraObject;
    InputHandler inputHandler;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public PlayerAnimatorHandler animatorHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    // Movement stats
    [SerializeField]
    float movementSpeed = 10;
    [SerializeField]
    float sprintSpeed = 14;
    [SerializeField]
    float rotationSpeed = 10;

    void Start()
    {
      playerStats = GetComponent<PlayerStats>();
      playerManager = GetComponent<PlayerManager>();
      rigidbody = GetComponent<Rigidbody>();
      inputHandler = GetComponent<InputHandler>();
      animatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
      cameraObject = Camera.main.transform;
      myTransform = transform;
      animatorHandler.Initialize();
    }

    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta)
    {
      Vector3 targetDir = Vector3.zero;
      float moveOverride = inputHandler.moveAmount;

      targetDir = cameraObject.forward * inputHandler.vertical;
      targetDir += cameraObject.right * inputHandler.horizontal;

      targetDir.Normalize();
      targetDir.y = 0;

      if (targetDir == Vector3.zero)
        targetDir = myTransform.forward;

      Quaternion targetRotation = Quaternion.LookRotation(targetDir);
      myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * delta);
    }

    public void HandleMovement(float delta)
    {
      if (playerManager.isInteracting)
        return;

      moveDirection = cameraObject.forward * inputHandler.vertical;
      moveDirection += cameraObject.right * inputHandler.horizontal;
      moveDirection.Normalize();
      moveDirection.y = 0;

      float speed = movementSpeed;

      if (inputHandler.sprintFlag && inputHandler.moveAmount > 0.5f && playerStats.currentStamina > 2)
      {
        speed = sprintSpeed;
        playerManager.isSprinting = true;
        // Define sprint cost elsewhere
        playerStats.UseStamina(2);
      }
      else
      {
        playerManager.isSprinting = false;
      }
      moveDirection *= speed;

      Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
      rigidbody.linearVelocity = projectedVelocity;

      animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

      if (animatorHandler.canRotate)
      {
        HandleRotation(delta);
      }
    }
    
    public void HandleRolling(float delta)
    {
      if (playerManager.isInteracting || playerStats.currentStamina < 120)
        return;

      if (inputHandler.rollFlag)
      {
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;

        if (inputHandler.moveAmount > 0)
        {
          animatorHandler.PlayTargetAnimation("Rolling", true);
          moveDirection.y = 0;
          Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
          myTransform.rotation = rollRotation;
          // Define roll cost elsewhere
          playerStats.UseStamina(120);
        }
      }
    }
  }
}
