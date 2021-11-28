using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;
    [SerializeField] private Vector3 minValue, maxValue;

    /// <summary>
    /// Basic camera control functions, camera follows the player and <br/>
    /// shifts either left or right to give the player a better view <br/>
    /// forward on a specified offset. Uses Lerp to smoothly move.
    /// </summary>
    private void LateUpdate()
    {
        Follow();
    }

    /// <summary>
    /// Follow method creates smooth movement of camera using linear interpolation(lerp)
    /// </summary>
    private void Follow()
    {
        Vector3 targetPosition = target.position + offset;

        Vector3 boundPosition = new Vector3(
          Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
          Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
          Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothPosition;
    }
}