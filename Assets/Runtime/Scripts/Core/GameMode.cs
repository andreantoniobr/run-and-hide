using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private float timeToReloadGame = 3f;

    private bool isStarted = true;

    public bool IsStarted => isStarted;

    public static event Action StartGameEvent;
    public static event Action GameWinEvent;
    public static event Action GameOverEvent;
    public static event Action GameReloadEvent;

    private void Awake()
    {
        SubscribeInEvents();
    }    

    private void OnDestroy()
    {        
        UnsubscribeInEvents();
    }

    private void Start()
    {
        StartGameEvent?.Invoke();
    }

    private void SubscribeInEvents()
    {        
        ScoreManager.WinScoreEvent += OnGameWin;
        EnemyController.PlayerInFieldOfViewEvent += OnGameOver;
    }

    private void UnsubscribeInEvents()
    {
        ScoreManager.WinScoreEvent -= OnGameWin;
        EnemyController.PlayerInFieldOfViewEvent -= OnGameOver;
    }

    private void OnGameWin()
    {
        GameWinEvent?.Invoke();
    }

    private void OnGameOver()
    {
        if (isStarted)
        {
            isStarted = false;
            GameOverEvent?.Invoke();
            StartCoroutine(ReloadGameCoroutine());
        }        
    }

    private void OnFinish()
    {
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(timeToReloadGame);
        GameReloadEvent?.Invoke();
        ReloadScene();
    }

    private void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
