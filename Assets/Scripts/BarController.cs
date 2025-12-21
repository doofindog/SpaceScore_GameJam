using UnityEngine;

public class BarController : MonoBehaviour
{

    [SerializeField] private bool m_isColliderEnabled = true;
    [SerializeField] private float m_colliderResetDelay = 0.5f;
    private float m_colliderTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
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
}
