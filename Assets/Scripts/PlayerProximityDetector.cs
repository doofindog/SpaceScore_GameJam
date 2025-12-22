using UnityEngine;

public class PlayerProximityDetector : MonoBehaviour
{


    [SerializeField] private GameObject m_player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball entered proximity detector");
            m_player.GetComponent<PlayerAi>().StartBallChase(other.gameObject);
        }
    }
}
