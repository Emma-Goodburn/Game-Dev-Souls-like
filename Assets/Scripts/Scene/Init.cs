using UnityEngine;

namespace Scene
{
  public class Init : MonoBehaviour
  {
    void Start()
    {
      gameObject.GetComponent<ViewManager>().LoadScene("Tutorial");
    }
  }
}
