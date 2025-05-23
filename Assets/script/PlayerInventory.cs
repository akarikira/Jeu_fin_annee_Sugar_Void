using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public PlayerInventory Instance;

    public bool hasKnife = false;
    public bool hasKey = false;
    public bool hasFiole = false;
    public bool hasFilledFiole = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}