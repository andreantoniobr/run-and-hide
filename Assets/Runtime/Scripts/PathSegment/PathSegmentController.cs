using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegmentController : MonoBehaviour
{
    [SerializeField] private PathSegment[] pathSegments;
    public PathSegment[] PathSegments => pathSegments;
}
