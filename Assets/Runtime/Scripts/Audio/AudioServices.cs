using UnityEngine;

public class AudioServices : AudioPlayerMain
{
    [SerializeField] private AudioClip gameoverAudioClip;
    [SerializeField] private AudioClip incrementScoreAudioClip;  

    private void Awake()
    {
        GameMode.GameOverEvent += OnGameOver;
        PathSegment.ActivePathEvent += OnIncrementScore;
    }

    private void OnDestroy()
    {
        GameMode.GameOverEvent -= OnGameOver;
        PathSegment.ActivePathEvent += OnIncrementScore;
    }    

    private void OnGameOver()
    {
        PlayAudioCue(gameoverAudioClip);
    }

    private void OnIncrementScore()
    {
        PlayAudioOneShot(incrementScoreAudioClip, 0.2f);
    }
}
