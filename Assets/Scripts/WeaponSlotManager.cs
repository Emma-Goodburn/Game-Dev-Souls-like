using UnityEngine;

namespace Player
{
  public class WeaponSlotManager : MonoBehaviour
  {
    WeaponHolderSlot weaponHolderSlot;

    DamageCollider damageCollider;

    private void Awake()
    {
      weaponHolderSlot = GetComponentInChildren<WeaponHolderSlot>();
    }
    public void LoadWeapon(WeaponItem weaponItem)
    {
      weaponHolderSlot.LoadWeaponModel(weaponItem);
      LoadWeaponDamageCollider();
    }
    
    private void LoadWeaponDamageCollider()
    {
      damageCollider = weaponHolderSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
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
