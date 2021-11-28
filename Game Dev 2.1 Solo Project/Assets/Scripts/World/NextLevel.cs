using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D player = other.gameObject.GetComponent<Rigidbody2D>();

        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
}
