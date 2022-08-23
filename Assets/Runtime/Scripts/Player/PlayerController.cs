using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isRunning;

    private float horizontalX;
    private float verticalZ;
    private Vector3 currentPosition;    

    public float HorizontalX => horizontalX;
    public float VerticalZ => verticalZ;
    public bool IsRunning => isRunning;

    private void Update()
    {
        horizontalX = Input.GetAxis("Horizontal");
        verticalZ = Input.GetAxis("Vertical");

        isRunning = false;
        if (horizontalX != 0 || verticalZ != 0)
        {
            isRunning = true;
        }
    }

    private void FixedUpdate()
    {
        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition()
    {        
        currentPosition = transform.position;
        if (Mathf.Abs(horizontalX) > Mathf.Abs(verticalZ))
        {
            currentPosition.x += horizontalX * speed * Time.fixedDeltaTime;
        }
        else
        {
            currentPosition.z += verticalZ * speed * Time.fixedDeltaTime;
        }
        transform.position = currentPosition;
    }
}
