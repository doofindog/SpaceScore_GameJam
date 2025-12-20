using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Transform m_ball;
    [SerializeField] private Transform m_ballSpawnPoint;
    [SerializeField] private int m_goalAScore = 1;
    [SerializeField] private int m_goalBScore = 2;
    [SerializeField] private int m_score = 3;
    public static GameplayManager Instance;

    public int Score => m_score;
    
    private void Awake()
    {
        Instance = this;
        RespawnBall();
    }
    
    public void OnGoalScored(string goalName)
    {
        if (goalName == "GoalA")
        {
            m_score += m_goalAScore;
        }

        if (goalName == "GoalB")
        {
            m_score += m_goalBScore;
        }
        
        RespawnBall();
    }

    private void RespawnBall()
    {
        m_ball.position = m_ballSpawnPoint.position;
    }
}
