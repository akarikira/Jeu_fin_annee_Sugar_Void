using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door;          // La porte à activer/désactiver
    public static bool hasKey;     // Est-ce que le joueur a la clé ?

    void Start()
    {
        hasKey = false;
        door.SetActive(true); // Par défaut, la porte est visible
    }

    // Appelée par la clé quand elle est collectée
    //public void UseKey()
    //{
    //    hasKey = true;

    //    if (hasKey)
    //    {
    //        door.SetActive(false); // Désactiver la porte
    //        Debug.Log("🔓 Porte ouverte !");
    //    }
    //}
}
