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
