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
    private bool puedeElegir;

    void Start()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.menuMusic);
        puedeElegir = true;
    }

    public void OnClickConnect(int nivel)
    {
        if(nombreUsuario.text.Length >= 1)
        {
            PhotonNetwork.NickName = nombreUsuario.text;

            PlayerPrefs.SetString("NombreJugador",nombreUsuario.text);

            if (nivel == 1 && puedeElegir == true)
            {
                boton.text = "Conectando...";
                puedeElegir=false;
                PhotonNetwork.ConnectUsingSettings();
                PlayerPrefs.SetInt("NivelSeleccionado", nivel);
            }
            if(nivel == 2 && puedeElegir == true)
            {
                boton2.text = "Conectando...";
                puedeElegir=false;
                PhotonNetwork.ConnectUsingSettings();
                PlayerPrefs.SetInt("NivelSeleccionado", nivel);
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        int nivel = PlayerPrefs.GetInt("NivelSeleccionado");
        SceneManager.LoadScene(nivel);
        AudioManager.Instance.StopAudio(AudioManager.Instance.menuMusic);
        AudioManager.Instance.PlayAudio(AudioManager.Instance.backgroundMusic);
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
