using UnityEngine;

public class LoadSceneScript : MonoBehaviour
{
    public GameObject _playerParent;
    public GameObject _player;
    public GameObject _cameraParent;
    public GameObject _cameraJardin;
    public GameObject _cameraSalle;
    public GameObject _canvas;
    public GameObject _imageBlack;
    public FadeTransition _fadeTransition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canvas = GameObject.Find("Canvas");
        _imageBlack = _canvas.transform.Find("ImageBlack").gameObject;
        _fadeTransition = _imageBlack.GetComponent<FadeTransition>();
        _playerParent = GameObject.Find("personnage");
        _player = _playerParent.transform.Find("Gyaru").gameObject;
        _cameraParent = GameObject.Find("camera");
        _cameraJardin = _cameraParent.transform.Find("jardin_cam").gameObject;
        _cameraSalle = _cameraParent.transform.Find("salle_portrait_cam").gameObject;
        _cameraJardin.SetActive(true);
        _cameraSalle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Load(string sceneName)
    {
        _fadeTransition.Fade();
        if (sceneName == "portail entree")
        {
            _player.transform.position = new Vector3(-0.07f, 2f, -64.4894f);
            _cameraSalle.SetActive(false);
            _cameraJardin.SetActive(true);

        }
        if(sceneName == "portail sortie")
        {
            _player.transform.position = new Vector3(-0.07f, 7.82f, -64.4894f);
            _cameraSalle.SetActive(true);
            _cameraJardin.SetActive(false);

        }
    }

}
