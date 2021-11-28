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

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            //Your pad must have a rigidbody as well as your player(I think I'm not sure I got
            other.gameObject.GetComponent<Rigidbody2D>
                    ().AddForce(Vector3.up * speed);

            anim.Play("PlantJump");
        }
    }
}
