using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class MoreLife : MonoBehaviourPunCallbacks
{
    [SerializeField] public int aumentoVida;

    private void Start()
    {
        aumentoVida = 10;
    }


    public void Morir()
    {
        Destroy(gameObject);
    }
}
