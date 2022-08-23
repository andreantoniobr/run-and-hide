using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment : MonoBehaviour
{
    [SerializeField] private PathModel[] pathModels;
    [SerializeField] private List<PathModel> pathModelsList;

    private PathType currentPathtype;

    public PathType CurrentPathtype => currentPathtype;

    private void Start()
    {
        SpawnPaths();
        SetPathType(PathType.Defaut);
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
                        currentPathtype = pathType;
                        pathModel.gameObject.SetActive(true);
                    }
                    else
                    {
                        pathModel.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
