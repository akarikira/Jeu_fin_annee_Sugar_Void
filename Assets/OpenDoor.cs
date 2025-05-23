using TMPro.Examples;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (Input.GetKeyDown(KeyCode.F) && DoorManager.hasKey)
            {
                door.SetActive(false);
            }
        }
    }
}