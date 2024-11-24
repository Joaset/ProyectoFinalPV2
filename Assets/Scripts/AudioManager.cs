using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioMixer musicMixer, effectsMixer;
    public AudioSource menuMusic, backgroundMusic, shoot, enemydead, dead, jump, winMusic, life;
    public static AudioManager Instance;
    [Range(-80, 10)]
    public float masterVol, effectsVol;
    [SerializeField] private bool musicControl = true;

    private void Awake()
    {
        if (AudioManager.Instance == null)
        {
            AudioManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void StopAudio(AudioSource audio)
    {
        audio.Stop();
    }

    public void SetMusicControl(bool control)
    {
        musicControl = control;
    }
}
