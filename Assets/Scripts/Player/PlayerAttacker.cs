using UnityEngine;

namespace Player
{
  public class PlayerAttacker : MonoBehaviour
  {
    PlayerAnimatorHandler animatorHandler;
    PlayerStats playerStats;

    private void Awake()
    {
      animatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
      playerStats = GetComponent<PlayerStats>();
    }

    public void HandleLightAttack(Item.WeaponItem weapon)
    {
      if (playerStats.currentStamina < 50)
        return;
        
      animatorHandler.PlayTargetAnimation(weapon.oneHandedLightAttack, true);
      playerStats.UseStamina(50);
    }

    public void HandleHeavyAttack(Item.WeaponItem weapon)
    {
      if (playerStats.currentStamina < 100)
        return;

      animatorHandler.PlayTargetAnimation(weapon.oneHandedHeavyAttack, true);
      playerStats.UseStamina(100);
    }
  }
}
