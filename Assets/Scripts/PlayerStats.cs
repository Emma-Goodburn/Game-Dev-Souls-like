using UnityEngine;

namespace Player
{
  public class PlayerStats : MonoBehaviour
  {
    public int maxHealth;
    public int maxStamina;
    public int currentHealth;
    public int currentStamina;
    public bool playerIsDead;
    public bool staminaEmpty;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    AnimatorHandler animatorHandler;

    private void Awake()
    {
      animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    void Start()
    {
      currentHealth = maxHealth;
      healthBar.SetMaxHealth(maxHealth);
      playerIsDead = false;

      currentStamina = maxStamina;
      staminaBar.SetMaxStamina(maxStamina);
      staminaEmpty = false;
    }

    public void TakeDamage(int damage)
    {
      currentHealth -= damage;
      healthBar.SetCurrentHealth(currentHealth);
      animatorHandler.PlayTargetAnimation("Damage", true);

      if (currentHealth <= 0)
      {
        currentHealth = 0;
        animatorHandler.PlayTargetAnimation("Death", true);
        playerIsDead = true;
      }
    }

    public void UseStamina(int cost)
    {
      currentStamina -= cost;
      staminaBar.SetCurrentStamina(currentStamina);

      if (currentStamina <= 0)
      {
        currentStamina = 0;
        staminaEmpty = true;
      }
    }
  }
}
