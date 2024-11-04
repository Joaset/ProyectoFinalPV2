using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    private bool puedeRecibirDaño;
    private float cooldownDaño;
    private SpriteRenderer spriteRenderer;
    private Image barraVida;
    private GameObject contadorVida;
    private CinemachineVirtualCamera camara;
    private UICamara camaraHud;
    private DisabledHud playerHud;

    void Start()
    {
        if (photonView.IsMine)
        {
            puedeRecibirDaño = true;
            cooldownDaño = 3f;
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerHud = GameObject.Find("Canvas").GetComponent<DisabledHud>();
            playerHud.SetPlayer(gameObject);
            barraVida = GameObject.Find("Canvas").transform.Find("Vida").GetComponent<Image>();
            contadorVida = GameObject.Find("CantVida");
            camara = FindObjectOfType<CinemachineVirtualCamera>();
            camara.Follow = gameObject.transform;
            camaraHud = GameObject.Find("Camera").GetComponent<UICamara>();
            camaraHud.SetPlayer(gameObject.transform);
        }
    }

    
    void Update()
    {
        if (photonView.IsMine)
        {
            barraVida.fillAmount = GameManager.Instance.vidaMaxima / 100;
            contadorVida.GetComponent<LifeCount>().ContarVida(GameManager.Instance.vidaMaxima);
        }
        //    barraVida.fillAmount = GameManager.Instance.vidaMaxima / 100;
        //contadorVida.GetComponent<LifeCount>().ContarVida(GameManager.Instance.vidaMaxima);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            if (!puedeRecibirDaño)
            {
                return;
            }

            puedeRecibirDaño = false;
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;
            GameManager.Instance.restarVida(collision.GetComponent<Enemies>().dañoCausado);
            gameObject.GetComponent<PlayerController>().AplicarGolpe();

            if (GameManager.Instance.vidaMaxima <= 0)
            {
                SceneManager.LoadScene(4);
                Destroy(gameObject);
                AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
            }

            Invoke("ActivarDaño", cooldownDaño);
        }

        if (collision.CompareTag("lava"))
        {
            if (!puedeRecibirDaño)
            {
                return;
            }

            puedeRecibirDaño = false;
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;
            GameManager.Instance.restarVida(collision.GetComponent<LavaDamage>().dañoCausado);
            gameObject.GetComponent<PlayerController>().AplicarGolpe();

            if (GameManager.Instance.vidaMaxima <= 0)
            {
                SceneManager.LoadScene(4);
                Destroy (gameObject);
                AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
            }

            Invoke("ActivarDaño", cooldownDaño);
        }
        if (collision.CompareTag("vida"))
        {
            if (GameManager.Instance.vidaMaxima < 100)
            {
                GameManager.Instance.sumarVida(collision.GetComponent<MoreLife>().aumentoVida);
                collision.GetComponent<MoreLife>().Morir();
                AudioManager.Instance.PlayAudio(AudioManager.Instance.life);
            }
        }
    }

    void ActivarDaño()
    {
        puedeRecibirDaño = true;
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }
}
