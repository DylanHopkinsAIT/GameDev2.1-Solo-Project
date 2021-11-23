using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;

    /// <summary>
    /// Basic camera control functions, camera follows the player and <br/>
    /// shifts either left or right to give the player a better view <br/>
    /// forward on a specified offset. Uses Lerp to smoothly move.
    /// </summary>
    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
