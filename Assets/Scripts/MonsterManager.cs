using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public int fearLevel = 0;
    public int monsterID = 1; // 1 o 2 dependiendo del monstruo activo
    public GameObject monster1;
    public GameObject monster2;

    public void HandleEEGAction(string action)
    {
        switch (monsterID)
        {
            case 1:
                if (action == "ShakeHands" || action == "BlinkFast")
                {
                    fearLevel += 2;
                    Debug.Log("👹 Monstruo 1 asustado (mover manos o parpadear). Miedo: " + fearLevel);
                    DismissMonster(monster1);
                }
                break;

            case 2:
                if (action == "EyesClosed" || action == "TurnHead")
                {
                    fearLevel += 2;
                    Debug.Log("👺 Monstruo 2 asustado (girar cabeza o cerrar ojos). Miedo: " + fearLevel);
                    DismissMonster(monster2);
                }
                break;
        }
    }

    void DismissMonster(GameObject monster)
    {
        monster.SetActive(false);
        Debug.Log("✅ Monstruo eliminado");
        // Lógica adicional: spawnear otro monstruo, cambiar escena, etc.
    }
}
