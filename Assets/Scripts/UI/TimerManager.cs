using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{

    [SerializeField] private TMP_Text m_timerText;

    private void Update()
    {
        UpdateTimer(GameplayManager.Instance.CurrentTime);
    }

    private void UpdateTimer(float time)
    {
        m_timerText.text = time.ToString("F0");
    }
}
