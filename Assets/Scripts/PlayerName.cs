using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private TMP_Text nombreJugador;

    [PunRPC]

    public void SetNameText(string nombre)
    {
        nombreJugador.text = nombre;
    }
}
