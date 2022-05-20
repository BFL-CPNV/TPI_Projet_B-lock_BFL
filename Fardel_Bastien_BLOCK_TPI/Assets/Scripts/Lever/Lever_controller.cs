/**********************************************
 * Projet : B'lock
 * Nom du fichier : Lever_controller.cs
 * 
 * Date des derniers changements : 20.05.2022
 * Version : 1.1
 * Auteur : Fardel Bastien
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_controller : MonoBehaviour
{
    // Variables
    private bool is_player_in_range;
    private bool is_lever_off = true; // �tat de base du levier
    private bool is_rewinding;

    // Objets du levier
    private Animator lever_animator;
    [SerializeField] private GameObject[] connected_obstacles;

    /// <summary>
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    private void Awake()
    {
        // r�cup�ration du composant attach� au levier
        lever_animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Update est appel� une fois par mise � jour de frame
    /// </summary>
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

    /// <summary>
    /// InteractWithLever est une fonction qui permet au joueur d'interagir avec les leviers du jeu, modifiant ainsi l'environnement de celui-ci pour r�soudre des puzzles
    /// </summary>
    private void InteractWithLever()
    {  
        is_lever_off = !is_lever_off;
        lever_animator.SetBool("is_lever_off", is_lever_off);
        lever_animator.SetTrigger("interact");
        foreach (var obstacle in connected_obstacles) 
        {
            obstacle.SetActive(!obstacle.activeSelf);
        }
    }

    /// <summary>
    /// OnTriggerEnter2D est une fonction sp�cifique qui est appel�e lorsque le collider du levier d�tecte un objet entrant celui-ci
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // V�rifie si le joueur a �t� d�tect� entrant dans la collision
        {
            is_player_in_range = true;
        }
    }

    /// <summary>
    /// OnTriggerExit2D est une fonction sp�cifique qui est appel�e lorsque le collider du levier d�tecte un objet quittant celui-ci
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision) // Comme pour l'entr�e, la sortie interdit l'interaction au levier.
    {
        if (collision.CompareTag("Player")) // V�rifie si le joueur a �t� d�tect� quittant la collision
        {
            is_player_in_range = false;
        }
    }
}
