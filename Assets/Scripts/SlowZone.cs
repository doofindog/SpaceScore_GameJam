using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlowZone : MonoBehaviour
{
    private float m_slowSize;

    private void Start()
    {
        float size = Random.Range(7, 12);
        transform.localScale = new Vector3(size, size, size);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<BallMotor>().m_movementMultiplier = 0.25f;
            Debug.Log("SlowZone: OnTriggerEnter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<BallMotor>().m_movementMultiplier = 1f;
            Debug.Log("SlowZone: OnTriggerExit");
        }
    }
}
