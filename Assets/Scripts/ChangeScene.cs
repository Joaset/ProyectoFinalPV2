using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviourPunCallbacks
{
   private void OnTriggerEnter2D(Collider2D collision) 
   {
        if (collision.gameObject.tag == "Player")
        {
            FinishGame finalizarJuego = FindObjectOfType<FinishGame>();
            if (finalizarJuego != null)
            {
                finalizarJuego.OnPlayerReachGoal();
            }
        }
    }
}
