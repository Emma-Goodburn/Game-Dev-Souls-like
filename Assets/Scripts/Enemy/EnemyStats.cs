using UnityEngine;

namespace Enemy
{
  public class EnemyStats : Character.CharacterStats
  {
    Animator animator;

    private void Awake()
    {
      animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
      currentHealth = maxHealth;
      isDead = false;
    }

    public void TakeDamage(int damage)
    {
      currentHealth -= damage;
      if (currentHealth > 0)
        animator.Play("Damage");

      if (currentHealth <= 0)
      {
        currentHealth = 0;
        animator.Play("Death");
        isDead = true;
      }
    }
  }
}
