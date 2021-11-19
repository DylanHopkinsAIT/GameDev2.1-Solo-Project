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


    void Update()
    {
        patrol();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundinfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundinfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                movingRight = true;
            }
        }
    }


}
