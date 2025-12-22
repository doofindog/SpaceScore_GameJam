using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(Rigidbody))]
public class BallMotor : MonoBehaviour
{
    public enum MovementType
    {
        Free,
        Aim
    }

    public bool isKicked;

    [SerializeField] private float m_kickResetTime = 0.5f;
    [SerializeField] private MovementType m_movementType = MovementType.Free;
    [SerializeField] private float m_moveForce = 20f;
    [SerializeField] private float m_maxSpeed = 8f;
    [SerializeField] public float m_movementMultiplier = 1f;

    private float m_timer;
    private Rigidbody m_rb;
    [SerializeField] private Vector3 m_input;
    [SerializeField] private Vector3 m_mouseDown;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.freezeRotation = false; // Allow rolling
    }

    private void Update()
    {
        if (isKicked)
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_kickResetTime)
            {
                m_timer = 0;
                isKicked = false;
            }

            return;
        }

        if (m_movementType == MovementType.Free)
        {
            float h = Input.GetAxisRaw("Horizontal"); // A / D
            float v = Input.GetAxisRaw("Vertical");   // W / S
            m_input = new Vector3(h, 0f, v).normalized;
        }

        if (m_movementType == MovementType.Aim)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseInput = Input.mousePosition;
                mouseInput.z = 10f;
                m_mouseDown = Camera.main.ScreenToWorldPoint(mouseInput);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mouseInput = Input.mousePosition;
                mouseInput.z = 10f;
                m_input = Camera.main.ScreenToWorldPoint(mouseInput) - m_mouseDown;
                m_input.x *= -1;
                m_input.z *= -1;
                m_input.Normalize();
                m_mouseDown = Vector3.zero;
            }
        }
    }

    private void FixedUpdate()
    {

        if (isKicked)
        {
            return;
        }

        if (m_movementType == MovementType.Free)
        {
            if (m_input.sqrMagnitude > 0f)
            {
                m_rb.AddForce(m_input * m_moveForce * m_movementMultiplier, ForceMode.Force);
            }

            // Optional: clamp max speed so it doesn't go crazy
        }

        if (m_movementType == MovementType.Aim)
        {
            if (m_input.sqrMagnitude > 0f)
            {
                m_rb.AddForce(m_input * m_moveForce * m_movementMultiplier, ForceMode.Impulse);
                Debug.Log("BallMotor: FixedUpdate: m_movementMultiplier: " + m_movementMultiplier);
                m_input = Vector3.zero;
            }
        }


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
