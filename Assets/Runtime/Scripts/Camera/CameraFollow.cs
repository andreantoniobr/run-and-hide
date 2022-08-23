using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform minPositionZ;
    [SerializeField] private Transform maxPositionZ;    
    [SerializeField] private Transform minPositionX;
    [SerializeField] private Transform maxPositionX;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float armrX = 5f;
    [SerializeField] private float armrZ = 5f;

    private void LateUpdate()
    {
        if (playerController)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = playerController.transform.position;
            //currentPosition.x = Mathf.Lerp(currentPosition.x, playerPosition.x, speed * Time.deltaTime);
            //targetPosition.x = Mathf.Clamp(targetPosition.x, minPositionX.position.x + armrX, maxPositionX.position.x - armrX);
            targetPosition.z = Mathf.Clamp(targetPosition.z, minPositionZ.position.z + armrZ, maxPositionZ.position.z - armrZ);
            transform.position = Vector3.Lerp(currentPosition, targetPosition, speed * Time.deltaTime);
        }        
    }
}
