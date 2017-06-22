using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = true;
        audioSource.loop = true;
        
        switch (SceneManager.GetActiveScene().name.Substring(5, 2))
        {
            case "00":
                audioSource.clip = Resources.Load("Sound/BGM/chord05") as AudioClip;
                break;
            case "01":
                audioSource.clip = Resources.Load("Sound/BGM/chord05") as AudioClip;
                break;
            default:
                break;
        }
    }

    public void PlayBGM()
    {
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
