using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    private bool puedeDisparar;
    private Rigidbody2D rigidBody;
    private Animator animator;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    void Start()
    {
        if (photonView.IsMine)
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            puedeDisparar = true;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            CambiarDireccion();
            Atacar();
        }
    }

    void CambiarDireccion()
    {
        if (rigidBody.velocity.x > 1)
        {
            firePoint.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (rigidBody.velocity.x < -1)
        {
            firePoint.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Atacar()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0 && puedeDisparar == true)
        {
            animator.SetBool("isShooting", true);
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.shoot);
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }
}
