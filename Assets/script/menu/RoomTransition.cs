using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public Transform newCameraTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = playerSpawnPoint.position;

            if (CameraController.Instance != null)
            {
                CameraController.Instance.MoveCameraTo(newCameraTarget);
            }
            else
            {
                Debug.LogWarning("CameraController.Instance is null");
            }
        }
    }
}
