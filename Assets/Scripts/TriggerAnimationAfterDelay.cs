using UnityEngine;
using System.Collections;

public class TriggerAnimationAfterDelay : MonoBehaviour
{
    [Tooltip("Animator of the object to animate.")]
    public Animator targetAnimator;

    [Tooltip("Name of the trigger parameter in the Animator.")]
    public string triggerName = "Activate";

    [Tooltip("Delay in seconds before the animation starts.")]
    public float delay = 2f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(ActivateAnimationAfterDelay());
        }
    }

    private IEnumerator ActivateAnimationAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogWarning("Target Animator is not assigned.");
        }
    }
}
