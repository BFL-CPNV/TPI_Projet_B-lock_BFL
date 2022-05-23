using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu_controller : MonoBehaviour
{
    // Permet de ne pas avoir le level à charger codé en dur, il suffit de le préciser dans l'inspecteur
    public string level_to_load;

    /// <summary>
    /// StartGame est une fonction qui est appelée lorsque le bouton du même nom est appuyé pour lancer le jeu
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(level_to_load);
    }

    /// <summary>
    /// QuitGame est une fonction qui est appelée lorsque le bouton du même nom est appuyé pour quitter le jeu
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
