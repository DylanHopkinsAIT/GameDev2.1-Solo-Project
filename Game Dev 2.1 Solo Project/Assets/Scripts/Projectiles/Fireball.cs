using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private int damage = 25;
    [SerializeField] private Rigidbody2D rb;


    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    /// <summary>
    /// This method causes the enemy to take damage if hit by fireball
    /// </summary>
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Slime enemy = hitInfo.GetComponent<Slime>();

        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
