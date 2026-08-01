using UnityEngine;

namespace Player
{
  public class PlayerManager : MonoBehaviour
  {
    InputHandler inputHandler;
    Animator animator;
    CameraHandler cameraHandler;
    PlayerMovement playerMovement;
    PlayerStats playerStats;


    public bool isInteracting;

    public bool isSprinting;

    public bool inTrigger;

    float timeDead = 0f;

    private void Awake()
    {
      cameraHandler = FindObjectOfType<CameraHandler>();
    }
        
    void Start()
    {
      inputHandler = GetComponent<InputHandler>();
      animator = GetComponentInChildren<Animator>();
      playerMovement = GetComponent<PlayerMovement>();
      playerStats = GetComponent<PlayerStats>();

      // Hide cursor and lock it to the center of the screen
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
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

      if (playerStats.isDead)
      {
        timeDead += delta;
        Scene.ContextualTextManager.Instance.DisplayMessage("Player defeated", false);
        if (timeDead >= 3f)
        {
          UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
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
