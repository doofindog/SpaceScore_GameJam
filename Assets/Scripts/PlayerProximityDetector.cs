using UnityEngine;

public class PlayerProximityDetector : MonoBehaviour
{


    [SerializeField] private GameObject m_player;

    private void OnTriggerEnter(Collider other)
    {

        // if (other.gameObject.CompareTag("Ball"))
        // {
        //     m_player.GetComponent<PlayerAi>().StartBallChase(other.gameObject);
        // }
    }
}
