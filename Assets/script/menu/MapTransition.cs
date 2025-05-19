using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    [Header("Nouvelle caméra à activer")]
    [SerializeField] private CinemachineCamera newCamera;

    [Header("Caméra à désactiver (optionnel)")]
    [SerializeField] private CinemachineCamera currentCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (currentCamera != null)
                currentCamera.Priority = 10;

            if (newCamera != null)
                newCamera.Priority = 20;
        }
    }
}
