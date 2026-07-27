using UnityEngine;

namespace Player
{
  public class PlayerAttacker : MonoBehaviour
  {
    AnimatorHandler animatorHandler;
    PlayerStats playerStats;

    private void Awake()
    {
      animatorHandler = GetComponentInChildren<AnimatorHandler>();
      playerStats = GetComponent<PlayerStats>();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
      if (playerStats.staminaEmpty)
        return;
        
      animatorHandler.PlayTargetAnimation(weapon.oneHandedLightAttack, true);
      playerStats.UseStamina(50);
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
      if (playerStats.staminaEmpty)
        return;
        
      animatorHandler.PlayTargetAnimation(weapon.oneHandedHeavyAttack, true);
      playerStats.UseStamina(100);
    }
  }
}
