using UnityEngine;

public class DisappearWhenSeen : MonoBehaviour
{
    [Tooltip("Time in seconds before the object disappears after being seen.")]
    public float timeToDisappear = 3f;

    private bool hasBeenSeen = false;
    private float timer = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please tag your camera as 'MainCamera'.");
        }
    }

    void Update()
    {
        if (mainCamera == null) return;

        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

        bool isInView = viewportPoint.z > 0 &&
                        viewportPoint.x > 0 && viewportPoint.x < 1 &&
                        viewportPoint.y > 0 && viewportPoint.y < 1;

        if (isInView)
        {
            if (!hasBeenSeen)
            {
                hasBeenSeen = true;
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= timeToDisappear)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            // Optional: Reset if object leaves view before timer completes
            // hasBeenSeen = false;
            // timer = 0f;
        }
    }
}
