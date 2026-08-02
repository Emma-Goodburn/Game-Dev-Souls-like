using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemy
{
  public class EnemyStats : Character.CharacterStats
  {
    Animator animator;

    // Boss health bar
    public HealthBar healthBar;

    private void Awake()
    {
      animator = GetComponentInChildren<Animator>();
    }

    // Reset variables
    void Start()
    {
      currentHealth = maxHealth;
      healthBar.SetMaxHealth(maxHealth);
      isDead = false;
    }

    // Reduce health and trigger damage or death animations
    public void TakeDamage(float damage)
    {
      currentHealth -= damage;
      healthBar.SetCurrentHealth(currentHealth);
      
      // No get hit animation for scorpion boss in level 2
      if (currentHealth > 0 && SceneManager.GetActiveScene().name == "Level2")
        animator.Play("GetHit");

      // Trigger death animation and set isDead to true
      if (currentHealth <= 0)
      {
        currentHealth = 0;
        animator.Play("Dead");
        isDead = true;
      }
    }
  }
}
