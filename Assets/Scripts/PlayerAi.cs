using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAi : MonoBehaviour
{
    [SerializeField] private float m_force = 10f;
    
        [Header("Movement")] 
    [SerializeField] private bool m_isMoving;
    [SerializeField] private float m_minRange;
    [SerializeField] private float m_maxRange;
    [SerializeField] private float m_moveDuration = 2f;

    private void Start()
    {
        GameplayManager.SendUpdate += Move;
    }

    private void OnDestroy()
    {
        GameplayManager.SendUpdate -= Move;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ball"))
            return;

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.right * m_force, ForceMode.Impulse);
    }

    private void Move()
    {
        StartCoroutine(MoveRandomlyTimed());
    }
    
    private IEnumerator MoveRandomlyTimed()
    {
        m_isMoving = true;

        float distance = Random.Range(m_minRange, m_maxRange);
        int direction = Random.value > 0.5f ? 1 : -1;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + transform.forward * distance * direction;

        if (targetPos.z > GameplayManager.Instance.Bounds.z)
        {
            direction = -1; 
        }
        else if (targetPos.z < -GameplayManager.Instance.Bounds.z)
        {
            direction = 1; 
        }

        targetPos = startPos + transform.forward * distance * direction;

        float elapsed = 0f;

        while (elapsed < m_moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / m_moveDuration);

            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        m_isMoving = false;
    }
}
