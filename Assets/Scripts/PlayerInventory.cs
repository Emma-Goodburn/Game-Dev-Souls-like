using UnityEngine;

namespace Player
{
  public class PlayerInventory : MonoBehaviour
  {
    WeaponHolderSlot weaponHolderSlot;

    public WeaponItem Weapon;

    private void Awake()
    {
      weaponHolderSlot = GetComponentInChildren<WeaponHolderSlot>();
    }

    private void Start()
    {
      weaponHolderSlot.LoadWeaponModel(Weapon);
    }
  }
}