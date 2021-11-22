using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float offset;
    [SerializeField] private float offsetSmoothing;
    private Vector3 playerPosition;
    private float dirHorizontal;


    private void Start()
    {
    }
    private void Update() 
    {
        dirHorizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(dirHorizontal);

        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if (dirHorizontal > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        else if (dirHorizontal < 0f)
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
