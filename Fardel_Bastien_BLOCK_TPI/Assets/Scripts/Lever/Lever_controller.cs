using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_controller : MonoBehaviour
{
    private bool is_player_in_range;
    private bool is_lever_on = false; // État de base du levier
    private bool is_lever_off = true; // État de base du levier

    [SerializeField] private Animator lever_animator;
    [SerializeField] GameObject connected_obstacle;

    // Update est appelé une fois par mise à jour de frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && is_player_in_range)
        {
            InteractWithLever();
        }
    }

    private void InteractWithLever()
    {
        //Debug.Log("Entering IntercatWithLever, lever_off :" + is_lever_off);

        is_lever_off = !is_lever_off;
        lever_animator.SetBool("is_lever_off", is_lever_off);
        lever_animator.SetTrigger("interact");
        connected_obstacle.SetActive(is_lever_off);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            is_player_in_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            is_player_in_range = false;
        }
    }
}
