using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviourPunCallbacks, IDamageable
{
    private SpriteRenderer spriteRenderer;
    private bool puedeRecibirDa�o;
    private float cooldownDa�o = 3f;

    void Start()
    {
        if (photonView.IsMine)
        {
            puedeRecibirDa�o = true;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void RecibirDa�o(float da�o)
    {
        if (!puedeRecibirDa�o) return;

        DesactivarDa�o();
        GameManager.Instance.restarVida(da�o);
        gameObject.GetComponent<PlayerMovement>().AplicarGolpe();

        if (GameManager.Instance.vidaMaxima <= 0)
        {
            Morir();
        }
        else
        {
            Invoke("ActivarDa�o", cooldownDa�o);
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

    private void ActivarDa�o()
    {
        puedeRecibirDa�o = true;
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }

    private void DesactivarDa�o()
    {
        puedeRecibirDa�o = false;
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }
}
