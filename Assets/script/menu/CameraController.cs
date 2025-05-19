using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            vcam = FindObjectOfType<CinemachineVirtualCamera>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MoveCameraTo(Transform newTarget)
    {
        if (vcam != null)
        {
            vcam.Follow = newTarget;
        }
        else
        {
            Debug.LogWarning("CinemachineVirtualCamera not found!");
        }
    }
}
