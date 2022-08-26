using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment : MonoBehaviour
{
    [SerializeField] private PathType defaultPathType;
    [SerializeField] private PathModel[] pathModels;
    [SerializeField] private List<PathModel> pathModelsList;

    private PathType currentPathtype = PathType.None;

    public PathType CurrentPathtype => currentPathtype;

    public static event Action ActivePathEvent;

    private void Start()
    {
        SpawnPaths();
        SetPathType(defaultPathType);
    }

    private void SpawnPaths()
    {
        if (pathModels.Length > 0)
        {
            for (int i = 0; i < pathModels.Length; i++)
            {
                PathModel pathModel = pathModels[i];
                if (pathModel)
                {
                    PathModel currentPathModel = Instantiate(pathModel, transform);
                    if (currentPathModel)
                    {
                        pathModelsList.Add(currentPathModel);
                    }
                }
            }
        }
    }

    public void SetPathType(PathType pathType)
    {
        if (pathModelsList.Count > 0)
        {
            for (int i = 0; i < pathModelsList.Count; i++)
            {
                PathModel pathModel = pathModelsList[i];
                if (pathModel)
                {
                    if (pathType == pathModel.PathType)
                    {
                        ActivePath(pathType, pathModel);
                    }
                    else
                    {
                        pathModel.gameObject.SetActive(false);
                    }
                }
            }
        }
               
    }

    private void ActivePath(PathType pathType, PathModel pathModel)
    {
        currentPathtype = pathType;        
        pathModel.gameObject.SetActive(true);
    }

    public void SetPathPlayer()
    {
        if (currentPathtype != PathType.Player)
        {
            ActivePathEvent?.Invoke();
            SetPathType(PathType.Player);
        }        
    }
}
