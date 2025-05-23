using UnityEngine;

public class LoadSceneScript : MonoBehaviour
{
    public GameObject _playerParent;
    public GameObject _player;
    public GameObject _cameraParent;
    public GameObject _cameraJardin;
    public GameObject _cameraSalle;
    public GameObject _cameraBibli;
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
        _cameraBibli = _cameraParent.transform.Find("bibli_cam").gameObject;
        _cameraBibli.SetActive(true);
        _cameraJardin.SetActive(false);
        _cameraSalle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Load(string sceneName)
    {
        _fadeTransition.Fade();
        if (sceneName == "portail sortie")
        {
            //_player.transform.position = new Vector3(-0.07f, 0f, -0f);
            _cameraBibli.SetActive(false);
            _cameraSalle.SetActive(false);
            _cameraJardin.SetActive(true);

        }
        if(sceneName == "portail entree")
        {
            //_player.transform.position = new Vector3(-0.07f, 0f, -0f);
            _cameraSalle.SetActive(true);
            _cameraJardin.SetActive(false);
            _cameraBibli.SetActive(false);

        }
        if(sceneName == "sortie_bibli")
        {
            //_player.transform.position = new Vector3(-0.07f, 0f, 0f);
            _cameraSalle.SetActive(false);
            _cameraJardin.SetActive(false);
            _cameraBibli.SetActive(true);

        }
        if (sceneName == "entre_jardin")
        {
            //_player.transform.position = new Vector3(-0.07f, 0f, 0f);
            _cameraSalle.SetActive(false);
            _cameraJardin.SetActive(true);
            _cameraBibli.SetActive(false);
        }
    }

}
