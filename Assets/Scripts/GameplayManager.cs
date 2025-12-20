using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    private enum MovementType
    {
        Side,
        Free
    }
    
    [SerializeField] private Transform m_ball;
    [SerializeField] private Transform m_ballSpawnPoint;
    [SerializeField] private int m_goalAScore = 1;
    [SerializeField] private int m_goalBScore = 2;
    [SerializeField] private int m_score = 3;

    public static Action SendUpdate; 
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

    private void Update()
    {
        ProcessNextUpdate();    
    }
    
    private void RespawnBall()
    {
        m_ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        m_ball.position = m_ballSpawnPoint.position;
    }

    private void ProcessNextUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendUpdate?.Invoke();
        }
    }
    
    
   
}
