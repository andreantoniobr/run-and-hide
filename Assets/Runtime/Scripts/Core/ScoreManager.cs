using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PathSegmentController pathSegmentController;

    [Range(0f, 1f)]
    [SerializeField] private float scorePercent;

    private PathSegment[] pathSegments;
    
    private int currentPathSegmentsAmount;
    private int pathSegmentsAmount;
    
    public static event Action WinScoreEvent;
    public static event Action<float> UpdateScoreEvent;

    private void Awake()
    {
        scorePercent = 0;
        pathSegmentsAmount = 0;
        currentPathSegmentsAmount = 0;

        if (pathSegmentController)
        {
            pathSegments = pathSegmentController.PathSegments;
            pathSegmentsAmount = pathSegments.Length;
            scorePercent = GetScorePercent();
        }

        PathSegment.ActivePathEvent += IncrementScore;
    }

    private void OnDestroy()
    {
        PathSegment.ActivePathEvent -= IncrementScore;
    }

    private float GetScorePercent()
    {
        return (float) currentPathSegmentsAmount / pathSegmentsAmount;
    }   

    private void IncrementScore()
    {
        if (currentPathSegmentsAmount < pathSegmentsAmount)
        {
            currentPathSegmentsAmount++;
        }
        scorePercent = GetScorePercent();
        UpdateScoreEvent?.Invoke(scorePercent);
        if (scorePercent >= 1)
        {
            WinScoreEvent?.Invoke();
        }
    }
}
