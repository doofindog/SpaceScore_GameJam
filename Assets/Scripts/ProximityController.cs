using UnityEngine;

public class ProximityController : MonoBehaviour
{

    [SerializeField] private float m_speed = 0.33f;

    private bool m_isCharging = false;
    private GameObject m_ball;

    private void FixedUpdate()
    {
        if (m_isCharging)
        {
            float step = m_speed * Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, m_ball.transform.position, step);
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
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
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}
