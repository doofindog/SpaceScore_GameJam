using UnityEngine;
using TMPro;

public class TimerPowerUp : MonoBehaviour
{
    [SerializeField] private float m_timerIncrease = 5f;
    [SerializeField] private TMP_Text m_timerText;

    private void Start()
    {
        m_timerIncrease = Random.Range(5, 10);
        m_timerText.text = "+" + m_timerIncrease.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameplayManager.Instance.AddTime(m_timerIncrease);
            Destroy(gameObject);
        }
    }
}
