using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks, IMovable
{
    private float velocidad;
    private float fuerzaSalto;
    private Rigidbody2D rigidBody;
    [SerializeField] private LayerMask capaSuelo;
    [SerializeField] private Transform suelo;
    private PlayerInput playerInput;
    private bool tocarSuelo;
    private float tocarSueloRadio;
    private float fuerzaGolpe;

    void Start()
    {
        if (photonView.IsMine)
        {
            rigidBody = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput>();
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
        }
    }

    public void Saltar()
    {
        if (playerInput.ApretarBottonSalto() && tocarSuelo)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, fuerzaSalto);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.jump);
        }
    }

    public void Mover()
    {
        float velX = playerInput.ApretarBotonMover();
        float velY = rigidBody.velocity.y;
        rigidBody.velocity = new Vector2(velX * velocidad, velY);
    }

    public void Girar()
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

    public bool GetTocarSuelo()
    {
        return tocarSuelo;
    }

    public float MoverPersonaje()
    {
        return rigidBody.velocity.x;
    }
}
