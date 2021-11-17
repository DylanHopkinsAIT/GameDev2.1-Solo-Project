using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    private Rigidbody2D player;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    //Using [SerializeField] allows the values to be changed in unity GUI, but is better practice than just making the variable public.
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float dashMultiplier = 0.05f;
    private float dirHorizontal = 0f;

    private bool canDoubleJump = false;
    private bool jumpKeyDown = false;
    private bool isDashing = false;
    private bool IsGrounded() { return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); }

    /*Create an enum (a group of read - only constants) to define what type of movement is happening.
     *This is more efficient than using true or false booleans for fall/jump/run/idle every time and also a lot tidier.
     *In this instance Idle = 0 | Walk = 1 | Jump = 2 | Fall = 3  | Dash = 4
     */
    private enum movementState
    {
        idle, //0
        walk, //1
        jump, //2
        fall, //3
        dash  //4
    };



    void Start() {
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        CharacterMovement();
        MovementState();
        CharacterDash();
    }

    /* Character Dash Script
     * Test what direction character is moving and dash in that direction
     */
    private void CharacterDash() {
        Vector3 MoveDirection = new Vector3();
        bool canDash;

        //Switch to test Horizontal Direction
        switch (dirHorizontal)
        {
            case float n when n > 0f: //Moving Right
                MoveDirection = Vector3.right;
                break;
            case float n when n < 0: //Moving Left
                MoveDirection = Vector3.left;
                break;
        }

        canDash = (Input.GetButton("Dash"));
        if (canDash)
        {
            transform.position += MoveDirection * dashMultiplier;
            isDashing = true;
            GamepadRumble.Rumble(true);
        }
        else
        {
            GamepadRumble.Rumble(false);
            isDashing = false;
        }

    }

/* Character Movement Script 
    * 
    * Rather than hard coding the input keys, use the inputs defined inside unity.
    * Go to Edit > Project Settings > Input Manager > Axes to see the different types available.
    * This can be better as it allows for multiple types of control (Keyboard, Joystick etc.) without hard coding for each.
    * 
    */
    private void CharacterMovement() {


        //Set float dirHorizontal equal to the horizontal axis ranging between -1 and 1, allowing analog support.
        dirHorizontal = Input.GetAxisRaw("Horizontal");

        //Player Move Left(-1 to -0.1) / Right (0.1 to 1) by using horizontal as a multiplier, times the moveSpeed.
        player.velocity = new Vector2(dirHorizontal * moveSpeed, player.velocity.y);

        //Double Jump Script
        jumpKeyDown = (Input.GetButtonDown("Jump"));

        if(jumpKeyDown)
        {
            //If jump key is pressed & isGrounded, allow player to jump and make doublejump available
            if (IsGrounded())
            {
                player.velocity = new Vector2(player.velocity.x, 0);
                player.velocity += new Vector2(0, jumpForce);
                canDoubleJump = true;
            }

            /*If jump key is pressed & player is NOT grounded (aka in air), allow player to jump a second
             * time and then make double jump unavailable until grounded again*/
            else if(canDoubleJump)
            {
                player.velocity = new Vector2(player.velocity.x, 0);
                player.velocity += new Vector2(0, jumpForce);
                canDoubleJump = false;
            }

        }

    }

    //Test how player is moving to determine what animation should play
    private void MovementState() {
        movementState state;

        //Switch for Horizontal Movement
        switch (dirHorizontal)
        {
            case float n when n > 0f: //When walking right
                state = movementState.walk;
                sprite.flipX = false;
                break;
            case float n when n < 0: //When walking left
                state = movementState.walk;
                sprite.flipX = true;
                break;
            default: //When idling
                state = movementState.idle;
                break;
        }

        //Only if player is dashing (Prevents animation conflics/overrides with vertical animations)
        if (isDashing)
        {
            //Switch for Dash Movement
            switch (dirHorizontal)
            {
                case float n when n > 0f: //Dashing right
                    state = movementState.dash;
                    sprite.flipX = false;
                    break;
                case float n when n < 0: //Dashing left
                    state = movementState.dash;
                    sprite.flipX = true;
                    break;
            }
        }

        //Only if player is not dashing (Prevents animation conflics/overrides with dash animations)
        else if (!(isDashing))
        {
            //Switch for any Vertical Movement
            switch (player.velocity.y)
            {
                case float n when n > .01f: //When Jumping
                    state = movementState.jump;
                    break;
                case float n when n < -0.0f: //When falling
                    state = movementState.fall;
                    break;        
            }
        }
        //Cast enum value (0/1/2/3/4 as mentioned when enum was initialized) as integer for unity animator 
        anim.SetInteger("state", (int)state);
    }

}