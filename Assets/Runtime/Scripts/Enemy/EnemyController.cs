using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementController))]
[RequireComponent(typeof(CharacterFieldOfView))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private PathSegmentController pathSegmentController;
    [SerializeField] private float moveDelay = 5f;
    [SerializeField] private float chaseEnemyDelay = 0.2f;
    [SerializeField] private bool isDead;

    private CharacterMovementController characterMovementController;    
    private CharacterFieldOfView characterFieldOfView;

    public bool IsDead => isDead;

    private void Awake()
    {
        if (!characterMovementController)
        {
            characterMovementController = GetComponent<CharacterMovementController>();
        }
        if (!characterFieldOfView)
        {
            characterFieldOfView = GetComponent<CharacterFieldOfView>();
        }       
    }

    private void Start()
    {
        StartCoroutine(ChasingEnemysCoroutine());
        StartCoroutine(MoveCoroutine());
    }    

    private IEnumerator ChasingEnemysCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(chaseEnemyDelay);
            ChasingEnemys();
        }
    }

    private void ChasingEnemys()
    {
        if (!isDead)
        {
            Transform target = GetClosetTarget();
            if (target)
            {
                Move();
            }
        }
    }

    private Transform GetClosetTarget()
    {
        Transform target = null;
        float minDistance = float.MaxValue;

        if (characterFieldOfView.VisibleTargets.Count > 0)
        {
            foreach (Transform currentTarget in characterFieldOfView.VisibleTargets)
            {
                if (currentTarget)
                {
                    float distanceToCurrentTarget = Vector3.Distance(transform.position, currentTarget.position);
                    if (distanceToCurrentTarget < minDistance)
                    {
                        target = currentTarget;
                        minDistance = distanceToCurrentTarget;
                    }
                }
            }
        }
        return target;
    }

    private IEnumerator MoveCoroutine()
    {        
        while (true)
        {            
            Move();
            yield return new WaitForSeconds(moveDelay);
        }    
    }

    private void Move()
    {
        if (GetRandomPathSegmentPosition(out Vector3 position))
        {
            SetDestination(position);
        }
    }

    private void SetDestination(Vector3 position)
    {
        if (characterMovementController)
        {
            characterMovementController.SetDestination(position);
        }
    }

    private bool GetRandomPathSegmentPosition(out Vector3 position)
    {
        bool canGetPosition = false;
        position = Vector3.zero;
        if (pathSegmentController)
        {
            PathSegment[] pathSegments = pathSegmentController.PathSegments;
            int pathSegmentsAmount = pathSegments.Length;
            if (pathSegmentsAmount > 0)
            {
                int randomPathSegmentIndex = Random.Range(0, pathSegmentsAmount);
                if (randomPathSegmentIndex >= 0 && randomPathSegmentIndex <= pathSegmentsAmount - 1)
                {
                    PathSegment pathSegment = pathSegments[randomPathSegmentIndex];
                    if (pathSegment)
                    {
                        canGetPosition = true;
                        position = pathSegment.transform.position;
                    }
                }
            }
        }
        return canGetPosition;
    }
}
