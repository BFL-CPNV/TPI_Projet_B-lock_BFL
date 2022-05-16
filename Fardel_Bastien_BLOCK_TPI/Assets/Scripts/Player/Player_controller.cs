using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    private bool is_grounded;
    private bool rewind;
    private bool is_rewinding = false;
    private float jump_counter = 0;
    [SerializeField] private float movement_speed;
    [SerializeField] private float jump_force;
    [SerializeField] private float check_radius;

    private Vector3 velocity = Vector3.zero;
    [SerializeField] private LayerMask what_is_ground;

    private Rigidbody2D player_rigidbody2d;
    private SpriteRenderer player_sprite_renderer;
    private Animator player_animator;
    private Transform feet_position;

    // Awake est appel� quand l'instance de script est charg�e
    private void Awake()
    {
        // r�cup�ration des composants attach�s au joueur
        player_rigidbody2d = GetComponent<Rigidbody2D>();
        player_sprite_renderer = GetComponent<SpriteRenderer>();
        player_animator = GetComponent<Animator>();
        feet_position = transform.GetChild(0).GetComponent<Transform>();
    }

    // FixedUpdate est appel� pour chaque trame avec un taux fixe, si le MonoBehaviour est activ�
    private void FixedUpdate()
    {
        if (!is_rewinding)
        {
            float horizontal_movement = Input.GetAxis("Horizontal") * movement_speed * Time.deltaTime; // r�cup�re l'input du joueur pour l'utiliser pour le d�placement

            PlayerMovement(horizontal_movement);
        }
    }

    // Update est appel� une fois par mise � jour de trame, la fonction de saut est ins�r� dans Update afin d'�tre le plus r�actif possible, apr�s un test il a �t� d�couvert que Update est plus r�actif pour le saut que FixedUpdate contrairement au mouvement
    private void Update()
    {
        if (!is_rewinding)
        {
            CheckIfGrounded(); // v�rifie si le joueur est au sol

            if (is_grounded && Input.GetKey(KeyCode.W))
            {
                // jump_counter permet d'�viter au joueur d'effectuer plus d'un saut sans devoir retoucher le sol
                jump_counter = 0;
                jump_counter += 1;
                player_animator.SetBool("is_jumping", true);
                player_rigidbody2d.velocity = Vector2.up * jump_force; // applique une force de saut au joueur
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (jump_counter < 1)
                    {
                        player_rigidbody2d.velocity = Vector2.up * jump_force;
                        jump_counter += 1;
                    }
                }

                if (is_grounded)
                {
                    player_animator.SetBool("is_jumping", false);
                    //Debug.Log("Is grounded yes");
                }
                /*else
                {
                    Debug.Log("Is grounded no");
                }*/
            }
        }
    }

    // LateUpdate est appel� pour chaque trame, une fois que toutes les updates sont effectu�es
    private void LateUpdate()
    {
        // placeholder for comments
        is_rewinding = Input.GetKey(KeyCode.R);

        if (is_rewinding)
        {
            player_animator.SetBool("rewind", is_rewinding);
        }
    }

    private void PlayerMovement(float horizontal_movement)
    {
        Vector3 targetVelocity = new Vector2(horizontal_movement, player_rigidbody2d.velocity.y); 
        FlipSprite(horizontal_movement); // appel de la fonction pour tourner le sprite du joueur lorsqu'il change de direction
        player_animator.SetFloat("speed", Mathf.Abs(horizontal_movement));

        player_rigidbody2d.velocity = Vector3.SmoothDamp(player_rigidbody2d.velocity, targetVelocity, ref velocity, .05f); //d�placement du joueur en appliquant une vitesse � son rigidbody
    }

    // CheckIfGrounded est une fonction qui v�rifie si le joueur est actuellement au sol en se basant sur la position de ses pieds et la distance de ceux-ci par rapport au sol
    private void CheckIfGrounded()
    {
        is_grounded = Physics2D.OverlapCircle(feet_position.position, check_radius, what_is_ground);
    }

    // FlipSprite est une fonction qui retourne le sprite du joueur pour qu'il soit face � la direction dans laquelle celui-ci se dirige
    private void FlipSprite(float horizontal_movement)
    {
        if (horizontal_movement > 0.01f)
        {
          player_sprite_renderer.flipX = false;
        }
        else if (horizontal_movement < -0.01F)
        {
          player_sprite_renderer.flipX = true;
        }
    }
}
