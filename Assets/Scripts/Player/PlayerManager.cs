using UnityEngine;

namespace Player
{
  public class PlayerManager : MonoBehaviour
  {
    InputHandler inputHandler;
    Animator animator;
    CameraHandler cameraHandler;
    PlayerMovement playerMovement;


    public bool isInteracting;

    public bool isSprinting;

    private void Awake()
    {
      cameraHandler = FindObjectOfType<CameraHandler>();
    }
        
    void Start()
    {
      inputHandler = GetComponent<InputHandler>();
      animator = GetComponentInChildren<Animator>();
      playerMovement = GetComponent<PlayerMovement>();
    }


    void Update()
    {
      float delta = Time.deltaTime;

      isInteracting = animator.GetBool("isInteracting");
      inputHandler.TickInput(delta);
      playerMovement.HandleMovement(delta);
      playerMovement.HandleRolling(delta);
    }

    private void FixedUpdate()
    {
      float delta = Time.fixedDeltaTime;

      if (cameraHandler != null)
      {
        cameraHandler.FollowTarget(delta);
        cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
      }
    }

    private void LateUpdate()
    {
      inputHandler.rollFlag = false;
      inputHandler.sprintFlag = false;
      inputHandler.leftMouseInput = false;
      inputHandler.rightMouseInput = false;
    }

  }
}
