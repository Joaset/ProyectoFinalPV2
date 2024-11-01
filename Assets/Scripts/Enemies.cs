using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float healthPoints;
    [SerializeField] private GameObject explosionEnemigo;
    [SerializeField] public float dañoCausado;


    void Start()
    {
        dañoCausado = 10f;
    }

   
    void Update()
    {
        
    }

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
}
