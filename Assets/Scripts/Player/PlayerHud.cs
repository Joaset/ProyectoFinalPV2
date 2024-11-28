using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image barraVida;
    [SerializeField] private GameObject contadorVida;
    private DisableHud playerHud;

    void Update()
    {
        if (photonView.IsMine)
        {
            contadorVida.GetComponent<LifeCount>().ContarVida(GameManager.Instance.vidaMaxima);
        }
    }

    public void ActualizarBarraVida()
    {
        if (barraVida != null)
        {
            barraVida.fillAmount = GameManager.Instance.vidaMaxima / 100f;
        }
    }

    public void Hud()
    {
        if (photonView.IsMine)
        {
            playerHud = GameObject.Find("Canvas").GetComponent<DisableHud>();
            playerHud.SetPlayer(gameObject);
            barraVida = GameObject.Find("Canvas").transform.Find("Vida").GetComponent<Image>();
            contadorVida = GameObject.Find("CantVida");
            ActualizarBarraVida();
        }
    }
}
