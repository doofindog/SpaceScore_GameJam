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
      m_levelType = (LevelType)Random.Range(0, Enum.GetValues(typeof(LevelType)).Length);
      switch (m_levelType)
      {
        case LevelType.Proximity:
          spawnProximityLevel();
          break;
        default:
           spawnProximityLevel();
          break;
      }
   }

   private void spawnProximityLevel()
   {
      // GameObject spawnPointParent = Instantiate(m_spawnPointParent, transform);
      var children = new List<Transform>();
      foreach (Transform child in m_spawnPointParent.transform)
      {
        Debug.Log("Adding spawn point: " + child.name + " " + child.position);
        children.Add(child);
      }

      // randomly select a spawn point
      int enemyCount = Random.Range(1, m_levelDifficulty + 1);
      for (int i = 0; i < enemyCount; i++)
      {
        int randomIndex = Random.Range(0, children.Count);
        Transform spawnPoint = children[randomIndex];
        Debug.Log("Spawning enemy at: " + spawnPoint.name + " " + randomIndex + " " + spawnPoint.position);
        children.Remove(spawnPoint);
        GameObject enemy = Instantiate(m_proximityEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
      }
   }
}
