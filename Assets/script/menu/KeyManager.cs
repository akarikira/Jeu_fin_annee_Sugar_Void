using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public Image[] keySlots; // Assigne ici tes 3 icônes de clé dans l'inspecteur
    public Sprite emptyKey;
    public Sprite filledKey;

    private bool[] keysCollected = new bool[3];

    public void CollectKey(int index)
    {
        if (index >= 0 && index < keysCollected.Length)
        {
            keysCollected[index] = true;
            keySlots[index].sprite = filledKey;
        }
    }

    private void Start()
    {
        // Initialise l'UI avec des clés vides
        for (int i = 0; i < keySlots.Length; i++)
        {
            keySlots[i].sprite = emptyKey;
        }
    }
}
