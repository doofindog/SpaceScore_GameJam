<<<<<<< HEAD
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    Quaternion rotOrig;

    void Awake()
    {
        rotOrig = transform.rotation;
    }

    void FixedUpdate()
    {
        transform.rotation = rotOrig;
    }
}
=======
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    Quaternion rotOrig;

    void Awake()
    {
        rotOrig = transform.rotation;
    }

    void FixedUpdate()
    {
        transform.rotation = rotOrig;
    }
}
>>>>>>> 1d819f56567ff7dcae037ff544ab51d67735aa00
