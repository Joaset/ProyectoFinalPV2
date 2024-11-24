using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;

public class Win : MonoBehaviour
{
    [SerializeField] private TMP_Text textoGanador;
    void Start()
    {
        textoGanador.text = "El ganador es " + PlayerPrefs.GetString("NombreJugador");
        AudioManager.Instance.PlayAudio(AudioManager.Instance.winMusic);
    }
    public void IniciarMenuPrincipal()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ResetVida();
        PhotonNetwork.Disconnect();
        AudioManager.Instance.StopAudio(AudioManager.Instance.winMusic);
    }
}
