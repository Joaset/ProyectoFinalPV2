using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    private float velocidad;
    private float fuerzaSalto;
    private Rigidbody2D rigidBody;
    [SerializeField] private LayerMask capaSuelo;
    private Animator animator;
    //private float velX;
    //private float velY;
    [SerializeField] private Transform suelo;
    private bool tocarSuelo;
    private float tocarSueloRadio;
    private float fuerzaGolpe;

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
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            tocarSuelo = Physics2D.OverlapCircle(suelo.position, tocarSueloRadio, capaSuelo);
            CambiarDireccion();

            if (tocarSuelo)
            {
                animator.SetBool("isJumping", false);
            }
            else
            {
                animator.SetBool("isJumping", true);
            }
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
        if (Input.GetButton("Jump") && tocarSuelo)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, fuerzaSalto);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.jump);
        }
    }

    void Mover()
    {
        float velX = Input.GetAxis("Horizontal");
        float velY = rigidBody.velocity.y;
        rigidBody.velocity = new Vector2(velX * velocidad, velY);

        if (rigidBody.velocity.x != 0)
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

    void CambiarDireccion()
    {
        if (rigidBody.velocity.x > 1)
        {
            transform.localScale = new Vector2(3, 3);
        }
        else if (rigidBody.velocity.x < -1)
        {
            transform.localScale = new Vector2(-3, 3);
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
            direccionGolpe = new Vector2(1, 1);
        }

        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);
    }
}
