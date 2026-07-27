using UnityEngine;

namespace Player
{
  public class EnemyStats : MonoBehaviour
  {
    public int maxHealth = 100;
    public int currentHealth;
    public bool enemyIsDead;

    Animator animator;

    private void Awake()
    {
      animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
      currentHealth = maxHealth;
      enemyIsDead = false;
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
        enemyIsDead = true;
      }
    }
  }
}
