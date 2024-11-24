using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviourPunCallbacks
{
    private bool puedeRecibirDaño;
    private float cooldownDaño;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Image barraVida;
    [SerializeField] private GameObject contadorVida;
    private DisableHud playerHud;

    void Start()
    {
        if (photonView.IsMine)
        {
            puedeRecibirDaño = true;
            cooldownDaño = 3f;
            spriteRenderer = GetComponent<SpriteRenderer>();
            GameManager.Instance.ResetVida();
            ActualizarBarraVida();
            playerHud = GameObject.Find("Canvas").GetComponent<DisableHud>();
            playerHud.SetPlayer(gameObject);
            barraVida = GameObject.Find("Canvas").transform.Find("Vida").GetComponent<Image>();
            contadorVida = GameObject.Find("CantVida");
        }
    }

    
    void Update()
    {
        if (photonView.IsMine)
        {
            contadorVida.GetComponent<LifeCount>().ContarVida(GameManager.Instance.vidaMaxima);
            ActualizarBarraVida();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
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
                    Destroy(gameObject);
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
    }

    void ActivarDaño()
    {
        puedeRecibirDaño = true;
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }

    private void ActualizarBarraVida()
    {
        if (barraVida != null)
        {
            barraVida.fillAmount = GameManager.Instance.vidaMaxima / 100f;
        }
    }
}
