using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float healthPoints;
    [SerializeField] private GameObject explosionEnemigo;
    [SerializeField] public float dañoCausado;
    [SerializeField] private AudioClip enemyDead;

    public void TomarDaño(float daño)
    {
        healthPoints -= daño;

        if (healthPoints<= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Instantiate(explosionEnemigo, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayAudio(AudioManager.Instance.enemydead);
        Destroy(gameObject);
    }

    public abstract void Move();
}
