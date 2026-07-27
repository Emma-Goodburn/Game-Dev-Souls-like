using UnityEngine;

namespace Player
{
  public class DamageCollider : MonoBehaviour
  {
    Collider damageCollider;

    public int currentWeaponDamage = 25;

    private void Awake()
    {
      damageCollider = GetComponent<Collider>();
      damageCollider.gameObject.SetActive(true);
      damageCollider.isTrigger = true;
      damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
      damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
      damageCollider.enabled = false;
    }
    
    private void OnTriggerEnter(Collider collision) {
      if (collision.tag == "PlayerTag")
      {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
          playerStats.TakeDamage(currentWeaponDamage);
        }
      }

      if (collision.tag == "EnemyTag")
      {
        EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

        if (enemyStats != null)
        {
          enemyStats.TakeDamage(currentWeaponDamage);
        }
      }
    }
  }
}
