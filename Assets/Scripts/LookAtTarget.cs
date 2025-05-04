using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);

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
            transform.LookAt(target);
            transform.Rotate(rotationOffset);
        }
    }
}
