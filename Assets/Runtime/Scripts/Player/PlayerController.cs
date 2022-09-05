using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isRunning;

    private bool isReceivingInput = true;
    private float horizontalX;
    private float verticalZ;
    private Vector3 currentPosition;

    public float HorizontalX => horizontalX;
    public float VerticalZ => verticalZ;
    public bool IsRunning => isRunning;


    private void Awake()
    {
        GameMode.GameOverEvent += OnGameOver;
    }

    private void OnDestroy()
    {
        GameMode.GameOverEvent -= OnGameOver;
    }

    private void OnGameOver()
    {
        if (isReceivingInput)
        {
            isReceivingInput = false;
            horizontalX = 0;
            verticalZ = 0;
            isRunning = false;
        }        
    }

    
    private void Update()
    {
        if (isReceivingInput)
        {

                            


#if UNITY_ANDROID
            UpdateTouchInput();
#endif
            //GetKeybordInput();
            isRunning = false;
            if (horizontalX != 0 || verticalZ != 0)
            {
                isRunning = true;
            }
        }        
    }

    private void UpdateTouchInput()
    {
        if (joystick)
        {
            horizontalX = joystick.Horizontal;
            verticalZ = joystick.Vertical;
        }               
    }

    private void GetKeybordInput()
    {
        horizontalX = Input.GetAxis("Horizontal");
        verticalZ = Input.GetAxis("Vertical");
        //Debug.Log($"{horizontalX} - {verticalZ}");       
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
