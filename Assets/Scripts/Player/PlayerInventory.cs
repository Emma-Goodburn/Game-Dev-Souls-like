using UnityEngine;

namespace Player
{
  public class PlayerInventory : MonoBehaviour
  {
    WeaponSlotManager weaponSlotManager;
    public Item.WeaponItem Weapon;

    private void Awake() {
      weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    private void Start()
    {
      weaponSlotManager.LoadWeapon(Weapon);
    }
  }
}