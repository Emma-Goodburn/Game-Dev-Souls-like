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

    // Poison variables
    private float poisonDuration;
    private float poisonDamage = 5f;

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
      if (poisonDuration > 0)
      {
        poisonDuration -= Time.deltaTime;
        currentHealth -= poisonDamage * Time.deltaTime;
        healthBar.SetCurrentHealth(currentHealth);
        if (poisonDuration <= 0)
        {
          poisonDuration = 0;
          GameObject.Find("Health Bar Fill").GetComponent<UnityEngine.UI.Image>().color = new Color32(204, 11, 11, 255);
        }
        if (currentHealth <= 0)
        {
          currentHealth = 0;
          animatorHandler.PlayTargetAnimation("Death", true);
          isDead = true;
        }
      }
    }

    // Reduce health and trigger damage or death animations
    public void TakeDamage(float damage)
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
    
    // Apply poison to player
    public void ApplyPoison()
    {
      poisonDuration = 5f;
      GameObject.Find("Health Bar Fill").GetComponent<UnityEngine.UI.Image>().color = new Color32(188, 0, 154, 255);
    }
  }
}
