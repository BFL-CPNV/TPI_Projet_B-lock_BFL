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
    /// Awake est appelé quand l'instance de script est chargée
    /// </summary>
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        player_script = Player.GetComponent<Player_controller>();
    }

    /// <summary>
    /// Update est appelé une fois par mise à jour de trame
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
    /// ResumeGame est une fonction qui est appelée lorsque le joueur souhaite reprendre la partie
    /// </summary>
    public void ResumeGame()
    {
        player_script.enabled = true; // Activation du script du joueur pour lui permettre de se déplacer
        pause_menu_ui.SetActive(false); // Désactivation du menu pause
        Time.timeScale = 1; // Réactivation du temps
        game_is_paused = false;
    }

    /// <summary>
    /// PauseGame est une fonction qui est appelée lorsque le joueur souhaite mettre en pause la partie
    /// </summary>
    private void PauseGame()
    {
        player_script.enabled = false; // Désactivation temporaire du script du joueur afin d'éviter les déplacements non voulus
        pause_menu_ui.SetActive(true); // Activation du menu pause
        Time.timeScale = 0; // Arrêt du temps
        game_is_paused = true;
    }

    /// <summary>
    /// QuitGame est une fonction qui est appelée lorsque le joueur appuie sur Quitter
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
