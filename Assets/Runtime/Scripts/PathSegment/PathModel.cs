using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathModel : MonoBehaviour
{
    [SerializeField] private PathType pathType;
    public PathType PathType => pathType;
}
