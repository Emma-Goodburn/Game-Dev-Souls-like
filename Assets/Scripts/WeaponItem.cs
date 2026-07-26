using UnityEngine;

namespace Player
{
  [CreateAssetMenu(menuName = "Items/Weapon Item")]
  public class WeaponItem : Item
  {
    public GameObject modelPrefab;
    public bool isUnarmed;

    public string oneHandedLightAttack;
    public string oneHandedHeavyAttack;
  }
}
