using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnChance : MonoBehaviour
{
    private void Awake()
    {
        if (Random.Range(0.0f, 1.0f) > 0.3f)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
