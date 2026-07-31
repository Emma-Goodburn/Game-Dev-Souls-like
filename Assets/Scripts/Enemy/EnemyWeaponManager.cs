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

    public void OpenDamageCollider()
    {
      damageCollider.EnableDamageCollider();
    }

    public void CloseDamageCollider()
    {
      damageCollider.DisableDamageCollider();
    }
  }
}
