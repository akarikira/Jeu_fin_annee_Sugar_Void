using UnityEngine;

public class TriggerLoadScene : MonoBehaviour
{
    public static string sceneName;
    public GameObject _gameManager;
    public LoadSceneScript _loadScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("GameManager");
        _loadScript = _gameManager.GetComponent<LoadSceneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Teleport"))
        {
            sceneName = collision.gameObject.name;
            _loadScript.Load(sceneName);
        }
    }
}
