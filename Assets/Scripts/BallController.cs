using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float m_moveForce = 20f;
    [SerializeField] private float m_maxSpeed = 8f;

    private Rigidbody m_rb;
    private Vector3 m_input;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.freezeRotation = false; // Allow rolling
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A / D
        float v = Input.GetAxisRaw("Vertical");   // W / S

        m_input = new Vector3(h, 0f, v).normalized;
    }

    private void FixedUpdate()
    {
        if (m_input.sqrMagnitude > 0f)
        {
            m_rb.AddForce(m_input * m_moveForce, ForceMode.Force);
        }

        // Optional: clamp max speed so it doesn't go crazy
        Vector3 flatVelocity = new Vector3(m_rb.linearVelocity.x, 0f, m_rb.linearVelocity.z);
        if (flatVelocity.magnitude > m_maxSpeed)
        {
            Vector3 limited = flatVelocity.normalized * m_maxSpeed;
            m_rb.linearVelocity = new Vector3(limited.x, m_rb.linearVelocity.y, limited.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameplayManager.Instance.OnGoalScored(collision.gameObject.name);
        }
    }
}
