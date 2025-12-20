using UnityEngine;

public class BallSpriteRotate : MonoBehaviour
{
    public Rigidbody rb;
    public float moveFactor;


    void FixedUpdate()
    {
        if (rb.linearVelocity.x > 0)    transform.Rotate(new Vector3(0, 0, -rb.linearVelocity.magnitude*moveFactor));
        else if (rb.linearVelocity.x < 0)    transform.Rotate(new Vector3(0, 0, rb.linearVelocity.magnitude*moveFactor));

    }
}
