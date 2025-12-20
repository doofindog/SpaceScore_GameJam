<<<<<<< HEAD
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
=======
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
>>>>>>> 1d819f56567ff7dcae037ff544ab51d67735aa00
