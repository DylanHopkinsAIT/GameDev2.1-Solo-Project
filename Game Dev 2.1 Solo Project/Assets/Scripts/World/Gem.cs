using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{

    private AudioSource gemPickup;

    private void Start()
    {
        gemPickup = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ScoreScript.scoreValue += 10;
            Destroy(this.gameObject);
            gemPickup.Play();
        }
        
    }
}
