using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float vidaMaxima
        ;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void restarVida(float vidaRestar)
    {
        vidaMaxima -= vidaRestar;
    }

    public void sumarVida(float vidaSumar)
    {
        vidaMaxima += vidaSumar;
    }

    public void ResetVida()
    {
        vidaMaxima = 100f;
    }
}
