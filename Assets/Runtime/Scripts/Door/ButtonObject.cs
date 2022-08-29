using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    [SerializeField] private bool isActive;

    public event Action<bool> ButtonActiveEvent;

    private void OnTriggerEnter(Collider other)
    {        
        Player player = other.GetComponent<Player>();
        if (player)
        {
            if (!isActive)
            {
                isActive = true;
                
            }
            else
            {
                isActive = false;
            }
            ButtonActiveEvent?.Invoke(isActive);
        }
    }
}
