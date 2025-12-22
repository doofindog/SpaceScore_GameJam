using System;
using System.Collections.Generic;
using Unity.Cinemachine;
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


    [Header("Level Prefabs")]
    [SerializeField] private GameObject m_spawnPointParent;
    [SerializeField] private GameObject m_proximityEnemyPrefab;


    public void PrepareLevel()
    {
        int enemyCount = Mathf.Min(5, (m_levelDifficulty / 3) + 1);
        BarController barController = GetComponentInChildren<BarController>();
        barController.SpawnEnemies(enemyCount);

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
    }

    private void SpawnProximityLevel()
    {
        var children = new List<Transform>();
        foreach (Transform child in m_spawnPointParent.transform)
        {
            children.Add(child);
        }

        int enemyCount = Random.Range(1, m_levelDifficulty + 1);
        for (int i = 0; i < enemyCount; i++)
        {
            int randomIndex = Random.Range(0, children.Count);
            Transform spawnPoint = children[randomIndex];
            children.Remove(spawnPoint);
            GameObject enemy = Instantiate(m_proximityEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
