using UnityEngine;
using UnityEditor;

public class MeshColliderAdder : EditorWindow
{
    [MenuItem("Tools/Add MeshColliders to All Selected Objects")]
    static void AddMeshColliders()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            AddCollidersRecursively(obj);
        }

        Debug.Log("MeshColliders added to all selected objects (and their children).");
    }

    static void AddCollidersRecursively(GameObject obj)
    {
        if (obj.GetComponent<MeshFilter>() && obj.GetComponent<MeshCollider>() == null)
        {
            obj.AddComponent<MeshCollider>();
        }

        foreach (Transform child in obj.transform)
        {
            AddCollidersRecursively(child.gameObject);
        }
    }
}
