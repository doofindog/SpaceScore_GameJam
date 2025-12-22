using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAi : MonoBehaviour
{
    public enum EnemyType
    {
        Blue,
        Red
    }

    [SerializeField] private Transform m_sprite;
    [SerializeField] private float m_force = 10f;
    [SerializeField] private EnemyType m_enemyType;

    [Header("Movement")]
    [SerializeField] private bool m_isMoving;
    [SerializeField] private bool m_isSpinning;
    [SerializeField] private float m_minRange;
    [SerializeField] private float m_maxRange;
    [SerializeField] private float m_moveDuration = 2f;
    [SerializeField] private float m_spinInterval = 3f;
    [SerializeField] private float m_spinSpeed = 100f;
    [SerializeField] private float m_spinDuration = 0.3f;
    [SerializeField] private Vector3 m_kickDirection;
    [SerializeField] private float m_kickForce;

    [Header("Sprites")]
    [SerializeField] private GameObject m_IndicatorPrefab;

    private GameObject m_indicator;
    private bool m_isChasingBall = false;
    private GameObject m_ball;

    private void Start()
    {
        GameplayManager.SendUpdate += Move;
        GenerateKickDirectionAndSprite();
    }

    private void FixedUpdate()
    {
        if (m_isChasingBall)
        {
            ChaseBall();
        }
    }

    private void GenerateKickDirectionAndSprite()
    {
        Collider collider = GetComponent<Collider>();
        float colliderWidth = collider.bounds.extents.x;
        m_kickForce = Random.Range(60f, 100f);

        m_kickDirection = new(1, 0f, Random.Range(-1f, 1f));


        float indicatorOffset = colliderWidth * 2.5f;
        if (m_enemyType == EnemyType.Red)
        {
            m_kickDirection.x *= -1;
            indicatorOffset = -colliderWidth * 2.5f;
        }

        Vector3 dir = new Vector3(m_kickDirection.x, 0f, m_kickDirection.z).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        Vector3 indicatorPosition = transform.position + Vector3.down * 0.62f;
        Quaternion indicatorRotation = Quaternion.Euler(0f, angle, 0f);

        if (m_indicator == null)
        {
            m_indicator = Instantiate(m_IndicatorPrefab);
        }

        m_indicator.transform.position = indicatorPosition;
        m_indicator.transform.rotation = indicatorRotation;
        m_indicator.SetActive(true);
    }

    private void OnDestroy()
    {
        GameplayManager.SendUpdate -= Move;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ball"))
            return;
        Debug.Log("OnTriggerEnter: " + other.gameObject.name);
        BallMotor ballMotor = other.gameObject.GetComponent<BallMotor>();
        ballMotor.isKicked = true;
        KickBall(other.gameObject);
    }

    public void KickBall(GameObject ball)
    {
        BallMotor ballMotor = ball.GetComponent<BallMotor>();
        ballMotor.isKicked = true;

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(m_kickDirection.normalized * m_kickForce, ForceMode.Impulse);
    }

    private void Move()
    {
        StartCoroutine(MoveRandomlyTimed());
    }

    private IEnumerator Spin()
    {
        m_indicator.SetActive(false);
        m_isSpinning = true;
        float startRotationAngle = transform.rotation.eulerAngles.z;
        float targetRotationAngle = startRotationAngle + 360f;
        float elapsed = 0f;

        while (elapsed < m_spinDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / m_spinDuration);
            float currentRotationAngle = Mathf.Lerp(startRotationAngle, targetRotationAngle, t);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotationAngle);
            yield return null;
        }

        m_isSpinning = false;
    }

    private IEnumerator MoveRandomlyTimed()
    {
        yield break;
        yield return StartCoroutine(Spin());
        m_isMoving = true;

        // float distance = Random.Range(m_minRange, m_maxRange);
        // int direction = Random.value > 0.5f ? 1 : -1;

        // Vector3 startPos = transform.position;
        // Vector3 targetPos = startPos + transform.forward * distance * direction;

        // if (targetPos.z > GameplayManager.Instance.Bounds.z)
        // {
        //     direction = -1;
        // }
        // else if (targetPos.z < -GameplayManager.Instance.Bounds.z)
        // {
        //     direction = 1;
        // }

        // targetPos = startPos + transform.forward * distance * direction;

        // float elapsed = 0f;

        // while (elapsed < m_moveDuration)
        // {
        //     elapsed += Time.deltaTime;
        //     float t = Mathf.Clamp01(elapsed / m_moveDuration);

        //     transform.position = Vector3.Lerp(startPos, targetPos, t);
        //     yield return null;
        // }

        // transform.position = targetPos;
        // m_isMoving = false;
        GenerateKickDirectionAndSprite();
    }

    public void StartBallChase(GameObject ball)
    {
        m_isChasingBall = true;
        m_ball = ball;
    }

    private void ChaseBall()
    {
        Vector3 direction = m_ball.transform.position - transform.position;
        float xDirection = direction.x;

        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 force = new(xDirection * m_force * Time.fixedDeltaTime, 0f, 0f);
        rb.AddForce(force, ForceMode.Force);
    }
}
