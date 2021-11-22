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

    /// <summary>
    /// Basic camera control functions, camera follows the player and <br/>
    /// shifts either left or right to give the player a better view <br/>
    /// forward on a specified offset. Uses Lerp to smoothly move.
    /// </summary>
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
