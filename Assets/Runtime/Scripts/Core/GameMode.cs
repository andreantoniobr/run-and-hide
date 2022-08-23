using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private float timeToReloadGame = 3f;

    private bool isStarted = false;

    public bool IsStarted => isStarted;

    //public static event Action StartGameEvent;
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

    private void SubscribeInEvents()
    {
    }

    private void UnsubscribeInEvents()
    {
    }

    private void OnGameWin()
    {
        GameWinEvent?.Invoke();
    }

    private void OnArmyDeath()
    {
        OnGameOver();
    }

    private void OnGameOver()
    {
        isStarted = false;
        GameOverEvent?.Invoke();
        StartCoroutine(ReloadGameCoroutine());
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
