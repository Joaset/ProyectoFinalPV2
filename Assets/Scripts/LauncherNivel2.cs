using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LauncherNivel2 : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            GameObject jugador = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation, 0);
            jugador.GetComponent<PhotonView>().RPC("SetNameText", RpcTarget.AllBuffered, PlayerPrefs.GetString("NombreJugador"));
        }
    }
}
