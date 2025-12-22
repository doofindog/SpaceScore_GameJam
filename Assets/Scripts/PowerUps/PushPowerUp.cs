using UnityEngine;

public class PushPowerUp : MonoBehaviour
{

    [SerializeField] private float m_pushForce = 20f;

    private bool m_isRotatingRight = true;

    private void Start()
    {
        float m_startingRotation = Random.Range(2f, 70f);
        transform.rotation = Quaternion.Euler(0f, m_startingRotation, 0f);
    }

    private void Update()
    {
        float rotationSpeed = 12f;
        float rotationDirection = 1f;
        float rotationAngle = transform.rotation.eulerAngles.y;
        if (!m_isRotatingRight)
        {
            rotationDirection = -1f;
        }
        if (rotationAngle <= 180f && rotationAngle >= 60f)
        {
            m_isRotatingRight = false;
        }
        else if (rotationAngle >= 180f && rotationAngle <= 325f)
        {
            Debug.Log("PushPowerUp: Update: Rotating right");
            m_isRotatingRight = true;
        }

        Debug.Log("PushPowerUp: Update: Rotation: " + transform.rotation.eulerAngles.y + " " + rotationDirection);
        float step = rotationSpeed * rotationDirection * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + step, 0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * m_pushForce, ForceMode.Impulse);

        }
    }
}
