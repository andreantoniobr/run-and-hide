using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerRotationController : MonoBehaviour
{    
    [SerializeField] private Transform playerModel;
    [SerializeField] private float turningRate = 720f;
    
    private PlayerController playerController;
    private Quaternion targetRotation = Quaternion.identity;

    private void Awake()
    {
        if (!playerController)
        {
            playerController = GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void SetBlendedEulerAngles(Vector3 angles)
    {
        targetRotation = Quaternion.Euler(angles);
    }

    private void UpdateRotation()
    {
        if (playerController && playerModel)
        {
            float verticalZ = playerController.VerticalZ;
            float horizontalX = playerController.HorizontalX;

            if (Mathf.Abs(horizontalX) > Mathf.Abs(verticalZ))
            {
                if (horizontalX > 0)
                {
                    SetBlendedEulerAngles(Vector3.up * 90);
                    //Debug.Log("direita");
                }
                else if (horizontalX < 0)
                {
                    SetBlendedEulerAngles(Vector3.up * 270);
                    //Debug.Log("esquerda");
                }
            }
            else
            {
                if (verticalZ > 0)
                {
                    SetBlendedEulerAngles(Vector3.zero);
                   //Debug.Log("cima");
                }
                else if (verticalZ < 0)
                {
                    SetBlendedEulerAngles(Vector3.up * 180);
                    //Debug.Log("baixo");
                }
            }
            

            //currentRotationAngle = transform.eulerAngles.y;
            //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, targetAngle, rotationSpeed * Time.deltaTime);
            //currentRotationAngle = Quaternion.Euler(0, currentRotationAngle, 0);
            playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, targetRotation, turningRate * Time.deltaTime);
        }
    }
}
