using UnityEngine;
using UnityEngine.UI;

namespace Player
{
  public class StaminaBar : MonoBehaviour
  {
    public Slider slider;

    public void SetMaxStamina(int maxStamina)
    {
      slider.maxValue = maxStamina;
      slider.value = maxStamina;
    }

    public void SetCurrentStamina(int currentStamina)
    {
      slider.value = currentStamina;
    }
  }
}
