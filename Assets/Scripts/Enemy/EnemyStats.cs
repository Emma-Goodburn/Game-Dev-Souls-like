using UnityEngine;

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
    public void TakeDamage(int damage)
    {
      currentHealth -= damage;
      healthBar.SetCurrentHealth(currentHealth);
      
      if (currentHealth > 0)
        animator.Play("GetHit");

      if (currentHealth <= 0)
      {
        currentHealth = 0;
        animator.Play("Dead");
        isDead = true;
      }
    }
  }
}
