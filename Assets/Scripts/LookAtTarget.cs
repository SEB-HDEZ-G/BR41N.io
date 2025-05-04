using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 rotationOffset = Vector3.zero;

    void Start()
    {
        if (target == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                target = mainCam.transform;
            }
            else
            {
                Debug.LogError("Main Camera not found! Please assign the target manually.");
            }
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // Ignore vertical difference
            if (direction.sqrMagnitude > 0.001f) // Prevent zero-length vector
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
            }
        }
    }
}
