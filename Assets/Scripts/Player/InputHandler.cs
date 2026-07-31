using UnityEngine;

namespace Player
{
  public class InputHandler : MonoBehaviour
  {
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool spaceInput;
    public bool shiftInput;
    public bool leftMouseInput;
    public bool rightMouseInput;
    public bool eInput;

    public bool rollFlag;
    public bool sprintFlag;


    PlayerControls inputActions;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    Scene.ViewManager viewManager;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
      playerAttacker = GetComponent<PlayerAttacker>();
      playerInventory = GetComponent<PlayerInventory>();
      playerManager = GetComponent<PlayerManager>();
      viewManager = GetComponent<Scene.ViewManager>();
    }

    public void OnEnable()
    {
      if (inputActions == null)
      {
        inputActions = new PlayerControls();
        inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
        inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
      }

      inputActions.Enable();
    }

    private void OnDisable()
    {
      inputActions.Disable();
    }

    public void TickInput(float delta)
    {
      HandleMoveInput(delta);
      HandleRollInput(delta);
      HandleSprintInput(delta);
      HandleAttackInput(delta);
      HandleInteractInput(delta);
    }

    private void HandleMoveInput(float delta)
    {
      horizontal = movementInput.x;
      vertical = movementInput.y;
      moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
      mouseX = cameraInput.x;
      mouseY = cameraInput.y;
    }

    private void HandleRollInput(float delta)
    {
      spaceInput = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
      if (spaceInput)
      {
        rollFlag = true;
      }
    }

    private void HandleSprintInput(float delta)
    {
      shiftInput = inputActions.PlayerActions.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
      if (shiftInput)
      {
        sprintFlag = true;
      }
    }

    private void HandleAttackInput(float delta)
    {
      inputActions.PlayerActions.LightAttack.performed += i => leftMouseInput = true;
      inputActions.PlayerActions.HeavyAttack.performed += i => rightMouseInput = true;

      if (leftMouseInput)
      {
        playerAttacker.HandleLightAttack(playerInventory.Weapon);
      }

      if (rightMouseInput)
      {
        playerAttacker.HandleHeavyAttack(playerInventory.Weapon);
      }
    }

    private void HandleInteractInput(float delta)
    {
      eInput = inputActions.PlayerActions.Interact.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
      if (eInput && playerManager.inTrigger)
      {
        viewManager.ChangeScene("Level1");
      }
    }
  }
}
