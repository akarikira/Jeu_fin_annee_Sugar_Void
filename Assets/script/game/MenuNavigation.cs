using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public Button[] buttons;        // Les boutons du menu à assigner dans l'inspecteur
    private int selectedIndex = 0;  // Le bouton actuellement sélectionné

    void Start()
    {
        UpdateButtonSelection(); // Met à jour la couleur du bouton sélectionné au démarrage
    }

    void Update()
    {
        // Déplacement vers le bas
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % buttons.Length;
            UpdateButtonSelection();
        }
        // Déplacement vers le haut
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = buttons.Length - 1;
            UpdateButtonSelection();
        }

        // Validation avec Espace ou Entrée
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].onClick.Invoke();
        }
    }

    void UpdateButtonSelection()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            ColorBlock colors = buttons[i].colors;
            if (i == selectedIndex)
            {
                colors.normalColor = Color.yellow; // Couleur de sélection
                buttons[i].colors = colors;
                buttons[i].Select();
            }
            else
            {
                colors.normalColor = Color.white;
                buttons[i].colors = colors;
            }
        }
    }

    // Appelé par les boutons ou par le clavier
    public void NewGame()
    {
        SceneManager.LoadScene("GameScene"); // ⚠️ Mets le vrai nom de ta scène
    }

    public void ExitGame()
    {
        Debug.Log("Quitter le jeu...");
        Application.Quit();
    }
}
