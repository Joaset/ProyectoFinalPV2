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
                float daño = collision.GetComponent<Enemy>().dañoCausado;
                playerHealth.RecibirDaño(daño);
            }

            if (collision.CompareTag("lava"))
            {
                float daño = collision.GetComponent<LavaDamage>().dañoCausado;
                playerHealth.RecibirDaño(daño);
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
