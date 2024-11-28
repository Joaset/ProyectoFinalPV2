using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    private bool puedeDisparar;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private PlayerInput playerInput;
    [SerializeField] private float tiempoEntreAtaques;
     private float tiempoSiguienteAtaque;

    void Start()
    {
        if (photonView.IsMine)
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerAnimation = GetComponent<PlayerAnimation>();
            playerInput = GetComponent<PlayerInput>();
            puedeDisparar = true;
            tiempoSiguienteAtaque = 0f;
        }
    }

    public void CambiarDireccion()
    {
        if (playerMovement.MoverPersonaje() > 1)
        {
            firePoint.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (playerMovement.MoverPersonaje() < -1)
        {
            firePoint.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void Atacar()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if (playerInput.ApretarBotonDisparo() && tiempoSiguienteAtaque <= 0 && puedeDisparar == true)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.shoot);
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    public float GetTiempoSiguienteAtaque()
    {
        return tiempoSiguienteAtaque;
    }

    public bool GetPuedeDisparar()
    {
        return puedeDisparar;
    }
}
