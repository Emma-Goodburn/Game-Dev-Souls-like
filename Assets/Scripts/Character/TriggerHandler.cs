using UnityEngine;

namespace Character
{
  public class TriggerHandler : MonoBehaviour
  {
    int damage;

    private void OnTriggerEnter(Collider collision)
    {
      if (collision.tag == "PlayerTag")
      {
        Player.PlayerStats playerStats = collision.GetComponent<Player.PlayerStats>();
        Enemy.EnemyManager enemyManager = GetComponentInParent<Enemy.EnemyManager>();
        // Find damage for current attack
        damage = enemyManager.currentAttack.damage;
        playerStats?.TakeDamage(damage);
        if (enemyManager.currentAttack.hasPoisonEffect == true)
        {
          playerStats?.ApplyPoison();
        }
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
          playerStats?.Lifesteal(damage / 10);
      }
    }
  }
}
