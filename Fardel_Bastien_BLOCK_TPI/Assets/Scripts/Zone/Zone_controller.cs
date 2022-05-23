/**********************************************
 * Projet : B'lock
 * Nom du fichier : Lever_controller.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.1
 * Auteur : Fardel Bastien
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone_controller : MonoBehaviour
{
    // Variables
    private bool player_passed_through = false;

    // Objets associés au contrôleur
    private GameObject Player;
    private Player_controller player_script;
    [SerializeField] private GameObject exit;

    /// <summary>
    /// Awake est appelé quand l'instance de script est chargée
    /// </summary>
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        player_script = Player.GetComponent<Player_controller>(); 
    }

    /// <summary>
    /// OnTriggerEnter2D est une fonction spécifique qui est appelée lorsque le collider détecte un objet entrant celui-ci
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !player_passed_through) // Le joueur doit être l'objet détecté dans la collision et il doit aussi y entrer pour la première fois
        {
            exit.SetActive(!exit.activeSelf); // Referme la sortie derrière le joueur
            player_script.recorded_data.Clear(); // Nettoie les données enregistrées pour empêcher le joueur de revenir à la zone précédente
            player_passed_through = !player_passed_through;
        }
    }
}
