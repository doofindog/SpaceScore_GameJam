using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public Transform ballSpawnPoint;
    public CinemachineVirtualCameraBase cameraBase;

    public enum LevelType
    {
        Proximity,
        Powerup,
        Chase,
        Puzzle,
        Shoot,
        Boss
    }

    [SerializeField] public LevelType m_levelType;
    [SerializeField] public int m_levelDifficulty = 1;
    [SerializeField] public int m_powerUpCount = 2;
    [SerializeField] public bool m_isLevelActive = true;


    [Header("Level Prefabs")]
    [SerializeField] private GameObject m_spawnPointParent;
    [SerializeField] private GameObject m_proximityEnemyPrefab;
    [SerializeField] private List<GameObject> m_powerUpPrefabs;


    public List<Transform> m_internalSpawnPoints;
    public List<GameObject> m_proximityEnemies;

    public void Awake()
    {
        m_proximityEnemies = new List<GameObject>();
    }

    public void DeactivateLevel()
    {
        m_isLevelActive = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (GameObject enemies in m_proximityEnemies)
        {
            enemies.SetActive(false);
        }
    }


    public void PrepareLevel()
    {
        int enemyCount = Mathf.Min(5, (m_levelDifficulty / 3) + 1);
        BarController barController = GetComponentInChildren<BarController>();
        barController.SpawnEnemies(enemyCount);
        m_internalSpawnPoints = new List<Transform>();
        foreach (Transform child in m_spawnPointParent.transform)
        {
            m_internalSpawnPoints.Add(child);
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2);
     
        m_levelType = (LevelType)Random.Range(0, Enum.GetValues(typeof(LevelType)).Length);
        switch (m_levelType)
        {
            case LevelType.Proximity:
                SpawnProximityLevel();
                break;
            default:
                SpawnProximityLevel();
                break;
        }

        for (int i = 0; i < m_powerUpCount; i++)
        {
            SpawnPowerUp();
        }
        
    }

    private Transform GetRandomSpawnPoint(bool remove = true)
    {
        int randomIndex = Random.Range(0, m_internalSpawnPoints.Count);
        Transform spawnPoint = m_internalSpawnPoints[randomIndex];
        if (remove)
        {
            m_internalSpawnPoints.Remove(spawnPoint);
        }
        return spawnPoint;
    }

    private void SpawnProximityLevel()
    {
        m_levelDifficulty = GameplayManager.Instance.levelDifficulty;
        int enemyCount = Random.Range(1, m_levelDifficulty + 1);
        enemyCount = Mathf.Clamp(enemyCount, 1, 6);
        Debug.Log("enemy Count" + enemyCount);
        
        for (int i = 0; i < enemyCount; i++)
        {
            if (i != 0 && Random.Range(0.0f, 100.0f) > 30.0f )
            {
                continue;
            }
            
            Transform spawnPoint = GetRandomSpawnPoint(remove: true);
            GameObject enemy = Instantiate(m_proximityEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            m_proximityEnemies.Add(enemy);
        }
    }

    private void SpawnPowerUp()
    {
        Transform spawnPoint = GetRandomSpawnPoint(remove: true);
        GameObject powerUp = Instantiate(m_powerUpPrefabs[Random.Range(0, m_powerUpPrefabs.Count)], spawnPoint.position, spawnPoint.rotation);
    }
}
