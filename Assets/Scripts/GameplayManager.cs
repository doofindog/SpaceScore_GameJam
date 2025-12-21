using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
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
    [SerializeField] private Vector3 m_bounds;
    [SerializeField] private float m_updateTime;

    [SerializeField] private GameObject m_ballPrefab;
    [SerializeField] private GameObject m_levelPrefab;
    [SerializeField] private List<Level> m_levels;
    [SerializeField] private Level m_currentLevel;
    
    private float m_timer;

    public static Action LevelCompleted;
    public static Action SendUpdate; 
    public static GameplayManager Instance;
    

    public int Score => m_score;
    public Vector3 Bounds => m_bounds * 0.5f;
    
    private void Awake()
    { 
        LevelCompleted += OnLevelComplete;
        
        Instance = this;
        SpawnLevel();
        RespawnBall();
    }

    private void OnDestroy()
    {
        LevelCompleted -= OnLevelComplete;
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
        
        if (m_ball != null && m_ball.position.y < -5.0f)
        {
            RespawnBall();
        }
    }
    
    private void RespawnBall()
    {
        if (m_ball == null)
        {
            m_ball = Instantiate(m_ball);
        }
        m_ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        m_ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
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

    private void OnLevelComplete()
    {
        SpawnLevel();
        RespawnBall();
    }

    private void SpawnLevel()
    {
        Vector3 spawnPosition = Vector3.zero;
        if (m_currentLevel != null)
        {
            spawnPosition = m_currentLevel.transform.position + transform.forward * 37;
            m_currentLevel.cameraBase.gameObject.SetActive(false);
        }

        var obj = Instantiate(m_levelPrefab, spawnPosition, Quaternion.identity);
        Level level = obj.GetComponent<Level>();
        
        //TODO : Remove this is temp
        obj.GetComponent<MeshRenderer>().material.color =
            new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        
        m_currentLevel = level;
        m_ballSpawnPoint = level.ballSpawnPoint;
    }
}
