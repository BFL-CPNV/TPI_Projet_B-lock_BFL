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

    // Objets associ�s au contr�leur
    private GameObject Player;
    private Player_controller player_script;
    [SerializeField] private GameObject exit;

    /// <summary>
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        player_script = Player.GetComponent<Player_controller>(); 
    }

    /// <summary>
    /// OnTriggerEnter2D est une fonction sp�cifique qui est appel�e lorsque le collider d�tecte un objet entrant celui-ci
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !player_passed_through) // Le joueur doit �tre l'objet d�tect� dans la collision et il doit aussi y entrer pour la premi�re fois
        {
            exit.SetActive(!exit.activeSelf); // Referme la sortie derri�re le joueur
            player_script.recorded_data.Clear(); // Nettoie les donn�es enregistr�es pour emp�cher le joueur de revenir � la zone pr�c�dente
            player_passed_through = !player_passed_through;
        }
    }
}
