using System;
using UnityEngine;

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
    [SerializeField] private Vector3 m_bounds;
    [SerializeField] private float m_updateTime;
    private float m_timer;
    
    public static Action SendUpdate; 
    public static GameplayManager Instance;

    public int Score => m_score;
    public Vector3 Bounds => m_bounds * 0.5f;
    
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
        
        if (m_ball.position.y < -5.0f)
        {
            RespawnBall();
        }
    }
    
    private void RespawnBall()
    {
        m_ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        m_ball.position = m_ballSpawnPoint.position;
    }

    private void ProcessNextUpdate()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     SendUpdate?.Invoke();
        // }

        m_timer += Time.deltaTime;
        if (m_timer >= m_updateTime)
        {
            m_timer = 0;
            SendUpdate?.Invoke();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, m_bounds);
    }
}
