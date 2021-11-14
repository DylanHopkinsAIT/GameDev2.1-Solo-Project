using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float dashMultiplier = 0.02f;
    private float dirHorizontal = 0f;

    private enum movementState {idle, walk, jump, fall, dash};

    private bool isDashing = false;
    private bool dashOffCooldown = false;
    private bool IsGrounded => Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);


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
        CharacterDash(); 
    }

    private void CharacterDash() 
    {

        Vector3 MoveDirection = new Vector3();
        bool canDash;

        //Moving Right (dirHorizontal is between 0.1 and 1)
        if (dirHorizontal > 0f)
        {
            MoveDirection = Vector3.right;
        }

        //Moving Left (dirHorizontal is between -1 and -0.1)
        else if (dirHorizontal < 0)
        {
            MoveDirection = Vector3.left;
        }

        canDash = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton5) && dashOffCooldown);

        if (canDash)
        {
            isDashing = true;
            transform.position += MoveDirection * dashMultiplier;
        }
        isDashing = false;
        dashOffCooldown = false;
        StartCoroutine(DashTimer());
    }

    private IEnumerator DashTimer() {
        yield return new WaitForSeconds(5f);
        dashOffCooldown = true;
    }


    private void CharacterMovement() {
        dirHorizontal = Input.GetAxisRaw("Horizontal");

        player.velocity = new Vector2(dirHorizontal * moveSpeed, player.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded)
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

        //Dashing Right (dirHorizontal is between 0.1 and 1)
        if (dirHorizontal > 0f && isDashing)
        {
            state = movementState.dash;
            sprite.flipX = false;
        }

        //Dashing Left (dirHorizontal is between -1 and -0.1)
        else if (dirHorizontal < 0 && isDashing)
        {
            state = movementState.dash;
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
        //Falling
        else if (player.velocity.y < -0.01f)
        {
            state = movementState.fall;
        }

        //Dashing Right (overrides jump/fall) (dirHorizontal is between 0.1 and 1)
        if (dirHorizontal > 0f && isDashing)
        {
            state = movementState.dash;
            sprite.flipX = false;
        }

        //Dashing Left (overrides jump/fall) (dirHorizontal is between -1 and -0.1)
        else if (dirHorizontal < 0 && isDashing)
        {
            state = movementState.dash;
            sprite.flipX = true;
        }


        //Cast enum value (0/1/2/3/4 as mentioned when enum was initialized) as integer for unity animator 
        anim.SetInteger("state", (int)state);
    }

}
