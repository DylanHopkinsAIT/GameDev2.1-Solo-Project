using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private Animator anim; 
    [SerializeField] private float speed;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// When player collides with plant, make player jump very high
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D player = other.gameObject.GetComponent<Rigidbody2D>();

        if (other.gameObject.CompareTag("Player"))
        {
            player.velocity = new Vector2(player.velocity.x, 0);
            player.AddForce(Vector3.up * speed);
            anim.Play("PlantJump");
        }


    }

}
