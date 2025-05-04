using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class TimelineTrigger : MonoBehaviour
{
    [Tooltip("Delay in seconds before playing the Timeline after the player enters the trigger.")]
    public float delayBeforePlay = 2f;

    [Tooltip("PlayableDirector component that controls the Timeline.")]
    public PlayableDirector timelineDirector;

    [Tooltip("Tag assigned to the player GameObject.")]
    public string playerTag = "Player";

    private bool hasTriggered = false;

    private void Reset()
    {
        // Ensure the Collider is set as a trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag(playerTag))
        {
            hasTriggered = true;
            StartCoroutine(PlayTimelineAfterDelay());
        }
    }

    private IEnumerator PlayTimelineAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforePlay);
        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }
        else
        {
            Debug.LogWarning("TimelineDirector is not assigned.");
        }
    }
}
