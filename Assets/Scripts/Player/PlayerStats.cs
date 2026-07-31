using System.Collections;
using UnityEngine;

namespace Player
{
  public class PlayerStats : Character.CharacterStats
  {
    // Player info
    public int maxStamina;
    public int currentStamina;

    // Stamina regen
    private float currentStaminaRecoveryTime;
    public int staminaRegenRate;
    public float staminaRegenDelay;

    // Health and stamina bar objects
    public HealthBar healthBar;
    public StaminaBar staminaBar;

    // Stamina costs
    public int rollCost = 120;
    public int sprintCost = 1;
    public int lightAttackCost = 50;
    public int heavyAttackCost = 100;


    // Required classes
    PlayerAnimatorHandler animatorHandler;

    private void Awake()
    {
      animatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
    }

    // Reset variables
    void Start()
    {
      currentHealth = maxHealth;
      healthBar.SetMaxHealth(maxHealth);
      isDead = false;

      currentStamina = maxStamina;
      staminaBar.SetMaxStamina(maxStamina);
    }

    // Regen stamina unless regen disabled
    private void Update()
    {
      if (currentStaminaRecoveryTime <= 0 && currentStamina < maxStamina)
      {
        currentStamina += staminaRegenRate;
        staminaBar.SetCurrentStamina(currentStamina);
      }
      if (currentStaminaRecoveryTime > 0)
      {
        currentStaminaRecoveryTime -= Time.deltaTime;
      }
    }

    // Reduce health and trigger damage or death animations
    public void TakeDamage(int damage)
    {
      currentHealth -= damage;
      healthBar.SetCurrentHealth(currentHealth);

      if (currentHealth > 0)
        animatorHandler.PlayTargetAnimation("Damage", true);

      if (currentHealth <= 0)
      {
        currentHealth = 0;
        animatorHandler.PlayTargetAnimation("Death", true);
        isDead = true;
      }
    }

    // Reduce total stamina and pause regen
    public void UseStamina(int cost)
    {
      currentStamina -= cost;
      staminaBar.SetCurrentStamina(currentStamina);
      currentStaminaRecoveryTime = staminaRegenDelay;
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
