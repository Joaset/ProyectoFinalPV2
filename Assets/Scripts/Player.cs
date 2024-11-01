using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private bool puedeRecibirDaño;
    private float cooldownDaño;
    private SpriteRenderer spriteRenderer;
    public GameObject gameOver;
    public Image barraVida;
    [SerializeField] private GameObject contadorVida;

    void Start()
    {
        puedeRecibirDaño = true;
        cooldownDaño = 3f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        barraVida.fillAmount = GameManager.Instance.vidaMaxima / 100;
        contadorVida.GetComponent<LifeCount>().ContarVida(GameManager.Instance.vidaMaxima);
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
                AudioManager.Instance.PlayAudio(AudioManager.Instance.dead);
                gameOver.SetActive(true);
                Time.timeScale = 0;
                GetComponent<PlayerController>().SetPuedeDisparar(false);
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
                AudioManager.Instance.PlayAudio(AudioManager.Instance.dead);
                gameOver.SetActive(true);
                Time.timeScale = 0;
                GetComponent<PlayerController>().SetPuedeDisparar(false);
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
