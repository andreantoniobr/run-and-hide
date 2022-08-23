using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathType
{
    Defaut,
    Player,
    Enemy,
}

public class CharacterPathController : MonoBehaviour
{
    [SerializeField] private PathType pathType;

    private void OnTriggerEnter(Collider other)
    {
        SetPathType(other);
    }

    private void OnTriggerStay(Collider other)
    {
        SetPathType(other);
    }

    private void SetPathType(Collider other)
    {
        PathSegment pathSegment = other.GetComponent<PathSegment>();
        if (pathSegment)
        {
            pathSegment.SetPathType(pathType);
        }
    }
}
