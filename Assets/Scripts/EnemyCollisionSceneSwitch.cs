using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollisionSceneSwitch : MonoBehaviour
{
    [Tooltip("Name of the scene to load upon collision with the player.")]
    public string sceneToLoad = "GameOverScene";

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
