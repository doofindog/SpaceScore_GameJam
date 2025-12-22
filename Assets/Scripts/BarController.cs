using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarController : MonoBehaviour
{

    [SerializeField] private bool m_isColliderEnabled = true;
    [SerializeField] private float m_colliderResetDelay = 0.5f;
    [SerializeField] private GameObject m_enemyPrefab;
    [SerializeField] private Transform m_spawnPointParent;

    private float m_colliderTimer;
    void Start()
    {

    }

    void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            BallMotor ballMotor = other.gameObject.GetComponent<BallMotor>();
            bool isBallKicked = other.gameObject.layer == LayerMask.NameToLayer("BallKicked");
            if (ballMotor.isKicked || isBallKicked)
            {
                ballMotor.isKicked = false;
                other.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }

    public void SpawnEnemies(int enemyCount)
    {
        var children = new List<Transform>();
        foreach (Transform child in m_spawnPointParent.transform)
        {
            children.Add(child);
        }
        for (int i = 0; i < enemyCount; i++)
        {
            int randomIndex = Random.Range(0, children.Count);
            Transform spawnPoint = children[randomIndex];
            Debug.Log("Spawning enemy at: " + spawnPoint.name + " " + randomIndex + " " + spawnPoint.position);
            children.Remove(spawnPoint);
            GameObject enemy = Instantiate(m_enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            Vector3 prefabScale = m_enemyPrefab.transform.localScale;
            enemy.transform.SetParent(transform);
            enemy.transform.localScale = prefabScale;
        }
    }
}
