using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_controller : MonoBehaviour
{
    private bool is_player_in_range;
    private bool is_lever_off = true; // �tat de base du levier

    [SerializeField] private Animator lever_animator;
    [SerializeField] GameObject connected_obstacle;

    // Update est appel� une fois par mise � jour de frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && is_player_in_range)
        {
            InteractWithLever();
        }
    }

    private void InteractWithLever()
    {
        //Debug.Log("Entering IntercatWithLever, lever_off :" + is_lever_off); Utilis� pour v�rifier l'interaction avec le levier et la valeur de lever_off afin de comprendre d'o� venait une erreur d'interaction delay�e

        is_lever_off = !is_lever_off;
        lever_animator.SetBool("is_lever_off", is_lever_off);
        lever_animator.SetTrigger("interact");
        connected_obstacle.SetActive(is_lever_off);

        // Cette section de code est l'ancienne version de gestion des leviers, celle-ci a �t� remplac� par la nouvelle version qui a �t� d�velopp�e avec l'aide du chef de projet
        /*if (!is_lever_off)
        {
            lever_animator.SetBool("is_lever_on", false);
            lever_animator.SetBool("is_lever_off", true);
            lever_animator.SetTrigger("interact");

            //Debug.Log("lever was turned on");
            is_lever_off = true;
            is_lever_on = false;

            connected_obstacle.SetActive(true);
        }
        else if (is_lever_off)
        {
            lever_animator.SetBool("is_lever_on", true);
            lever_animator.SetBool("is_lever_off", false);
            lever_animator.SetTrigger("interact");

            Debug.Log("lever was turned off");
            is_lever_off = false;
            is_lever_on = true;

            connected_obstacle.SetActive(false);
        } //*/
    }

    private void OnTriggerEnter2D(Collider2D collision) // Lors de l'entr�e du joueur dans la zone du levier, celui-ci autorise l'interaction avec celui-ci
    {
        if (collision.CompareTag("Player"))
        {
            is_player_in_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // Comme pour l'entr�e, la sortie interdit l'interaction au levier.
    {
        if (collision.CompareTag("Player"))
        {
            is_player_in_range = false;
        }
    }
}
