using UnityEngine;

namespace Character
{
  public class DamageCollider : MonoBehaviour
  {
    Collider[] damageColliders;

    private void Start()
    {
      damageColliders = GetComponentsInChildren<Collider>();
      foreach (Collider damageCollider in damageColliders)
      {
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
      }
    }

    public void EnableDamageCollider()
    {
      if (damageColliders.Length == 1)
        damageColliders[0].enabled = true;
    }

    public void DisableDamageCollider()
    {
      if (damageColliders.Length == 1)
        damageColliders[0].enabled = false;
    }

    public void EnableSpecificDamageCollider(string tag)
    {
      foreach (Collider damageCollider in damageColliders)
      {
        if (damageCollider.tag == tag)
        {
          damageCollider.enabled = true;
          break;
        }
      }
    }

    public void DisableSpecificDamageCollider(string tag)
    {
      foreach (Collider damageCollider in damageColliders)
      {
        if (damageCollider.tag == tag)
        {
          damageCollider.enabled = false;
          break;
        }
      }
    }
  }
}
