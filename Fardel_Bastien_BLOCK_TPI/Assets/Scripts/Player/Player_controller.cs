using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    [SerializeField] private float movement_speed;
    [SerializeField] private bool is_grounded;
    [SerializeField] private LayerMask what_is_ground;
    [SerializeField] private float jump_force;
    private float jump_counter = 0;
    [SerializeField] private float check_radius;
    [SerializeField] private Transform feet_position;

    [SerializeField] private Animator player_animator;
    [SerializeField] private Rigidbody2D player_rigidbody2d;
    [SerializeField] private SpriteRenderer player_sprite_renderer;
    [SerializeField] private Recorder player_recorder;

    private Vector3 velocity = Vector3.zero;

    // Awake est appelé quand l'instance de script est chargée
    private void Awake()
    {
        player_rigidbody2d = GetComponent<Rigidbody2D>();
        player_recorder.GetComponent<Recorder>();
    }

    // Start est appelé avant la première mise à jour de frame.
    private void Start()
    {

    }

    // FixedUpdate est appelé pour chaque trame avec un taux fixe, si le MonoBehaviour est activé.
    private void FixedUpdate()
    {
        float horizontal_movement = Input.GetAxis("Horizontal") * movement_speed * Time.deltaTime;

        PlayerMovement(horizontal_movement);
    }

    // Update est appelé une fois par mise à jour de trame, la fonction de saut est inséré dans Update afin d'être le plus réactif possible, après un test il a été découvert que Update est plus réactif pour le saut que FixedUpdate contrairement au mouvement.
    private void Update()
    {
        CheckIfGrounded();

        if (is_grounded == true && Input.GetKeyDown(KeyCode.W))
        {
            jump_counter = 0;
            jump_counter += 1;
            player_animator.SetBool("is_jumping", true);
            player_rigidbody2d.velocity = Vector2.up * jump_force;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (jump_counter < 1)
                {
                    player_rigidbody2d.velocity = Vector2.up * jump_force;
                    jump_counter += 1;
                }
            }

            if (is_grounded == true)
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

    // LateUpdate est appelé pour chaque trame, une fois que toutes les updates sont effectuées
    private void LateUpdate()
    {
       // Rewind_Data data = new Rewind_Data(this.transform.position);
       // player_recorder.RecordRewindFrame(data);
    }

    private void PlayerMovement(float horizontal_movement)
    {
        Vector3 targetVelocity = new Vector2(horizontal_movement, player_rigidbody2d.velocity.y);
        FlipSprite(horizontal_movement);
        player_animator.SetFloat("speed", Mathf.Abs(horizontal_movement));

        player_rigidbody2d.velocity = Vector3.SmoothDamp(player_rigidbody2d.velocity, targetVelocity, ref velocity, .05f);


    }

    private void CheckIfGrounded()
    {
        is_grounded = Physics2D.OverlapCircle(feet_position.position, check_radius, what_is_ground);
    }

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

    public void SetDataForFrame(Rewind_Data data)
    {
        this.transform.position = data.position;
    }
}
