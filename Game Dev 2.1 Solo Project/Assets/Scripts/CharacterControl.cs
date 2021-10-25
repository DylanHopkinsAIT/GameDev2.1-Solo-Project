using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private float dirHorizontal = 0f;

    private enum movementState {idle, walk, jump, fall };

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        MovementState();
    }

    private void CharacterMovement() {
        dirHorizontal = Input.GetAxisRaw("Horizontal");

        player.velocity = new Vector2(dirHorizontal * moveSpeed, player.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
        }
    }

    private void MovementState() 
    {
        movementState state;

        //Moving Right (dirHorizontal is between 0.1 and 1)
        if (dirHorizontal > 0f)
        {
            state = movementState.walk;
            sprite.flipX = false;
        }

        //Moving Left (dirHorizontal is between -1 and -0.1)
        else if (dirHorizontal < 0)
        {
            state = movementState.walk;
            sprite.flipX = true;
        }

        //Idle (dirHorizontal is 0)
        else
        {
            state = movementState.idle;
        }

        //Jumping
        if (player.velocity.y > .1f)
        {
            state = movementState.jump;
        }
        else if (player.velocity.y < -0.01f)
        {
            state = movementState.fall;
        }

        //Cast enum value (0/1/2/3 as mentioned when enum was initialized) as integer for unity animator 
        anim.SetInteger("state", (int)state);
    }
}
