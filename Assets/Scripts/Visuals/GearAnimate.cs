using UnityEngine;

public class GearAnimate : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward*speed);
    }
}
