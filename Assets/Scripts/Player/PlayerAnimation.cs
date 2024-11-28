using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;

public class PlayerAnimation : MonoBehaviourPunCallbacks
{   
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private PlayerShooting playerShooting;
    void Start()
    {
        if (photonView.IsMine)
        {
            animator = GetComponent<Animator>();
            playerMovement = GetComponent<PlayerMovement>();
            playerInput = GetComponent<PlayerInput>();
            playerShooting = GetComponent<PlayerShooting>();
        }
    }

    void Update()
    {
        
    }

    public void AnimarSalto()
    {
        if (photonView.IsMine)
        {
            if (playerMovement.GetTocarSuelo())
            {
                animator.SetBool("isJumping", false);
            }
            else
            {
                animator.SetBool("isJumping", true);
            }
        }
    }

    public void AnimarMovimiento()
    {
        if (playerMovement.MoverPersonaje() != 0)
        {
            animator.SetBool("isRunning", true);
            if (playerInput.ApretarBotonDisparo())
            {
                animator.SetBool("isRunningShoot", true);
            }
            else
            {
                animator.SetBool("isRunningShoot", false);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void AnimarDisparo()
    {
        if (playerInput.ApretarBotonDisparo() && playerShooting.GetTiempoSiguienteAtaque() <= 0 && playerShooting.GetPuedeDisparar() == true)
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }
}
