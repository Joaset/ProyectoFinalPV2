using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviourPunCallbacks
{
    private bool gameFinished = false;
    private int winnerActorNumber = -1;

    public void OnPlayerReachGoal()
    {
        if (gameFinished) return;
        gameFinished = true;
        //photonView.RPC("DecideWinner", RpcTarget.All);
        photonView.RPC("NotifyWinner", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    [PunRPC]
    //public void DecideWinner()
    //{
    //    if (PhotonNetwork.LocalPlayer.IsMasterClient)
    //    {
    //        SceneManager.LoadScene(3);
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene(4);
    //    }
    //}

    public void NotifyWinner(int winnerActorNumber)
    {
        if (this.winnerActorNumber == -1)
        {
            this.winnerActorNumber = winnerActorNumber;
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == this.winnerActorNumber)
        {
            SceneManager.LoadScene(3);
            AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
        }
        else
        {
            SceneManager.LoadScene(4);
            AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
        }
    }
}
