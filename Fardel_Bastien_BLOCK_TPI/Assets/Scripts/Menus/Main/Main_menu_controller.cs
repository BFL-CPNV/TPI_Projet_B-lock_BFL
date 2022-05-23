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
    // Permet de ne pas avoir le level � charger cod� en dur, il suffit de le pr�ciser dans l'inspecteur
    [SerializeField] private string level_to_load;

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
