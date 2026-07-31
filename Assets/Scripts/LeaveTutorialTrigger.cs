using UnityEngine;

namespace Scene
{
  public class LeaveTutorialTrigger : MonoBehaviour
  {
    Player.PlayerManager playerManager;

    private void Awake() {
      playerManager = GameObject.FindWithTag("PlayerTag").GetComponent<Player.PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
      ContextualTextManager.Instance.DisplayMessage("Press E to exit tutorial", false);
      playerManager.inTrigger = true;
    }
    
    private void OnTriggerExit(Collider other) {
      ContextualTextManager.Instance.ClearMessage();
      playerManager.inTrigger = false;
    }
  }
}
