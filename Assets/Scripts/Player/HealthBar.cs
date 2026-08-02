using UnityEngine;
using UnityEngine.UI;

namespace Player
{
  public class HealthBar : MonoBehaviour
  {
    public Slider slider;

    public void SetMaxHealth(int maxHealth)
    {
      slider.maxValue = maxHealth;
      slider.value = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
      slider.value = currentHealth;
    }
  }
}
