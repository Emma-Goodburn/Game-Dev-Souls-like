using UnityEngine;

namespace Enemy
{
  public class EnemyWeaponManager : MonoBehaviour
  {
    Character.DamageCollider damageCollider;

    private void Start() 
    {
      damageCollider = GetComponentInChildren<Character.DamageCollider>();
    }

    // Enable and disable collider when only one exists (player and wolf boss)
    public void OpenDamageCollider()
    {
      damageCollider.EnableDamageCollider();
    }

    public void CloseDamageCollider()
    {
      damageCollider.DisableDamageCollider();
    }

    // Enable and disable specific colliders when multiple exist (scorpion boss)
    public void OpenLeftClawCollider()
    {
      damageCollider.EnableSpecificDamageCollider("LeftClaw");
    }

    public void OpenRightClawCollider()
    {
      damageCollider.EnableSpecificDamageCollider("RightClaw");
    }

    public void OpenStingerCollider()
    {
      damageCollider.EnableSpecificDamageCollider("Stinger");
    }

    public void OpenBothClawColliders()
    {
      OpenLeftClawCollider();
      OpenRightClawCollider();
    }

    public void CloseLeftClawCollider()
    {
      damageCollider.DisableSpecificDamageCollider("LeftClaw");
    }

    public void CloseRightClawCollider()
    {
      damageCollider.DisableSpecificDamageCollider("RightClaw");
    }

    public void CloseStingerCollider()
    {
      damageCollider.DisableSpecificDamageCollider("Stinger");
    }

    public void CloseBothClawColliders()
    {
      CloseLeftClawCollider();
      CloseRightClawCollider();
    }
  }
}
