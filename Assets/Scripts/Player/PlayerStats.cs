using System.Collections;
using UnityEngine;

namespace Player
{
  public class PlayerStats : MonoBehaviour
  {
    // Player info
    public int maxHealth;
    public int maxStamina;
    public int currentHealth;
    public int currentStamina;

    // Lock out variables
    public bool playerIsDead;

    // Stamina regen
    private bool allowStaminaRegen;
    public int staminaRegenRate;
    public float staminaRegenDelay;

    // Health and stamina bar objects
    public HealthBar healthBar;
    public StaminaBar staminaBar;

    // Required classes
    AnimatorHandler animatorHandler;

    private void Awake()
    {
      animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    // Reset variables
    void Start()
    {
      currentHealth = maxHealth;
      healthBar.SetMaxHealth(maxHealth);
      playerIsDead = false;

      currentStamina = maxStamina;
      staminaBar.SetMaxStamina(maxStamina);
    }

    // Regen stamina unless regen disabled
    private void Update()
    {
      if (allowStaminaRegen && currentStamina < maxStamina)
      {
        currentStamina += staminaRegenRate;
        staminaBar.SetCurrentStamina(currentStamina);
      }
    }

    // Reduce health and trigger damage or death animations
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

    // Reduce total stamina and pause regen
    public void UseStamina(int cost)
    {
      currentStamina -= cost;
      staminaBar.SetCurrentStamina(currentStamina);
      allowStaminaRegen = false;
      StartCoroutine(PauseStaminaRegen(staminaRegenDelay));
    }

    // Prevent stamina regen until delayTime has passed
    private IEnumerator PauseStaminaRegen(float delayTime)
    {
      yield return new WaitForSeconds(delayTime);

      allowStaminaRegen = true;
    }
    
    // Heal player on damage dealt
    public void Lifesteal(int damage)
    {
      if (currentHealth < maxHealth)
      {
        currentHealth += damage;
        healthBar.SetCurrentHealth(currentHealth);
      }
    }
  }
}
