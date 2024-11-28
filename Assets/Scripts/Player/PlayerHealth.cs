using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviourPunCallbacks, IDamageable
{
    private SpriteRenderer spriteRenderer;
    private bool puedeRecibirDaño;
    private float cooldownDaño = 3f;

    void Start()
    {
        if (photonView.IsMine)
        {
            puedeRecibirDaño = true;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void RecibirDaño(float daño)
    {
        if (!puedeRecibirDaño) return;

        DesactivarDaño();
        GameManager.Instance.restarVida(daño);
        gameObject.GetComponent<PlayerMovement>().AplicarGolpe();

        if (GameManager.Instance.vidaMaxima <= 0)
        {
            Morir();
        }
        else
        {
            Invoke("ActivarDaño", cooldownDaño);
        }
    }

    public void Curar(int vida)
    {
        if (GameManager.Instance.vidaMaxima < 100)
        {
            GameManager.Instance.sumarVida(vida);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.life);
        }
    }

    private void Morir()
    {
        SceneManager.LoadScene(4);
        Destroy(gameObject);
        AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
    }

    private void ActivarDaño()
    {
        puedeRecibirDaño = true;
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }

    private void DesactivarDaño()
    {
        puedeRecibirDaño = false;
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }
}
