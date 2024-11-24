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
    [SerializeField] private TMP_Text boton2;
    [SerializeField] private GameObject niveles;
    [SerializeField] private GameObject menu;
    void Start()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.menuMusic);
    }

    public void OnClickConnect(int nivel)
    {
        if(nombreUsuario.text.Length >= 1)
        {
            PhotonNetwork.NickName = nombreUsuario.text;

            PlayerPrefs.SetString("NombreJugador",nombreUsuario.text);
            if (nivel == 1)
            {
                boton.text = "Conectando...";
            }
            if(nivel == 2)
            {
                boton2.text = "Conectando...";
            }
            PhotonNetwork.ConnectUsingSettings();
            PlayerPrefs.SetInt("NivelSeleccionado", nivel);
        }
    }

    public override void OnConnectedToMaster()
    {
        int nivel = PlayerPrefs.GetInt("NivelSeleccionado");
        SceneManager.LoadScene(nivel);
        AudioManager.Instance.StopAudio(AudioManager.Instance.menuMusic);
        AudioManager.Instance.PlayAudio(AudioManager.Instance.backgroundMusic);
        AudioManager.Instance.SetMusicControl(false);
    }

    public void CambiarNivel()
    {
        niveles.SetActive(true);
        menu.SetActive(false);
    }

    public void Volver()
    {
        niveles.SetActive(false);
        menu.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
