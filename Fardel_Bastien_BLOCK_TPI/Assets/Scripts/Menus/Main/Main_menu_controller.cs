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

public class Main_menu_controller : MonoBehaviour
{
    // Permet de ne pas avoir le level à charger codé en dur, il suffit de le préciser dans l'inspecteur
    [SerializeField] private string level_to_load;

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
