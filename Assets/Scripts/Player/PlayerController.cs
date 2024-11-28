using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private float velocidad;
    private float fuerzaSalto;
    private Rigidbody2D rigidBody;
    [SerializeField] private LayerMask capaSuelo;
    private Animator animator;
    private float velX;
    private float velY;
    [SerializeField] private Transform suelo;
    private bool tocarSuelo;
    private float tocarSueloRadio;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    private float fuerzaGolpe;
    private bool puedeDisparar;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;

    void Start()
    {
        if (photonView.IsMine)
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            velocidad = 5f;
            fuerzaSalto = 12f;
            tocarSueloRadio = 0.2f;
            fuerzaGolpe = 500f;
            puedeDisparar = true;
        }
    }

    
    void Update()
    {
        if (photonView.IsMine)
        {
            tocarSuelo = Physics2D.OverlapCircle(suelo.position, tocarSueloRadio, capaSuelo);
            Girar();

            if (tocarSuelo)
            {
                animator.SetBool("isJumping", false);
            }
            else
            {
                animator.SetBool("isJumping", true);
            }

            Atacar();
        }
    }

    private void FixedUpdate() 
    {
        if (photonView.IsMine)
        {
            Mover();
            Saltar();
        }
    }

    void Saltar()
    {
        if(Input.GetButton("Jump") && tocarSuelo)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x,fuerzaSalto);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.jump);
        }
    }

    void Mover()
    {
        velX = Input.GetAxis("Horizontal");
        velY = rigidBody.velocity.y;
        rigidBody.velocity = new Vector2(velX * velocidad, velY);

        if(rigidBody.velocity.x != 0)
        {
            animator.SetBool("isRunning", true);
            if (Input.GetButton("Fire1"))
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

    void Girar()
    {
        if(rigidBody.velocity.x > 1)
        {
            transform.localScale = new Vector2( 3, 3);
            firePoint.transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(rigidBody.velocity.x < -1)
        {
            transform.localScale = new Vector2( -3, 3);
            firePoint.transform.eulerAngles = new Vector3(0,180,0);
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

    public void AplicarGolpe()
    {
        Vector2 direccionGolpe;

        if (rigidBody.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else
        {
            direccionGolpe = new Vector2(1,1);
        }

        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);
    }

    public void SetPuedeDisparar(bool disparar)
    {
        puedeDisparar = disparar;
    }
}
