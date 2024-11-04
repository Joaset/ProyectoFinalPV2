using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField nombreUsuario;
    [SerializeField] private TMP_Text boton;
    void Start()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.menuMusic);
    }
    //public void Jugar()
    //{
    //    SceneManager.LoadScene(1);
    //    AudioManager.Instance.StopAudio(AudioManager.Instance.menuMusic);
    //    AudioManager.Instance.PlayAudio(AudioManager.Instance.backgroundMusic);
    //    AudioManager.Instance.SetMusicControl(false);
    //}

    public void OnClickConnect()
    {
        if(nombreUsuario.text.Length >= 1)
        {
            PhotonNetwork.NickName = nombreUsuario.text;

            PlayerPrefs.SetString("NombreJugador",nombreUsuario.text);

            boton.text = "Conectando...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(1);
        AudioManager.Instance.StopAudio(AudioManager.Instance.menuMusic);
        AudioManager.Instance.PlayAudio(AudioManager.Instance.backgroundMusic);
        AudioManager.Instance.SetMusicControl(false);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
