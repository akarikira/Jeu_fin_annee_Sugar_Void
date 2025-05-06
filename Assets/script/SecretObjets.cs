using UnityEngine;

public class SecretObject : MonoBehaviour
{
    [Header("Options")]
    public GameObject hiddenObject;
    public Material revealedMaterial;
    
    public void ShowHiddenContent()
    {
        if(hiddenObject != null) 
            hiddenObject.SetActive(true);
        
        if(revealedMaterial != null)
            GetComponent<Renderer>().material = revealedMaterial;
    }
}