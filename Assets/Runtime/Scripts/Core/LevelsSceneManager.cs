using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameLevel
{
    public int LevelNumberID;
    public string LevelSceneName;
    public bool IsFinished;
}

public class LevelsSceneManager : MonoBehaviour
{
    [SerializeField] private float timeToLoadScene = 0.5f;
    [SerializeField] private GameLevel currentGameLevel;
    [SerializeField] private GameLevel[] gameLevels;

    private static LevelsSceneManager instance;
    public static LevelsSceneManager Instance => instance;

    //private LevelManager levelManager;

    
    private void Awake()
    {
        SetThisInstance();
        GameMode.StartGameEvent += OnStartGame;
        GameMode.GameWinEvent += OnGameWin;
        GameMode.GameOverEvent += OnGameOver;
    }

    private void OnDestroy()
    {
        GameMode.GameWinEvent -= OnGameWin;
        GameMode.GameOverEvent -= OnGameOver;
    }    

    /*
    //TODO: VERIFY WITH EVENTS
    private void Update()
    {
        VerifyIfCurrentLevelFinished();
    }*/

    private void Start()
    {
        //levelManager = LevelManager.Instance;

    }

    private void SetThisInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    /*
    private void VerifyIfCurrentLevelFinished()
    {
        if (currentGameLevel.IsFinished)
        {
            SetLevelIsFinished();
            Debug.Log("cena terminou");
            PassToNextLevel();
        }
    }*/

    private void SetLevelIsFinished()
    {
        foreach (GameLevel gameLevel in gameLevels)
        {
            if (gameLevel.LevelNumberID == currentGameLevel.LevelNumberID)
            {
                gameLevel.IsFinished = true;
                break;
            }
        }
    }

    private void PassToNextLevel()
    {
        GameLevel gameLevel = GetNextLevel();
        if (gameLevel != null && !string.IsNullOrEmpty(gameLevel.LevelSceneName))
        {
            Debug.Log(gameLevel.LevelSceneName);
            currentGameLevel = gameLevel;
            StartCoroutine(LoadNextSceneCoroutine(gameLevel));
        }
    }

    private GameLevel GetNextLevel()
    {
        GameLevel nextLevel = null;
        foreach (GameLevel gameLevel in gameLevels)
        {
            if (gameLevel.LevelNumberID == currentGameLevel.LevelNumberID + 1 && gameLevel.IsFinished == false)
            {
                nextLevel = gameLevel;
                break;
            }
        }
        if (nextLevel == null)
        {
            nextLevel = GetRandomLevel();
        }        
        return nextLevel;
    }

    private GameLevel GetRandomLevel()
    {
        GameLevel level = null;
        int levelsAmount = gameLevels.Length;
        if (levelsAmount > 0)
        {
            int levelIndex = Random.Range(0, levelsAmount);
            if (levelIndex >= 0 && levelIndex <= levelsAmount - 1)
            {
                level = gameLevels[levelIndex];
            }
        }
        return level;
    }

    private IEnumerator LoadSceneCoroutine()
    {
        yield return StartCoroutine(UIAnimationsController.Instance.PlayFadeOut());
    }

    private IEnumerator LoadNextSceneCoroutine(GameLevel gameLevel)
    {
        UIManager.Instance.ScreenController.SetActiveScreen(ScreenType.LevelCleared);
        yield return new WaitForSeconds(timeToLoadScene);
        yield return StartCoroutine(UIAnimationsController.Instance.PlayFadeIn());        
        UIManager.Instance.ScreenController.SetInactiveAllScreens();
        SceneManager.LoadScene(gameLevel.LevelSceneName);
        yield return StartCoroutine(LoadSceneCoroutine());
    }

    public void OnGameWin()
    {
        SetLevelIsFinished();
        PassToNextLevel();
    }

    private void OnStartGame()
    {
        UIManager.Instance.ScreenController.SetInactiveAllScreens();
    }

    private void OnGameOver()
    {
        UIManager.Instance.ScreenController.SetActiveScreen(ScreenType.LevelFail);
    }
}
