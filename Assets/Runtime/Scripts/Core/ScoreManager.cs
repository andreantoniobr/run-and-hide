using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PathSegmentController pathSegmentController;
    [SerializeField] private float updateScoreDelay = 1f;

    private PathSegment[] pathSegments;
    private int score;

    private void Awake()
    {
        if (pathSegmentController)
        {
            pathSegments = pathSegmentController.PathSegments;
        }
    }

    private void Start()
    {
        StartCoroutine(UpdateScoreCouroutine());
    }

    private IEnumerator UpdateScoreCouroutine()
    {
        while (true)
        {
            UpdateScore();
            Debug.Log(score);
            yield return new WaitForSeconds(updateScoreDelay);
        }
    }

    private void UpdateScore()
    {
        score = 0;
        if (pathSegments.Length > 0)
        {
            for (int i = 0; i < pathSegments.Length; i++)
            {
                PathSegment pathSegment = pathSegments[i];
                if (pathSegment && pathSegment.CurrentPathtype == PathType.Player)
                {
                    score++;
                }
            }
        }
    }
}
