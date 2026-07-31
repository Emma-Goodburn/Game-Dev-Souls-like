using UnityEngine;

namespace Player
{
  public class WeaponSlotManager : MonoBehaviour
  {
    WeaponHolderSlot weaponHolderSlot;

    Character.DamageCollider damageCollider;

    private void Awake()
    {
      weaponHolderSlot = GetComponentInChildren<WeaponHolderSlot>();
    }
    public void LoadWeapon(Item.WeaponItem weaponItem)
    {
      weaponHolderSlot.LoadWeaponModel(weaponItem);
      LoadWeaponDamageCollider();
    }
    
    private void LoadWeaponDamageCollider()
    {
      damageCollider = weaponHolderSlot.currentWeaponModel.GetComponentInChildren<Character.DamageCollider>();
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
