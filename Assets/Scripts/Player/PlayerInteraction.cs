using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPunCallbacks
{
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.CompareTag("enemy"))
            {
                float da�o = collision.GetComponent<Enemy>().da�oCausado;
                playerHealth.RecibirDa�o(da�o);
            }

            if (collision.CompareTag("lava"))
            {
                float da�o = collision.GetComponent<LavaDamage>().da�oCausado;
                playerHealth.RecibirDa�o(da�o);
            }

            if (collision.CompareTag("vida"))
            {
                int aumentoVida = collision.GetComponent<MoreLife>().aumentoVida;
                playerHealth.Curar(aumentoVida);
                collision.GetComponent<MoreLife>().Morir();
            }
        }
    }
}
