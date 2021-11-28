using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetect : MonoBehaviour
{
    GameObject Slime;
    // Start is called before the first frame update
    void Start()
    {
        Slime = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        Slime.GetComponent<SpriteRenderer>().flipY = true;
        Slime.GetComponent<Collider2D>().enabled = false;
        Vector3 movement = new Vector3(Random.Range(40, 70), Random.Range(-40, 40), 0f);
        Slime.transform.position += movement * Time.deltaTime;
    }
}