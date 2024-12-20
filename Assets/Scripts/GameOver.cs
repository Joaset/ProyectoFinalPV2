using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviourPunCallbacks
{
    void Start()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.dead);
    }

    public void IniciarMenuPrincipal()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ResetVida();
        PhotonNetwork.Disconnect();
        AudioManager.Instance.StopAudio(AudioManager.Instance.dead);
    }
}
