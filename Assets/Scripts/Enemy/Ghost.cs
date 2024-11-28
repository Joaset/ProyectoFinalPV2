using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy, IDamageable, IMovable
{
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private float speed;
    private bool debeMover;
    private bool debeEsperar;
    private bool mueveA;
    private bool mueveB;
    private float tiempoEspera;
    private bool seMueve;

    void Start()
    {
        healthPoints = 4;
        mueveA = true;
        mueveB = true;
        debeMover = true;
        speed = 3f;
        seMueve = true;
        tiempoEspera = 1.5f;
        debeEsperar = true;
        dañoCausado = 10f;
    }


    void Update()
    {
        if (debeMover)
        {
            Mover();
        }
    }

    public void Mover()
    {
        float disttanciaA = Vector2.Distance(transform.position, pointA.position);
        float disttanciaB = Vector2.Distance(transform.position, pointB.position);

        if (disttanciaA > 0.1f && mueveA)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (disttanciaA < 0.3f && seMueve)
            {
                if (debeEsperar)
                {
                    StartCoroutine(Espera());
                    mueveA = false;
                    mueveB = true;
                }

                else
                {
                    mueveA = false;
                    mueveB = true;
                }
            }
        }

        if (disttanciaB > 0.1f && mueveB)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (disttanciaB < 0.3f && seMueve)
            {
                if (debeEsperar)
                {
                    StartCoroutine(Espera());
                    mueveA = true;
                    mueveB = false;
                }

                else
                {
                    mueveA = true;
                    mueveB = false;
                }
            }
        }
    }

    public void RecibirDaño(float daño)
    {
        healthPoints -= daño;

        if (healthPoints <= 0)
        {
            Morir();
        }
    }

    IEnumerator Espera()
    {
        debeMover = false;
        seMueve = false;
        yield return new WaitForSeconds(tiempoEspera);
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        debeMover = true;
        seMueve = true;
    }
}
