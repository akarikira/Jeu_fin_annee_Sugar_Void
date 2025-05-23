using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName; // "Gyaru" ou "Goth"
    [TextArea(2, 4)]
    public string text;
}
