using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void Jugar()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        AudioManager.Instance.StopAudio(AudioManager.Instance.menuMusic);
        AudioManager.Instance.PlayAudio(AudioManager.Instance.backgroundMusic);
        AudioManager.Instance.SetMusicControl(false);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
