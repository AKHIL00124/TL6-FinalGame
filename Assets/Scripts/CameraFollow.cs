using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;

    [SerializeField]
    private float smoothSpeed;

    [SerializeField]
    private float depth;

    // ------   This will update the camera slightly after the player movement is updated...
    private void LateUpdate(){
        //---- This will lock the position of the camera at level height to keep it linear
        Vector3 lockPosition = new Vector3(playerTransform.position.x, 0, depth);
        Vector3 desiredPosition = lockPosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
