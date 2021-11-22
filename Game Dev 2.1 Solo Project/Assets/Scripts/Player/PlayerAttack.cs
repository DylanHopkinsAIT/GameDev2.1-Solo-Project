using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]private Transform firePosition;
    [SerializeField]private GameObject fireball;
    private float dirHorizontal;

    // Update is called once per frame
    void Update()
    {
        shootFireball();
    }

    private void Start()
    {
    }

    private void shootFireball()
    {
        dirHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {

            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(fireball, firePosition.position, firePosition.rotation);
    }


















}


