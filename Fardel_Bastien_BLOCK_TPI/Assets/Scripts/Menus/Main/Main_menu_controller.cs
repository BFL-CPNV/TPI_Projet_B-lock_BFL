using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu_controller : MonoBehaviour
{
    // Permet de ne pas avoir le level � charger cod� en dur, il suffit de le pr�ciser dans l'inspecteur
    public string level_to_load;

    /// <summary>
    /// StartGame est une fonction qui est appel�e lorsque le bouton du m�me nom est appuy� pour lancer le jeu
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(level_to_load);
    }

    /// <summary>
    /// QuitGame est une fonction qui est appel�e lorsque le bouton du m�me nom est appuy� pour quitter le jeu
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
