using UnityEngine;

public class ProximityController : MonoBehaviour
{

    private bool m_isCharging = false;
    private GameObject m_ball;

    private void FixedUpdate()
    {
        if (m_isCharging)
        {
            Rigidbody rb = m_ball.GetComponent<Rigidbody>();
            Vector3 direction = (m_ball.transform.position - transform.position);
            direction = new((direction.x + rb.linearVelocity.x) / 10, 0, (direction.z + rb.linearVelocity.z) / 10);
            GetComponent<Rigidbody>().AddForce((direction), ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            m_isCharging = true;
            m_ball = other.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            m_isCharging = false;
            m_ball = null;
        }
    }
}
