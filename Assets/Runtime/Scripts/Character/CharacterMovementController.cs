using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
public class CharacterMovementController : MonoBehaviour
{       
    [SerializeField] private bool isAtDestination;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isDancing;

    private AIPath ai;

    public bool IsAtDestination => isAtDestination;
    public bool IsRunning => isRunning;
    public bool IsDancing => isDancing;

    private void Awake()
    {
        ai = GetComponent<AIPath>();
        GameMode.GameWinEvent += OnGameWin;
    }
    
    private void Update()
    {
        if (ai.reachedEndOfPath)
        {
            isAtDestination = true;
            isRunning = false;
        }
        else
        {
            isAtDestination = false;
            isRunning = true;
        }
    }

    private void OnGameWin()
    {
        if (!isDancing)
        {
            isDancing = true;
        }
    }

    public void SetDestination(Vector3 destination)
    {
        ai.destination = destination;
    }

    public void StopImmediately()
    {
        ai.destination = transform.position;
        isRunning = false;
        isAtDestination = true;
    }
}
