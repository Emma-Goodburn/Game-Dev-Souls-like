using UnityEngine;

namespace Player
{
  public class DamageCollider : MonoBehaviour
  {
    Collider damageCollider;

    public int currentWeaponDamage = 10;

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
        playerStats?.TakeDamage(currentWeaponDamage);
      }

      if (collision.tag == "EnemyTag")
      {
        PlayerStats playerStats = GetComponentInParent<PlayerStats>();
        Enemy.EnemyStats enemyStats = collision.GetComponent<Enemy.EnemyStats>();
        enemyStats?.TakeDamage(currentWeaponDamage);
        
        if (!enemyStats.isDead)
          playerStats?.Lifesteal(currentWeaponDamage/10);
      }
    }
  }
}
