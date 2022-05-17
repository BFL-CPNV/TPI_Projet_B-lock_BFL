using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
 * Projet : B'lock
 * Nom du fichier : Lever_controller.cs
 * 
 * Date des derniers changements : 17.05.2022
 * Version : 1.0
 * Auteur : Fardel Bastien
 **********************************************/

public class Lever_controller : MonoBehaviour // MonoBehaviour signifie que le script sera rattach� � un objet
{
    private bool is_player_in_range;
    private bool is_lever_off = true; // �tat de base du levier
    private bool is_rewinding;

    [SerializeField] private Animator lever_animator;
    [SerializeField] GameObject connected_obstacle;

    // Update est appel� une fois par mise � jour de frame
    void Update()
    {
        is_rewinding = Input.GetKey(KeyCode.R);

        if (!is_rewinding) // S'assure que le joueur ne puisse pas interagir avec le levier s'il remonte le temps
        {
            if (Input.GetKeyDown(KeyCode.E) && is_player_in_range)
            {
                InteractWithLever();
            }
        }
    }

    // InteractWithLever est une fonction qui permet au joueur d'interagir avec les leviers du jeu, modifiant ainsi l'environnement de celui-ci pour r�soudre des puzzles
    private void InteractWithLever()
    {  
        is_lever_off = !is_lever_off;
        lever_animator.SetBool("is_lever_off", is_lever_off);
        lever_animator.SetTrigger("interact");
        connected_obstacle.SetActive(is_lever_off);
    }

    // OnTriggerEnter2D est une fonction sp�cifique qui est appel�e lorsque le collider du levier d�tecte un objet entrant celui-ci
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // V�rifie si le joueur a �t� d�tect� entrant dans la collision
        {
            is_player_in_range = true;
        }
    }

    // OnTriggerExit2D est une fonction sp�cifique qui est appel�e lorsque le collider du levier d�tecte un objet quittant celui-ci
    private void OnTriggerExit2D(Collider2D collision) // Comme pour l'entr�e, la sortie interdit l'interaction au levier.
    {
        if (collision.CompareTag("Player")) // V�rifie si le joueur a �t� d�tect� quittant la collision
        {
            is_player_in_range = false;
        }
    }
}
