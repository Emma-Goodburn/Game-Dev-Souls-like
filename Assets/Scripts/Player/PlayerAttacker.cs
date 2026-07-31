using UnityEngine;

namespace Player
{
  public class PlayerAttacker : MonoBehaviour
  {
    PlayerAnimatorHandler animatorHandler;
    PlayerStats playerStats;

    public bool isHeavyAttack = false;

    private void Awake()
    {
      animatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
      playerStats = GetComponent<PlayerStats>();
    }

    public void HandleLightAttack(Item.WeaponItem weapon)
    {
      if (playerStats.currentStamina < 50)
        return;

      isHeavyAttack = false;
      animatorHandler.PlayTargetAnimation(weapon.oneHandedLightAttack, true);
      playerStats.UseStamina(playerStats.lightAttackCost);
    }

    public void HandleHeavyAttack(Item.WeaponItem weapon)
    {
      if (playerStats.currentStamina < 100)
        return;

      isHeavyAttack = true;
      animatorHandler.PlayTargetAnimation(weapon.oneHandedHeavyAttack, true);
      playerStats.UseStamina(playerStats.heavyAttackCost);
    }
  }
}
