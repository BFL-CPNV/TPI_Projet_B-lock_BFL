using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    private const int LINE_TO_READ = 0;

    private bool is_grounded;
    private bool is_rewinding = false;
    private float jump_counter = 0;
    private float horizontal_movement;
    [SerializeField] private float movement_speed;
    [SerializeField] private float jump_force;
    [SerializeField] private float check_radius;

    private Vector3 velocity = Vector3.zero;
    [SerializeField] private LayerMask what_is_ground;

    private Rigidbody2D player_rigidbody2d;
    private SpriteRenderer player_sprite_renderer;
    private Animator player_animator;
    private Transform feet_position;
    private List<RewindData> recorded_data = new List<RewindData>();

    // Awake est appelé quand l'instance de script est chargée
    private void Awake()
    {
        // récupération des composants attachés au joueur
        player_rigidbody2d = GetComponent<Rigidbody2D>();
        player_sprite_renderer = GetComponent<SpriteRenderer>();
        player_animator = GetComponent<Animator>();
        feet_position = transform.GetChild(0).GetComponent<Transform>();

        // s'assurer que la liste est vide avant de l'utiliser
        recorded_data.Clear();
    }

    // FixedUpdate est appelé pour chaque trame avec un taux fixe, si le MonoBehaviour est activé
    private void FixedUpdate()
    {
        // placeholder for comments
        is_rewinding = Input.GetKey(KeyCode.R);
        player_animator.SetBool("rewind", is_rewinding);

        if (is_rewinding)
        {
            RewindData();
        }
        else
        {
            horizontal_movement = Input.GetAxis("Horizontal") * movement_speed * Time.deltaTime; // récupère l'input du joueur pour l'utiliser pour le déplacement

            CheckIfGrounded();
            PlayerMovement(horizontal_movement);
            RecordData(transform.position.x, transform.position.y, player_sprite_renderer.flipX, is_grounded, horizontal_movement);
        }
    }

    // Update est appelé une fois par mise à jour de trame, la fonction de saut est inséré dans Update afin d'être le plus réactif possible, après un test il a été découvert que Update est plus réactif pour le saut que FixedUpdate contrairement au mouvement
    private void Update()
    {
        if (!is_rewinding)
        {
            CheckIfGrounded(); // vérifie si le joueur est au sol

            if (is_grounded && Input.GetKey(KeyCode.W))
            {
                // jump_counter permet d'éviter au joueur d'effectuer plus d'un saut sans devoir retoucher le sol
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

    private void PlayerMovement(float horizontal_movement)
    {
        Vector3 targetVelocity = new Vector2(horizontal_movement, player_rigidbody2d.velocity.y); 
        FlipSprite(horizontal_movement); // appel de la fonction pour tourner le sprite du joueur lorsqu'il change de direction
        player_animator.SetFloat("speed", Mathf.Abs(horizontal_movement));

        player_rigidbody2d.velocity = Vector3.SmoothDamp(player_rigidbody2d.velocity, targetVelocity, ref velocity, .05f); //déplacement du joueur en appliquant une vitesse à son rigidbody
    }

    // CheckIfGrounded est une fonction qui vérifie si le joueur est actuellement au sol en se basant sur la position de ses pieds et la distance de ceux-ci par rapport au sol
    private void CheckIfGrounded()
    {
        is_grounded = Physics2D.OverlapCircle(feet_position.position, check_radius, what_is_ground);
    }

    // FlipSprite est une fonction qui retourne le sprite du joueur pour qu'il soit face à la direction dans laquelle celui-ci se dirige
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

    private void RecordData(float x_position ,float y_position ,bool is_flipped ,bool is_grounded ,float current_speed)
    {
        RewindData data_to_record = new RewindData();
        Vector2 player_current_position = new Vector2(x_position, y_position);

        data_to_record.player_position = player_current_position;
        data_to_record.is_flipped = is_flipped;
        data_to_record.is_grounded = is_grounded;
        data_to_record.player_speed = current_speed;

        recorded_data.Add(data_to_record);
    }

    private void RewindData()
    {
        if (recorded_data.Count > 0)
        {
            int index = recorded_data.Count - 1;

            transform.position = recorded_data[index].player_position;
            player_sprite_renderer.flipX = recorded_data[index].is_flipped;
            is_grounded = recorded_data[index].is_grounded;
            player_animator.SetBool("is_jumping", is_grounded);
            player_animator.SetFloat("speed", recorded_data[index].player_speed);

            recorded_data.RemoveAt(index);
        }
    }
}
