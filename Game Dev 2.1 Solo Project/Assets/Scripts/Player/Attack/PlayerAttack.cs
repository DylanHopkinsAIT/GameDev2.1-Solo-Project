using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private Transform firePosision;
    [SerializeField]private GameObject fireball;

    // Update is called once per frame
    void Update()
    {
        shootFireball();
    }

    private void shootFireball()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(fireball, firePosision.position, firePosision.rotation);
    }
















}


