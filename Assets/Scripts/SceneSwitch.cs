using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [Tooltip("Drag the scene to load from the Build Settings")]
    public int sceneBuildIndex; // For inspector

    [Header("Trigger Mode")]
    public bool requireKeyPress = false;
    public KeyCode interactionKey = KeyCode.E;

    [Header("Timer Mode (Overrides Key Press)")]
    public bool useTimer = false;
    public float delayBeforeLoad = 3f;

    [Header("Canvas Animation Settings")]
    public bool playCanvasAnimation = false;
    public Animator canvasAnimator;
    public string canvasTriggerName = "Play";
    public float canvasAnimationDelay = 1f; // Time to wait for the animation to play

    private bool playerInTrigger = false;
    private float timer = 0f;
    private bool hasActivated = false;

    void Update()
    {
        if (hasActivated)
            return;

        // Timer-based loading
        if (playerInTrigger && useTimer)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeLoad)
            {
                hasActivated = true;
                StartCoroutine(HandleSceneTransition());
            }
        }
        // Key press loading
        else if (playerInTrigger && requireKeyPress && Input.GetKeyDown(interactionKey))
        {
            hasActivated = true;
            StartCoroutine(HandleSceneTransition());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasActivated)
            return;

        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            timer = 0f; // Reset timer on enter

            // Immediate load if no special mode
            if (!requireKeyPress && !useTimer)
            {
                hasActivated = true;
                StartCoroutine(HandleSceneTransition());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (hasActivated)
            return;

        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            timer = 0f; // Reset timer on exit
        }
    }

    private IEnumerator HandleSceneTransition()
    {
        if (playCanvasAnimation && canvasAnimator != null)
        {
            canvasAnimator.gameObject.SetActive(true); // Ensure the Canvas is active
            canvasAnimator.SetTrigger(canvasTriggerName);
            yield return new WaitForSeconds(canvasAnimationDelay);
        }

        SceneManager.LoadScene(sceneBuildIndex);
    }
}
