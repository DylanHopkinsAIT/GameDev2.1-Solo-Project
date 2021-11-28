using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private Transform groundDetection;
    [SerializeField] private int health = 100;
    private bool movingRight = true;
    private AudioSource slimeDie;

    private void Start()
    {
        slimeDie = GetComponent<AudioSource>();
    }

    void Update()
    {
        patrol();
        if (transform.position.y <= -7)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Causes the slime to die, destroys its instance
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
        slimeDie.Play();
    }

    /// <summary>
    /// This method controls the slimes patrolling in game<br/>
    /// Slimes will always travel towards the edge of its platform<br/>
    /// and if it reaches the end it turns around and moves in the <br/>
    /// opposite direction
    /// </summary>
    private void patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Using a raycast from ground detector object which is offset forwards from slime to determine if slime is about to fall off
        RaycastHit2D groundinfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        //If no ground
        if (groundinfo.collider == false)
        {
            //Turn left
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                movingRight = false;
            }
            //Turn right
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                movingRight = true;
            }
        }
    }

}