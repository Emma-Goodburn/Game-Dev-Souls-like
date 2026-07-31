using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scene
{
  public class ContextualTextManager : MonoBehaviour
  {
    public static ContextualTextManager Instance;

    [SerializeField] private TMP_Text m_contextualText;

    private bool m_timed = false;
    private float timer = 0;
    private const float c_time = 2;

    private void Awake()
    {
      if (Instance != null && Instance != this)
      {
        Destroy(this);
      }
      else
      {
        Instance = this;
      }
    }

    public void DisplayMessage(string _msg, bool _timed = false)
    {
      if (!m_timed)
      {
        timer = 0;
        m_timed = _timed;
        m_contextualText.text = _msg;
      }
    }

    public void ClearMessage()
    {
      m_contextualText.text = string.Empty;
    }

    private void Update()
    {
      if (m_timed)
      {
        timer += Time.deltaTime;
        if (timer >= c_time)
        {
          ClearMessage();
          timer = 0;
          m_timed = false;
        }
      }
    }
  }
}