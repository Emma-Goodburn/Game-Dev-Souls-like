using UnityEngine;

namespace Character
{
  public class DamageCollider : MonoBehaviour
  {
    Collider[] damageColliders;

    int damage;

    private void Awake()
    {
      damageColliders = GetComponents<Collider>();
      foreach (Collider damageCollider in damageColliders)
      {
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
      }
    }

    public void EnableDamageCollider()
    {
      if (damageColliders.Length == 1)
        damageColliders[0].enabled = true;
    }

    public void DisableDamageCollider()
    {
      if (damageColliders.Length == 1)
        damageColliders[0].enabled = false;
    }

    public void EnableSpecificDamageCollider(string tag)
    {
      foreach (Collider damageCollider in damageColliders)
      {
        if (damageCollider.tag == tag)
        {
          damageCollider.enabled = true;
          break;
        }
      }
    }

    public void DisableSpecificDamageCollider(string tag)
    {
      foreach (Collider damageCollider in damageColliders)
      {
        if (damageCollider.tag == tag)
        {
          damageCollider.enabled = false;
          break;
        }
      }
    }
    
    private void OnTriggerEnter(Collider collision) {
      if (collision.tag == "PlayerTag")
      {
        Player.PlayerStats playerStats = collision.GetComponent<Player.PlayerStats>();
        Enemy.EnemyManager enemyManager = GetComponentInParent<Enemy.EnemyManager>();
        // Find damage for current attack
        damage = enemyManager.currentAttack.damage;
        playerStats?.TakeDamage(damage);
      }

      if (collision.tag == "EnemyTag")
      {
        Player.PlayerStats playerStats = GetComponentInParent<Player.PlayerStats>();
        Enemy.EnemyStats enemyStats = collision.GetComponent<Enemy.EnemyStats>();
        Player.PlayerAttacker playerAttacker = GetComponentInParent<Player.PlayerAttacker>();
        Player.PlayerInventory playerInventory = GetComponentInParent<Player.PlayerInventory>();
        // Asign correct damage value
        if (playerAttacker.isHeavyAttack)
        {
          damage = playerInventory.Weapon.heavyAttackDamage;
        }
        else
        {
          damage = playerInventory.Weapon.lightAttackDamage;
        }
        
        enemyStats?.TakeDamage(damage);
        
        if (!enemyStats.isDead)
          playerStats?.Lifesteal(damage/10);
      }
    }
  }
}
