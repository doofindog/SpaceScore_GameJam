using System;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_levelPrefab;
    private void Start()
    {
        GameplayManager.LevelCompleted += SpawnNext;
    }

    private void OnDestroy()
    {
        GameplayManager.LevelCompleted -= SpawnNext;
    }


    private void SpawnNext()
    {
        
    }
}
