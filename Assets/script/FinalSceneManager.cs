using UnityEngine;

public class FinalSceneManager : MonoBehaviour
{
    public GameObject noir;
    public DialogueLine[] finalDialogue;

    public void StartFinalSequence()
    {
        StartCoroutine(PlayFinalScene());
    }

    private System.Collections.IEnumerator PlayFinalScene()
    {
        // Affiche l’écran noir
        noir.SetActive(true);

        yield return new WaitForSeconds(1.5f); // petit délai pour le drama

        // Lance le dernier dialogue
        DialogueSystem.Instance.ShowDialogue(finalDialogue);
    }
}
