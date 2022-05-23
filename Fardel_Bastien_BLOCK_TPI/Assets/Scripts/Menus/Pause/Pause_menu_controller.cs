/**********************************************
 * Projet : B'lock
 * Nom du fichier : Player_controller.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Fardel Bastien
 **********************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu_controller : MonoBehaviour
{
    // Variables
    public static bool game_is_paused = false;

    // Objets
    private Player_controller player_script;
    private GameObject Player;
    [SerializeField]private GameObject pause_menu_ui;

    /// <summary>
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        player_script = Player.GetComponent<Player_controller>();
    }

    /// <summary>
    /// Update est appel� une fois par mise � jour de trame
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (game_is_paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// ResumeGame est une fonction qui est appel�e lorsque le joueur souhaite reprendre la partie
    /// </summary>
    public void ResumeGame()
    {
        player_script.enabled = true; // Activation du script du joueur pour lui permettre de se d�placer
        pause_menu_ui.SetActive(false); // D�sactivation du menu pause
        Time.timeScale = 1; // R�activation du temps
        game_is_paused = false;
    }

    /// <summary>
    /// PauseGame est une fonction qui est appel�e lorsque le joueur souhaite mettre en pause la partie
    /// </summary>
    private void PauseGame()
    {
        player_script.enabled = false; // D�sactivation temporaire du script du joueur afin d'�viter les d�placements non voulus
        pause_menu_ui.SetActive(true); // Activation du menu pause
        Time.timeScale = 0; // Arr�t du temps
        game_is_paused = true;
    }

    /// <summary>
    /// QuitGame est une fonction qui est appel�e lorsque le joueur appuie sur Quitter
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
