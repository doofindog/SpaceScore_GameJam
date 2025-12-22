using System;
using UnityEngine;
using Random = System.Random;

public class ProximityController : MonoBehaviour
{

    [SerializeField] private float m_speed = 0.33f;

    private bool m_isCharging = false;
    [SerializeField] private GameObject m_ball;

    private void Start()
    {
        m_speed = UnityEngine.Random.Range(0.3f, 1.0f);
        m_ball = GameplayManager.Instance.Ball.gameObject;
    }

    private void Update()
    {
        // if (m_isCharging)
        // {
        //     float step = m_speed * Time.fixedDeltaTime;
        //     transform.position = Vector3.MoveTowards(transform.position, m_ball.transform.position, step);
        // }
        // else
        // {
        //     Rigidbody rb = GetComponent<Rigidbody>();
        //     rb.linearVelocity = Vector3.zero;
        // }

        if (m_ball == null)
        {
            m_ball = GameplayManager.Instance.Ball.gameObject;
            return;
        }
        
        float step = m_speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_ball.transform.position, step);
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
