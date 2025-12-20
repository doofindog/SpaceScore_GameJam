using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_scoreText;
    
    public void Update()
    {
        UpdateScore(GameplayManager.Instance.Score);
    }

    private void UpdateScore(int score)
    {
        m_scoreText.text = score.ToString();
    }
}
