using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviourPunCallbacks
{
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private PlayerInput playerInput;
    private PlayerHud playerHud;
    private PlayerInteraction playerInteraction;
    private PlayerShooting playerShooting;

    void Start()
    {
        if (photonView.IsMine)
        {
            playerHealth = GetComponent<PlayerHealth>();
            playerMovement = GetComponent<PlayerMovement>();
            playerAnimation = GetComponent<PlayerAnimation>();
            playerInput = GetComponent<PlayerInput>();
            playerHud = GetComponent<PlayerHud>();
            playerInteraction = GetComponent<PlayerInteraction>();
            playerShooting = GetComponent<PlayerShooting>();
            playerHud.Hud();
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            playerAnimation.AnimarSalto();
            playerMovement.Girar();
            playerHud.ActualizarBarraVida();
            playerShooting.CambiarDireccion();
            playerAnimation.AnimarDisparo();
            playerShooting.Atacar();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            playerMovement.Mover();
            playerAnimation.AnimarMovimiento();
            playerMovement.Saltar();
        }
    }
}
