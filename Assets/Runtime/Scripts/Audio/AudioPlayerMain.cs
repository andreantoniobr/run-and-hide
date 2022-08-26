using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayerMain : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }        
    }

    protected void PlayAudioCue(AudioClip audioClip, float volume = 1f, bool isLooping = false)
    {
        if (audioSource)
        {
            if (audioSource.outputAudioMixerGroup && audioClip)
            {
                audioSource.clip = audioClip;
                audioSource.loop = isLooping;
                audioSource.volume = volume;
                audioSource.Play();
            }
            else
            {
                PrintAudioMixerGroupError();
            }
        }        
    }

    protected void PlayAudioOneShot(AudioClip audioClip, float volume = 1f) 
    {
        if (audioSource)
        {
            if (audioSource.outputAudioMixerGroup && audioClip)
            {
                audioSource.volume = volume;
                audioSource.PlayOneShot(audioClip);                
            }
            else
            {
                PrintAudioMixerGroupError();
            }
        }
    }

    protected void StopAudio()
    {
        if (audioSource)
        {
            audioSource.Stop();
        }
    }

    private void PrintAudioMixerGroupError()
    {
        Debug.Log("Erro: Every AudioSource must have an AudioMixerGroup assigned.");
    }
}
