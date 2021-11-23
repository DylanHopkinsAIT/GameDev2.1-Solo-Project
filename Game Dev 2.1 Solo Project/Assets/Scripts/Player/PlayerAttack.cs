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

    /// <summary>
    /// Causes the player to shoot a fireball forwards
    /// </summary>
    private void shootFireball()
    {
        dirHorizontal = Input.GetAxisRaw("Horizontal");

        if
            (Input.GetButtonDown("Fire1"))

        {
            Shoot();
            GamepadRumble.Rumble(true);
        }
        else{
            GamepadRumble.Rumble(false);
        }
    }

    /// <summary>
    /// Instanciates a fireball and sets its position and rotation
    /// </summary>
    private void Shoot()
    {
        Instantiate(fireball, firePosition.position, firePosition.rotation);
    }



















}


