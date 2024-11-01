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
    public Slider masterSlider, effectslider;
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
    void Start()
    {
        masterSlider.value = masterVol;
        effectslider.value = effectsVol;
        masterSlider.minValue = -80;
        masterSlider.maxValue = 10;
        effectslider.minValue = -80;
        effectslider.maxValue = 10;
    }

    void Update()
    {
        if (musicControl == true) 
        {
            MasterVolume();
            EffectsVolume();
        }
    }

    public void MasterVolume()
    {
        musicMixer.SetFloat("masterVolume", masterSlider.value);
    }

    public void EffectsVolume()
    {
        effectsMixer.SetFloat("effectsVolume", effectslider.value);
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
