using System;
using UnityEngine;

public class LevelCompleteCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ball"))
            return;
        
        GameplayManager.LevelCompleted?.Invoke();
    }
}
