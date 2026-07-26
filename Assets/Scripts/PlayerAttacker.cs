using UnityEngine;

namespace Player
{
  public class PlayerAttacker : MonoBehaviour
  {
    AnimatorHandler animatorHandler;

    private void Awake()
    {
      animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
      animatorHandler.PlayTargetAnimation(weapon.oneHandedLightAttack, true);
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
      animatorHandler.PlayTargetAnimation(weapon.oneHandedHeavyAttack, true);
    }
  }
}
