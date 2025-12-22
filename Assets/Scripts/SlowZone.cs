using UnityEngine;

public class SlowZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<BallMotor>().m_movementMultiplier = 0.1f;
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
